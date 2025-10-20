using UnityEngine;
using Unity.Netcode;

namespace Unity.VRTemplate.MultiUser
{
    /// <summary>
    /// Synchronizes interactable objects across the network
    /// Handles position, rotation, and grab state for VR objects
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class NetworkedInteractable : NetworkBehaviour
    {
        [Header("Sync Settings")]
        [SerializeField] private float syncRate = 30f;
        [SerializeField] private bool syncPosition = true;
        [SerializeField] private bool syncRotation = true;
        [SerializeField] private float positionThreshold = 0.01f;
        [SerializeField] private float rotationThreshold = 1f;
        
        // Network variables
        private NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();
        private NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>();
        private NetworkVariable<bool> networkIsGrabbed = new NetworkVariable<bool>();
        private NetworkVariable<ulong> networkGrabbedByClient = new NetworkVariable<ulong>();
        
        // Components
        private Rigidbody rb;
        private bool isGrabbed;
        private ulong grabbedByClientId;
        private float syncTimer;
        
        // Previous values for threshold checking
        private Vector3 lastSentPosition;
        private Quaternion lastSentRotation;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        
        public override void OnNetworkSpawn()
        {
            // Subscribe to network variable changes
            networkPosition.OnValueChanged += OnPositionChanged;
            networkRotation.OnValueChanged += OnRotationChanged;
            networkIsGrabbed.OnValueChanged += OnGrabStateChanged;
            networkGrabbedByClient.OnValueChanged += OnGrabbedByClientChanged;
            
            if (IsOwner)
            {
                // Initialize network variables with current transform
                networkPosition.Value = transform.position;
                networkRotation.Value = transform.rotation;
            }
        }
        
        private void Update()
        {
            if (!IsSpawned) return;
            
            // Only the owner (grabbing client or server) sends updates
            if (IsOwner)
            {
                SyncTransformData();
            }
        }
        
        private void SyncTransformData()
        {
            syncTimer += Time.deltaTime;
            if (syncTimer >= 1f / syncRate)
            {
                syncTimer = 0f;
                
                bool positionChanged = syncPosition && Vector3.Distance(transform.position, lastSentPosition) > positionThreshold;
                bool rotationChanged = syncRotation && Quaternion.Angle(transform.rotation, lastSentRotation) > rotationThreshold;
                
                if (positionChanged || rotationChanged)
                {
                    // Update network variables
                    if (syncPosition)
                    {
                        networkPosition.Value = transform.position;
                        lastSentPosition = transform.position;
                    }
                    
                    if (syncRotation)
                    {
                        networkRotation.Value = transform.rotation;
                        lastSentRotation = transform.rotation;
                    }
                }
            }
        }
        
        /// <summary>
        /// Called when a player grabs this object
        /// </summary>
        public void OnGrabbed()
        {
            if (!IsSpawned) return;
            
            ulong clientId = NetworkManager.Singleton.LocalClientId;
            
            if (IsServer)
            {
                // Server directly updates
                SetGrabState(true, clientId);
            }
            else
            {
                // Client requests grab
                RequestGrabServerRpc(clientId);
            }
        }
        
        /// <summary>
        /// Called when a player releases this object
        /// </summary>
        public void OnReleased()
        {
            if (!IsSpawned) return;
            
            ulong clientId = NetworkManager.Singleton.LocalClientId;
            
            if (IsServer)
            {
                // Server directly updates
                SetGrabState(false, 0);
            }
            else
            {
                // Client requests release
                RequestReleaseServerRpc(clientId);
            }
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void RequestGrabServerRpc(ulong clientId)
        {
            // Check if object is already grabbed
            if (!networkIsGrabbed.Value)
            {
                SetGrabState(true, clientId);
                
                // Transfer ownership to grabbing client
                GetComponent<NetworkObject>().ChangeOwnership(clientId);
            }
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void RequestReleaseServerRpc(ulong clientId)
        {
            // Only allow release if this client is the one who grabbed it
            if (networkIsGrabbed.Value && networkGrabbedByClient.Value == clientId)
            {
                SetGrabState(false, 0);
                
                // Return ownership to server
                GetComponent<NetworkObject>().ChangeOwnership(NetworkManager.ServerClientId);
            }
        }
        
        private void SetGrabState(bool grabbed, ulong clientId)
        {
            networkIsGrabbed.Value = grabbed;
            networkGrabbedByClient.Value = clientId;
        }
        
        // Network variable change handlers
        private void OnPositionChanged(Vector3 oldValue, Vector3 newValue)
        {
            if (!IsOwner)
            {
                transform.position = newValue;
            }
        }
        
        private void OnRotationChanged(Quaternion oldValue, Quaternion newValue)
        {
            if (!IsOwner)
            {
                transform.rotation = newValue;
            }
        }
        
        private void OnGrabStateChanged(bool oldValue, bool newValue)
        {
            isGrabbed = newValue;
            
            // Update physics based on grab state
            if (rb != null)
            {
                rb.isKinematic = newValue;
                rb.useGravity = !newValue;
            }
            
            // Visual feedback
            UpdateGrabVisuals(newValue);
        }
        
        private void OnGrabbedByClientChanged(ulong oldValue, ulong newValue)
        {
            grabbedByClientId = newValue;
        }
        
        private void UpdateGrabVisuals(bool grabbed)
        {
            // Change color or add outline to indicate grab state
            var renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                var material = renderer.material;
                if (grabbed)
                {
                    // Highlight when grabbed
                    material.color = Color.yellow;
                }
                else
                {
                    // Return to original color
                    material.color = Color.white;
                }
            }
        }
        
        /// <summary>
        /// Check if this object is currently grabbed
        /// </summary>
        public bool IsGrabbed => networkIsGrabbed.Value;
        
        /// <summary>
        /// Get the client ID of who is grabbing this object
        /// </summary>
        public ulong GrabbedByClientId => networkGrabbedByClient.Value;
    }
}