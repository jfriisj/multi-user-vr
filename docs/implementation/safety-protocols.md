[← Documentation Home](../README.md) · [← Implementation](README.md)

# Safety Protocols (Escalation, Procedures, Validation)

Clear, testable actions to prevent collisions and manage emergencies. Safety takes precedence over UX.

## Escalation Ladder (Req 3.2)
1) Safe → No action beyond passive indicators
2) Warning → Visual overlays + gentle haptics
3) Critical → Strong haptics + movement restriction + optional passthrough fade

### Visual Overlays
- Head-locked boundary ring and remote-user halo (yellow for warning, red for critical)
- Optional world-space cones toward approaching user

### Haptics (XR Interaction Toolkit)
```csharp
namespace MultiUserVR.Safety
{
    using UnityEngine;
    using UnityEngine.XR;

    public static class Haptics
    {
        public static void Pulse(InputDevice device, float amplitude, float duration)
        {
            if (device.isValid)
                device.SendHapticImpulse(0u, Mathf.Clamp01(amplitude), duration);
        }
    }
}
```

### Movement Restriction
- Disable teleport/locomotion providers or clamp movement speed to near-zero when Critical.
```csharp
namespace MultiUserVR.Safety
{
    using UnityEngine;
    using UnityEngine.XR.Interaction.Toolkit;

    public class MovementGate : MonoBehaviour
    {
        public ActionBasedContinuousMoveProvider move;
        public TeleportationProvider teleport;
        public void SetRestricted(bool on)
        {
            if (move) move.enabled = !on;
            if (teleport) teleport.enabled = !on;
        }
    }
}
```

## SafetyCoordinator (policy)
```csharp
namespace MultiUserVR.Safety
{
    using UnityEngine;
    using UnityEngine.XR;

    public class SafetyCoordinator : MonoBehaviour
    {
        public MovementGate gate;
        public Renderer headHalo;
        public Color warn = new(1f,0.85f,0f,0.6f);
        public Color crit = new(1f,0f,0f,0.6f);

        public InputDevice left, right;

        void Start()
        {
            // Acquire devices (simplified)
            var lefts = new System.Collections.Generic.List<InputDevice>();
            var rights = new System.Collections.Generic.List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, lefts);
            InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rights);
            if (lefts.Count>0) left = lefts[0];
            if (rights.Count>0) right = rights[0];
        }

        public void OnProximityChanged(ProximityState state, float minDist)
        {
            switch (state)
            {
                case ProximityState.Safe:
                    if (gate) gate.SetRestricted(false);
                    if (headHalo) headHalo.material.color = Color.clear;
                    break;
                case ProximityState.Warning:
                    if (headHalo) headHalo.material.color = warn;
                    Haptics.Pulse(left, 0.2f, 0.05f);
                    Haptics.Pulse(right, 0.2f, 0.05f);
                    break;
                case ProximityState.Critical:
                    if (gate) gate.SetRestricted(true);
                    if (headHalo) headHalo.material.color = crit;
                    Haptics.Pulse(left, 0.6f, 0.1f);
                    Haptics.Pulse(right, 0.6f, 0.1f);
                    break;
            }
        }
    }
}
```

Connect: ProximityMonitor.OnStateChanged += coordinator.OnProximityChanged.

## Emergency Procedures (testable)
- Instant Freeze: call gate.SetRestricted(true) for all players; display red overlay; require operator reset
- Quick Exit: map a dedicated input (e.g., long-press Menu) to fade to passthrough and pause the app
- Room Clear: host broadcast to clients to fade out, disable inputs, and show guidance overlay

## Validation Checklist
- Induce Warning by controlled approach at ~1.25 m; verify gentle haptics and halos
- Induce Critical at ≤0.75 m; verify locomotion disabled, strong haptics, clear visuals
- Simulate tracking loss: system must default to safe posture (restrict motion if uncertain)
- Latency spikes: dwell timers should prevent oscillation
- Logging: record time-in-state and min distances for post-run review
