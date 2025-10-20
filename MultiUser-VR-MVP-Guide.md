# MR Multiplayer Tabletop MVP Guide

This project now uses Unity’s MR Multiplayer Tabletop template as the baseline (template: `com.unity.template.mr-multiplayer@1.0.3`). The Unity project lives in `mr-multiplayer/`. All docs and workflows align with the template’s packages: XR Interaction Toolkit, Netcode for GameObjects, AR Foundation, and Unity Services (Authentication, Multiplayer, Vivox).

Reference: https://docs.unity3d.com/Packages/com.unity.template.mr-multiplayer@1.0/manual/index.html

## 🚀 Quick start

1) Open the Unity project at `mr-multiplayer/` using a template-supported Editor version (see Unity docs).
2) Optional: Sign in to Unity Services if you plan to use Authentication/UGS Multiplayer/Relay/Vivox.
3) Open a sample scene and press Play:
   - Minimal sample: `Assets/XRMP/BasicScene.unity`
   - Tabletop example: `Assets/MRTabletopAssets/Games/Chess/Scenes/SlicesChess.unity`
4) Interact using XR Interaction Toolkit inputs (mouse/keyboard via XR Device Simulator in Editor, or VR controllers on device).

## 🎮 Multiplayer testing

Option A — Unity Multiplayer Play Mode (recommended for Editor-only)
- Install/enable the Multiplayer Play Mode package and run multiple local player instances.
- Validate NGO spawn, movement, and object sync without building to device.

Option B — ParrelSync (Editor clones)
- Use the ParrelSync extension to open clone Editor instances and connect as host/client.

Option C — Device testing (Quest 3)
- Build APKs and run on multiple devices; connect using NGO Unity Transport on LAN or UGS Relay/Lobby for NAT traversal.

## 📱 VR mode (Quest 3)

- Build Settings: Android, ARM64
- XR: OpenXR with Meta OpenXR plugin
- Test in-Editor with XR Device Simulator (Samples → XRI → XR Device Simulator) before deploying to devices

## 🌐 Networking (NGO)

- Framework: Netcode for GameObjects (2.x) with Unity Transport
- Topology: Client/Server (Host is server)
- Cloud (optional): UGS Authentication + Lobby/Relay; Vivox for voice
- Useful prefabs/scenes:
  - `Assets/XRMP/Prefabs/Managers/Network Manager XR Multiplayer.prefab`
  - `Assets/XRMP/BasicScene.unity`
  - Tabletop assets under `Assets/MRTabletopAssets/`

## 🔧 What this MVP provides (via the template)

- Multiplayer-ready scenes and prefabs demonstrating XR + NGO integration
- XR Interaction Toolkit-driven interactions in MR/AR/VR modes (we target VR)
- Sample tabletop games (sandbox, slingshot, chess) for reference
- Editor-first multiplayer testing flows

## 🔍 Local and cloud test flows

- Local Editor: Multiplayer Play Mode or ParrelSync clones
- Local LAN (devices): Unity Transport over Wi‑Fi
- Cloud (optional): UGS Lobby/Relay for cross‑network sessions

## 📋 Dependencies (from Packages/manifest.json)

- XR Interaction Toolkit 3.x (`com.unity.xr.interaction.toolkit`)
- Netcode for GameObjects 2.x (`com.unity.netcode.gameobjects`)
- AR Foundation 6.x (`com.unity.xr.arfoundation`)
- XR Hands (`com.unity.xr.hands`)
- OpenXR (`com.unity.xr.openxr`) + Meta OpenXR (`com.unity.xr.meta-openxr`)
- Unity Services (Authentication, Multiplayer, Vivox)
- Multiplayer Tools (`com.unity.multiplayer.tools`)

## 📚 Project documentation map

- Architecture: `docs/architecture/` (system, network, Unity integration)
- Implementation: `docs/implementation/` (XR/NGO patterns, performance, safety)
- Workflows: `docs/workflows/` (dev process, testing, deployment, demos)

## 🗺️ Migration notes (from the previous MVP)

- Previous custom components (e.g., bespoke managers or auto‑config scripts) should be replaced or aligned with the template’s NGO/XRI patterns and prefabs.
- Use the template’s scenes/prefabs as canonical references; add wrappers only where required.

## 🔒 Post‑MVP Enhancements (optional)

- Safety system and guardian integration: see `docs/implementation/*`
- Research instrumentation and metrics: see `docs/implementation/*` and `docs/workflows/research-procedures.md`
- Demo/presentation procedures: see `docs/workflows/*`

## ✅ Next steps

- Validate multiplayer in Editor with 2–3 local players
- Deploy to Quest 3 devices and verify NGO sync
- Iterate features using template‑aligned architecture and docs
