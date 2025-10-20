[← Documentation Home](../README.md) · [← Implementation](README.md)

# Common Issues (Quick Fixes)

Practical fixes for frequently seen problems.

## Build & Deploy
- Error: "INSTALL_FAILED_VERSION_DOWNGRADE"
  - Fix: Uninstall previous app (Settings → Apps) or use `adb install -r` to replace
- Editor can't find Android SDK/NDK
  - Fix: Unity Hub → Installs → Add Android modules; set paths in Preferences

## XR Setup
- Black screen on device
  - Fix: Ensure OpenXR enabled for Android; scene added to Build Settings
- Tracking lost often
  - Fix: Improve lighting; clear cameras; enable hand/controller tracking appropriately

## Networking
- Client stuck on connecting
  - Fix: Verify host running; same WiFi; check port 7777; retry Join
- Only one player visible
  - Fix: Ensure Player Prefab assigned; NetworkObject present; no console errors

## Interaction
- Grab doesn't transfer
  - Fix: Call RequestGrabServerRpc on grab; object has NetworkObject + script
- Objects jitter when grabbed
  - Fix: Reduce physics timestep or send rate; avoid multiple writers to transform

## Safety
- No warnings near other users
  - Fix: Increase Warning/Critical radii; ensure remote transforms assigned
- Boundary not visible
  - Fix: Ensure guardian permission granted; fallback to XR boundary points

If unresolved, see [Debugging Guide](../workflows/debugging-guide.md) and [Error Resolution](../workflows/error-resolution.md).
