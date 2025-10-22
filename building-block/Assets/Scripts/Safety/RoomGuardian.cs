using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Collections.Generic;
using System.Linq;

namespace MultiUserVR.Safety
{
    /// <summary>
    /// Phase 3: Room boundary safety system that monitors user distance to walls and furniture.
    /// Integrates with CollisionPreventionSystem to provide comprehensive physical safety.
    /// 
    /// CRITICAL: Prevents users from walking into walls, furniture, and other physical obstacles
    /// detected by MRUK while immersed in VR.
    /// </summary>
    public class RoomGuardian : MonoBehaviour
    {
        [Header("Safety Distance Thresholds (meters)")]
        [Tooltip("Distance from wall/furniture at which warning activates")]
        [SerializeField] private float warningDistance = 0.5f;
        
        [Tooltip("Distance from wall/furniture at which danger warning activates")]
        [SerializeField] private float dangerDistance = 0.3f;

        [Header("Tracking Configuration")]
        [Tooltip("How often to check distances (seconds)")]
        [SerializeField] private float checkInterval = 0.1f;

        [Header("References")]
        [Tooltip("Reference to CollisionPreventionSystem for unified passthrough control")]
        [SerializeField] private CollisionPreventionSystem collisionSystem;

        [Header("Debug")]
        [SerializeField] private bool showDebugGizmos = true;
        [SerializeField] private bool logBoundaryEvents = true;

        // MRUK references
        private MRUK mruk;
        private MRUKRoom currentRoom;
        
        // Tracking
        private Transform userTransform;
        private float lastCheckTime = 0f;
        
        // State tracking
        private bool isNearWall = false;
        private bool isNearFurniture = false;
        private float distanceToNearestWall = float.MaxValue;
        private float distanceToNearestFurniture = float.MaxValue;
        private Vector3 nearestWallPoint = Vector3.zero;
        private Vector3 nearestFurniturePoint = Vector3.zero;

        private void Awake()
        {
            // Find CollisionPreventionSystem if not assigned
            if (collisionSystem == null)
            {
                collisionSystem = FindFirstObjectByType<CollisionPreventionSystem>();
                if (collisionSystem == null)
                {
                    Debug.LogWarning("[RoomGuardian] CollisionPreventionSystem not found. Passthrough integration disabled.");
                }
            }

            // Get user transform (camera rig center eye)
            var cameraRig = FindFirstObjectByType<OVRCameraRig>();
            if (cameraRig != null)
            {
                userTransform = cameraRig.centerEyeAnchor;
            }
            else
            {
                Debug.LogError("[RoomGuardian] OVRCameraRig not found! RoomGuardian disabled.");
                enabled = false;
                return;
            }
        }

        private void Start()
        {
            // Subscribe to MRUK room events
            mruk = MRUK.Instance;
            if (mruk != null)
            {
                mruk.RoomCreatedEvent.AddListener(OnRoomCreated);
                mruk.RoomUpdatedEvent.AddListener(OnRoomUpdated);
                
                // Check if room already exists
                if (mruk.Rooms.Count > 0)
                {
                    currentRoom = mruk.Rooms[0];
                    if (logBoundaryEvents)
                    {
                        Debug.Log($"[RoomGuardian] Initialized with existing room");
                    }
                }
            }
            else
            {
                Debug.LogWarning("[RoomGuardian] MRUK instance not found. Waiting for room scan...");
            }
        }

        private void OnDestroy()
        {
            if (mruk != null)
            {
                mruk.RoomCreatedEvent.RemoveListener(OnRoomCreated);
                mruk.RoomUpdatedEvent.RemoveListener(OnRoomUpdated);
            }
        }

        private void OnRoomCreated(MRUKRoom room)
        {
            currentRoom = room;
            if (logBoundaryEvents)
            {
                Debug.Log($"[RoomGuardian] Room created! Monitoring {room.Anchors.Count} room elements");
            }
        }

        private void OnRoomUpdated(MRUKRoom room)
        {
            if (currentRoom == room)
            {
                if (logBoundaryEvents)
                {
                    Debug.Log($"[RoomGuardian] Room updated! Now monitoring {room.Anchors.Count} room elements");
                }
            }
        }

        private void Update()
        {
            if (currentRoom == null || userTransform == null)
            {
                return;
            }

            // Throttle checks
            if (Time.time - lastCheckTime < checkInterval)
            {
                return;
            }
            lastCheckTime = Time.time;

            CheckWallProximity();
            CheckFurnitureProximity();
            UpdatePassthroughState();
        }

        private void CheckWallProximity()
        {
            Vector3 userPosition = userTransform.position;
            float closestWallDistance = float.MaxValue;
            Vector3 closestWallPoint = Vector3.zero;

            // Get all wall anchors
            var wallAnchors = currentRoom.WallAnchors;
            
            foreach (var wallAnchor in wallAnchors)
            {
                if (wallAnchor == null) continue;

                // Get closest point on wall plane to user
                Vector3 closestPoint;
                float distance = wallAnchor.GetClosestSurfacePosition(userPosition, out closestPoint);

                if (distance < closestWallDistance)
                {
                    closestWallDistance = distance;
                    closestWallPoint = closestPoint;
                }
            }

            bool wasNearWall = isNearWall;
            distanceToNearestWall = closestWallDistance;
            nearestWallPoint = closestWallPoint;
            isNearWall = closestWallDistance < warningDistance;

            // Log state changes
            if (logBoundaryEvents && isNearWall != wasNearWall)
            {
                if (isNearWall)
                {
                    Debug.LogWarning($"[RoomGuardian] WALL PROXIMITY! Distance: {closestWallDistance:F2}m");
                }
                else
                {
                    Debug.Log($"[RoomGuardian] Wall proximity cleared");
                }
            }
        }

        private void CheckFurnitureProximity()
        {
            Vector3 userPosition = userTransform.position;
            float closestFurnitureDistance = float.MaxValue;
            Vector3 closestFurniturePoint = Vector3.zero;

            // Get all furniture/object anchors (tables, couches, etc.)
            var furnitureLabels = new[] 
            { 
                MRUKAnchor.SceneLabels.TABLE,
                MRUKAnchor.SceneLabels.COUCH,
                MRUKAnchor.SceneLabels.OTHER,
                MRUKAnchor.SceneLabels.STORAGE,
                MRUKAnchor.SceneLabels.BED,
                MRUKAnchor.SceneLabels.SCREEN,
                MRUKAnchor.SceneLabels.LAMP,
                MRUKAnchor.SceneLabels.PLANT
            };

            foreach (var label in furnitureLabels)
            {
                var anchors = currentRoom.Anchors.Where(a => a.Label == label).ToList();
                
                foreach (var anchor in anchors)
                {
                    if (anchor == null) continue;

                    // Get closest point on furniture volume to user
                    Vector3 closestPoint;
                    float distance = anchor.GetClosestSurfacePosition(userPosition, out closestPoint);

                    if (distance < closestFurnitureDistance)
                    {
                        closestFurnitureDistance = distance;
                        closestFurniturePoint = closestPoint;
                    }
                }
            }

            bool wasNearFurniture = isNearFurniture;
            distanceToNearestFurniture = closestFurnitureDistance;
            nearestFurniturePoint = closestFurniturePoint;
            isNearFurniture = closestFurnitureDistance < warningDistance;

            // Log state changes
            if (logBoundaryEvents && isNearFurniture != wasNearFurniture)
            {
                if (isNearFurniture)
                {
                    Debug.LogWarning($"[RoomGuardian] FURNITURE PROXIMITY! Distance: {closestFurnitureDistance:F2}m");
                }
                else
                {
                    Debug.Log($"[RoomGuardian] Furniture proximity cleared");
                }
            }
        }

        private void UpdatePassthroughState()
        {
            if (collisionSystem == null)
            {
                return;
            }

            // Determine if passthrough should be active based on boundary proximity
            bool shouldActivatePassthrough = false;
            float closestBoundaryDistance = Mathf.Min(distanceToNearestWall, distanceToNearestFurniture);

            if (closestBoundaryDistance < dangerDistance)
            {
                // Danger zone - activate passthrough
                shouldActivatePassthrough = true;
            }
            else if (closestBoundaryDistance < warningDistance)
            {
                // Warning zone - partial passthrough
                shouldActivatePassthrough = true;
            }

            // Update collision system
            // Note: CollisionPreventionSystem will blend this with user proximity warnings
            if (shouldActivatePassthrough)
            {
                collisionSystem.SetPassthroughEnabled(true);
            }
        }

        private void OnDrawGizmos()
        {
            if (!showDebugGizmos || userTransform == null)
            {
                return;
            }

            Vector3 userPosition = userTransform.position;

            // Draw warning distance sphere (yellow)
            Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
            Gizmos.DrawWireSphere(userPosition, warningDistance);

            // Draw danger distance sphere (red)
            Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
            Gizmos.DrawWireSphere(userPosition, dangerDistance);

            // Draw line to nearest wall
            if (isNearWall && nearestWallPoint != Vector3.zero)
            {
                Gizmos.color = distanceToNearestWall < dangerDistance ? Color.red : Color.yellow;
                Gizmos.DrawLine(userPosition, nearestWallPoint);
                Gizmos.DrawSphere(nearestWallPoint, 0.05f);
            }

            // Draw line to nearest furniture
            if (isNearFurniture && nearestFurniturePoint != Vector3.zero)
            {
                Gizmos.color = distanceToNearestFurniture < dangerDistance ? Color.red : Color.yellow;
                Gizmos.DrawLine(userPosition, nearestFurniturePoint);
                Gizmos.DrawCube(nearestFurniturePoint, Vector3.one * 0.1f);
            }
        }

        /// <summary>
        /// Public API: Get distance to nearest wall
        /// </summary>
        public float GetDistanceToNearestWall() => distanceToNearestWall;

        /// <summary>
        /// Public API: Get distance to nearest furniture
        /// </summary>
        public float GetDistanceToNearestFurniture() => distanceToNearestFurniture;

        /// <summary>
        /// Public API: Check if user is near any boundary
        /// </summary>
        public bool IsNearBoundary() => isNearWall || isNearFurniture;

        /// <summary>
        /// Public API: Get closest boundary distance
        /// </summary>
        public float GetClosestBoundaryDistance() => Mathf.Min(distanceToNearestWall, distanceToNearestFurniture);
    }
}
