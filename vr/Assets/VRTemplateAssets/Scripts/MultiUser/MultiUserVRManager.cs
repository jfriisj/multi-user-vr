using UnityEngine;
using Unity.Netcode;

namespace Unity.VRTemplate.MultiUser
{
    /// <summary>
    /// Simple multi-user VR manager using Unity Netcode
    /// Handles 3-user VR sessions with head and hand tracking synchronization
    /// </summary>
    public class MultiUserVRManager : NetworkBehaviour
    {
        [Header("Session Settings")]
        [SerializeField] private int maxPlayers = 3;
        [SerializeField] private bool autoStartHost = true;
        
        [Header("Debug")]
        [SerializeField] private bool showDebugUI = true;
        
        [Header("Events")]
        public UnityEngine.Events.UnityEvent OnSessionStarted;
        public UnityEngine.Events.UnityEvent OnPlayerJoined;
        public UnityEngine.Events.UnityEvent OnPlayerLeft;
        
        // Singleton instance
        public static MultiUserVRManager Instance { get; private set; }
        
        // Session state
        public bool IsSessionActive => NetworkManager.Singleton != null && NetworkManager.Singleton.IsListening;
        public int ConnectedPlayers => NetworkManager.Singleton != null ? (int)NetworkManager.Singleton.ConnectedClients.Count : 0;
        
        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            if (autoStartHost)
            {
                StartHost();
            }
        }
        
        /// <summary>
        /// Start as host - can accept up to 2 more players
        /// </summary>
        public void StartHost()
        {
            if (NetworkManager.Singleton == null)
            {
                Debug.LogError("NetworkManager not found! Please add NetworkManager to scene.");
                return;
            }
            
            NetworkManager.Singleton.StartHost();
            Debug.Log("Started as host");
            OnSessionStarted?.Invoke();
            
            // Initialize local player
            InitializeLocalPlayer();
        }
        
        /// <summary>
        /// Join existing session as client
        /// </summary>
        public void JoinSession()
        {
            if (NetworkManager.Singleton == null)
            {
                Debug.LogError("NetworkManager not found!");
                return;
            }
            
            NetworkManager.Singleton.StartClient();
            Debug.Log("Joining session as client");
        }
        
        /// <summary>
        /// Leave current session
        /// </summary>
        public void LeaveSession()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.Shutdown();
                Debug.Log("Left session");
            }
        }
        
        private void InitializeLocalPlayer()
        {
            // Find XR Origin and add network sync components
            var xrOrigin = FindObjectOfType<Unity.XR.CoreUtils.XROrigin>();
            if (xrOrigin != null)
            {
                // Add player sync component if not already present
                var playerSync = xrOrigin.GetComponent<VRPlayerSync>();
                if (playerSync == null)
                {
                    playerSync = xrOrigin.gameObject.AddComponent<VRPlayerSync>();
                }
                
                Debug.Log("Local VR player initialized with network sync");
            }
            else
            {
                Debug.LogError("XR Origin not found! Make sure you have XR Origin in the scene.");
            }
        }
        
        // Network callbacks
        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
                NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
            }
        }
        
        public override void OnNetworkDespawn()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
                NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
            }
        }
        
        private void OnClientConnected(ulong clientId)
        {
            Debug.Log($"Player connected: {clientId}");
            OnPlayerJoined?.Invoke();
            
            // Check if we've reached max players
            if (ConnectedPlayers >= maxPlayers)
            {
                Debug.Log($"Max players ({maxPlayers}) reached");
            }
        }
        
        private void OnClientDisconnected(ulong clientId)
        {
            Debug.Log($"Player disconnected: {clientId}");
            OnPlayerLeft?.Invoke();
        }
        
        private void OnGUI()
        {
            if (!showDebugUI) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 300, 200));
            GUILayout.Label("=== Multi-User VR Manager ===");
            GUILayout.Label($"Session Active: {IsSessionActive}");
            GUILayout.Label($"Connected Players: {ConnectedPlayers}/{maxPlayers}");
            
            if (NetworkManager.Singleton != null)
            {
                GUILayout.Label($"Role: {(NetworkManager.Singleton.IsHost ? "Host" : "Client")}");
            }
            
            GUILayout.Space(10);
            
            if (!IsSessionActive)
            {
                if (GUILayout.Button("Start Host"))
                {
                    StartHost();
                }
                if (GUILayout.Button("Join Session"))
                {
                    JoinSession();
                }
            }
            else
            {
                if (GUILayout.Button("Leave Session"))
                {
                    LeaveSession();
                }
            }
            
            GUILayout.EndArea();
        }
    }
}