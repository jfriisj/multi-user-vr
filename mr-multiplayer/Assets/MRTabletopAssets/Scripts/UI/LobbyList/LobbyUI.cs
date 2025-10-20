using System.Collections;
using Unity.Services.Lobbies.Models;
using XRMultiplayer;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Vivox;

namespace UnityEngine.XR.Templates.MRTTabletopAssets
{
    /// <summary>
    /// Main lobby UI controller that supports both Cloud (UGS) and LAN Direct connection modes.
    /// Provides unified entry point for multiplayer session management.
    /// </summary>
    public class LobbyUI : MonoBehaviour
    {
        [Header("Lobby List")]
        [SerializeField] Transform m_LobbyListParent;
        [SerializeField] GameObject m_LobbyListPrefab;
        [SerializeField] float m_AutoRefreshTime = 5.0f;
        [SerializeField] float m_RefreshCooldownTime = .5f;

        [Header("Connection Texts")]
        [SerializeField] TMP_Text m_ConnectionUpdatedText;
        [SerializeField] TMP_Text m_ConnectionSuccessText;
        [SerializeField] TMP_Text m_ConnectionFailedText;

        [Header("Room Creation")]
        [SerializeField] TMP_InputField m_RoomNameText;
        [SerializeField] Toggle m_PrivacyToggle;

        [Header("Connection Panels")]
        [SerializeField] GameObject[] m_ConnectionSubPanels;

        [Header("LAN Connection Integration")]
        [SerializeField, Tooltip("Reference to LANConnectionUI component for LAN mode")]
        LANConnectionUI m_LANConnectionUI;

        [SerializeField, Tooltip("Toggle for switching between Cloud and LAN modes")]
        Toggle m_ConnectionModeToggle;

        [SerializeField, Tooltip("Text label showing current connection mode")]
        TMP_Text m_ConnectionModeLabel;

        [SerializeField, Tooltip("Panel ID for LAN connection panel (added to m_ConnectionSubPanels)")]
        int m_LANPanelIndex = 7;

        VoiceChatManager m_VoiceChatManager;
        ConnectionModeManager m_ConnectionModeManager;

        Coroutine m_UpdateLobbiesRoutine;
        Coroutine m_CooldownFillRoutine;

        bool m_Private = false;
        int m_PlayerCount;

        // Track current connection mode
        ConnectionModeManager.ConnectionMode m_CurrentMode = ConnectionModeManager.ConnectionMode.Cloud;

        const string k_CloudModeLabel = "Cloud (Internet)";
        const string k_LANModeLabel = "LAN Direct";

        private void Awake()
        {
            m_VoiceChatManager = FindFirstObjectByType<VoiceChatManager>();
            m_ConnectionModeManager = FindFirstObjectByType<ConnectionModeManager>();

            // Subscribe to status updates
            LobbyManager.status.Subscribe(ConnectedUpdated);

            // Find LANConnectionUI if not assigned
            if (m_LANConnectionUI == null)
            {
                m_LANConnectionUI = FindFirstObjectByType<LANConnectionUI>();
            }

            // Subscribe to connection mode events
            if (m_ConnectionModeManager != null)
            {
                m_ConnectionModeManager.OnModeChanged += OnConnectionModeChanged;
                m_CurrentMode = m_ConnectionModeManager.CurrentMode;
            }
            else
            {
                Debug.LogWarning("[LobbyUI] ConnectionModeManager not found. Operating in Cloud-only mode.");
            }
        }

        private void Start()
        {
            if (m_PrivacyToggle != null)
                m_PrivacyToggle.onValueChanged.AddListener(TogglePrivacy);

            m_PlayerCount = XRINetworkGameManager.maxPlayers;

            XRINetworkGameManager.Instance.connectionFailedAction += FailedToConnect;
            XRINetworkGameManager.Instance.connectionUpdated += ConnectedUpdated;

            // Setup connection mode toggle
            if (m_ConnectionModeToggle != null)
            {
                m_ConnectionModeToggle.onValueChanged.AddListener(OnConnectionModeToggleChanged);

                // Initialize toggle state based on current mode without invoking callback
                m_ConnectionModeToggle.SetIsOnWithoutNotify(m_CurrentMode == ConnectionModeManager.ConnectionMode.LANDirect);
            }

            // Update mode label and panels
            UpdateConnectionModeLabel();
            UpdateUIForConnectionMode();

            foreach (Transform t in m_LobbyListParent)
            {
                Destroy(t.gameObject);
            }
        }

        private void OnEnable()
        {
            CheckInternetAsync();
        }

        private void OnDisable()
        {
            HideLobbies();
        }

        private void OnDestroy()
        {
            XRINetworkGameManager.Instance.connectionFailedAction -= FailedToConnect;
            XRINetworkGameManager.Instance.connectionUpdated -= ConnectedUpdated;

            LobbyManager.status.Unsubscribe(ConnectedUpdated);

            // Unsubscribe from connection mode events
            if (m_ConnectionModeManager != null)
            {
                m_ConnectionModeManager.OnModeChanged -= OnConnectionModeChanged;
            }

            // Remove UI listeners to avoid leaks
            if (m_ConnectionModeToggle != null)
            {
                m_ConnectionModeToggle.onValueChanged.RemoveListener(OnConnectionModeToggleChanged);
            }
            if (m_PrivacyToggle != null)
            {
                m_PrivacyToggle.onValueChanged.RemoveListener(TogglePrivacy);
            }
        }

        #region Connection Mode Management

        /// <summary>
        /// Called when connection mode toggle is changed by user.
        /// </summary>
        /// <param name="isLANMode">True if LAN mode is selected, false for Cloud mode</param>
        private void OnConnectionModeToggleChanged(bool isLANMode)
        {
            if (m_ConnectionModeManager == null)
            {
                Debug.LogWarning("[LobbyUI] Cannot switch mode: ConnectionModeManager not available.");
                return;
            }

            ConnectionModeManager.ConnectionMode newMode = isLANMode 
                ? ConnectionModeManager.ConnectionMode.LANDirect 
                : ConnectionModeManager.ConnectionMode.Cloud;

            // Only switch if different from current mode
            if (newMode != m_CurrentMode)
            {
                m_ConnectionModeManager.SetConnectionMode(newMode);
            }
        }

        /// <summary>
        /// Called when connection mode changes (from ConnectionModeManager).
        /// </summary>
        /// <param name="newMode">The new connection mode</param>
        private void OnConnectionModeChanged(ConnectionModeManager.ConnectionMode newMode)
        {
            m_CurrentMode = newMode;

            // Update toggle without triggering event
            if (m_ConnectionModeToggle != null)
            {
                m_ConnectionModeToggle.SetIsOnWithoutNotify(newMode == ConnectionModeManager.ConnectionMode.LANDirect);
            }

            // Update mode label
            UpdateConnectionModeLabel();

            // Update UI to show appropriate panel
            UpdateUIForConnectionMode();
        }

        /// <summary>
        /// Updates the connection mode label text.
        /// </summary>
        private void UpdateConnectionModeLabel()
        {
            if (m_ConnectionModeLabel != null)
            {
                m_ConnectionModeLabel.text = m_CurrentMode == ConnectionModeManager.ConnectionMode.LANDirect 
                    ? k_LANModeLabel 
                    : k_CloudModeLabel;
            }
        }

        /// <summary>
        /// Updates UI panels based on current connection mode.
        /// </summary>
        private void UpdateUIForConnectionMode()
        {
            if (m_CurrentMode == ConnectionModeManager.ConnectionMode.LANDirect)
            {
                // Show LAN panel
                ToggleConnectionSubPanel(m_LANPanelIndex);
            }
            else
            {
                // Show main cloud panel
                ToggleConnectionSubPanel(0);
            }
        }

        /// <summary>
        /// Checks if current mode is LAN Direct.
        /// </summary>
        /// <returns>True if in LAN mode</returns>
        private bool IsLANMode()
        {
            return m_CurrentMode == ConnectionModeManager.ConnectionMode.LANDirect;
        }

        #endregion

        public async void CheckInternetAsync()
        {
            if (XRINetworkGameManager.Instance == null)
                return;

            // Skip authentication check in LAN mode
            if (IsLANMode())
            {
                CheckForInternet();
                return;
            }

            if (!XRINetworkGameManager.Instance.IsAuthenticated())
            {
                ToggleConnectionSubPanel(6);
                await XRINetworkGameManager.Instance.Authenticate();
            }
            CheckForInternet();
        }

        void CheckForInternet()
        {
            // In LAN mode, internet is not required
            if (IsLANMode())
            {
                ToggleConnectionSubPanel(m_LANPanelIndex);
                return;
            }

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                ToggleConnectionSubPanel(6);
            }
            else
            {
                ToggleConnectionSubPanel(0);
            }
        }

        public void CreateLobby()
        {
            // Only allow cloud lobby creation in Cloud mode
            if (IsLANMode())
            {
                Debug.LogWarning("[LobbyUI] Cannot create cloud lobby in LAN mode. Switch to Cloud mode first.");
                FailedToConnect("Cannot create lobby in LAN Direct mode. Switch to Cloud mode.");
                return;
            }

            XRINetworkGameManager.Connected.Subscribe(OnConnected);
            if (string.IsNullOrEmpty(m_RoomNameText.text) || m_RoomNameText.text == "<Room Name>")
            {
                m_RoomNameText.text = $"{XRINetworkGameManager.LocalPlayerName.Value}'s Table";
            }
            XRINetworkGameManager.Instance.CreateNewLobby(m_RoomNameText.text, m_Private, m_PlayerCount);
            m_ConnectionSuccessText.text = $"Joining {m_RoomNameText.text}";
        }

        public void UpdatePlayerCount(int count)
        {
            m_PlayerCount = Mathf.Clamp(count, 1, XRINetworkGameManager.maxPlayers);
        }

        public void CancelConnection()
        {
            XRINetworkGameManager.Instance.CancelMatchmaking();
        }

        /// <summary>
        /// Set the room name
        /// </summary>
        /// <param name="roomName">The name of the room</param>
        /// <remarks> This function is called from <see cref="XRIKeyboardDisplay"/>
        public void SetRoomName(string roomName)
        {
            if (!string.IsNullOrEmpty(roomName))
            {
                m_RoomNameText.text = roomName;
            }
        }

        /// <summary>
        /// Join a room by code
        /// </summary>
        /// <param name="roomCode">The room code to join</param>
        /// <remarks> This function is called from <see cref="XRIKeyboardDisplay"/>
        public void EnterRoomCode(string roomCode)
        {
            // Only allow cloud lobby join in Cloud mode
            if (IsLANMode())
            {
                Debug.LogWarning("[LobbyUI] Cannot join cloud lobby in LAN mode. Switch to Cloud mode first.");
                FailedToConnect("Cannot join lobby by code in LAN Direct mode. Switch to Cloud mode.");
                return;
            }

            if (roomCode.Length < 5)
            {
                ToggleConnectionSubPanel(5);
                return;
            }
            ToggleConnectionSubPanel(3);
            XRINetworkGameManager.Connected.Subscribe(OnConnected);
            XRINetworkGameManager.Instance.JoinLobbyByCode(roomCode.ToUpper());
            m_ConnectionSuccessText.text = $"Joining Room: {roomCode.ToUpper()}";
        }

        public void JoinLobby(Lobby lobby)
        {
            // Only allow cloud lobby join in Cloud mode
            if (IsLANMode())
            {
                Debug.LogWarning("[LobbyUI] Cannot join cloud lobby in LAN mode. Switch to Cloud mode first.");
                FailedToConnect("Cannot join lobby in LAN Direct mode. Switch to Cloud mode.");
                return;
            }

            ToggleConnectionSubPanel(3);
            XRINetworkGameManager.Connected.Subscribe(OnConnected);
            XRINetworkGameManager.Instance.JoinLobbySpecific(lobby);
            m_ConnectionSuccessText.text = $"Joining {lobby.Name}";
        }

        public void QuickJoinLobby()
        {
            // Only allow quick join in Cloud mode
            if (IsLANMode())
            {
                Debug.LogWarning("[LobbyUI] Cannot quick join in LAN mode. Switch to Cloud mode first.");
                FailedToConnect("Quick Join not available in LAN Direct mode. Switch to Cloud mode.");
                return;
            }

            XRINetworkGameManager.Connected.Subscribe(OnConnected);
            XRINetworkGameManager.Instance.QuickJoinLobby();
            m_ConnectionSuccessText.text = "Joining Random";
        }

        public void SetVoiceChatAudidibleDistance(int audibleDistance)
        {
            if (audibleDistance <= m_VoiceChatManager.ConversationalDistance)
            {
                audibleDistance = m_VoiceChatManager.ConversationalDistance + 1;
            }
            m_VoiceChatManager.AudibleDistance = audibleDistance;
        }

        public void SetVoiceChatConversationalDistance(int conversationalDistance)
        {
            m_VoiceChatManager.ConversationalDistance = conversationalDistance;
        }

        public void SetVoiceChatAudioFadeIntensity(float fadeIntensity)
        {
            m_VoiceChatManager.AudioFadeIntensity = fadeIntensity;
        }

        public void SetVoiceChatAudioFadeModel(int fadeModel)
        {
            m_VoiceChatManager.AudioFadeModel = (AudioFadeModel)fadeModel;
        }

        public void TogglePrivacy(bool toggle)
        {
            m_Private = toggle;
        }

        public void ToggleConnectionSubPanel(int panelId)
        {
            for (int i = 0; i < m_ConnectionSubPanels.Length; i++)
            {
                m_ConnectionSubPanels[i].SetActive(i == panelId);
            }


            if (panelId == 1)
            {
                ShowLobbies();
            }
            else
            {
                HideLobbies();
            }
        }

        void OnConnected(bool connected)
        {
            if (connected)
            {
                ToggleConnectionSubPanel(4);

                // Unsubscribe from the event after connection to prevent multiple subscriptions
                XRINetworkGameManager.Connected.Unsubscribe(OnConnected);
            }
        }

        void ConnectedUpdated(string update)
        {
            m_ConnectionUpdatedText.text = $"<b>Status:</b> {update}";
        }

        public void FailedToConnect(string reason)
        {
            ToggleConnectionSubPanel(5);
            m_ConnectionFailedText.text = $"<b>Error:</b> {reason}";
        }

        public void HideLobbies()
        {
            EnableRefresh();
            if (m_UpdateLobbiesRoutine != null) StopCoroutine(m_UpdateLobbiesRoutine);
        }

        public void ShowLobbies()
        {
            // Only show lobbies in Cloud mode
            if (IsLANMode())
            {
                Debug.LogWarning("[LobbyUI] Lobby list not available in LAN mode.");
                return;
            }

            GetAllLobbies();
            if (m_UpdateLobbiesRoutine != null) StopCoroutine(m_UpdateLobbiesRoutine);
            m_UpdateLobbiesRoutine = StartCoroutine(UpdateAvailableLobbies());
        }

        IEnumerator UpdateAvailableLobbies()
        {
            while (true)
            {
                yield return new WaitForSeconds(m_AutoRefreshTime);
                GetAllLobbies();
            }
        }

        bool onCD = false;
        void EnableRefresh()
        {
            onCD = false;
        }

        IEnumerator UpdateButtonCooldown()
        {
            onCD = true;
            yield return new WaitForSeconds(m_RefreshCooldownTime);
            EnableRefresh();
        }

        async void GetAllLobbies()
        {
            // Skip in LAN mode
            if (IsLANMode())
            {
                return;
            }

            if (onCD || (int)XRINetworkGameManager.CurrentConnectionState.Value < 2) return;
            if (m_CooldownFillRoutine != null) StopCoroutine(m_CooldownFillRoutine);
            m_CooldownFillRoutine = StartCoroutine(UpdateButtonCooldown());

            QueryResponse lobbies = await LobbyManager.GetLobbiesAsync();

            foreach (Transform t in m_LobbyListParent)
            {
                Destroy(t.gameObject);
            }

            if (lobbies.Results != null || lobbies.Results.Count > 0)
            {
                foreach (var lobby in lobbies.Results)
                {
                    if (LobbyManager.CheckForLobbyFilter(lobby))
                    {
                        continue;
                    }

                    if (LobbyManager.CheckForIncompatibilityFilter(lobby))
                    {
                        LobbyListSlotUI newLobbyUI = Instantiate(m_LobbyListPrefab, m_LobbyListParent).GetComponent<LobbyListSlotUI>();
                        newLobbyUI.CreateNonJoinableLobbyUI(lobby, this, "Version Conflict");
                        continue;
                    }

                    if (LobbyManager.CanJoinLobby(lobby))
                    {
                        LobbyListSlotUI newLobbyUI = Instantiate(m_LobbyListPrefab, m_LobbyListParent).GetComponent<LobbyListSlotUI>();
                        newLobbyUI.CreateLobbyUI(lobby, this);
                    }
                }
            }
        }
    }
}