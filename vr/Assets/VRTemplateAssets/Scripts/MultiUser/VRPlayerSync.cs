using UnityEngine;
using Unity.Netcode;

namespace Unity.VRTemplate.MultiUser
{
    /// <summary>
    /// Synchronizes VR player head and hand positions across the network
    /// Attaches to XR Origin to sync head, left hand, and right hand transforms
    /// </summary>
    public class VRPlayerSync : NetworkBehaviour
    {
        [Header("VR Components")]
        [SerializeField] private Transform headTransform;
        [SerializeField] private Transform leftHandTransform;
        [SerializeField] private Transform rightHandTransform;
        
        [Header("Sync Settings")]
        [SerializeField] private float syncRate = 20f; // Updates per second
        [SerializeField] private bool interpolateMovement = true;
        
        [Header("Visual Representation")]
        [SerializeField] private GameObject remotePlayerPrefab;
        
        // Network variables for position and rotation
        private NetworkVariable<Vector3> networkHeadPosition = new NetworkVariable<Vector3>();
        private NetworkVariable<Quaternion> networkHeadRotation = new NetworkVariable<Quaternion>();
        private NetworkVariable<Vector3> networkLeftHandPosition = new NetworkVariable<Vector3>();
        private NetworkVariable<Quaternion> networkLeftHandRotation = new NetworkVariable<Quaternion>();
        private NetworkVariable<Vector3> networkRightHandPosition = new NetworkVariable<Vector3>();
        private NetworkVariable<Quaternion> networkRightHandRotation = new NetworkVariable<Quaternion>();
        
        // Local player tracking
        private bool isLocalPlayer;
        private float syncTimer;
        
        // Remote player visualization
        private GameObject remotePlayerInstance;
        private Transform remoteHead, remoteLeftHand, remoteRightHand;
        
        private void Awake()
        {
            // Auto-find VR components if not assigned
            FindVRComponents();
        }
        
        private void FindVRComponents()
        {
            var xrOrigin = GetComponent<Unity.XR.CoreUtils.XROrigin>();
            if (xrOrigin == null) return;
            
            // Find head (main camera)
            if (headTransform == null)
            {
                headTransform = xrOrigin.Camera?.transform;
            }
            
            // Find hand controllers in XR Origin hierarchy
            if (leftHandTransform == null || rightHandTransform == null)
            {
                var controllers = GetComponentsInChildren<Transform>();
                foreach (var controller in controllers)
                {
                    if (controller.name.ToLower().Contains("left") && controller.name.ToLower().Contains("controller"))
                    {
                        leftHandTransform = controller;
                    }
                    else if (controller.name.ToLower().Contains("right") && controller.name.ToLower().Contains("controller"))
                    {
                        rightHandTransform = controller;
                    }
                }
            }
        }
        
        public override void OnNetworkSpawn()
        {
            isLocalPlayer = IsOwner;
            
            if (!isLocalPlayer)
            {
                // This is a remote player - create visual representation
                CreateRemotePlayerVisual();
                
                // Subscribe to network variable changes
                networkHeadPosition.OnValueChanged += OnHeadPositionChanged;
                networkHeadRotation.OnValueChanged += OnHeadRotationChanged;
                networkLeftHandPosition.OnValueChanged += OnLeftHandPositionChanged;
                networkLeftHandRotation.OnValueChanged += OnLeftHandRotationChanged;
                networkRightHandPosition.OnValueChanged += OnRightHandPositionChanged;
                networkRightHandRotation.OnValueChanged += OnRightHandRotationChanged;
            }
        }
        
        private void Update()
        {
            if (!IsSpawned) return;
            
            if (isLocalPlayer)
            {
                // Local player: Send our transform data to network
                SyncLocalPlayerData();
            }
        }
        
        private void SyncLocalPlayerData()
        {
            syncTimer += Time.deltaTime;
            if (syncTimer >= 1f / syncRate)
            {
                syncTimer = 0f;
                
                // Send our current VR positions to other clients
                if (IsServer)
                {
                    // Server can directly set network variables
                    UpdateNetworkVariables();
                }
                else
                {
                    // Client sends RPC to server
                    UpdatePlayerTransformServerRpc(
                        headTransform.position, headTransform.rotation,
                        leftHandTransform.position, leftHandTransform.rotation,
                        rightHandTransform.position, rightHandTransform.rotation
                    );
                }
            }
        }
        
        private void UpdateNetworkVariables()
        {
            if (headTransform != null)
            {
                networkHeadPosition.Value = headTransform.position;
                networkHeadRotation.Value = headTransform.rotation;
            }
            
            if (leftHandTransform != null)
            {
                networkLeftHandPosition.Value = leftHandTransform.position;
                networkLeftHandRotation.Value = leftHandTransform.rotation;
            }
            
            if (rightHandTransform != null)
            {
                networkRightHandPosition.Value = rightHandTransform.position;
                networkRightHandRotation.Value = rightHandTransform.rotation;
            }
        }
        
        [ServerRpc]
        private void UpdatePlayerTransformServerRpc(
            Vector3 headPos, Quaternion headRot,
            Vector3 leftHandPos, Quaternion leftHandRot,
            Vector3 rightHandPos, Quaternion rightHandRot)
        {
            // Server updates network variables
            networkHeadPosition.Value = headPos;
            networkHeadRotation.Value = headRot;
            networkLeftHandPosition.Value = leftHandPos;
            networkLeftHandRotation.Value = leftHandRot;
            networkRightHandPosition.Value = rightHandPos;
            networkRightHandRotation.Value = rightHandRot;
        }
        
        private void CreateRemotePlayerVisual()
        {
            if (remotePlayerPrefab != null)
            {
                remotePlayerInstance = Instantiate(remotePlayerPrefab, transform);
            }
            else
            {
                // Create simple visual representation
                remotePlayerInstance = new GameObject("Remote Player Visual");
                remotePlayerInstance.transform.SetParent(transform);
                
                // Create simple sphere representations
                remoteHead = CreateVisualSphere("Head", Color.blue, 0.2f).transform;
                remoteLeftHand = CreateVisualSphere("Left Hand", Color.green, 0.1f).transform;
                remoteRightHand = CreateVisualSphere("Right Hand", Color.red, 0.1f).transform;
                
                remoteHead.SetParent(remotePlayerInstance.transform);
                remoteLeftHand.SetParent(remotePlayerInstance.transform);
                remoteRightHand.SetParent(remotePlayerInstance.transform);
            }
        }
        
        private GameObject CreateVisualSphere(string name, Color color, float size)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = name;
            sphere.transform.localScale = Vector3.one * size;
            
            var renderer = sphere.GetComponent<Renderer>();
            var material = new Material(Shader.Find("Standard"));
            material.color = color;
            renderer.material = material;
            
            return sphere;
        }
        
        // Network variable change handlers for smooth interpolation
        private void OnHeadPositionChanged(Vector3 oldValue, Vector3 newValue)
        {
            if (remoteHead != null)
            {
                remoteHead.position = newValue;
            }
        }
        
        private void OnHeadRotationChanged(Quaternion oldValue, Quaternion newValue)
        {
            if (remoteHead != null)
            {
                remoteHead.rotation = newValue;
            }
        }
        
        private void OnLeftHandPositionChanged(Vector3 oldValue, Vector3 newValue)
        {
            if (remoteLeftHand != null)
            {
                remoteLeftHand.position = newValue;
            }
        }
        
        private void OnLeftHandRotationChanged(Quaternion oldValue, Quaternion newValue)
        {
            if (remoteLeftHand != null)
            {
                remoteLeftHand.rotation = newValue;
            }
        }
        
        private void OnRightHandPositionChanged(Vector3 oldValue, Vector3 newValue)
        {
            if (remoteRightHand != null)
            {
                remoteRightHand.position = newValue;
            }
        }
        
        private void OnRightHandRotationChanged(Quaternion oldValue, Quaternion newValue)
        {
            if (remoteRightHand != null)
            {
                remoteRightHand.rotation = newValue;
            }
        }
    }
}