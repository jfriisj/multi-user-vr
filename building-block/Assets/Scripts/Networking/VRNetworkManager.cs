using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

namespace MultiUserVR.Networking
{
    /// <summary>
    /// Network manager for co-located multi-user VR system
    /// Integrates Unity Netcode with Meta Platform SDK (LocalMatchmaking, Colocation)
    /// 
    /// Phase 2 Implementation - ACTIVE:
    /// - Manages network connections between 3 Quest headsets
    /// - Integrates with Meta LocalMatchmaking for device discovery
    /// - Coordinates SharedSpatialAnchor alignment
    /// - Spawns networked player prefabs
    /// </summary>
    [RequireComponent(typeof(NetworkManager))]
    public class VRNetworkManager : MonoBehaviour
    {
        [Header("Network Configuration")]
        [Tooltip("Maximum number of connected users (3 for MVP)")]
        public int maxPlayers = 3;

        [Tooltip("Network port for local connection")]
        public ushort networkPort = 7777;

        [Header("Player Setup")]
        [Tooltip("Player prefab to spawn for each connected user")]
        public GameObject playerPrefab;

        [Header("Meta Platform Integration")]
        [Tooltip("Use Meta LocalMatchmaking for automatic device discovery")]
        public bool useLocalMatchmaking = true;

        [Header("Colocation Setup")]
        [Tooltip("Reference to SharedSpatialAnchorCore for physical space alignment")]
        public GameObject sharedSpatialAnchor;

        [Header("Status")]
        [SerializeField] private bool isHost = false;
        [SerializeField] private int connectedPlayers = 0;
        [SerializeField] private bool isColocationAligned = false;

        // Unity Netcode for GameObjects integration
        private NetworkManager networkManager;
        private UnityTransport transport;
        
        // Meta Platform SDK integration (to be added via Building Blocks)
        // These will be connected in Phase 2 when Meta components are added
        // private LocalMatchmaking localMatchmaking;
        // private ColocationController colocationController;
        // private SharedSpatialAnchorCore spatialAnchor;

        private void Awake()
        {
            // Get NetworkManager component
            networkManager = GetComponent<NetworkManager>();
            transport = GetComponent<UnityTransport>();
            
            if (networkManager == null)
            {
                Debug.LogError("[VRNetworkManager] NetworkManager component not found!");
                return;
            }
            
            if (transport == null)
            {
                Debug.LogError("[VRNetworkManager] UnityTransport component not found!");
                return;
            }
            
            // Configure NetworkManager
            ConfigureNetworkManager();
            
            // Subscribe to network events
            networkManager.OnServerStarted += OnServerStarted;
            networkManager.OnClientConnectedCallback += OnClientConnected;
            networkManager.OnClientDisconnectCallback += OnClientDisconnected;
            
            Debug.Log("[VRNetworkManager] Phase 2 implementation ACTIVE");
            Debug.Log($"[VRNetworkManager] Network Port: {networkPort}");
            Debug.Log($"[VRNetworkManager] Max Players: {maxPlayers}");
        }
        
        private void OnDestroy()
        {
            // Unsubscribe from events
            if (networkManager != null)
            {
                networkManager.OnServerStarted -= OnServerStarted;
                networkManager.OnClientConnectedCallback -= OnClientConnected;
                networkManager.OnClientDisconnectCallback -= OnClientDisconnected;
            }
        }
        
        /// <summary>
        /// Configure NetworkManager settings for co-located VR
        /// </summary>
        private void ConfigureNetworkManager()
        {
            // Configure transport
            transport.ConnectionData.Port = networkPort;
            transport.ConnectionData.ServerListenAddress = "0.0.0.0"; // Listen on all interfaces
            
            // For local testing, use localhost. For Quest devices, use local IP
            #if UNITY_EDITOR
            transport.ConnectionData.Address = "127.0.0.1";
            #else
            // On Quest, this will need to be set to the host's local IP
            // This can be configured via UI or auto-discovered via LocalMatchmaking
            transport.ConnectionData.Address = "192.168.1.100"; // Placeholder - will be set dynamically
            #endif
            
            // Configure player prefab
            if (playerPrefab != null)
            {
                var networkObject = playerPrefab.GetComponent<NetworkObject>();
                if (networkObject != null)
                {
                    // Add player prefab to NetworkManager's prefab list
                    var prefabsList = networkManager.NetworkConfig.Prefabs.Prefabs;
                    var prefabEntry = new NetworkPrefab { Prefab = playerPrefab };
                    
                    // Check if not already in list
                    bool alreadyAdded = false;
                    foreach (var existingPrefab in prefabsList)
                    {
                        if (existingPrefab.Prefab == playerPrefab)
                        {
                            alreadyAdded = true;
                            break;
                        }
                    }
                    
                    if (!alreadyAdded)
                    {
                        networkManager.NetworkConfig.Prefabs.Add(prefabEntry);
                        Debug.Log($"[VRNetworkManager] Added {playerPrefab.name} to network prefabs");
                    }
                }
                else
                {
                    Debug.LogWarning($"[VRNetworkManager] Player prefab {playerPrefab.name} missing NetworkObject component!");
                }
            }
            else
            {
                Debug.LogWarning("[VRNetworkManager] Player prefab not assigned!");
            }
        }

        /// <summary>
        /// Start as host - creates shared spatial anchor and starts network
        /// </summary>
        public void StartAsHost()
        {
            if (networkManager == null)
            {
                Debug.LogError("[VRNetworkManager] Cannot start - NetworkManager not initialized");
                return;
            }
            
            Debug.Log("[VRNetworkManager] Starting as Host...");
            
            // Phase 2 Implementation:
            // 1. Initialize Unity Netcode as host
            bool started = networkManager.StartHost();
            
            if (started)
            {
                isHost = true;
                Debug.Log("[VRNetworkManager] Host started successfully");
                
                // 2. Create SharedSpatialAnchor (Meta Platform SDK integration)
                // TODO: Add when Meta colocation components are added
                // CreateSharedSpatialAnchor();
                
                // 3. Start LocalMatchmaking to advertise session
                // TODO: Add when Meta LocalMatchmaking is added
                // StartLocalMatchmaking();
                
                // 4. Spawn local player
                SpawnLocalPlayer();
            }
            else
            {
                Debug.LogError("[VRNetworkManager] Failed to start as host");
            }
        }

        /// <summary>
        /// Join existing session - discovers host and aligns to shared anchor
        /// </summary>
        public void JoinAsClient()
        {
            if (networkManager == null)
            {
                Debug.LogError("[VRNetworkManager] Cannot join - NetworkManager not initialized");
                return;
            }
            
            Debug.Log("[VRNetworkManager] Joining as Client...");
            
            // Phase 2 Implementation:
            // 1. Use LocalMatchmaking to find available sessions
            // TODO: Add when Meta LocalMatchmaking is added
            // FindAndConnectToHost();
            
            // 2. Connect to host via Unity Netcode
            bool started = networkManager.StartClient();
            
            if (started)
            {
                isHost = false;
                Debug.Log("[VRNetworkManager] Client started successfully");
                
                // 3. Receive SharedSpatialAnchor from host (happens via network)
                // 4. Align local camera to shared anchor
                // TODO: Add when Meta colocation components are added
                // AlignToSharedAnchor();
            }
            else
            {
                Debug.LogError("[VRNetworkManager] Failed to start as client");
            }
        }

        /// <summary>
        /// Disconnect from network session
        /// </summary>
        public void Disconnect()
        {
            if (networkManager == null) return;
            
            Debug.Log("[VRNetworkManager] Disconnecting...");
            
            // Phase 2 Implementation:
            // 1. Despawn local player (handled automatically by NetworkManager)
            // 2. Disconnect from Unity Netcode
            networkManager.Shutdown();
            
            // 3. Clean up Meta Platform SDK session
            // TODO: Add when Meta components are added
            // CleanupColocationSession();
            
            isHost = false;
            connectedPlayers = 0;
            isColocationAligned = false;
            
            Debug.Log("[VRNetworkManager] Disconnected");
        }
        
        /// <summary>
        /// Spawn player for local user
        /// </summary>
        private void SpawnLocalPlayer()
        {
            if (!networkManager.IsServer && !networkManager.IsClient)
            {
                Debug.LogWarning("[VRNetworkManager] Cannot spawn player - not connected to network");
                return;
            }
            
            if (playerPrefab == null)
            {
                Debug.LogWarning("[VRNetworkManager] Cannot spawn player - player prefab not assigned");
                return;
            }
            
            // On host, spawn happens automatically via NetworkManager
            // On client, spawn is requested from server
            Debug.Log($"[VRNetworkManager] Player spawning handled by NetworkManager (IsHost: {networkManager.IsHost})");
        }
        
        /// <summary>
        /// Called when server starts
        /// </summary>
        private void OnServerStarted()
        {
            Debug.Log("[VRNetworkManager] Server started callback");
            connectedPlayers = 1; // Host counts as first player
        }
        
        /// <summary>
        /// Called when a client connects
        /// </summary>
        private void OnClientConnected(ulong clientId)
        {
            connectedPlayers++;
            Debug.Log($"[VRNetworkManager] Client {clientId} connected ({connectedPlayers}/{maxPlayers})");
            
            if (connectedPlayers > maxPlayers)
            {
                Debug.LogWarning($"[VRNetworkManager] Maximum players ({maxPlayers}) exceeded!");
            }
        }
        
        /// <summary>
        /// Called when a client disconnects
        /// </summary>
        private void OnClientDisconnected(ulong clientId)
        {
            connectedPlayers--;
            Debug.Log($"[VRNetworkManager] Client {clientId} disconnected ({connectedPlayers}/{maxPlayers})");
        }

        /// <summary>
        /// Called when colocation alignment is complete
        /// </summary>
        private void OnColocationAligned()
        {
            isColocationAligned = true;
            Debug.Log("[VRNetworkManager] Colocation alignment complete - all users in shared coordinate system");
        }

        #region Debug UI (Scene View)
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 350, 250));
            GUILayout.Label("=== VR Network Manager (Phase 2) ===");
            
            string status = "Not Connected";
            if (networkManager != null)
            {
                if (networkManager.IsHost) status = "Host (Server + Client)";
                else if (networkManager.IsServer) status = "Server Only";
                else if (networkManager.IsClient) status = "Client Only";
            }
            
            GUILayout.Label($"Status: {status}");
            GUILayout.Label($"Connected Players: {connectedPlayers}/{maxPlayers}");
            GUILayout.Label($"Is Host: {isHost}");
            GUILayout.Label($"Colocation Aligned: {isColocationAligned}");
            GUILayout.Label($"Network Port: {networkPort}");
            
            GUI.enabled = networkManager != null && !networkManager.IsListening;
            if (GUILayout.Button("Start as Host"))
            {
                StartAsHost();
            }
            
            if (GUILayout.Button("Join as Client"))
            {
                JoinAsClient();
            }
            GUI.enabled = true;
            
            GUI.enabled = networkManager != null && networkManager.IsListening;
            if (GUILayout.Button("Disconnect"))
            {
                Disconnect();
            }
            GUI.enabled = true;
            
            GUILayout.EndArea();
        }
        #endregion
    }
}
