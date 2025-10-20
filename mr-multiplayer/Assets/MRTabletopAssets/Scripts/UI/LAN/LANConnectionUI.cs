using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XRMultiplayer;

namespace UnityEngine.XR.Templates.MRTTabletopAssets
{
    /// <summary>
    /// User interface component for LAN connection management.
    /// Provides mode selection, IP address entry, and connection status display.
    /// </summary>
    /// <remarks>
    /// This component extends the existing LobbyUI patterns to support LAN Direct connections.
    /// It integrates with ConnectionModeManager and LANConnectionManager while maintaining
    /// VR-friendly design principles and accessibility standards.
    /// </remarks>
    public class LANConnectionUI : MonoBehaviour
    {
        [Header("Connection Mode")]
        [SerializeField, Tooltip("Toggle for switching between Cloud and LAN modes")]
        private Toggle m_ConnectionModeToggle;

        [SerializeField, Tooltip("Text label for connection mode toggle")]
        private TMP_Text m_ConnectionModeLabel;

        [Header("LAN Connection Panel")]
        [SerializeField, Tooltip("Panel containing LAN-specific UI elements")]
        private GameObject m_LANConnectionPanel;

        [SerializeField, Tooltip("Panel containing Cloud-specific UI elements")]
        private GameObject m_CloudConnectionPanel;

        [Header("IP Address Entry")]
        [SerializeField, Tooltip("Input field for entering host IP address")]
        private TMP_InputField m_IPAddressInput;

        [SerializeField, Tooltip("Text displaying the local IP address for hosting")]
        private TMP_Text m_LocalIPDisplay;

        [SerializeField, Tooltip("Validation feedback text for IP input")]
        private TMP_Text m_IPValidationText;

        [Header("Connection Buttons")]
        [SerializeField, Tooltip("Button to start hosting a LAN session")]
        private Button m_HostButton;

        [SerializeField, Tooltip("Button to join a LAN session as client")]
        private Button m_JoinButton;

        [SerializeField, Tooltip("Button to disconnect from current session")]
        private Button m_DisconnectButton;

        [Header("Status Display")]
        [SerializeField, Tooltip("Text displaying current connection status")]
        private TMP_Text m_ConnectionStatusText;

        [SerializeField, Tooltip("Text displaying detailed status messages")]
        private TMP_Text m_StatusDetailText;

        [SerializeField, Tooltip("Loading indicator for connection attempts")]
        private GameObject m_LoadingIndicator;

        [Header("Settings")]
        [SerializeField, Tooltip("Default port for LAN connections")]
        private ushort m_DefaultPort = 7777;

        [SerializeField, Tooltip("Enable real-time IP validation feedback")]
        private bool m_EnableRealtimeValidation = true;

        [SerializeField, Tooltip("IP address placeholder text")]
        private string m_IPPlaceholderText = "192.168.1.100";

        [Header("Colors")]
        [SerializeField, Tooltip("Color for successful status messages")]
        private Color m_SuccessColor = new Color(0.2f, 0.8f, 0.2f);

        [SerializeField, Tooltip("Color for error status messages")]
        private Color m_ErrorColor = new Color(0.8f, 0.2f, 0.2f);

        [SerializeField, Tooltip("Color for warning status messages")]
        private Color m_WarningColor = new Color(0.9f, 0.7f, 0.2f);

        [SerializeField, Tooltip("Color for info status messages")]
        private Color m_InfoColor = new Color(0.6f, 0.6f, 0.6f);

        // Component references
        private ConnectionModeManager m_ConnectionModeManager;
        private LANConnectionManager m_LANConnectionManager;
        private XRINetworkGameManager m_GameManager;

        // State tracking
        private bool m_IsInitialized;
        private ConnectionModeManager.ConnectionMode m_CurrentMode;
        private string m_CurrentIPInput;

        #region Unity Lifecycle

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void Awake()
        {
            // Find required components
            m_ConnectionModeManager = FindFirstObjectByType<ConnectionModeManager>();
            m_LANConnectionManager = FindFirstObjectByType<LANConnectionManager>();
            m_GameManager = FindFirstObjectByType<XRINetworkGameManager>();

            // Validate critical dependencies
            if (m_ConnectionModeManager == null)
            {
                Debug.LogError("[LANConnectionUI] ConnectionModeManager not found! LAN UI will not function.");
                enabled = false;
                return;
            }

            if (m_LANConnectionManager == null)
            {
                Debug.LogWarning("[LANConnectionUI] LANConnectionManager not found. LAN mode will not be available.");
            }
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void Start()
        {
            InitializeUI();
            SubscribeToEvents();
            m_IsInitialized = true;

            // Set initial mode based on ConnectionModeManager
            m_CurrentMode = m_ConnectionModeManager.CurrentMode;
            UpdateUIForMode(m_CurrentMode);
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void OnDestroy()
        {
            // Remove UI listeners to avoid leaks
            if (m_ConnectionModeToggle != null)
                m_ConnectionModeToggle.onValueChanged.RemoveListener(OnConnectionModeToggled);

            if (m_IPAddressInput != null)
            {
                m_IPAddressInput.onValueChanged.RemoveListener(OnIPAddressInputChanged);
                m_IPAddressInput.onEndEdit.RemoveListener(OnIPAddressEndEdit);
            }

            if (m_HostButton != null) m_HostButton.onClick.RemoveListener(OnHostButtonClicked);
            if (m_JoinButton != null) m_JoinButton.onClick.RemoveListener(OnJoinButtonClicked);
            if (m_DisconnectButton != null) m_DisconnectButton.onClick.RemoveListener(OnDisconnectButtonClicked);

            UnsubscribeFromEvents();
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes UI components and sets up initial state.
        /// </summary>
        private void InitializeUI()
        {
            // Setup connection mode toggle
            if (m_ConnectionModeToggle != null)
            {
                m_ConnectionModeToggle.onValueChanged.AddListener(OnConnectionModeToggled);
                m_ConnectionModeToggle.SetIsOnWithoutNotify(m_ConnectionModeManager.CurrentMode == ConnectionModeManager.ConnectionMode.LANDirect);
            }

            // Setup IP address input
            if (m_IPAddressInput != null)
            {
                m_IPAddressInput.onValueChanged.AddListener(OnIPAddressInputChanged);
                m_IPAddressInput.onEndEdit.AddListener(OnIPAddressEndEdit);
                
                // Set placeholder
                if (m_IPAddressInput.placeholder is TMP_Text placeholder)
                {
                    placeholder.text = m_IPPlaceholderText;
                }
            }

            // Setup connection buttons
            if (m_HostButton != null)
            {
                m_HostButton.onClick.AddListener(OnHostButtonClicked);
            }

            if (m_JoinButton != null)
            {
                m_JoinButton.onClick.AddListener(OnJoinButtonClicked);
            }

            if (m_DisconnectButton != null)
            {
                m_DisconnectButton.onClick.AddListener(OnDisconnectButtonClicked);
            }

            // Display local IP address
            UpdateLocalIPDisplay();

            // Hide loading indicator initially
            if (m_LoadingIndicator != null)
            {
                m_LoadingIndicator.SetActive(false);
            }

            // Set initial status
            UpdateConnectionStatus("Ready", m_InfoColor);
        }

        /// <summary>
        /// Subscribes to all necessary events.
        /// </summary>
        private void SubscribeToEvents()
        {
            // Connection mode events
            if (m_ConnectionModeManager != null)
            {
                m_ConnectionModeManager.OnModeChanged += OnModeChanged;
                m_ConnectionModeManager.OnModeSwitchStarted += OnModeSwitchStarted;
                m_ConnectionModeManager.OnModeSwitchCompleted += OnModeSwitchCompleted;
                m_ConnectionModeManager.OnModeSwitchFailed += OnModeSwitchFailed;
            }

            // LAN connection events
            if (m_LANConnectionManager != null)
            {
                m_LANConnectionManager.OnStatusChanged += OnLANStatusChanged;
                m_LANConnectionManager.OnConnectionFailed += OnLANConnectionFailed;
                m_LANConnectionManager.OnConnectionSuccess += OnLANConnectionSuccess;
            }

            // Subscribe to bindable variable for reactive updates
            if (ConnectionModeManager.CurrentModeBindable != null)
            {
                ConnectionModeManager.CurrentModeBindable.Subscribe(OnModeBindableChanged);
            }
        }

        /// <summary>
        /// Unsubscribes from all events.
        /// </summary>
        private void UnsubscribeFromEvents()
        {
            // Connection mode events
            if (m_ConnectionModeManager != null)
            {
                m_ConnectionModeManager.OnModeChanged -= OnModeChanged;
                m_ConnectionModeManager.OnModeSwitchStarted -= OnModeSwitchStarted;
                m_ConnectionModeManager.OnModeSwitchCompleted -= OnModeSwitchCompleted;
                m_ConnectionModeManager.OnModeSwitchFailed -= OnModeSwitchFailed;
            }

            // LAN connection events
            if (m_LANConnectionManager != null)
            {
                m_LANConnectionManager.OnStatusChanged -= OnLANStatusChanged;
                m_LANConnectionManager.OnConnectionFailed -= OnLANConnectionFailed;
                m_LANConnectionManager.OnConnectionSuccess -= OnLANConnectionSuccess;
            }

            // Unsubscribe from bindable variable
            if (ConnectionModeManager.CurrentModeBindable != null)
            {
                ConnectionModeManager.CurrentModeBindable.Unsubscribe(OnModeBindableChanged);
            }
        }

        #endregion

        #region UI Callbacks

        /// <summary>
        /// Called when connection mode toggle is changed.
        /// </summary>
        private void OnConnectionModeToggled(bool isLANMode)
        {
            ConnectionModeManager.ConnectionMode targetMode = isLANMode 
                ? ConnectionModeManager.ConnectionMode.LANDirect 
                : ConnectionModeManager.ConnectionMode.Cloud;

            Debug.Log($"[LANConnectionUI] User toggled connection mode to: {targetMode}");

            // Request mode change through ConnectionModeManager
            if (m_ConnectionModeManager != null)
            {
                m_ConnectionModeManager.SetConnectionMode(targetMode);
            }
        }

        /// <summary>
        /// Called when IP address input value changes.
        /// </summary>
        private void OnIPAddressInputChanged(string value)
        {
            m_CurrentIPInput = value;

            // Provide real-time validation feedback if enabled
            if (m_EnableRealtimeValidation && !string.IsNullOrEmpty(value))
            {
                bool isValid = m_LANConnectionManager != null && m_LANConnectionManager.ValidateIPAddress(value);
                UpdateIPValidationFeedback(isValid);
            }
        }

        /// <summary>
        /// Called when IP address input editing ends.
        /// </summary>
        private void OnIPAddressEndEdit(string value)
        {
            // Validate final IP address
            if (!string.IsNullOrEmpty(value))
            {
                bool isValid = m_LANConnectionManager != null && m_LANConnectionManager.ValidateIPAddress(value);
                UpdateIPValidationFeedback(isValid);

                if (!isValid)
                {
                    UpdateConnectionStatus("Invalid IP address format", m_ErrorColor);
                }
            }
        }

        /// <summary>
        /// Called when Host button is clicked.
        /// </summary>
        private void OnHostButtonClicked()
        {
            Debug.Log("[LANConnectionUI] Host button clicked");

            if (m_LANConnectionManager == null)
            {
                UpdateConnectionStatus("LAN Connection Manager not available", m_ErrorColor);
                return;
            }

            // Ensure we're in LAN mode
            if (m_CurrentMode != ConnectionModeManager.ConnectionMode.LANDirect)
            {
                UpdateConnectionStatus("Please switch to LAN Direct mode first", m_WarningColor);
                return;
            }

            // Start hosting
            UpdateConnectionStatus("Starting host...", m_InfoColor);
            ShowLoadingIndicator(true);
            m_LANConnectionManager.HostLAN(m_DefaultPort);
        }

        /// <summary>
        /// Called when Join button is clicked.
        /// </summary>
        private void OnJoinButtonClicked()
        {
            Debug.Log("[LANConnectionUI] Join button clicked");

            if (m_LANConnectionManager == null)
            {
                UpdateConnectionStatus("LAN Connection Manager not available", m_ErrorColor);
                return;
            }

            // Ensure we're in LAN mode
            if (m_CurrentMode != ConnectionModeManager.ConnectionMode.LANDirect)
            {
                UpdateConnectionStatus("Please switch to LAN Direct mode first", m_WarningColor);
                return;
            }

            // Validate IP address
            if (string.IsNullOrEmpty(m_CurrentIPInput))
            {
                UpdateConnectionStatus("Please enter a host IP address", m_WarningColor);
                return;
            }

            if (!m_LANConnectionManager.ValidateIPAddress(m_CurrentIPInput))
            {
                UpdateConnectionStatus("Invalid IP address format", m_ErrorColor);
                return;
            }

            // Start joining
            UpdateConnectionStatus($"Connecting to {m_CurrentIPInput}...", m_InfoColor);
            ShowLoadingIndicator(true);
            m_LANConnectionManager.JoinLAN(m_CurrentIPInput, m_DefaultPort);
        }

        /// <summary>
        /// Called when Disconnect button is clicked.
        /// </summary>
        private void OnDisconnectButtonClicked()
        {
            Debug.Log("[LANConnectionUI] Disconnect button clicked");

            if (m_ConnectionModeManager != null)
            {
                UpdateConnectionStatus("Disconnecting...", m_InfoColor);
                m_ConnectionModeManager.Disconnect();
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles connection mode changes.
        /// </summary>
        private void OnModeChanged(ConnectionModeManager.ConnectionMode mode)
        {
            Debug.Log($"[LANConnectionUI] Mode changed to: {mode}");
            m_CurrentMode = mode;
            UpdateUIForMode(mode);
        }

        /// <summary>
        /// Handles bindable variable mode changes (reactive updates).
        /// </summary>
        private void OnModeBindableChanged(ConnectionModeManager.ConnectionMode mode)
        {
            // Sync toggle state without triggering event
            if (m_ConnectionModeToggle != null)
            {
                m_ConnectionModeToggle.SetIsOnWithoutNotify(mode == ConnectionModeManager.ConnectionMode.LANDirect);
            }
        }

        /// <summary>
        /// Handles mode switch start event.
        /// </summary>
        private void OnModeSwitchStarted(ConnectionModeManager.ConnectionMode targetMode)
        {
            UpdateConnectionStatus($"Switching to {targetMode} mode...", m_InfoColor);
            ShowLoadingIndicator(true);
        }

        /// <summary>
        /// Handles mode switch completion.
        /// </summary>
        private void OnModeSwitchCompleted(ConnectionModeManager.ConnectionMode mode)
        {
            UpdateConnectionStatus($"Switched to {mode} mode", m_SuccessColor);
            ShowLoadingIndicator(false);
        }

        /// <summary>
        /// Handles mode switch failure.
        /// </summary>
        private void OnModeSwitchFailed(string reason)
        {
            UpdateConnectionStatus($"Mode switch failed: {reason}", m_ErrorColor);
            ShowLoadingIndicator(false);
        }

        /// <summary>
        /// Handles LAN connection status changes.
        /// </summary>
        private void OnLANStatusChanged(LANConnectionManager.LANConnectionStatus status)
        {
            Debug.Log($"[LANConnectionUI] LAN status changed to: {status}");

            switch (status)
            {
                case LANConnectionManager.LANConnectionStatus.Disconnected:
                    UpdateConnectionStatus("Disconnected", m_InfoColor);
                    ShowLoadingIndicator(false);
                    UpdateButtonStates(true, true, false);
                    break;

                case LANConnectionManager.LANConnectionStatus.Connecting:
                    UpdateConnectionStatus("Connecting...", m_InfoColor);
                    ShowLoadingIndicator(true);
                    UpdateButtonStates(false, false, false);
                    break;

                case LANConnectionManager.LANConnectionStatus.Connected:
                    UpdateConnectionStatus("Connected", m_SuccessColor);
                    ShowLoadingIndicator(false);
                    UpdateButtonStates(false, false, true);
                    break;

                case LANConnectionManager.LANConnectionStatus.Failed:
                    UpdateConnectionStatus("Connection failed", m_ErrorColor);
                    ShowLoadingIndicator(false);
                    UpdateButtonStates(true, true, false);
                    break;

                case LANConnectionManager.LANConnectionStatus.Timeout:
                    UpdateConnectionStatus("Connection timed out", m_ErrorColor);
                    ShowLoadingIndicator(false);
                    UpdateButtonStates(true, true, false);
                    break;
            }
        }

        /// <summary>
        /// Handles LAN connection failure.
        /// </summary>
        private void OnLANConnectionFailed(string reason)
        {
            Debug.LogWarning($"[LANConnectionUI] LAN connection failed: {reason}");
            UpdateConnectionStatus($"Connection failed: {reason}", m_ErrorColor);
            ShowLoadingIndicator(false);
            UpdateButtonStates(true, true, false);
        }

        /// <summary>
        /// Handles LAN connection success.
        /// </summary>
        private void OnLANConnectionSuccess()
        {
            Debug.Log("[LANConnectionUI] LAN connection successful");
            
            if (m_LANConnectionManager.IsHosting)
            {
                UpdateConnectionStatus($"Hosting on {m_LANConnectionManager.LocalIPAddress}", m_SuccessColor);
            }
            else
            {
                UpdateConnectionStatus($"Connected to {m_CurrentIPInput}", m_SuccessColor);
            }
            
            ShowLoadingIndicator(false);
        }

        #endregion

        #region UI Update Methods

        /// <summary>
        /// Updates UI elements based on current connection mode.
        /// </summary>
        private void UpdateUIForMode(ConnectionModeManager.ConnectionMode mode)
        {
            // Show/hide appropriate panels
            bool isLANMode = (mode == ConnectionModeManager.ConnectionMode.LANDirect);

            if (m_LANConnectionPanel != null)
            {
                m_LANConnectionPanel.SetActive(isLANMode);
            }

            if (m_CloudConnectionPanel != null)
            {
                m_CloudConnectionPanel.SetActive(!isLANMode);
            }

            // Update connection mode label
            if (m_ConnectionModeLabel != null)
            {
                m_ConnectionModeLabel.text = isLANMode ? "LAN Direct" : "Cloud (Internet)";
            }

            // Update local IP display
            if (isLANMode)
            {
                UpdateLocalIPDisplay();
            }

            // Update button states
            UpdateButtonStates(isLANMode, isLANMode, false);
        }

        /// <summary>
        /// Updates the local IP address display.
        /// </summary>
        private void UpdateLocalIPDisplay()
        {
            if (m_LocalIPDisplay != null && m_LANConnectionManager != null)
            {
                string localIP = m_LANConnectionManager.GetLocalIPAddress();
                m_LocalIPDisplay.text = $"<b>Your IP:</b> {localIP}";
            }
        }

        /// <summary>
        /// Updates IP validation feedback text.
        /// </summary>
        private void UpdateIPValidationFeedback(bool isValid)
        {
            if (m_IPValidationText == null) return;

            if (string.IsNullOrEmpty(m_CurrentIPInput))
            {
                m_IPValidationText.text = "";
                return;
            }

            if (isValid)
            {
                m_IPValidationText.text = "<color=#00FF00>✓</color> Valid IP address";
                m_IPValidationText.color = m_SuccessColor;
            }
            else
            {
                m_IPValidationText.text = "<color=#FF0000>✗</color> Invalid format";
                m_IPValidationText.color = m_ErrorColor;
            }
        }

        /// <summary>
        /// Updates connection status display.
        /// </summary>
        private void UpdateConnectionStatus(string message, Color color)
        {
            if (m_ConnectionStatusText != null)
            {
                m_ConnectionStatusText.text = $"<b>Status:</b> {message}";
                m_ConnectionStatusText.color = color;
            }
        }

        /// <summary>
        /// Updates detailed status message.
        /// </summary>
        private void UpdateStatusDetail(string message)
        {
            if (m_StatusDetailText != null)
            {
                m_StatusDetailText.text = message;
            }
        }

        /// <summary>
        /// Shows or hides the loading indicator.
        /// </summary>
        private void ShowLoadingIndicator(bool show)
        {
            if (m_LoadingIndicator != null)
            {
                m_LoadingIndicator.SetActive(show);
            }
        }

        /// <summary>
        /// Updates button interactable states.
        /// </summary>
        private void UpdateButtonStates(bool hostEnabled, bool joinEnabled, bool disconnectEnabled)
        {
            if (m_HostButton != null)
            {
                m_HostButton.interactable = hostEnabled;
            }

            if (m_JoinButton != null)
            {
                m_JoinButton.interactable = joinEnabled;
            }

            if (m_DisconnectButton != null)
            {
                m_DisconnectButton.interactable = disconnectEnabled;
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Manually refresh the local IP address display.
        /// Useful when network interfaces change.
        /// </summary>
        public void RefreshLocalIP()
        {
            UpdateLocalIPDisplay();
            Debug.Log("[LANConnectionUI] Local IP refreshed");
        }

        /// <summary>
        /// Programmatically set the IP address input field.
        /// </summary>
        /// <param name="ipAddress">IP address to set</param>
        public void SetIPAddress(string ipAddress)
        {
            if (m_IPAddressInput != null)
            {
                m_IPAddressInput.text = ipAddress;
                m_CurrentIPInput = ipAddress;
            }
        }

        /// <summary>
        /// Gets the current IP address input value.
        /// </summary>
        public string GetIPAddress()
        {
            return m_CurrentIPInput;
        }

        #endregion

        #region Editor Helpers

#if UNITY_EDITOR
        /// <summary>
        /// Context menu to test UI initialization.
        /// </summary>
        [ContextMenu("Test Initialize UI")]
        private void TestInitializeUI()
        {
            if (!m_IsInitialized)
            {
                InitializeUI();
            }
            Debug.Log("[LANConnectionUI] UI initialized for testing");
        }

        /// <summary>
        /// Context menu to simulate connection success.
        /// </summary>
        [ContextMenu("Test Connection Success")]
        private void TestConnectionSuccess()
        {
            OnLANConnectionSuccess();
        }

        /// <summary>
        /// Context menu to simulate connection failure.
        /// </summary>
        [ContextMenu("Test Connection Failure")]
        private void TestConnectionFailure()
        {
            OnLANConnectionFailed("Test error message");
        }

        /// <summary>
        /// Context menu to refresh local IP.
        /// </summary>
        [ContextMenu("Refresh Local IP")]
        private void TestRefreshLocalIP()
        {
            RefreshLocalIP();
        }
#endif

        #endregion
    }
}