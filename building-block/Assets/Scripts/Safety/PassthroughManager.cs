using UnityEngine;
using System.Collections;

namespace MultiUserVR.Safety
{
    /// <summary>
    /// Phase 3: Centralized passthrough manager with color tinting and smooth transitions.
    /// Blends multiple safety triggers (user proximity + wall proximity) for unified MR experience.
    /// 
    /// CRITICAL: Provides visual feedback through passthrough color tinting to indicate safety zones.
    /// Green = safe, yellow = warning, red = danger.
    /// </summary>
    public class PassthroughManager : MonoBehaviour
    {
        [Header("Passthrough Control")]
        [Tooltip("OVRPassthroughLayer to control (auto-finds if not set)")]
        [SerializeField] private OVRPassthroughLayer passthroughLayer;

        [Header("Color Tinting")]
        [Tooltip("Safe zone color (no proximity warnings)")]
        [SerializeField] private Color safeColor = new Color(0f, 1f, 0f, 0.3f); // Green with transparency

        [Tooltip("Warning zone color (approaching boundary/user)")]
        [SerializeField] private Color warningColor = new Color(1f, 1f, 0f, 0.5f); // Yellow

        [Tooltip("Danger zone color (very close to collision)")]
        [SerializeField] private Color dangerColor = new Color(1f, 0f, 0f, 0.8f); // Red

        [Header("Transition Settings")]
        [Tooltip("Speed of color and opacity transitions")]
        [SerializeField] private float transitionSpeed = 3f;

        [Tooltip("Minimum opacity when passthrough is active")]
        [SerializeField] private float minOpacity = 0.3f;

        [Tooltip("Maximum opacity when passthrough is active")]
        [SerializeField] private float maxOpacity = 1.0f;

        [Header("Safety System Integration")]
        [Tooltip("CollisionPreventionSystem reference")]
        [SerializeField] private CollisionPreventionSystem collisionSystem;

        [Tooltip("RoomGuardian reference")]
        [SerializeField] private RoomGuardian roomGuardian;

        [Header("Debug")]
        [SerializeField] private bool logStateChanges = true;

        // State tracking
        private Color currentColor = Color.clear;
        private Color targetColor = Color.clear;
        private float currentOpacity = 0f;
        private float targetOpacity = 0f;
        private bool isPassthroughActive = false;

        public enum SafetyZone
        {
            Safe,
            Warning,
            Danger
        }

        private SafetyZone currentZone = SafetyZone.Safe;

        private void Awake()
        {
            // Auto-find passthrough layer if not assigned
            if (passthroughLayer == null)
            {
                passthroughLayer = FindFirstObjectByType<OVRPassthroughLayer>();
                if (passthroughLayer == null)
                {
                    Debug.LogError("[PassthroughManager] OVRPassthroughLayer not found! PassthroughManager disabled.");
                    enabled = false;
                    return;
                }
            }

            // Auto-find safety systems
            if (collisionSystem == null)
            {
                collisionSystem = FindFirstObjectByType<CollisionPreventionSystem>();
            }

            if (roomGuardian == null)
            {
                roomGuardian = FindFirstObjectByType<RoomGuardian>();
            }

            // Initialize passthrough to hidden state
            currentColor = safeColor;
            targetColor = safeColor;
            currentOpacity = 0f;
            targetOpacity = 0f;
            UpdatePassthroughVisuals();
        }

        private void Update()
        {
            DetermineSafetyZone();
            UpdateColorAndOpacity();
            UpdatePassthroughVisuals();
        }

        private void DetermineSafetyZone()
        {
            SafetyZone newZone = SafetyZone.Safe;
            bool shouldShowPassthrough = false;

            // Check user proximity (from CollisionPreventionSystem)
            if (collisionSystem != null)
            {
                var proximityLevel = collisionSystem.GetCurrentProximityLevel();
                
                switch (proximityLevel)
                {
                    case CollisionPreventionSystem.ProximityLevel.Critical:
                    case CollisionPreventionSystem.ProximityLevel.Danger:
                        newZone = SafetyZone.Danger;
                        shouldShowPassthrough = true;
                        break;
                    
                    case CollisionPreventionSystem.ProximityLevel.Warning:
                        newZone = SafetyZone.Warning;
                        shouldShowPassthrough = true;
                        break;
                }
            }

            // Check boundary proximity (from RoomGuardian)
            if (roomGuardian != null && roomGuardian.IsNearBoundary())
            {
                float boundaryDistance = roomGuardian.GetClosestBoundaryDistance();
                
                if (boundaryDistance < 0.3f) // Danger threshold
                {
                    newZone = SafetyZone.Danger;
                    shouldShowPassthrough = true;
                }
                else if (boundaryDistance < 0.5f) // Warning threshold
                {
                    // Only upgrade to warning if not already in danger from user proximity
                    if (newZone == SafetyZone.Safe)
                    {
                        newZone = SafetyZone.Warning;
                    }
                    shouldShowPassthrough = true;
                }
            }

            // Update zone
            if (newZone != currentZone)
            {
                if (logStateChanges)
                {
                    Debug.Log($"[PassthroughManager] Safety zone changed: {currentZone} → {newZone}");
                }
                currentZone = newZone;
            }

            // Update passthrough visibility
            isPassthroughActive = shouldShowPassthrough;
        }

        private void UpdateColorAndOpacity()
        {
            // Determine target color based on zone
            switch (currentZone)
            {
                case SafetyZone.Safe:
                    targetColor = safeColor;
                    targetOpacity = isPassthroughActive ? minOpacity : 0f;
                    break;

                case SafetyZone.Warning:
                    targetColor = warningColor;
                    targetOpacity = Mathf.Lerp(minOpacity, maxOpacity, 0.5f);
                    break;

                case SafetyZone.Danger:
                    targetColor = dangerColor;
                    targetOpacity = maxOpacity;
                    break;
            }

            // Smooth color transition
            currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * transitionSpeed);
            
            // Smooth opacity transition
            currentOpacity = Mathf.Lerp(currentOpacity, targetOpacity, Time.deltaTime * transitionSpeed);
        }

        private void UpdatePassthroughVisuals()
        {
            if (passthroughLayer == null)
            {
                return;
            }

            // Update opacity
            passthroughLayer.textureOpacity = currentOpacity;

            // Update color tinting
            // OVRPassthroughLayer uses colorScale and colorOffset for tinting
            // colorScale: Multiplies the passthrough color
            // colorOffset: Adds to the passthrough color

            // Apply color tint by mixing with white
            float tintStrength = currentOpacity;
            Color tintedColor = Color.Lerp(Color.white, currentColor, tintStrength);
            
            passthroughLayer.colorScale = tintedColor;
            passthroughLayer.colorOffset = Color.black; // No offset, just tinting

            // Show/hide passthrough layer
            passthroughLayer.hidden = currentOpacity < 0.01f;
        }

        /// <summary>
        /// Public API: Manually enable/disable passthrough
        /// </summary>
        public void SetPassthroughEnabled(bool enabled)
        {
            isPassthroughActive = enabled;
        }

        /// <summary>
        /// Public API: Get current safety zone
        /// </summary>
        public SafetyZone GetCurrentZone() => currentZone;

        /// <summary>
        /// Public API: Check if passthrough is currently visible
        /// </summary>
        public bool IsPassthroughVisible() => currentOpacity > 0.01f;

        /// <summary>
        /// Public API: Force immediate transition to a specific color
        /// </summary>
        public void SetImmediateColor(Color color, float opacity)
        {
            currentColor = color;
            targetColor = color;
            currentOpacity = opacity;
            targetOpacity = opacity;
            UpdatePassthroughVisuals();
        }

        /// <summary>
        /// Public API: Pulse passthrough with a color (for alerts)
        /// </summary>
        public void PulsePassthrough(Color color, float duration = 0.5f)
        {
            StartCoroutine(PulseCoroutine(color, duration));
        }

        private IEnumerator PulseCoroutine(Color color, float duration)
        {
            Color originalColor = targetColor;
            float originalOpacity = targetOpacity;

            // Pulse in
            targetColor = color;
            targetOpacity = maxOpacity;

            yield return new WaitForSeconds(duration * 0.5f);

            // Pulse out
            targetColor = originalColor;
            targetOpacity = originalOpacity;
        }
    }
}
