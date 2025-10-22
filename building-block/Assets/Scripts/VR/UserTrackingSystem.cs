using UnityEngine;

namespace MultiUserVR.VR
{
    /// <summary>
    /// Tracks the position and rotation of the VR camera rig for collision detection
    /// and safety systems. Essential for Phase 3 collision prevention.
    /// </summary>
    [RequireComponent(typeof(OVRCameraRig))]
    public class UserTrackingSystem : MonoBehaviour
    {
        [Header("Tracking Configuration")]
        [Tooltip("Unique identifier for this user (0, 1, or 2 for 3-user system)")]
        public int userId = 0;

        [Header("Position Tracking")]
        [Tooltip("Head position in world space")]
        [SerializeField] private Vector3 headPosition;

        [Tooltip("Head rotation in world space")]
        [SerializeField] private Quaternion headRotation;

        [Tooltip("Left controller position")]
        [SerializeField] private Vector3 leftControllerPosition;

        [Tooltip("Right controller position")]
        [SerializeField] private Vector3 rightControllerPosition;

        [Header("References")]
        private OVRCameraRig cameraRig;
        private Transform centerEyeAnchor;
        private Transform leftHandAnchor;
        private Transform rightHandAnchor;

        [Header("Debug Visualization")]
        [Tooltip("Enable debug visualization of tracked positions")]
        public bool showDebugVisualization = true;

        [Tooltip("Color for this user's debug visualization")]
        public Color debugColor = Color.green;

        #region Properties
        /// <summary>Current head position in world space</summary>
        public Vector3 HeadPosition => headPosition;

        /// <summary>Current head rotation in world space</summary>
        public Quaternion HeadRotation => headRotation;

        /// <summary>Current left controller position in world space</summary>
        public Vector3 LeftControllerPosition => leftControllerPosition;

        /// <summary>Current right controller position in world space</summary>
        public Vector3 RightControllerPosition => rightControllerPosition;

        /// <summary>Is this the local player?</summary>
        public bool IsLocalPlayer => true; // Will be set by NetworkObject in Phase 2
        #endregion

        private void Awake()
        {
            cameraRig = GetComponent<OVRCameraRig>();
            
            if (cameraRig == null)
            {
                Debug.LogError($"[UserTracking] OVRCameraRig not found on {gameObject.name}!");
                enabled = false;
                return;
            }

            InitializeTrackingAnchors();
        }

        private void InitializeTrackingAnchors()
        {
            // Get tracking anchors from OVRCameraRig
            centerEyeAnchor = cameraRig.centerEyeAnchor;
            leftHandAnchor = cameraRig.leftHandAnchor;
            rightHandAnchor = cameraRig.rightHandAnchor;

            if (centerEyeAnchor == null)
            {
                Debug.LogWarning($"[UserTracking] Center eye anchor not found. OVRCameraRig may not be fully initialized.");
            }

            Debug.Log($"[UserTracking] Initialized tracking for User {userId}");
        }

        private void Update()
        {
            UpdateTrackedPositions();
        }

        private void UpdateTrackedPositions()
        {
            // Update head tracking
            if (centerEyeAnchor != null)
            {
                headPosition = centerEyeAnchor.position;
                headRotation = centerEyeAnchor.rotation;
            }

            // Update controller tracking
            if (leftHandAnchor != null)
            {
                leftControllerPosition = leftHandAnchor.position;
            }

            if (rightHandAnchor != null)
            {
                rightControllerPosition = rightHandAnchor.position;
            }
        }

        private void OnDrawGizmos()
        {
            if (!showDebugVisualization) return;

            // Draw head position
            Gizmos.color = debugColor;
            Gizmos.DrawWireSphere(headPosition, 0.15f); // Head sphere (15cm radius)

            // Draw controller positions
            Gizmos.color = debugColor * 0.7f;
            Gizmos.DrawWireSphere(leftControllerPosition, 0.05f);
            Gizmos.DrawWireSphere(rightControllerPosition, 0.05f);

            // Draw user ID label (visible in Scene view)
            #if UNITY_EDITOR
            UnityEditor.Handles.Label(headPosition + Vector3.up * 0.3f, $"User {userId}");
            #endif
        }

        /// <summary>
        /// Get distance to another user's head position
        /// Used for collision detection in Phase 3
        /// </summary>
        public float GetDistanceToUser(UserTrackingSystem otherUser)
        {
            if (otherUser == null) return float.MaxValue;
            return Vector3.Distance(headPosition, otherUser.headPosition);
        }

        /// <summary>
        /// Check if this user is within danger zone of another user
        /// </summary>
        public bool IsInDangerZone(UserTrackingSystem otherUser, float dangerDistance = 0.5f)
        {
            return GetDistanceToUser(otherUser) < dangerDistance;
        }

        #region Phase 2 - Network Synchronization (Placeholder)
        // These methods will be implemented in Phase 2 when adding Unity Netcode

        /// <summary>
        /// Called when this becomes a networked object (Phase 2)
        /// </summary>
        public void OnNetworkSpawn()
        {
            // Will be implemented with NetworkBehaviour in Phase 2
            Debug.Log($"[UserTracking] User {userId} spawned on network");
        }

        /// <summary>
        /// Called to sync position across network (Phase 2)
        /// </summary>
        public void SyncPositionOverNetwork()
        {
            // Will send headPosition, headRotation, controller positions via RPC
            // Implemented in Phase 2 with Unity Netcode
        }
        #endregion
    }
}
