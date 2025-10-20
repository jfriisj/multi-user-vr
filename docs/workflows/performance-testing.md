[← Documentation Home](../README.md) · [← Workflows](README.md)

# Performance Testing (On‑Device · Repeatable)

Procedures to detect performance issues early and validate NFRs on Quest 3.

## Environment
- Build type: Development build for diagnostics; disable deep logging during measurements
- Devices: 3× Quest 3 on same WiFi; chargers connected
- Profiler: attach to Player; enable GPU profiling when needed

## Scenarios
1) Idle scene (no users nearby) – baseline
2) Locomotion + head/hand motion – typical
3) Object grab/manipulation – peak updates
4) Safety edge cases – close proximity, boundary approach
5) Stress – many props (profiling only)

## Metrics & Thresholds
- FPS ≥ 90; GPU ≤ 6 ms; CPU ≤ 4.5 ms
- RTT EMA < 10 ms; bandwidth peaks < 10 KB/s
- Memory < 4 GB; no allocation spikes during gameplay (>2 ms GC)

## Procedure
1) Deploy build to all devices (see Deployment Guide)
2) Attach Profiler to Host; record 60–120 s per scenario
3) Capture: Timeline, Rendering, Networking modules; save .data files
4) In‑app HUD: record EMA readings (FPS/RTT/bytes) and note spikes
5) Export CSV logs from DataCollector (tracking/safety) for correlation
6) File artifacts under `Logs/` with scenario labels

## CPU vs GPU Diagnosis
- CPU bound: high main/render thread time; reduce scripting/physics, batch work
- GPU bound: high render time; lower resolution/FFR, reduce overdraw, shadows, tri count

## Pass/Fail Gates (CI or Manual)
- [ ] All scenarios meet thresholds above
- [ ] No >3 ms spikes in frame time for >1% frames
- [ ] Safety protocols unaffected by optimization changes
- [ ] Network smooth under 2–5% simulated loss

## Reporting
- Include profiler captures, CSV summaries (SessionSummary), device list, app version
- Highlight regressions vs previous build; track trends over time

Cross‑refs: [VR Performance](../implementation/vr-performance.md), [Network Performance](../implementation/network-performance.md), [Network Optimization](../implementation/network-optimization.md).
