[← Documentation Home](../README.md) · [← Implementation](README.md)

# Network Optimization (VR + NGO)

Optimize bandwidth and latency while maintaining comfort and safety (Req 4.3).

## Budgets & Targets
- Baseline per-client: ~2–5 KB/s idle; peaks < 10 KB/s during interactions.
- Latency: keep one-way < 10 ms on LAN; total < 20 ms for pose sync.

## Configuration
- NetworkManager → NetworkConfig:
  - ClientConnectionBufferTimeout: small (e.g., 10 s)
  - TickRate: 30–60 (balance CPU vs. latency)
- Unity Transport:
  - Enable platform-optimized pipeline; keep MTU defaults; avoid fragmentation.

## Data Reduction Techniques
- Send rate control: 20 Hz for head/hands, 30 Hz only while grabbing.
- Change thresholds: only send when pose delta exceeds position/rotation thresholds.
- Quantization: pack Vector3/Quaternion to fewer bits when using Custom Messaging (e.g., 1 cm, 1 deg precision).
- Sparse updates: stop updates for off-screen or distant peers.

## Object Lifecycle
- Pool networked objects to avoid spawn/despawn bursts.
- Disable replication for idle components; re-enable on interaction.

## Interpolation Strategy
- Interpolate remote avatars; avoid extrapolation to prevent discomfort.
- Use time-aligned buffers (100–150 ms) if jittery networks encountered.

## Security Best Practices
- Server authority for critical state; never trust client transforms blindly.
- Validate RPC intents; implement cooldowns on ownership requests.
- For off-LAN, use Relay with authenticated join codes; prefer encrypted transport when available.

## Monitoring & Debugging
- Unity Profiler → Networking → Netcode for GameObjects: track bandwidth, messages, object counts.
- Add in-game net stats (rtt, bytes/s, tickrate).
- Record session metrics (CSV/JSON) to correlate with safety incidents.

## Validation Checklist
- Idle bandwidth within budget; peaks under control during heavy grabs.
- Smooth motion under 2–5% packet loss (no oscillation due to dwell timers).
- No security violations: invalid ownership, speed spikes are rejected server-side.
