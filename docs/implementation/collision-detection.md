[← Documentation Home](../README.md) · [← Implementation](README.md)

# Collision Detection (Safety-First, Tested)

Reliable, low-latency user-to-user proximity detection for co-located VR (3 users). Safety takes precedence over performance.

## Objectives (Req 3.1)
- Detect approaching collisions between users (head and hands at minimum)
- Escalate warnings based on distance, velocity, and time-at-risk
- Remain robust under intermittent tracking or packet loss

## Safety Zones and Escalation
- Critical radius Rc (default 0.75 m): immediate restriction if violated
- Warning radius Rw (default 1.25 m): visual/haptic warnings; pre-emptive slow-down
- Dynamic expansion: R' = R + k · |relativeVelocity| (k ≈ 0.25 s)
- Dwell time: require sustained violation (e.g., ≥200 ms) before escalating, to avoid false positives

## Data Sources
- Local poses: XR Origin → head, left/right controller (or hands)
- Remote poses: Received via networking (see Avatar Synchronization)
- Guardian bounds (optional): for wall proximity checks (see Guardian Integration)

## Component Overview
- PoseProvider: central source of local head/hand poses
- ProximityMonitor: computes pairwise distances and risk states
- SafetyCoordinator: executes protocol actions (visual, haptic, movement restriction)

## ProximityMonitor (reference implementation)
```csharp
namespace MultiUserVR.Safety
{
    using System.Collections.Generic;
    using UnityEngine;

    public enum ProximityState { Safe, Warning, Critical }

    [System.Serializable]
    public class ProximityConfig
    {
        public float WarningRadius = 1.25f;
        public float CriticalRadius = 0.75f;
        public float VelocityGain = 0.25f;    // meters per (m/s)
        public float DwellMs = 200f;          // time to confirm state
        public float Smoothing = 0.15f;       // exp smoothing factor [0..1]
    }

    public class TrackedPoint
    {
        public Transform T;
        public Vector3 SmoothedPos;
        public Vector3 PrevPos;
        public Vector3 Velocity;
        public bool Valid => T != null;
        public void Update(float a)
        {
            if (!Valid) return;
            var p = T.position;
            Velocity = (p - PrevPos) / Mathf.Max(Time.deltaTime, 1e-3f);
            PrevPos = p;
            SmoothedPos = Vector3.Lerp(SmoothedPos == default ? p : SmoothedPos, p, a);
        }
    }

    public class ProximityMonitor : MonoBehaviour
    {
        [Header("Config")] public ProximityConfig Config = new();
        [Header("Local")] public Transform head; public Transform leftHand; public Transform rightHand;
        [Header("Remotes")] public List<Transform> remoteHeads = new();

        public ProximityState CurrentState { get; private set; } = ProximityState.Safe;
        public System.Action<ProximityState, float> OnStateChanged; // state, minDistance

        private readonly List<TrackedPoint> _locals = new();
        private readonly List<TrackedPoint> _remotes = new();
        private float _stateTimer;

        void Awake()
        {
            _locals.AddRange(new[] { new TrackedPoint{T=head}, new TrackedPoint{T=leftHand}, new TrackedPoint{T=rightHand} });
            foreach (var r in remoteHeads) _remotes.Add(new TrackedPoint{T=r});
        }

        void Update()
        {
            foreach (var tp in _locals) tp.Update(Config.Smoothing);
            foreach (var tp in _remotes) tp.Update(Config.Smoothing);

            float minDist = float.MaxValue; float relSpeed = 0f;
            foreach (var a in _locals)
            foreach (var b in _remotes)
            {
                if (!a.Valid || !b.Valid) continue;
                var d = Vector3.Distance(a.SmoothedPos, b.SmoothedPos);
                if (d < minDist)
                {
                    minDist = d;
                    relSpeed = (a.Velocity - b.Velocity).magnitude;
                }
            }

            var warn = Config.WarningRadius + Config.VelocityGain * relSpeed;
            var crit = Config.CriticalRadius + Config.VelocityGain * relSpeed;
            var target = ProximityState.Safe;
            if (minDist <= crit) target = ProximityState.Critical;
            else if (minDist <= warn) target = ProximityState.Warning;

            if (target == CurrentState)
            {
                _stateTimer = 0f; // stable
            }
            else
            {
                _stateTimer += Time.deltaTime * 1000f;
                if (_stateTimer >= Config.DwellMs)
                {
                    CurrentState = target;
                    _stateTimer = 0f;
                    OnStateChanged?.Invoke(CurrentState, minDist);
                }
            }
        }
    }
}
```

### Integration Notes
- Assign local head/hands from XR Origin; feed at least one remote head for each other user.
- Subscribe to OnStateChanged in SafetyCoordinator to trigger protocols.
- Use FixedUpdate or Update; prefer Update + smoothing to reduce jitter.

## Testing Strategy (PlayMode)
1. Simulate remote transforms with gizmo objects; move along scripted paths to validate thresholds.
2. Verify dwell time prevents flicker at boundaries.
3. Stress test: Inject packet loss by pausing remote updates; monitor stability.
4. Verify escalation ladder (visual→haptic→restriction) triggers only after state change callback.
5. Record metrics (min distance, time-in-state) to CSV for analysis.

## Performance Considerations
- Limit pairwise checks to head/hands; extend to torso if needed.
- Use simple Euclidean distances; avoid physics queries unless required.
- Never downscale safety checks to meet frame budget—reduce scene complexity instead.
