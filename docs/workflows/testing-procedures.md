[← Documentation Home](../README.md) · [← Workflows](README.md)

# Testing Procedures (VR, Networking, Safety)

Comprehensive tests across EditMode, PlayMode, and devices (Req 5.2). Avoid duplication—see also Single-Headset-Testing-Guide.md.

## Test Layers
- EditMode: algorithmic components (safety math, boundary math, serializers)
- PlayMode: scene-level integration (XR Origin, NGO spawn, hand/head sync)
- On-Device: Quest 3 multi-user, network conditions, performance, safety

## EditMode (Unity Test Framework)
- ProximityMonitor: thresholds, dwell-time, velocity gain behavior
- BoundaryMath: distance-to-polygon cases, degenerate polygons
- Serialization: NetworkPlayerData roundtrip

Run via CLI (see Development Process) or Test Runner.

## PlayMode (XR + NGO)
- Player spawn: Host + clients spawn Player Prefabs
- Pose sync: remote avatars interpolate smoothly at 20 Hz
- Ownership: grab conflict resolution; server grants single owner
- Safety: warning→critical transitions trigger correct protocols

Suggested approach: use Unity’s XR simulation and NGO local transport when available.

## On-Device (Quest 3)
- Topology: 1 Host + 2 Clients on same WiFi
- Steps: see docs/Single-Headset-Testing-Guide.md for Editor+Headset flow; replicate with 3 devices
- Acceptance:
  - Smooth head/hand sync (no rubber-banding)
  - Collision prevention triggers correctly with dwell-time stability
  - Guardian warnings near walls; no boundary crossings
  - Bandwidth within targets; profiler shows stable frame timing (90 FPS)

## Performance & Network Validation
- Profiler → Networking: confirm bandwidth budgets (2–5 KB/s idle; <10 KB/s peaks)
- Record latency (RTT) and time-to-replicate avatar motion (<20 ms total)
- Packet loss simulation: throttle WiFi or use router QoS; system remains usable

## Test Artifacts
- Store NUnit XML and logs under `Logs/`
- Export CSV for safety metrics (min distance, time-in-state)
- Attach artifacts to PRs for reviewer verification
