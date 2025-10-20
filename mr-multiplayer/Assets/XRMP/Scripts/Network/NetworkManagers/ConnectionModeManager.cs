using System;
using Unity.Netcode;
using Unity.XR.CoreUtils.Bindings.Variables;
using UnityEngine;

namespace XRMultiplayer
{
    /// <summary>
    /// Central coordinator for managing connection modes (Cloud/LAN) in the multiplayer system.
    /// Provides seamless switching between Unity Gaming Services (UGS) cloud lobbies and direct LAN connections.
    /// </summary>
    /// <remarks>
    /// This component acts as a facade between UI, LANConnectionManager, and LobbyManager,
    /// ensuring clean state transitions and maintaining the event-driven architecture.
    /// It does not break existing cloud functionality and keeps connection modes loosely coupled.
    /// </remarks>
    public class ConnectionModeManager : MonoBehaviour
    {
        /// <summary>
        /// Available connection modes for multiplayer sessions.
        /// </summary>
        public enum ConnectionMode
        {
            /// <summary>
            /// Cloud mode using Unity Gaming Services (UGS) Lobby and Relay.
            /// Requires internet connectivity and UGS authentication.
            /// </summary>
            Cloud,

            /// <summary>
            /// Local Area Network mode using direct IP connections.
            /// Works without internet connectivity, requires manual IP address entry.
            /// </summary>
            LANDirect
        }

        [Header("Connection Mode Settings")]
        [SerializeField, Tooltip("Default connection mode on startup")]
        private ConnectionMode m_DefaultMode = ConnectionMode.Cloud;

        [SerializeField, Tooltip("Allow automatic mode switching when cloud services fail")]
        private bool m_AutoSwitchOnCloudFailure = true;

        [SerializeField, Tooltip("Enable verbose debug logging")]
        private bool m_DebugLogging = true;

        /// <summary>
        /// Singleton instance for global access.
        /// </summary>
        public static ConnectionModeManager Instance { get; private set; }

        /// <summary>
        /// Current active connection mode.
        /// </summary>
        public ConnectionMode CurrentMode { get; private set; }

        /// <summary>
        /// Bindable variable for the current connection mode.
        /// Subscribe to this for reactive UI updates.
        /// </summary>
        public static IReadOnlyBindableVariable<ConnectionMode> CurrentModeBindable => s_CurrentModeBindable;
        static readonly BindableEnum<ConnectionMode> s_CurrentModeBindable = new BindableEnum<ConnectionMode>(ConnectionMode.Cloud);

        /// <summary>
        /// Whether the manager is currently in the process of switching modes.
        /// </summary>
        public bool IsSwitchingMode { get; private set; }

        /// <summary>
        /// Event invoked when connection mode changes.
        /// Provides the new mode.
        /// </summary>
        public event Action<ConnectionMode> OnModeChanged;

        /// <summary>
        /// Event invoked when mode switching starts.
        /// </summary>
        public event Action<ConnectionMode> OnModeSwitchStarted;

        /// <summary>
        /// Event invoked when mode switching completes successfully.
        /// </summary>
        public event Action<ConnectionMode> OnModeSwitchCompleted;

        /// <summary>
        /// Event invoked when mode switching fails.
        /// Provides error message.
        /// </summary>
        public event Action<string> OnModeSwitchFailed;

        // Component references
        private XRINetworkGameManager m_GameManager;
        private LobbyManager m_LobbyManager;
        private LANConnectionManager m_LANConnectionManager;

        const string k_DebugPrepend = "<color=#FF6B6B>[Connection Mode Manager]</color> ";

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Log("Duplicate ConnectionModeManager found, destroying.", 2);
                Destroy(gameObject);
                return;
            }
            Instance = this;

            // Find required components
            m_GameManager = FindFirstObjectByType<XRINetworkGameManager>();
            m_LobbyManager = FindFirstObjectByType<LobbyManager>();
            m_LANConnectionManager = FindFirstObjectByType<LANConnectionManager>();

            // Validate component dependencies
            if (m_GameManager == null)
            {
                LogError("XRINetworkGameManager not found! ConnectionModeManager requires it.");
                enabled = false;
                return;
            }

            if (m_LobbyManager == null)
            {
                LogWarning("LobbyManager not found. Cloud mode will not be available.");
            }

            if (m_LANConnectionManager == null)
            {
                LogWarning("LANConnectionManager not found. LAN mode will not be available.");
            }

            // Set initial mode
            CurrentMode = m_DefaultMode;
            s_CurrentModeBindable.Value = CurrentMode;
            Log($"Initialized with default mode: {CurrentMode}");
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void Start()
        {
            // Subscribe to cloud connection failure events for auto-switching
            if (m_AutoSwitchOnCloudFailure && m_LobbyManager != null)
            {
                m_LobbyManager.OnLobbyFailed += OnCloudConnectionFailed;
            }

            // Subscribe to game manager connection events
            if (m_GameManager != null)
            {
                m_GameManager.connectionFailedAction += OnConnectionFailed;
            }

            // Subscribe to LAN connection events
            if (m_LANConnectionManager != null)
            {
                m_LANConnectionManager.OnConnectionFailed += OnLANConnectionFailed;
            }
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void OnDestroy()
        {
            // Unsubscribe from events
            if (m_LobbyManager != null)
            {
                m_LobbyManager.OnLobbyFailed -= OnCloudConnectionFailed;
            }

            if (m_GameManager != null)
            {
                m_GameManager.connectionFailedAction -= OnConnectionFailed;
            }

            if (m_LANConnectionManager != null)
            {
                m_LANConnectionManager.OnConnectionFailed -= OnLANConnectionFailed;
            }

            if (Instance == this)
            {
                Instance = null;
            }
        }

        /// <summary>
        /// Sets the connection mode and handles necessary state transitions.
        /// Automatically disconnects from current session if connected.
        /// </summary>
        /// <param name="mode">Target connection mode</param>
        /// <param name="forceSwitch">Force mode switch even if already in that mode</param>
        public void SetConnectionMode(ConnectionMode mode, bool forceSwitch = false)
        {
            // Check if already in this mode
            if (CurrentMode == mode && !forceSwitch)
            {
                Log($"Already in {mode} mode. Use forceSwitch=true to force mode switch.");
                return;
            }

            // Check if already switching
            if (IsSwitchingMode)
            {
                LogWarning("Already switching connection mode. Please wait.");
                return;
            }

            // Validate mode availability
            if (mode == ConnectionMode.Cloud && m_LobbyManager == null)
            {
                LogError("Cannot switch to Cloud mode: LobbyManager not available.");
                OnModeSwitchFailed?.Invoke("Cloud mode not available: LobbyManager not found.");
                return;
            }

            if (mode == ConnectionMode.LANDirect && m_LANConnectionManager == null)
            {
                LogError("Cannot switch to LAN mode: LANConnectionManager not available.");
                OnModeSwitchFailed?.Invoke("LAN mode not available: LANConnectionManager not found.");
                return;
            }

            Log($"Switching connection mode: {CurrentMode} → {mode}");
            StartModeSwitchInternal(mode);
        }

        /// <summary>
        /// Gets whether a specific connection mode is available.
        /// </summary>
        /// <param name="mode">Mode to check</param>
        /// <returns>True if mode is available</returns>
        public bool IsModeAvailable(ConnectionMode mode)
        {
            switch (mode)
            {
                case ConnectionMode.Cloud:
                    return m_LobbyManager != null;
                case ConnectionMode.LANDirect:
                    return m_LANConnectionManager != null;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets whether currently connected in any mode.
        /// </summary>
        /// <returns>True if connected</returns>
        public bool IsConnected()
        {
            if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsListening)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Disconnects from current session regardless of mode.
        /// </summary>
        public async void Disconnect()
        {
            Log("Disconnecting from current session...");

            if (!IsConnected())
            {
                Log("Not currently connected.");
                return;
            }

            try
            {
                // Disconnect based on current mode
                switch (CurrentMode)
                {
                    case ConnectionMode.Cloud:
                        if (m_GameManager != null)
                        {
                            await m_GameManager.DisconnectAsync();
                        }
                        break;

                    case ConnectionMode.LANDirect:
                        if (m_LANConnectionManager != null)
                        {
                            m_LANConnectionManager.Disconnect();
                        }
                        break;
                }

                Log("Disconnected successfully.");
            }
            catch (Exception ex)
            {
                LogError($"Error during disconnect: {ex.Message}");
            }
        }

        /// <summary>
        /// Suggests the best connection mode based on current conditions.
        /// </summary>
        /// <returns>Recommended connection mode</returns>
        public ConnectionMode SuggestBestMode()
        {
            // Check cloud availability
            bool cloudAvailable = m_LobbyManager != null && AuthenticationManager.IsAuthenticated();
            bool lanAvailable = m_LANConnectionManager != null;

            if (cloudAvailable)
            {
                Log("Suggesting Cloud mode (authenticated and available)");
                return ConnectionMode.Cloud;
            }
            else if (lanAvailable)
            {
                Log("Suggesting LAN mode (cloud not available)");
                return ConnectionMode.LANDirect;
            }
            else
            {
                LogWarning("No connection modes available!");
                return CurrentMode; // Return current mode as fallback
            }
        }

        #region Internal Methods

        /// <summary>
        /// Internal method to handle mode switching logic.
        /// </summary>
        private async void StartModeSwitchInternal(ConnectionMode targetMode)
        {
            IsSwitchingMode = true;
            OnModeSwitchStarted?.Invoke(targetMode);

            try
            {
                // Disconnect from current session if connected
                if (IsConnected())
                {
                    Log("Disconnecting from current session before mode switch...");
                    await DisconnectInternal();
                }

                // Update mode
                ConnectionMode previousMode = CurrentMode;
                CurrentMode = targetMode;
                s_CurrentModeBindable.Value = targetMode;

                Log($"Connection mode switched to: {targetMode}");

                // Notify listeners
                OnModeChanged?.Invoke(targetMode);
                OnModeSwitchCompleted?.Invoke(targetMode);

                IsSwitchingMode = false;
            }
            catch (Exception ex)
            {
                LogError($"Mode switch failed: {ex.Message}");
                OnModeSwitchFailed?.Invoke($"Mode switch failed: {ex.Message}");
                IsSwitchingMode = false;
            }
        }

        /// <summary>
        /// Internal disconnect method used during mode switching.
        /// </summary>
        private async System.Threading.Tasks.Task DisconnectInternal()
        {
            switch (CurrentMode)
            {
                case ConnectionMode.Cloud:
                    if (m_GameManager != null)
                    {
                        await m_GameManager.DisconnectAsync();
                    }
                    break;

                case ConnectionMode.LANDirect:
                    if (m_LANConnectionManager != null)
                    {
                        m_LANConnectionManager.Disconnect();
                    }
                    // Wait a frame to ensure cleanup
                    await System.Threading.Tasks.Task.Delay(100);
                    break;
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles cloud connection failures for auto-switching.
        /// </summary>
        private void OnCloudConnectionFailed(string reason)
        {
            if (m_AutoSwitchOnCloudFailure && CurrentMode == ConnectionMode.Cloud)
            {
                LogWarning($"Cloud connection failed: {reason}. Suggesting LAN mode.");
                
                // Don't auto-switch, just suggest - let user decide
                // This prevents unexpected behavior
                if (IsModeAvailable(ConnectionMode.LANDirect))
                {
                    Log("LAN Direct mode is available as an alternative.");
                }
            }
        }

        /// <summary>
        /// Handles general connection failures.
        /// </summary>
        private void OnConnectionFailed(string reason)
        {
            Log($"Connection failed in {CurrentMode} mode: {reason}");
        }

        /// <summary>
        /// Handles LAN connection failures.
        /// </summary>
        private void OnLANConnectionFailed(string reason)
        {
            Log($"LAN connection failed: {reason}");
        }

        #endregion

        #region Logging

        /// <summary>
        /// Logs a message with Connection Mode Manager prefix.
        /// </summary>
        private void Log(string message, int level = 0)
        {
            if (m_DebugLogging || level > 0)
            {
                Utils.Log($"{k_DebugPrepend}{message}", level);
            }
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        private void LogWarning(string message)
        {
            Utils.LogWarning($"{k_DebugPrepend}{message}");
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        private void LogError(string message)
        {
            Utils.Log($"{k_DebugPrepend}{message}", 2);
        }

        #endregion

        #region Editor Helpers

#if UNITY_EDITOR
        /// <summary>
        /// Context menu to switch to Cloud mode.
        /// </summary>
        [ContextMenu("Switch to Cloud Mode")]
        private void SwitchToCloudMode()
        {
            SetConnectionMode(ConnectionMode.Cloud);
        }

        /// <summary>
        /// Context menu to switch to LAN mode.
        /// </summary>
        [ContextMenu("Switch to LAN Mode")]
        private void SwitchToLANMode()
        {
            SetConnectionMode(ConnectionMode.LANDirect);
        }

        /// <summary>
        /// Context menu to test mode availability.
        /// </summary>
        [ContextMenu("Test Mode Availability")]
        private void TestModeAvailability()
        {
            Debug.Log($"=== Connection Mode Availability ===");
            Debug.Log($"Cloud Mode Available: {IsModeAvailable(ConnectionMode.Cloud)}");
            Debug.Log($"LAN Mode Available: {IsModeAvailable(ConnectionMode.LANDirect)}");
            Debug.Log($"Current Mode: {CurrentMode}");
            Debug.Log($"Is Connected: {IsConnected()}");
            Debug.Log($"Suggested Mode: {SuggestBestMode()}");
        }

        /// <summary>
        /// Context menu to test disconnect.
        /// </summary>
        [ContextMenu("Test Disconnect")]
        private void TestDisconnect()
        {
            Disconnect();
        }
#endif

        #endregion
    }
}
