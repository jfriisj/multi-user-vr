using UnityEngine;
using Unity.Netcode;

namespace Unity.VRTemplate.MultiUser
{
    /// <summary>
    /// Quick setup helper for multi-user VR scene
    /// Automatically configures networking components on scene objects
    /// </summary>
    public class MultiUserVRSetup : MonoBehaviour
    {
        [Header("Auto Setup")]
        [SerializeField] private bool setupOnStart = true;
        [SerializeField] private bool addNetworkManagerIfMissing = true;
        [SerializeField] private bool configureInteractables = true;
        
        [Header("Network Settings")]
        [SerializeField] private int maxConnections = 3;
        [SerializeField] private ushort port = 7777;
        
        private void Start()
        {
            if (setupOnStart)
            {
                SetupMultiUserVR();
            }
        }
        
        [ContextMenu("Setup Multi-User VR")]
        public void SetupMultiUserVR()
        {
            Debug.Log("Setting up Multi-User VR...");
            
            // 1. Setup Network Manager
            SetupNetworkManager();
            
            // 2. Setup MultiUser Manager
            SetupMultiUserManager();
            
            // 3. Setup VR Player Sync
            SetupVRPlayerSync();
            
            // 4. Setup Networked Interactables
            if (configureInteractables)
            {
                SetupNetworkedInteractables();
            }
            
            Debug.Log("Multi-User VR setup complete!");
        }
        
        private void SetupNetworkManager()
        {
            var networkManager = FindObjectOfType<NetworkManager>();
            
            if (networkManager == null && addNetworkManagerIfMissing)
            {
                // Create NetworkManager GameObject
                var nmObject = new GameObject("NetworkManager");
                networkManager = nmObject.AddComponent<NetworkManager>();
                
                Debug.Log("Created NetworkManager");
            }
            
            if (networkManager != null)
            {
                // Configure transport
                var transport = networkManager.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
                if (transport == null)
                {
                    transport = networkManager.gameObject.AddComponent<Unity.Netcode.Transports.UTP.UnityTransport>();
                }
                
                transport.ConnectionData.Port = port;
                
                Debug.Log($"Configured NetworkManager: Port {port}");
            }
        }
        
        private void SetupMultiUserManager()
        {
            var multiUserManager = FindObjectOfType<MultiUserVRManager>();
            
            if (multiUserManager == null)
            {
                // Create MultiUserManager GameObject
                var muObject = new GameObject("MultiUserVRManager");
                multiUserManager = muObject.AddComponent<MultiUserVRManager>();
                
                // Add NetworkObject component
                var networkObject = muObject.AddComponent<NetworkObject>();
                
                Debug.Log("Created MultiUserVRManager");
            }
        }
        
        private void SetupVRPlayerSync()
        {
            // Find XR Origin
            var xrOrigin = FindObjectOfType<Unity.XR.CoreUtils.XROrigin>();
            if (xrOrigin == null)
            {
                Debug.LogWarning("XR Origin not found! Make sure you have XR Origin in the scene.");
                return;
            }
            
            // Add VRPlayerSync if not already present
            var playerSync = xrOrigin.GetComponent<VRPlayerSync>();
            if (playerSync == null)
            {
                playerSync = xrOrigin.gameObject.AddComponent<VRPlayerSync>();
                Debug.Log("Added VRPlayerSync to XR Origin");
            }
            
            // Add NetworkObject if not present
            var networkObject = xrOrigin.GetComponent<NetworkObject>();
            if (networkObject == null)
            {
                networkObject = xrOrigin.gameObject.AddComponent<NetworkObject>();
                Debug.Log("Added NetworkObject to XR Origin");
            }
        }
        
        private void SetupNetworkedInteractables()
        {
            // Find all interactable objects in the scene
            var interactables = FindObjectsOfType<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            
            int configuredCount = 0;
            foreach (var interactable in interactables)
            {
                // Add NetworkedInteractable component
                var networkedInteractable = interactable.GetComponent<NetworkedInteractable>();
                if (networkedInteractable == null)
                {
                    networkedInteractable = interactable.gameObject.AddComponent<NetworkedInteractable>();
                    configuredCount++;
                }
                
                // Add NetworkObject component
                var networkObject = interactable.GetComponent<NetworkObject>();
                if (networkObject == null)
                {
                    networkObject = interactable.gameObject.AddComponent<NetworkObject>();
                }
                
                // Ensure Rigidbody is present (required by NetworkedInteractable)
                var rigidbody = interactable.GetComponent<Rigidbody>();
                if (rigidbody == null)
                {
                    rigidbody = interactable.gameObject.AddComponent<Rigidbody>();
                }
            }
            
            Debug.Log($"Configured {configuredCount} networked interactables");
        }
        
        [ContextMenu("Test Network Connection")]
        public void TestNetworkConnection()
        {
            var networkManager = NetworkManager.Singleton;
            if (networkManager == null)
            {
                Debug.LogError("NetworkManager not found!");
                return;
            }
            
            if (!networkManager.IsListening)
            {
                Debug.Log("Starting test host...");
                networkManager.StartHost();
            }
            else
            {
                Debug.Log($"Network Status - Is Host: {networkManager.IsHost}, Is Client: {networkManager.IsClient}, Connected Clients: {networkManager.ConnectedClients.Count}");
            }
        }
    }
}