[← Documentation Home](../README.md) · [← Workflows](README.md)

# Error Resolution (Step‑by‑Step)

Guided flows to resolve issues quickly. Follow each step in order; stop when resolved.

## Flow A: Cannot Join Session
1) Same Network? Ensure host and clients on same WiFi (5 GHz preferred)
2) Host Running? Start Host first; confirm on‑screen status shows "Host"
3) Player Prefab Assigned? NetworkManager → Player Prefab set; contains NetworkObject
4) Port Open? Ensure port 7777 allowed on network (guest networks may block)
5) Retry: Quit app on client and relaunch; try Join again
6) Logs: If still failing, collect `adb logcat -s Unity` during Join; attach to issue

## Flow B: Players Visible But Not Moving
1) Verify VRPlayerSync on Player Prefab; owner writes pose via NetworkVariable
2) Check sendRateHz (20) and owner status (IsOwner true on local player)
3) Inspect Networking profiler for pose updates; confirm deltas changing
4) Interpolate: remote avatar transforms should lerp/slerp toward network state
5) Logs: capture profiler snapshot and Unity logs if no updates seen

## Flow C: Objects Do Not Sync
1) Ensure object has NetworkObject + NetworkedInteractable (or NetworkTransform)
2) Ownership: call RequestGrabServerRpc on grab; ReleaseServerRpc on drop
3) Server Authority: verify server accepted ownership; check IsGrabbed flag
4) Send Rates: increase to 30 Hz while grabbed; reduce when idle
5) Logs: capture Networking profiler data during interaction

## Flow D: Safety Not Triggering
1) Assign local head/hands and remote head to ProximityMonitor
2) Increase Warning/Critical radii; reduce dwell time if too slow
3) Subscribe SafetyCoordinator to OnStateChanged; verify MovementGate wired
4) Validate in editor with gizmo transforms moving toward each other

## Flow E: Performance Below 90 FPS
1) Determine CPU vs GPU bound (Profiler)
2) CPU bound: reduce scripts/physics, batch work, pool objects, lower NGO TickRate
3) GPU bound: enable FFR, reduce draw calls/overdraw, disable real‑time shadows
4) Re‑test scenarios; ensure EMA HUD shows recovery to ≥ 90 FPS

## Flow F: Guardian/Boundary Problems
1) Re‑run room setup on each device; clear/redo Guardian
2) Use fallback XR boundary points if Meta boundary unavailable
3) Ensure boundary distance fed to SafetyCoordinator to pre‑empt collisions

If still unresolved, escalate with:
- Repro steps, build version, device list
- Profiler data (.data), CSV logs, and Unity logcat excerpt
