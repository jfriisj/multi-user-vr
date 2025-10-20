using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace XRMultiplayer
{
    /// <summary>
    /// Comprehensive connection status monitoring and error recovery for LAN connections.
    /// Provides timeout handling, error classification, user feedback, and automatic reconnection.
    /// </summary>
    /// <remarks>
    /// This component enhances LANConnectionManager with robust error handling, detailed status reporting,
    /// and reconnection logic for temporary network interruptions. It integrates with the existing
    /// notification system and provides clear, actionable feedback to users.
    /// </remarks>
    public class LANConnectionStatus : MonoBehaviour
    {
        /// <summary>
        /// Detailed error categories for LAN connection failures.
        /// </summary>
        public enum ErrorCategory
        {
            None,
            InvalidIPAddress,
            NetworkUnreachable,
            ConnectionTimeout,
            HostNotFound,
            PortBlocked,
            ConnectionLost,
            UnknownError
        }

        /// <summary>
        /// Reconnection state machine.
        /// </summary>
        public enum ReconnectionState
        {
            Idle,
            Attempting,
            Succeeded,
            Failed
        }

        [Header("Timeout Settings")]
        [SerializeField, Tooltip("Connection timeout in seconds (requirement 5.5)")]
        private float m_ConnectionTimeout = 5.0f;

        [SerializeField, Tooltip("Reconnection timeout in seconds (requirement 5.4)")]
        private float m_ReconnectionTimeout = 30.0f;

        [SerializeField, Tooltip("Time between reconnection attempts")]
        private float m_ReconnectionInterval = 3.0f;

        [Header("Reconnection Settings")]
        [SerializeField, Tooltip("Enable automatic reconnection on connection loss")]
        private bool m_EnableAutoReconnect = true;

        [SerializeField, Tooltip("Maximum number of reconnection attempts")]
        private int m_MaxReconnectionAttempts = 10;

        [Header("User Feedback")]
        [SerializeField, Tooltip("Show connection countdown timer")]
        private bool m_ShowCountdownTimer = true;

        [SerializeField, Tooltip("Display duration for status messages")]
        private float m_StatusDisplayDuration = 3.0f;

        [Header("Debug")]
        [SerializeField, Tooltip("Enable verbose debug logging")]
        private bool m_DebugLogging = true;

        /// <summary>
        /// Current error category if connection failed.
        /// </summary>
        public ErrorCategory CurrentError { get; private set; } = ErrorCategory.None;

        /// <summary>
        /// Current reconnection state.
        /// </summary>
        public ReconnectionState CurrentReconnectionState { get; private set; } = ReconnectionState.Idle;

        /// <summary>
        /// Whether connection is currently in progress.
        /// </summary>
        public bool IsConnecting { get; private set; }

        /// <summary>
        /// Whether reconnection is currently in progress.
        /// </summary>
        public bool IsReconnecting => CurrentReconnectionState == ReconnectionState.Attempting;

        /// <summary>
        /// Remaining time for current connection timeout.
        /// </summary>
        public float RemainingTimeout { get; private set; }

        /// <summary>
        /// Current reconnection attempt count.
        /// </summary>
        public int ReconnectionAttemptCount { get; private set; }

        /// <summary>
        /// Event invoked when error category changes.
        /// </summary>
        public event Action<ErrorCategory> OnErrorCategoryChanged;

        /// <summary>
        /// Event invoked when reconnection state changes.
        /// </summary>
        public event Action<ReconnectionState> OnReconnectionStateChanged;

        /// <summary>
        /// Event invoked when timeout countdown updates (for UI).
        /// </summary>
        public event Action<float> OnTimeoutCountdownUpdate;

        /// <summary>
        /// Event invoked when reconnection attempt count changes.
        /// </summary>
        public event Action<int, int> OnReconnectionAttemptUpdate; // current, max

        // Component references
        private LANConnectionManager m_LANConnectionManager;
        private NetworkManager m_NetworkManager;
        private XRINetworkGameManager m_GameManager;

        // State tracking
        private Coroutine m_ConnectionTimeoutCoroutine;
        private Coroutine m_ReconnectionCoroutine;
        private string m_LastConnectionIP;
        private bool m_WasHosting;
        private bool m_ConnectionLostDuringGameplay;

        const string k_DebugPrepend = "<color=#FF6B6B>[LAN Connection Status]</color> ";

        #region Unity Lifecycle

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void Awake()
        {
            // Find required components
            m_LANConnectionManager = FindFirstObjectByType<LANConnectionManager>();
            m_NetworkManager = FindFirstObjectByType<NetworkManager>();
            m_GameManager = FindFirstObjectByType<XRINetworkGameManager>();

            // Validate dependencies
            if (m_LANConnectionManager == null)
            {
                Debug.LogError($"{k_DebugPrepend}LANConnectionManager not found! Component will be disabled.");
                enabled = false;
                return;
            }

            if (m_NetworkManager == null)
            {
                Debug.LogWarning($"{k_DebugPrepend}NetworkManager not found. Some features may not work.");
            }
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void Start()
        {
            SubscribeToEvents();
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void OnDestroy()
        {
            UnsubscribeFromEvents();
            StopAllMonitoring();
        }

        #endregion

        #region Event Subscription

        /// <summary>
        /// Subscribes to all necessary events.
        /// </summary>
        private void SubscribeToEvents()
        {
            if (m_LANConnectionManager != null)
            {
                m_LANConnectionManager.OnStatusChanged += OnLANStatusChanged;
                m_LANConnectionManager.OnConnectionFailed += OnLANConnectionFailed;
                m_LANConnectionManager.OnConnectionSuccess += OnLANConnectionSuccess;
            }

            if (m_NetworkManager != null)
            {
                m_NetworkManager.OnClientDisconnectCallback += OnClientDisconnected;
                m_NetworkManager.OnServerStarted += OnServerStarted;
                m_NetworkManager.OnClientConnectedCallback += OnClientConnected;
            }
        }

        /// <summary>
        /// Unsubscribes from all events.
        /// </summary>
        private void UnsubscribeFromEvents()
        {
            if (m_LANConnectionManager != null)
            {
                m_LANConnectionManager.OnStatusChanged -= OnLANStatusChanged;
                m_LANConnectionManager.OnConnectionFailed -= OnLANConnectionFailed;
                m_LANConnectionManager.OnConnectionSuccess -= OnLANConnectionSuccess;
            }

            if (m_NetworkManager != null)
            {
                m_NetworkManager.OnClientDisconnectCallback -= OnClientDisconnected;
                m_NetworkManager.OnServerStarted -= OnServerStarted;
                m_NetworkManager.OnClientConnectedCallback -= OnClientConnected;
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Called when LAN connection status changes.
        /// </summary>
        private void OnLANStatusChanged(LANConnectionManager.LANConnectionStatus status)
        {
            LogDebug($"LAN status changed to: {status}");

            switch (status)
            {
                case LANConnectionManager.LANConnectionStatus.Connecting:
                    HandleConnectingState();
                    break;

                case LANConnectionManager.LANConnectionStatus.Connected:
                    HandleConnectedState();
                    break;

                case LANConnectionManager.LANConnectionStatus.Disconnected:
                    HandleDisconnectedState();
                    break;

                case LANConnectionManager.LANConnectionStatus.Failed:
                    HandleFailedState();
                    break;

                case LANConnectionManager.LANConnectionStatus.Timeout:
                    HandleTimeoutState();
                    break;
            }
        }

        /// <summary>
        /// Called when LAN connection fails.
        /// </summary>
        private void OnLANConnectionFailed(string reason)
        {
            LogDebug($"LAN connection failed: {reason}");

            // Classify the error
            ClassifyError(reason);

            // Display error to user (requirement 5.3)
            DisplayErrorMessage(reason);

            // Stop connection timeout monitoring
            StopConnectionTimeout();
        }

        /// <summary>
        /// Called when LAN connection succeeds.
        /// </summary>
        private void OnLANConnectionSuccess()
        {
            LogDebug("LAN connection succeeded");

            // Display success message (requirement 5.2)
            if (PlayerHudNotification.Instance != null)
            {
                PlayerHudNotification.Instance.ShowText("<b>Status:</b> Connected (LAN Direct)", m_StatusDisplayDuration);
            }

            // Stop connection timeout monitoring
            StopConnectionTimeout();

            // Reset error state
            SetErrorCategory(ErrorCategory.None);

            // If this was a reconnection attempt, mark as succeeded
            if (IsReconnecting)
            {
                SetReconnectionState(ReconnectionState.Succeeded);
                StopReconnection();
            }
        }

        /// <summary>
        /// Called when client disconnects from server.
        /// </summary>
        private void OnClientDisconnected(ulong clientId)
        {
            // Only handle if this is the local client
            if (m_NetworkManager != null && m_NetworkManager.LocalClientId == clientId)
            {
                LogDebug($"Local client disconnected (ID: {clientId})");

                // Check if we were in LAN mode and connected
                if (m_LANConnectionManager != null && m_LANConnectionManager.IsLANModeActive)
                {
                    m_ConnectionLostDuringGameplay = XRINetworkGameManager.Connected.Value;

                    // Attempt automatic reconnection if enabled (requirement 5.4)
                    if (m_EnableAutoReconnect && m_ConnectionLostDuringGameplay)
                    {
                        LogDebug("Connection lost during gameplay, attempting reconnection...");
                        SetErrorCategory(ErrorCategory.ConnectionLost);
                        StartReconnection();
                    }
                }
            }
        }

        /// <summary>
        /// Called when server starts.
        /// </summary>
        private void OnServerStarted()
        {
            LogDebug("Server started in LAN mode");
            m_WasHosting = true;
        }

        /// <summary>
        /// Called when client connects to server.
        /// </summary>
        private void OnClientConnected(ulong clientId)
        {
            LogDebug($"Client connected (ID: {clientId})");
        }

        #endregion

        #region State Handlers

        /// <summary>
        /// Handles Connecting state.
        /// </summary>
        private void HandleConnectingState()
        {
            IsConnecting = true;

            // Start connection timeout monitoring (requirement 5.1)
            StartConnectionTimeout();

            // Display connecting message with countdown
            if (PlayerHudNotification.Instance != null && m_ShowCountdownTimer)
            {
                PlayerHudNotification.Instance.ShowText("<b>Status:</b> Connecting to LAN...", m_ConnectionTimeout);
            }
        }

        /// <summary>
        /// Handles Connected state.
        /// </summary>
        private void HandleConnectedState()
        {
            IsConnecting = false;
            m_ConnectionLostDuringGameplay = false;
        }

        /// <summary>
        /// Handles Disconnected state.
        /// </summary>
        private void HandleDisconnectedState()
        {
            IsConnecting = false;
            StopConnectionTimeout();
        }

        /// <summary>
        /// Handles Failed state.
        /// </summary>
        private void HandleFailedState()
        {
            IsConnecting = false;
            StopConnectionTimeout();
        }

        /// <summary>
        /// Handles Timeout state.
        /// </summary>
        private void HandleTimeoutState()
        {
            IsConnecting = false;
            SetErrorCategory(ErrorCategory.ConnectionTimeout);

            // Display timeout error (requirement 5.5)
            DisplayErrorMessage("Connection timeout. Please check network and try again.");

            StopConnectionTimeout();
        }

        #endregion

        #region Connection Timeout Monitoring

        /// <summary>
        /// Starts connection timeout monitoring.
        /// </summary>
        private void StartConnectionTimeout()
        {
            StopConnectionTimeout();
            m_ConnectionTimeoutCoroutine = StartCoroutine(ConnectionTimeoutCoroutine());
        }

        /// <summary>
        /// Stops connection timeout monitoring.
        /// </summary>
        private void StopConnectionTimeout()
        {
            if (m_ConnectionTimeoutCoroutine != null)
            {
                StopCoroutine(m_ConnectionTimeoutCoroutine);
                m_ConnectionTimeoutCoroutine = null;
            }
            RemainingTimeout = 0f;
        }

        /// <summary>
        /// Coroutine for connection timeout monitoring.
        /// </summary>
        private IEnumerator ConnectionTimeoutCoroutine()
        {
            RemainingTimeout = m_ConnectionTimeout;

            while (RemainingTimeout > 0)
            {
                yield return new WaitForSeconds(0.1f);
                RemainingTimeout -= 0.1f;

                // Notify listeners for UI updates
                OnTimeoutCountdownUpdate?.Invoke(RemainingTimeout);
            }

            // Timeout reached
            LogDebug("Connection timeout reached");
            SetErrorCategory(ErrorCategory.ConnectionTimeout);

            // Abort connection attempt
            if (m_LANConnectionManager != null)
            {
                m_LANConnectionManager.Disconnect();
            }
        }

        #endregion

        #region Reconnection Logic

        /// <summary>
        /// Starts automatic reconnection attempts.
        /// </summary>
        private void StartReconnection()
        {
            if (!m_EnableAutoReconnect)
            {
                LogDebug("Auto-reconnect disabled, skipping reconnection");
                return;
            }

            StopReconnection();
            SetReconnectionState(ReconnectionState.Attempting);
            ReconnectionAttemptCount = 0;
            m_ReconnectionCoroutine = StartCoroutine(ReconnectionCoroutine());
        }

        /// <summary>
        /// Stops reconnection attempts.
        /// </summary>
        private void StopReconnection()
        {
            if (m_ReconnectionCoroutine != null)
            {
                StopCoroutine(m_ReconnectionCoroutine);
                m_ReconnectionCoroutine = null;
            }

            if (CurrentReconnectionState == ReconnectionState.Attempting)
            {
                SetReconnectionState(ReconnectionState.Idle);
            }
        }

        /// <summary>
        /// Coroutine for automatic reconnection (requirement 5.4).
        /// </summary>
        private IEnumerator ReconnectionCoroutine()
        {
            float elapsedTime = 0f;

            LogDebug($"Starting reconnection attempts (max {m_MaxReconnectionAttempts} attempts over {m_ReconnectionTimeout}s)");

            if (PlayerHudNotification.Instance != null)
            {
                PlayerHudNotification.Instance.ShowText("<b>Status:</b> Connection lost, attempting reconnection...", m_ReconnectionTimeout);
            }

            while (elapsedTime < m_ReconnectionTimeout && ReconnectionAttemptCount < m_MaxReconnectionAttempts)
            {
                yield return new WaitForSeconds(m_ReconnectionInterval);
                elapsedTime += m_ReconnectionInterval;

                ReconnectionAttemptCount++;
                OnReconnectionAttemptUpdate?.Invoke(ReconnectionAttemptCount, m_MaxReconnectionAttempts);

                LogDebug($"Reconnection attempt {ReconnectionAttemptCount}/{m_MaxReconnectionAttempts}");

                // Attempt reconnection
                if (m_LANConnectionManager != null && !string.IsNullOrEmpty(m_LastConnectionIP))
                {
                    if (m_WasHosting)
                    {
                        // If we were hosting, restart host
                        m_LANConnectionManager.HostLAN();
                    }
                    else
                    {
                        // If we were client, rejoin
                        m_LANConnectionManager.JoinLAN(m_LastConnectionIP);
                    }

                    // Wait for connection result
                    yield return new WaitForSeconds(m_ConnectionTimeout);

                    // Check if reconnection succeeded
                    if (m_LANConnectionManager.Status == LANConnectionManager.LANConnectionStatus.Connected)
                    {
                        LogDebug("Reconnection succeeded!");
                        SetReconnectionState(ReconnectionState.Succeeded);

                        if (PlayerHudNotification.Instance != null)
                        {
                            PlayerHudNotification.Instance.ShowText("<b>Status:</b> Reconnected successfully", m_StatusDisplayDuration);
                        }

                        yield break;
                    }
                }
            }

            // Reconnection failed
            LogDebug("Reconnection attempts exhausted");
            SetReconnectionState(ReconnectionState.Failed);

            if (PlayerHudNotification.Instance != null)
            {
                PlayerHudNotification.Instance.ShowText("<b>Error:</b> Reconnection failed. Please reconnect manually.", m_StatusDisplayDuration);
            }
        }

        #endregion

        #region Error Classification

        /// <summary>
        /// Classifies error based on failure message.
        /// </summary>
        private void ClassifyError(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                SetErrorCategory(ErrorCategory.UnknownError);
                return;
            }

            errorMessage = errorMessage.ToLower();

            if (errorMessage.Contains("invalid") && errorMessage.Contains("ip"))
            {
                SetErrorCategory(ErrorCategory.InvalidIPAddress);
            }
            else if (errorMessage.Contains("timeout"))
            {
                SetErrorCategory(ErrorCategory.ConnectionTimeout);
            }
            else if (errorMessage.Contains("unreachable") || errorMessage.Contains("network"))
            {
                SetErrorCategory(ErrorCategory.NetworkUnreachable);
            }
            else if (errorMessage.Contains("host") && errorMessage.Contains("not found"))
            {
                SetErrorCategory(ErrorCategory.HostNotFound);
            }
            else if (errorMessage.Contains("port") || errorMessage.Contains("blocked"))
            {
                SetErrorCategory(ErrorCategory.PortBlocked);
            }
            else if (errorMessage.Contains("lost") || errorMessage.Contains("disconnected"))
            {
                SetErrorCategory(ErrorCategory.ConnectionLost);
            }
            else
            {
                SetErrorCategory(ErrorCategory.UnknownError);
            }
        }

        /// <summary>
        /// Sets the current error category and notifies listeners.
        /// </summary>
        private void SetErrorCategory(ErrorCategory category)
        {
            if (CurrentError != category)
            {
                CurrentError = category;
                OnErrorCategoryChanged?.Invoke(category);
                LogDebug($"Error category changed to: {category}");
            }
        }

        /// <summary>
        /// Sets the reconnection state and notifies listeners.
        /// </summary>
        private void SetReconnectionState(ReconnectionState state)
        {
            if (CurrentReconnectionState != state)
            {
                CurrentReconnectionState = state;
                OnReconnectionStateChanged?.Invoke(state);
                LogDebug($"Reconnection state changed to: {state}");
            }
        }

        #endregion

        #region User Feedback

        /// <summary>
        /// Displays error message with suggested remedies (requirement 5.3).
        /// </summary>
        private void DisplayErrorMessage(string errorMessage)
        {
            string remedy = GetErrorRemedy(CurrentError);
            string fullMessage = $"<b>Error:</b> {errorMessage}\n{remedy}";

            if (PlayerHudNotification.Instance != null)
            {
                PlayerHudNotification.Instance.ShowText(fullMessage, m_StatusDisplayDuration * 2);
            }

            LogDebug($"Error displayed: {errorMessage} | Remedy: {remedy}");
        }

        /// <summary>
        /// Gets suggested remedy for error category.
        /// </summary>
        private string GetErrorRemedy(ErrorCategory category)
        {
            switch (category)
            {
                case ErrorCategory.InvalidIPAddress:
                    return "<b>Solution:</b> Check IP address format (e.g., 192.168.1.100)";

                case ErrorCategory.NetworkUnreachable:
                    return "<b>Solution:</b> Ensure both devices are on the same WiFi network";

                case ErrorCategory.ConnectionTimeout:
                    return "<b>Solution:</b> Check network connection and try again";

                case ErrorCategory.HostNotFound:
                    return "<b>Solution:</b> Verify host IP address and ensure host is running";

                case ErrorCategory.PortBlocked:
                    return "<b>Solution:</b> Check firewall settings and port availability";

                case ErrorCategory.ConnectionLost:
                    return "<b>Solution:</b> Attempting automatic reconnection...";

                case ErrorCategory.UnknownError:
                    return "<b>Solution:</b> Check network settings and restart connection";

                default:
                    return "";
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Manually triggers reconnection attempt.
        /// </summary>
        public void ManualReconnect()
        {
            if (IsReconnecting)
            {
                LogDebug("Reconnection already in progress");
                return;
            }

            LogDebug("Manual reconnection triggered");
            StartReconnection();
        }

        /// <summary>
        /// Cancels ongoing reconnection attempts.
        /// </summary>
        public void CancelReconnection()
        {
            LogDebug("Reconnection cancelled");
            StopReconnection();
            SetReconnectionState(ReconnectionState.Failed);
        }

        /// <summary>
        /// Stores connection information for reconnection purposes.
        /// </summary>
        public void StoreConnectionInfo(string ipAddress, bool wasHosting)
        {
            m_LastConnectionIP = ipAddress;
            m_WasHosting = wasHosting;
            LogDebug($"Connection info stored: IP={ipAddress}, Hosting={wasHosting}");
        }

        /// <summary>
        /// Gets detailed error information for current error.
        /// </summary>
        public string GetDetailedErrorInfo()
        {
            string errorName = CurrentError.ToString();
            string remedy = GetErrorRemedy(CurrentError);
            return $"Error: {errorName}\n{remedy}";
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Stops all monitoring coroutines.
        /// </summary>
        private void StopAllMonitoring()
        {
            StopConnectionTimeout();
            StopReconnection();
        }

        /// <summary>
        /// Logs debug message if debug logging is enabled.
        /// </summary>
        private void LogDebug(string message)
        {
            if (m_DebugLogging)
            {
                Utils.Log($"{k_DebugPrepend}{message}");
            }
        }

        #endregion
    }
}
