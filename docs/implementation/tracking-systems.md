[← Documentation Home](../README.md) · [← Implementation](README.md)

# Tracking Systems (XR Input, XR Hands, Tracking Integrity)

Configure tracking and monitor tracking integrity for Quest 3.

## Controller/Head Pose Sources
- XR Origin (Action-based) provides Camera (HMD) and Left/Right Controller transforms
- Use these as sources for synchronization and safety systems

## Hand Tracking (Optional)
1) Enable in Project Settings → OpenXR (Android): Hand Tracking feature
2) Install XR Hands 1.4+ (Package Manager)
3) Use XR Hands Sample or substitute controller transforms when hands unavailable

## Tracking Monitor (detect loss and recovery)
```csharp
namespace MultiUserVR.VR.Tracking
{
    using UnityEngine;
    using UnityEngine.XR;
    using System.Collections.Generic;

    public class TrackingMonitor : MonoBehaviour
    {
        public enum Node { Head, LeftHand, RightHand }
        public Node node;
        public float lostTimeout = 1.0f;
        public bool IsTracked { get; private set; }
        public float TimeSinceLastTracked { get; private set; }

        private InputDevice _device;

        void OnEnable() => Acquire();

        void Update()
        {
            if (!_device.isValid) Acquire();
            bool tracked = false;
            if (_device.isValid && _device.TryGetFeatureValue(CommonUsages.isTracked, out tracked))
            {
                if (tracked) TimeSinceLastTracked = 0f; else TimeSinceLastTracked += Time.deltaTime;
                IsTracked = tracked && TimeSinceLastTracked < lostTimeout;
            }
        }

        private void Acquire()
        {
            var role = node == Node.Head ? XRNode.Head : node == Node.LeftHand ? XRNode.LeftHand : XRNode.RightHand;
            _device = InputDevices.GetDeviceAtXRNode(role);
        }
    }
}
```

## Pose Provider (for sync/safety)
```csharp
namespace MultiUserVR.VR.Tracking
{
    using UnityEngine;

    public class PoseProvider : MonoBehaviour
    {
        public Transform head;
        public Transform leftHand;
        public Transform rightHand;

        public (Vector3, Quaternion) HeadPose => (head.position, head.rotation);
        public (Vector3, Quaternion) LeftPose => (leftHand.position, leftHand.rotation);
        public (Vector3, Quaternion) RightPose => (rightHand.position, rightHand.rotation);
    }
}
```

## Best Practices
- Keep update rate reasonable (e.g., 20 Hz for network sync)
- Prefer world-space poses for consistency across devices
- When tracking is lost, freeze last-known pose or fade avatar to avoid jitter
- For safety, feed PoseProvider into proximity checks at fixed intervals

## Verification
- Move head/hands; TrackingMonitor.IsTracked toggles appropriately in Editor → Inspector
- Disconnect/reconnect controllers; monitor recovery within lostTimeout
- Ensure PoseProvider outputs match XR Origin transforms
