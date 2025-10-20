[← Documentation Home](../README.md) · [← Implementation](README.md)

# Network Performance (3‑User NGO · Low Latency)

Practical guidance to meet VR networking NFRs on Quest 3 while preserving comfort.

## Targets
- One‑way latency < 10 ms LAN; pose replication budget < 20 ms end‑to‑end
- Idle bandwidth ~ 2–5 KB/s per client; interaction peaks < 10 KB/s

## Configuration
- NGO TickRate: 30–60; start at 30 and raise only if CPU allows
- Pose streams: 20 Hz; event RPCs reliable, continuous data unreliable (sequenced)
- Ownership: server‑authoritative; temporary ownership for grabs; server resolves conflicts

## Data Packing
- Send only changed components (delta thresholds)
- Quantize vectors/quaternions when using Custom Messaging (1 cm, 1°)
- Coalesce small updates into one message per tick where possible

## Interest Management
- Distance‑based throttling: suspend updates beyond 10–15 m
- Scene partitioning (zones): publish only to subscribers

## Jitter Handling
- Interpolate avatars; avoid extrapolation to prevent discomfort
- Maintain 100–150 ms interpolation buffer if jittery; shrink on stable networks

## Monitoring
- Unity Profiler → Networking module (bytes/s, message counts, object counts)
- In‑app stats from RollingMetrics (RTT EMA, bytes/s EMA)
- CSV logging via DataCollector for offline analysis (see Data Collection)

## Troubleshooting
- Spikes: verify coalescing and thresholds; reduce send rate temporarily
- Rubber‑banding: ensure buffer adequate; check time sync between host/clients
- Ownership glitches: enforce server checks; reset ownership on timeouts

## Acceptance Checks
- [ ] RTT EMA < 10 ms; p95 < 20 ms on LAN
- [ ] Idle bandwidth within 2–5 KB/s; peaks < 10 KB/s during grabs
- [ ] No duplicate ownership; conflict resolution deterministic
- [ ] Smooth motion without oscillation at 2–5% packet loss

See also: [Network Optimization](network-optimization.md) and [Synchronization Patterns](synchronization-patterns.md).
