using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using MultiUserVR.VR;

namespace MultiUserVR.Safety
{
    /// <summary>
    /// Phase 3: Multi-user collision prevention system for co-located VR.
    /// Monitors distances between all users in shared physical space and activates warnings/passthrough.
    /// 
    /// CRITICAL: This is a safety-critical component for co-located VR where users share the same room
    /// but cannot see each other. Collision prevention is the #1 priority.
    /// </summary>
    public class CollisionPreventionSystem : MonoBehaviour
    {
        [Header("Safety Distance Thresholds (meters)")]
        [Tooltip("Distance at which yellow warning appears")]
        [SerializeField] private float warningDistance = 1.0f;
        
        [Tooltip("Distance at which red danger warning + passthrough activates")]
        [SerializeField] private float dangerDistance = 0.5f;
        
        [Tooltip("Critical distance - emergency full passthrough")]
        [SerializeField] private float criticalDistance = 0.3f;

        [Header("Warning Visual Settings")]
        [SerializeField] private Color warningColor = new Color(1f, 1f, 0f, 0.3f); // Yellow semi-transparent
        [SerializeField] private Color dangerColor = new Color(1f, 0f, 0f, 0.5f); // Red semi-transparent
        [SerializeField] private float pulseSpeed = 2f;
        
        [Header("Haptic Feedback Settings")]
        [SerializeField] private bool enableHaptics = true;
        [SerializeField] private float hapticIntensityWarning = 0.3f;
        [SerializeField] private float hapticIntensityDanger = 0.7f;
        [SerializeField] private float hapticDuration = 0.1f;

        [Header("Passthrough Settings")]
        [SerializeField] private OVRPassthroughLayer passthroughLayer;
        [SerializeField] private float passthroughFadeSpeed = 2f;
        [SerializeField] private float warningPassthroughOpacity = 0.3f;
        [SerializeField] private float dangerPassthroughOpacity = 0.6f;
        [SerializeField] private float criticalPassthroughOpacity = 1.0f;

        [Header("Debug")]
        [SerializeField] private bool showDebugGizmos = true;
        [SerializeField] private bool logProximityEvents = true;

        // Tracking
        private List<UserTrackingSystem> allUsers = new List<UserTrackingSystem>();
        private UserTrackingSystem localUser;
        private Dictionary<UserTrackingSystem, ProximityState> userProximityStates = new Dictionary<UserTrackingSystem, ProximityState>();
        
        // Warning visuals
        private GameObject warningOverlay;
        private MeshRenderer warningRenderer;
        private Material warningMaterial;
        
        // State tracking
        private ProximityLevel currentProximityLevel = ProximityLevel.Safe;
        private float currentPassthroughOpacity = 0f;
        private float targetPassthroughOpacity = 0f;
        private float lastHapticTime = 0f;

        public enum ProximityLevel
        {
            Safe,       // > warningDistance
            Warning,    // Between danger and warning distance
            Danger,     // Between critical and danger distance
            Critical    // < criticalDistance
        }

        private struct ProximityState
        {
            public float Distance;
            public ProximityLevel Level;
            public float LastWarningTime;
        }

        private void Awake()
        {
            // Find passthrough layer if not assigned
            if (passthroughLayer == null)
            {
                passthroughLayer = FindFirstObjectByType<OVRPassthroughLayer>();
                if (passthroughLayer == null)
                {
                    Debug.LogError("[CollisionPrevention] No OVRPassthroughLayer found in scene! Passthrough safety features disabled.");
                }
            }

            CreateWarningOverlay();
        }

        private void Start()
        {
            // Find local user
            localUser = GetComponent<UserTrackingSystem>();
            if (localUser == null)
            {
                Debug.LogError("[CollisionPrevention] No UserTrackingSystem component found on this GameObject!");
                enabled = false;
                return;
            }

            // Initially disable passthrough
            if (passthroughLayer != null)
            {
                passthroughLayer.hidden = true;
                passthroughLayer.textureOpacity = 0f;
            }

            // Find all other users in scene (will be updated when network spawns players)
            UpdateUserList();
        }

        private void Update()
        {
            // Update list of users (handles network-spawned players)
            UpdateUserList();

            if (allUsers.Count == 0)
            {
                // No other users - safe
                currentProximityLevel = ProximityLevel.Safe;
                targetPassthroughOpacity = 0f;
                return;
            }

            // Check distances to all users
            ProximityLevel closestProximityLevel = ProximityLevel.Safe;
            float closestDistance = float.MaxValue;
            UserTrackingSystem closestUser = null;

            foreach (var otherUser in allUsers)
            {
                if (otherUser == localUser || otherUser == null) continue;

                float distance = localUser.GetDistanceToUser(otherUser);
                ProximityLevel level = GetProximityLevel(distance);

                // Update proximity state for this user
                if (!userProximityStates.ContainsKey(otherUser))
                {
                    userProximityStates[otherUser] = new ProximityState();
                }

                ProximityState state = userProximityStates[otherUser];
                ProximityLevel previousLevel = state.Level;
                state.Distance = distance;
                state.Level = level;
                userProximityStates[otherUser] = state;

                // Log proximity level changes
                if (logProximityEvents && level != previousLevel)
                {
                    Debug.Log($"[CollisionPrevention] User {otherUser.userId} proximity changed: {previousLevel} → {level} (distance: {distance:F2}m)");
                }

                // Track closest user
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestProximityLevel = level;
                    closestUser = otherUser;
                }
            }

            // Update safety systems based on closest user
            UpdateProximityLevel(closestProximityLevel, closestDistance, closestUser);

            // Update passthrough opacity smoothly
            if (passthroughLayer != null)
            {
                currentPassthroughOpacity = Mathf.Lerp(currentPassthroughOpacity, targetPassthroughOpacity, Time.deltaTime * passthroughFadeSpeed);
                passthroughLayer.textureOpacity = currentPassthroughOpacity;
                passthroughLayer.hidden = currentPassthroughOpacity <= 0.01f;
            }

            // Update warning overlay
            UpdateWarningOverlay();
        }

        private void UpdateUserList()
        {
            allUsers.Clear();
            allUsers.AddRange(FindObjectsByType<UserTrackingSystem>(FindObjectsSortMode.None));
        }

        private ProximityLevel GetProximityLevel(float distance)
        {
            if (distance <= criticalDistance) return ProximityLevel.Critical;
            if (distance <= dangerDistance) return ProximityLevel.Danger;
            if (distance <= warningDistance) return ProximityLevel.Warning;
            return ProximityLevel.Safe;
        }

        private void UpdateProximityLevel(ProximityLevel newLevel, float distance, UserTrackingSystem nearestUser)
        {
            if (currentProximityLevel != newLevel)
            {
                // Level changed - trigger appropriate responses
                switch (newLevel)
                {
                    case ProximityLevel.Safe:
                        OnEnterSafeZone();
                        break;
                    case ProximityLevel.Warning:
                        OnEnterWarningZone(distance, nearestUser);
                        break;
                    case ProximityLevel.Danger:
                        OnEnterDangerZone(distance, nearestUser);
                        break;
                    case ProximityLevel.Critical:
                        OnEnterCriticalZone(distance, nearestUser);
                        break;
                }

                currentProximityLevel = newLevel;
            }
            else
            {
                // Continue existing level - may need to update haptics
                UpdateHapticsForLevel(newLevel);
            }
        }

        private void OnEnterSafeZone()
        {
            if (logProximityEvents)
            {
                Debug.Log("[CollisionPrevention] Entered SAFE zone");
            }
            
            targetPassthroughOpacity = 0f;
            
            if (warningRenderer != null)
            {
                warningRenderer.enabled = false;
            }
        }

        private void OnEnterWarningZone(float distance, UserTrackingSystem nearestUser)
        {
            if (logProximityEvents)
            {
                Debug.LogWarning($"[CollisionPrevention] Entered WARNING zone! User {nearestUser?.userId} at {distance:F2}m");
            }

            targetPassthroughOpacity = warningPassthroughOpacity;
            
            if (warningRenderer != null)
            {
                warningRenderer.enabled = true;
                warningMaterial.color = warningColor;
            }

            TriggerHapticFeedback(hapticIntensityWarning);
        }

        private void OnEnterDangerZone(float distance, UserTrackingSystem nearestUser)
        {
            if (logProximityEvents)
            {
                Debug.LogWarning($"[CollisionPrevention] Entered DANGER zone! User {nearestUser?.userId} at {distance:F2}m - PASSTHROUGH ACTIVATING");
            }

            targetPassthroughOpacity = dangerPassthroughOpacity;
            
            if (warningRenderer != null)
            {
                warningRenderer.enabled = true;
                warningMaterial.color = dangerColor;
            }

            TriggerHapticFeedback(hapticIntensityDanger);
        }

        private void OnEnterCriticalZone(float distance, UserTrackingSystem nearestUser)
        {
            if (logProximityEvents)
            {
                Debug.LogError($"[CollisionPrevention] CRITICAL COLLISION RISK! User {nearestUser?.userId} at {distance:F2}m - FULL PASSTHROUGH!");
            }

            targetPassthroughOpacity = criticalPassthroughOpacity;
            
            if (warningRenderer != null)
            {
                warningRenderer.enabled = true;
                warningMaterial.color = dangerColor;
            }

            // Continuous strong haptics in critical zone
            TriggerHapticFeedback(1.0f);
        }

        private void UpdateHapticsForLevel(ProximityLevel level)
        {
            // Repeat haptics based on proximity level
            float hapticInterval = level switch
            {
                ProximityLevel.Warning => 2.0f,    // Every 2 seconds
                ProximityLevel.Danger => 1.0f,     // Every second
                ProximityLevel.Critical => 0.3f,   // 3 times per second
                _ => float.MaxValue
            };

            if (Time.time - lastHapticTime >= hapticInterval)
            {
                float intensity = level switch
                {
                    ProximityLevel.Warning => hapticIntensityWarning,
                    ProximityLevel.Danger => hapticIntensityDanger,
                    ProximityLevel.Critical => 1.0f,
                    _ => 0f
                };

                TriggerHapticFeedback(intensity);
            }
        }

        private void TriggerHapticFeedback(float intensity)
        {
            if (!enableHaptics) return;

            lastHapticTime = Time.time;

            // Trigger haptics on both controllers
            OVRInput.SetControllerVibration(1f, intensity, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(1f, intensity, OVRInput.Controller.RTouch);

            // Stop after duration
            StartCoroutine(StopHapticsAfterDelay(hapticDuration));
        }

        private System.Collections.IEnumerator StopHapticsAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
        }

        private void CreateWarningOverlay()
        {
            // Create a full-screen quad overlay for warnings
            warningOverlay = new GameObject("CollisionWarningOverlay");
            warningOverlay.transform.SetParent(transform);
            warningOverlay.transform.localPosition = new Vector3(0, 0, 1.5f); // 1.5m in front
            warningOverlay.transform.localRotation = Quaternion.identity;
            warningOverlay.transform.localScale = Vector3.one * 3f; // Large overlay

            // Create mesh
            MeshFilter meshFilter = warningOverlay.AddComponent<MeshFilter>();
            meshFilter.mesh = CreateQuadMesh();

            // Create material
            warningRenderer = warningOverlay.AddComponent<MeshRenderer>();
            warningMaterial = new Material(Shader.Find("UI/Default")); // Use unlit shader
            warningMaterial.color = warningColor;
            warningMaterial.SetFloat("_Mode", 3); // Transparent mode
            warningMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            warningMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            warningMaterial.SetInt("_ZWrite", 0);
            warningMaterial.DisableKeyword("_ALPHATEST_ON");
            warningMaterial.EnableKeyword("_ALPHABLEND_ON");
            warningMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            warningMaterial.renderQueue = 3000;
            warningRenderer.material = warningMaterial;
            warningRenderer.enabled = false;

            // Make overlay always face camera (attached to local user)
            warningOverlay.transform.SetParent(localUser != null ? localUser.transform : transform);
        }

        private Mesh CreateQuadMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = new Vector3[]
            {
                new Vector3(-1, -1, 0),
                new Vector3(1, -1, 0),
                new Vector3(-1, 1, 0),
                new Vector3(1, 1, 0)
            };
            mesh.uv = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };
            mesh.triangles = new int[] { 0, 2, 1, 2, 3, 1 };
            mesh.RecalculateNormals();
            return mesh;
        }

        private void UpdateWarningOverlay()
        {
            if (warningRenderer == null || !warningRenderer.enabled) return;

            // Pulse effect for warnings
            float pulse = (Mathf.Sin(Time.time * pulseSpeed) + 1f) * 0.5f; // 0-1 range
            Color currentColor = currentProximityLevel == ProximityLevel.Warning ? warningColor : dangerColor;
            currentColor.a *= (0.5f + pulse * 0.5f); // Pulse between 50% and 100% alpha
            warningMaterial.color = currentColor;

            // Keep overlay facing camera
            if (Camera.main != null)
            {
                warningOverlay.transform.LookAt(Camera.main.transform);
                warningOverlay.transform.Rotate(0, 180, 0); // Face camera
            }
        }

        private void OnDrawGizmos()
        {
            if (!showDebugGizmos || localUser == null) return;

            Vector3 headPos = localUser.HeadPosition;

            // Draw proximity zones
            Gizmos.color = new Color(1f, 1f, 0f, 0.3f); // Warning
            Gizmos.DrawWireSphere(headPos, warningDistance);

            Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f); // Danger
            Gizmos.DrawWireSphere(headPos, dangerDistance);

            Gizmos.color = new Color(1f, 0f, 0f, 0.7f); // Critical
            Gizmos.DrawWireSphere(headPos, criticalDistance);

            // Draw lines to other users
            foreach (var otherUser in allUsers)
            {
                if (otherUser == null || otherUser == localUser) continue;

                float distance = localUser.GetDistanceToUser(otherUser);
                ProximityLevel level = GetProximityLevel(distance);

                Color lineColor = level switch
                {
                    ProximityLevel.Warning => Color.yellow,
                    ProximityLevel.Danger => new Color(1f, 0.5f, 0f),
                    ProximityLevel.Critical => Color.red,
                    _ => Color.green
                };

                Gizmos.color = lineColor;
                Gizmos.DrawLine(headPos, otherUser.HeadPosition);
                
                // Draw distance text in Scene view
                #if UNITY_EDITOR
                UnityEditor.Handles.Label(
                    Vector3.Lerp(headPos, otherUser.HeadPosition, 0.5f),
                    $"{distance:F2}m"
                );
                #endif
            }
        }

        /// <summary>
        /// Public API: Manually enable/disable passthrough
        /// </summary>
        public void SetPassthroughEnabled(bool enabled)
        {
            targetPassthroughOpacity = enabled ? 1.0f : 0f;
        }

        /// <summary>
        /// Public API: Get current proximity level
        /// </summary>
        public ProximityLevel GetCurrentProximityLevel()
        {
            return currentProximityLevel;
        }

        /// <summary>
        /// Public API: Get distance to nearest user
        /// </summary>
        public float GetDistanceToNearestUser()
        {
            if (allUsers.Count == 0) return float.MaxValue;

            float minDistance = float.MaxValue;
            foreach (var otherUser in allUsers)
            {
                if (otherUser == null || otherUser == localUser) continue;
                float distance = localUser.GetDistanceToUser(otherUser);
                minDistance = Mathf.Min(minDistance, distance);
            }

            return minDistance;
        }
    }
}
