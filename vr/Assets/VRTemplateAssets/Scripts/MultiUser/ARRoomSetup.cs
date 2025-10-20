using UnityEngine;
using Unity.Netcode;
using Meta.XR.MRUtilityKit;

namespace Unity.VRTemplate.MultiUser
{
    /// <summary>
    /// Handles AR room setup, passthrough, and spatial coordinate system
    /// Creates shared reference points for multi-user AR experiences
    /// </summary>
    public class ARRoomSetup : NetworkBehaviour
    {
        [Header("AR Configuration")]
        [SerializeField] private bool enablePassthrough = true;
        [SerializeField] private bool autoStartAR = false;
        [SerializeField] private GameObject roomOriginMarkerPrefab;
        
        [Header("Room Calibration")]
        [SerializeField] private KeyCode placeOriginKey = KeyCode.O;
        [SerializeField] private KeyCode toggleARModeKey = KeyCode.M;
        
        // Room tracking components
        private OVRPassthroughLayer passthroughLayer;
        private OVRCameraRig cameraRig;
        private Camera centerEyeCamera;
        
        // Room coordinate system
        private Transform roomOrigin;
        private bool isARModeActive = false;
        private bool roomCalibrated = false;
        
        // Network variables for room coordination
        private NetworkVariable<bool> hasRoomOrigin = new NetworkVariable<bool>(false);
        private NetworkVariable<Vector3> roomOriginPosition = new NetworkVariable<Vector3>(Vector3.zero);
        private NetworkVariable<Vector3> roomOriginRotation = new NetworkVariable<Vector3>(Vector3.zero);
        
        public bool IsARModeActive =&gt; isARModeActive;
        public bool IsRoomCalibrated =&gt; roomCalibrated;
        public Transform RoomOrigin =&gt; roomOrigin;
        
        private void Start()
        {
            SetupARComponents();
            
            if (autoStartAR)
            {
                EnableARMode();
            }
        }
        
        private void SetupARComponents()
        {
            // Find camera rig
            cameraRig = FindObjectOfType<OVRCameraRig>();
            if (cameraRig == null)
            {
                Debug.LogError("ARRoomSetup: No OVRCameraRig found! Make sure XR Origin is properly configured.");
                return;
            }
            
            centerEyeCamera = cameraRig.centerEyeAnchor.GetComponent&lt;Camera&gt;();
            
            // Setup passthrough
            if (enablePassthrough)
            {
                SetupPassthrough();
            }
            
            // Listen for network changes
            hasRoomOrigin.OnValueChanged += OnRoomOriginChanged;
            roomOriginPosition.OnValueChanged += OnRoomOriginPositionChanged;
            roomOriginRotation.OnValueChanged += OnRoomOriginRotationChanged;
        }
        
        private void SetupPassthrough()
        {
            // Create passthrough layer if it doesn't exist
            passthroughLayer = FindObjectOfType<OVRPassthroughLayer>();
            
            if (passthroughLayer == null)
            {
                var passthroughObj = new GameObject("OVR Passthrough Layer");
                passthroughLayer = passthroughObj.AddComponent<OVRPassthroughLayer>();
            }
            
            passthroughLayer.textureOpacity = 0f; // Start with VR mode
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            // Toggle AR mode
            if (Input.GetKeyDown(toggleARModeKey))
            {
                if (isARModeActive)
                {
                    DisableARMode();
                }
                else
                {
                    EnableARMode();
                }
            }
            
            // Place room origin (host only)
            if (Input.GetKeyDown(placeOriginKey) && IsHost && isARModeActive)
            {
                PlaceRoomOrigin();
            }
        }
        
        public void EnableARMode()
        {
            if (passthroughLayer == null)
            {
                Debug.LogWarning("Passthrough layer not available");
                return;
            }
            
            // Enable passthrough
            passthroughLayer.textureOpacity = 1f;
            isARModeActive = true;
            
            // Adjust camera clear settings for AR
            if (centerEyeCamera != null)
            {
                centerEyeCamera.clearFlags = CameraClearFlags.Color;
                centerEyeCamera.backgroundColor = Color.clear;
            }
            
            Debug.Log("AR Mode Enabled - Press 'O' to place room origin (Host only)");
        }
        
        public void DisableARMode()
        {
            if (passthroughLayer != null)
            {
                passthroughLayer.textureOpacity = 0f;
            }
            
            isARModeActive = false;
            
            // Restore VR camera settings
            if (centerEyeCamera != null)
            {
                centerEyeCamera.clearFlags = CameraClearFlags.Skybox;
            }
            
            Debug.Log("VR Mode Enabled");
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void PlaceRoomOriginServerRpc(Vector3 position, Vector3 rotation)
        {
            // Update network variables
            roomOriginPosition.Value = position;
            roomOriginRotation.Value = Quaternion.Euler(rotation).eulerAngles;
            hasRoomOrigin.Value = true;
            
            Debug.Log($"Room origin placed at: {position}");
        }
        
        public void PlaceRoomOrigin()
        {
            if (!IsHost || !isARModeActive)
            {
                Debug.LogWarning("Only host can place room origin in AR mode");
                return;
            }
            
            // Use current head position as room origin
            Vector3 headPosition = cameraRig.centerEyeAnchor.position;
            Vector3 headRotation = cameraRig.centerEyeAnchor.eulerAngles;
            
            // Place at floor level
            headPosition.y = 0f;
            headRotation.x = 0f;
            headRotation.z = 0f;
            
            PlaceRoomOriginServerRpc(headPosition, headRotation);
        }
        
        private void OnRoomOriginChanged(bool previousValue, bool newValue)
        {
            if (newValue && !roomCalibrated)
            {
                CalibrateRoomCoordinates();
            }
        }
        
        private void OnRoomOriginPositionChanged(Vector3 previousValue, Vector3 newValue)
        {
            if (hasRoomOrigin.Value)
            {
                UpdateRoomOrigin();
            }
        }
        
        private void OnRoomOriginRotationChanged(Vector3 previousValue, Vector3 newValue)
        {
            if (hasRoomOrigin.Value)
            {
                UpdateRoomOrigin();
            }
        }
        
        private void CalibrateRoomCoordinates()
        {
            Debug.Log("Calibrating room coordinates...");
            
            // Create or update room origin object
            if (roomOrigin == null)
            {
                var originObj = new GameObject("Room Origin");
                roomOrigin = originObj.transform;
                
                // Add visual marker if prefab is assigned
                if (roomOriginMarkerPrefab != null)
                {
                    Instantiate(roomOriginMarkerPrefab, roomOrigin);
                }
            }
            
            UpdateRoomOrigin();
            roomCalibrated = true;
            
            Debug.Log("Room coordinate system calibrated!");
        }
        
        private void UpdateRoomOrigin()
        {
            if (roomOrigin != null)
            {
                roomOrigin.position = roomOriginPosition.Value;
                roomOrigin.rotation = Quaternion.Euler(roomOriginRotation.Value);
            }
        }
        
        /// &lt;summary&gt;
        /// Converts world position to room-relative position
        /// &lt;/summary&gt;
        public Vector3 WorldToRoomPosition(Vector3 worldPos)
        {
            if (!roomCalibrated || roomOrigin == null)
                return worldPos;
                
            return roomOrigin.InverseTransformPoint(worldPos);
        }
        
        /// &lt;summary&gt;
        /// Converts room-relative position to world position
        /// &lt;/summary&gt;
        public Vector3 RoomToWorldPosition(Vector3 roomPos)
        {
            if (!roomCalibrated || roomOrigin == null)
                return roomPos;
                
            return roomOrigin.TransformPoint(roomPos);
        }
        
        private void OnGUI()
        {
            if (!isARModeActive) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 300, 200));
            GUILayout.Label("=== AR Room Setup ===");
            GUILayout.Label($"AR Mode: {(isARModeActive ? "ACTIVE" : "DISABLED")}");
            GUILayout.Label($"Room Calibrated: {(roomCalibrated ? "YES" : "NO")}");
            
            if (IsHost)
            {
                GUILayout.Label("HOST CONTROLS:");
                GUILayout.Label("O - Place Room Origin");
            }
            else
            {
                GUILayout.Label("CLIENT - Waiting for host to set room origin");
            }
            
            GUILayout.Label("M - Toggle AR/VR Mode");
            GUILayout.EndArea();
        }
    }
}