[← Documentation Home](../README.md) · [← Implementation](README.md)

# VR Performance (Meta Quest 3 · 90 FPS Target)

Actionable rendering and runtime optimizations for Quest 3 that preserve comfort and presence.

## Targets & Budgets (NFRs)
- Frame rate: 90 FPS → 11.11 ms/frame budget
- Suggested split: CPU (Main + Render) ≤ 4.5 ms, GPU ≤ 6.0 ms, headroom ≥ 0.6 ms
- Memory: < 4 GB; avoid spikes > 3.5 GB

## Rendering Pipeline
- Single Pass Instanced (XR) to reduce draw overhead
- MSAA: start at 2x; only raise to 4x if aliasing visible and GPU has headroom
- Foveated Rendering: enable Fixed Foveated Rendering (FFR) Medium; adjust per scene
- Post‑processing: keep minimal (bloom/AA disabled or mobile‑tuned)
- Transparency/Overdraw: minimize alpha‑blended surfaces; prefer cutouts and atlas sprites
- Lighting: bake static lights; limit real‑time shadows; prefer one punctual light w/o shadows
- Materials: prefer single‑pass URP/Lit Mobile variants; avoid expensive shader features

## Geometry & Batching
- Draw Calls: aim < 1.2k for typical scenes; < 2k worst‑case
- Triangles: keep visible tri count modest (< 1.5M on screen typical)
- Use GPU Instancing for repeated meshes; enable Static/Dynamic Batching where beneficial
- LOD Groups: 2–3 levels for medium/large meshes; crossfade disabled for mobile

## Textures & Memory
- Compression: ASTC for color textures; ETC for UI where appropriate
- Mipmaps: always; set correct max sizes (512–2k typical for props)
- Streaming: enable texture streaming where viable; budget streaming pool
- Avoid large RenderTextures; reuse buffers; disable XR mirror display

## Physics & Simulation
- Fixed Timestep: 1/60 s; Maximum Allowed Timestep ≤ 1/15 s
- Limit rigidbodies and continuous collision; use discrete where possible
- Avoid per‑frame physics queries in Update; batch in FixedUpdate if needed
- Pool objects; avoid frequent Instantiate/Destroy

## XR & Input
- Avoid heavy work in tracked device callbacks; cache transforms
- Prefer world‑space poses; avoid nested scaling in XR Origin
- Skip unnecessary IK for remote avatars (interpolate instead)

## Comfort & Presence
- Keep frame pacing stable; avoid spikes > 3 ms
- Minimize rotational acceleration and vignette only if needed
- Ensure latency low: reduce script GC allocations; use object pools

## Monitoring
- Unity Profiler attached to device (Editor → Analysis → Profiler → Attach to Player)
- Frame Debugger for draw call analysis
- In‑app HUD (RollingMetrics) for FPS EMA and thresholds (green ≥ 90, yellow 80–89, red < 80)

## Optimization Workflow
1) Identify if CPU‑ or GPU‑bound (Profiler → Timeline & Rendering)
2) If CPU‑bound: reduce scripts per‑frame work, jobify, debounce events, lower physics cost
3) If GPU‑bound: reduce resolution scale/FFR, disable shadows, reduce overdraw/tri count
4) Re‑measure on device; iterate until headroom ≥ 10%

## Checklist
- [ ] 90 FPS stable in target scenes
- [ ] GPU time ≤ 6 ms, CPU time ≤ 4.5 ms
- [ ] Draw calls within budget; no excessive overdraw
- [ ] Memory < 4 GB; no spikes on scene load
- [ ] No GC spikes > 2 ms during gameplay
