using UnityEngine;
using Unity.Netcode;

namespace Unity.VRTemplate.MultiUser
{
    /// <summary>
    /// Extended VR player synchronization for AR experiences
    /// Handles avatar positioning in real-world coordinates with room calibration
    /// </summary>
    public class ARPlayerSync : NetworkBehaviour
    {
        [Header("AR Avatar Settings")]
        [SerializeField] private GameObject arAvatarPrefab;
        [SerializeField] private bool showARAvatar = true;
        [SerializeField] private float avatarScale = 1f;
        
        [Header("Tracking Components")]
        [SerializeField] private Transform headTransform;
        [SerializeField] private Transform leftHandTransform;
        [SerializeField] private Transform rightHandTransform;
        
        // Network synchronized positions (room-relative)
        private NetworkVariable<Vector3> networkHeadPosition = new NetworkVariable<Vector3>();
        private NetworkVariable<Vector3> networkHeadRotation = new NetworkVariable<Vector3>();
        private NetworkVariable<Vector3> networkLeftHandPosition = new NetworkVariable<Vector3>();
        private NetworkVariable<Vector3> networkLeftHandRotation = new NetworkVariable<Vector3>();
        private NetworkVariable<Vector3> networkRightHandPosition = new NetworkVariable<Vector3>();
        private NetworkVariable<Vector3> networkRightHandRotation = new NetworkVariable<Vector3>();
        
        // AR avatar visual components
        private GameObject arAvatarInstance;
        private Transform arHeadVisual;
        private Transform arLeftHandVisual;
        private Transform arRightHandVisual;
        
        // References
        private ARRoomSetup roomSetup;
        private Unity.XR.CoreUtils.XROrigin xrOrigin;
        
        private void Start()
        {
            SetupARAvatar();
            FindTrackingComponents();
            
            // Find room setup component
            roomSetup = FindObjectOfType<ARRoomSetup>();
            
            // Subscribe to network variable changes
            networkHeadPosition.OnValueChanged += OnHeadPositionChanged;
            networkHeadRotation.OnValueChanged += OnHeadRotationChanged;
            networkLeftHandPosition.OnValueChanged += OnLeftHandPositionChanged;
            networkLeftHandRotation.OnValueChanged += OnLeftHandRotationChanged;
            networkRightHandPosition.OnValueChanged += OnRightHandPositionChanged;
            networkRightHandRotation.OnValueChanged += OnRightHandRotationChanged;
        }
        
        private void FindTrackingComponents()
        {
            // Find XR Origin
            xrOrigin = FindObjectOfType<Unity.XR.CoreUtils.XROrigin>();
            
            if (xrOrigin != null)
            {
                // Get head tracking
                if (headTransform == null)
                    headTransform = xrOrigin.Camera.transform;
                
                // Try to find hand tracking
                var leftController = xrOrigin.transform.Find("Camera Offset/LeftHand Controller");
                if (leftController != null && leftHandTransform == null)
                    leftHandTransform = leftController;
                    
                var rightController = xrOrigin.transform.Find("Camera Offset/RightHand Controller");
                if (rightController != null && rightHandTransform == null)
                    rightHandTransform = rightController;
            }
        }
        
        private void SetupARAvatar()
        {
            if (!IsOwner) // Only create avatar for remote players
            {
                CreateARAvatar();
            }
        }
        
        private void CreateARAvatar()
        {
            if (arAvatarPrefab != null)
            {
                arAvatarInstance = Instantiate(arAvatarPrefab);
                arAvatarInstance.name = $"AR Avatar - Player {OwnerClientId}";
            }
            else
            {
                // Create simple avatar with primitives
                arAvatarInstance = new GameObject($"AR Avatar - Player {OwnerClientId}");
                
                // Head visual
                var headObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                headObj.name = "Head";
                headObj.transform.SetParent(arAvatarInstance.transform);
                headObj.transform.localScale = Vector3.one * 0.2f;
                headObj.GetComponent<Renderer>().material.color = Color.blue;
                arHeadVisual = headObj.transform;
                
                // Left hand visual
                var leftHandObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                leftHandObj.name = "LeftHand";
                leftHandObj.transform.SetParent(arAvatarInstance.transform);
                leftHandObj.transform.localScale = Vector3.one * 0.08f;
                leftHandObj.GetComponent<Renderer>().material.color = Color.green;
                arLeftHandVisual = leftHandObj.transform;
                
                // Right hand visual
                var rightHandObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rightHandObj.name = "RightHand";
                rightHandObj.transform.SetParent(arAvatarInstance.transform);
                rightHandObj.transform.localScale = Vector3.one * 0.08f;
                rightHandObj.GetComponent<Renderer>().material.color = Color.red;
                arRightHandVisual = rightHandObj.transform;
            }
            
            // Make avatar semi-transparent for AR
            MakeAvatarTransparent();
        }
        
        private void MakeAvatarTransparent()
        {
            if (arAvatarInstance == null) return;
            
            var renderers = arAvatarInstance.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                if (renderer.material != null)
                {
                    // Create transparent material
                    var material = new Material(renderer.material);
                    material.SetFloat("_Mode", 3); // Transparent mode
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                    
                    var color = material.color;
                    color.a = 0.7f; // Semi-transparent
                    material.color = color;
                    
                    renderer.material = material;
                }
            }
        }
        
        private void Update()
        {
            if (IsOwner)
            {
                UpdateOwnTracking();
            }
            
            UpdateAvatarVisibility();
        }
        
        private void UpdateOwnTracking()
        {
            if (roomSetup == null || !roomSetup.IsRoomCalibrated)
                return;
                
            // Convert world positions to room-relative positions
            if (headTransform != null)
            {
                Vector3 roomRelativePos = roomSetup.WorldToRoomPosition(headTransform.position);
                Vector3 roomRelativeRot = headTransform.rotation.eulerAngles;
                
                UpdateHeadPositionServerRpc(roomRelativePos, roomRelativeRot);
            }
            
            if (leftHandTransform != null)
            {
                Vector3 roomRelativePos = roomSetup.WorldToRoomPosition(leftHandTransform.position);
                Vector3 roomRelativeRot = leftHandTransform.rotation.eulerAngles;
                
                UpdateLeftHandPositionServerRpc(roomRelativePos, roomRelativeRot);
            }
            
            if (rightHandTransform != null)
            {
                Vector3 roomRelativePos = roomSetup.WorldToRoomPosition(rightHandTransform.position);
                Vector3 roomRelativeRot = rightHandTransform.rotation.eulerAngles;
                
                UpdateRightHandPositionServerRpc(roomRelativePos, roomRelativeRot);
            }
        }
        
        private void UpdateAvatarVisibility()
        {
            if (arAvatarInstance == null) return;
            
            // Show/hide avatar based on AR mode and room calibration
            bool shouldShow = showARAvatar && 
                             roomSetup != null && 
                             roomSetup.IsARModeActive && 
                             roomSetup.IsRoomCalibrated &&
                             !IsOwner; // Don't show own avatar
                             
            arAvatarInstance.SetActive(shouldShow);
        }
        
        [ServerRpc]
        private void UpdateHeadPositionServerRpc(Vector3 position, Vector3 rotation)
        {
            networkHeadPosition.Value = position;
            networkHeadRotation.Value = rotation;
        }
        
        [ServerRpc]
        private void UpdateLeftHandPositionServerRpc(Vector3 position, Vector3 rotation)
        {
            networkLeftHandPosition.Value = position;
            networkLeftHandRotation.Value = rotation;
        }
        
        [ServerRpc]
        private void UpdateRightHandPositionServerRpc(Vector3 position, Vector3 rotation)
        {
            networkRightHandPosition.Value = position;
            networkRightHandRotation.Value = rotation;
        }
        
        private void OnHeadPositionChanged(Vector3 previousValue, Vector3 newValue)
        {
            UpdateAvatarTransform(arHeadVisual, newValue, networkHeadRotation.Value);
        }
        
        private void OnHeadRotationChanged(Vector3 previousValue, Vector3 newValue)
        {
            UpdateAvatarTransform(arHeadVisual, networkHeadPosition.Value, newValue);
        }
        
        private void OnLeftHandPositionChanged(Vector3 previousValue, Vector3 newValue)
        {
            UpdateAvatarTransform(arLeftHandVisual, newValue, networkLeftHandRotation.Value);
        }
        
        private void OnLeftHandRotationChanged(Vector3 previousValue, Vector3 newValue)
        {
            UpdateAvatarTransform(arLeftHandVisual, networkLeftHandPosition.Value, newValue);
        }
        
        private void OnRightHandPositionChanged(Vector3 previousValue, Vector3 newValue)
        {
            UpdateAvatarTransform(arRightHandVisual, newValue, networkRightHandRotation.Value);
        }
        
        private void OnRightHandRotationChanged(Vector3 previousValue, Vector3 newValue)
        {
            UpdateAvatarTransform(arRightHandVisual, networkRightHandPosition.Value, newValue);
        }
        
        private void UpdateAvatarTransform(Transform avatarPart, Vector3 roomPosition, Vector3 rotation)
        {
            if (avatarPart == null || roomSetup == null || !roomSetup.IsRoomCalibrated)
                return;
                
            // Convert room-relative position back to world position
            Vector3 worldPosition = roomSetup.RoomToWorldPosition(roomPosition);
            avatarPart.position = worldPosition;
            avatarPart.rotation = Quaternion.Euler(rotation);
        }
        
        public override void OnDestroy()
        {
            if (arAvatarInstance != null)
            {
                Destroy(arAvatarInstance);
            }
            
            base.OnDestroy();
        }
    }
}