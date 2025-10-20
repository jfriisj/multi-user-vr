[← Documentation Home](../README.md) · [← Workflows](README.md)

# Debugging Guide (VR + Networking)

Step-by-step diagnostics to resolve common VR and networking issues without requiring advanced tools.

## Before You Start (Quick Checks)
- Headsets in Developer Mode; USB debugging enabled (for builds/logs)
- Same WiFi network for all devices; 5 GHz preferred
- Unity version 2022.3 LTS; packages installed (OpenXR, XRI, Meta XR, NGO)
- Scene includes XR Origin, NetworkManager, Player Prefab assigned

## XR/VR Issues
### 1) App shows flat screen / no VR
- Cause: XR not enabled or wrong platform
- Fix:
  1. File → Build Settings → Switch Platform to Android
  2. Project Settings → XR Plugin Management → Android → Enable OpenXR
  3. Project Settings → OpenXR → Add Meta controller profile; apply

### 2) Controllers/hands not tracked
- Cause: Input setup missing or hand tracking disabled
- Fix:
  1. Import XRI Input Samples (Package Manager → XR Interaction Toolkit)
  2. Verify XR Origin (Action-based) exists in scene
  3. Enable hand tracking (OpenXR Android → Hand Tracking) if using hands
  4. In Play Mode, click Game View to focus (Editor controls)

### 3) View shakes or teleports unexpectedly
- Cause: XR Origin scaling/parenting or physics collisions
- Fix:
  1. Keep XR Origin scale at (1,1,1); avoid nested scaled parents
  2. Remove colliders from XR Origin hierarchy; use CharacterController
  3. Verify Fixed Timestep = 0.0167 (Project Settings → Time)

## Networking Issues
### 4) Client cannot join host
- Cause: Network mismatch or transport blocked
- Fix:
  1. Ensure both devices on same network; verify IP address of host
  2. Use Unity Transport default port (7777) and confirm no VPN/firewall blocks
  3. Start Host first; then press Join on client

### 5) Players connect but don't see each other
- Cause: Player Prefab not assigned / not spawned
- Fix:
  1. Select NetworkManager → Player Prefab: assign your Player Prefab
  2. Ensure Player Prefab contains NetworkObject component
  3. Check console for NGO warnings about missing prefabs

### 6) Objects not syncing between users
- Cause: Missing NetworkObject/authority or update disabled
- Fix:
  1. Ensure each networked object has NetworkObject + synchronization script (e.g., NetworkedInteractable)
  2. Verify server authority; ownership granted on grab; released on drop
  3. Check send rates (20–30 Hz) and thresholds; see Synchronization Patterns

## Safety Issues
### 7) Safety warnings never appear
- Cause: ProximityMonitor not wired or thresholds too small
- Fix:
  1. Assign local/remote transforms to ProximityMonitor
  2. Increase Warning/Critical radii; verify dwell time not too long
  3. Subscribe SafetyCoordinator to OnStateChanged

### 8) Movement not restricted on critical
- Cause: MovementGate not connected
- Fix:
  1. Add MovementGate; link ContinuousMoveProvider/TeleportationProvider
  2. Ensure SafetyCoordinator toggles MovementGate in Critical state

## Diagnostics & Tools
- Unity Profiler: attach to device; inspect Timeline (CPU) and Rendering (GPU)
- Networking Profiler module: bytes/s, message counts, object counts
- In‑app HUD: enable RollingMetrics for FPS/RTT/bytes EMA
- Logs: use adb to fetch logs when needed (developers with adb):
  1. Install Android platform tools
  2. `adb devices` shows the headset
  3. `adb logcat -s Unity` to view game logs

## When to Escalate
- Persisting crashes or freezes: collect logcat and repro steps
- Deterministic desyncs: capture Networking profiler data + scene setup
- Performance drops: record profiler data file (.data) and note scenario

See also: [Testing Procedures](testing-procedures.md), [Network Performance](../implementation/network-performance.md), [Safety Protocols](../implementation/safety-protocols.md).
