# Multi-User VR Documentation

This documentation now aligns with Unity’s MR Multiplayer Tabletop template. The Unity project is in `mr-multiplayer/` and includes XR Interaction Toolkit, Netcode for GameObjects, AR Foundation, and Unity Services integrations.

## 📋 Documentation Structure

### 🏗️ Architecture Documentation
- [System Overview](architecture/README.md) — High-level architecture and component relationships
- [System Architecture Diagrams](architecture/system-overview.md) — Layered and component views
- [Component Relationships](architecture/component-diagram.md) — Detailed interactions
- [Data Flow Diagrams](architecture/data-flow.md) — Runtime data paths
- [Network Topology](architecture/network-topology.md) — NGO client/server, UGS options
- [Unity Integration](architecture/unity-integration.md) — Scenes, prefabs, and settings

### 💻 Implementation Guides
- [Implementation Overview](implementation/README.md)
- [VR Setup Guide](implementation/vr-setup.md) — OpenXR + Meta plugin
- [Avatar Synchronization](implementation/avatar-synchronization.md) — NGO sync patterns
- [Tracking Systems](implementation/tracking-systems.md)
- [Collision Detection](implementation/collision-detection.md)
- [Safety Protocols](implementation/safety-protocols.md)
- [Guardian Integration](implementation/guardian-integration.md)
- [Synchronization Patterns](implementation/synchronization-patterns.md)
- [Network Optimization](implementation/network-optimization.md)
- [Data Collection](implementation/data-collection.md)
- [Metrics Tracking](implementation/metrics-tracking.md)
- [Component Relationships](implementation/component-relationships.md)
- [VR Performance](implementation/vr-performance.md)
- [Network Performance](implementation/network-performance.md)
- [Common Issues](implementation/common-issues.md)
- [Showcase Scenarios](implementation/showcase-scenarios.md)

### 📝 Workflow Documentation
- [Workflow Overview](workflows/README.md)
- [Development Process](workflows/development-process.md)
- [Testing Procedures](workflows/testing-procedures.md)
- [Deployment Guide](workflows/deployment-guide.md)
- [Research Procedures](workflows/research-procedures.md)
- [Performance Testing](workflows/performance-testing.md)
- [Debugging Guide](workflows/debugging-guide.md)
- [Error Resolution](workflows/error-resolution.md)
- [Demo Setup](workflows/demo-setup.md)
- [Presentation Guide](workflows/presentation-guide.md)

## 🚀 Quick start (template‑aligned)

- Open `mr-multiplayer/` in a supported Unity LTS
- Open `Assets/XRMP/BasicScene.unity` or `Assets/MRTabletopAssets/Games/Chess/Scenes/SlicesChess.unity`
- Test multiplayer in Editor with Multiplayer Play Mode or ParrelSync; build to Quest 3 for device tests

## 🔧 Technology Stack Reference

- Unity Editor: LTS supported by template
- Target Platform: Meta Quest 3 (Android)
- XR: XR Interaction Toolkit 3.x, XR Hands, AR Foundation, OpenXR + Meta OpenXR
- Networking: Netcode for GameObjects 2.x, Unity Transport; optional UGS Authentication/Lobby/Relay; Vivox for voice
- Tools: Unity Multiplayer Tools; optional Multiplayer Play Mode, ParrelSync

## 📊 Performance Targets

- Frame Rate: 90 FPS per headset
- Network Latency: <20 ms perceived avatar sync
- Safety Response: <100 ms collision detection response

## 🏠 Physical Requirements

- Room: ≥2m x 2m per user (3 users)
- Hardware: 3x Meta Quest 3
- Network: Wi‑Fi 5 (802.11ac)+

## 📚 Additional Resources

- [Unity MR Multiplayer Tabletop docs](https://docs.unity3d.com/Packages/com.unity.template.mr-multiplayer@1.0/manual/index.html)
- [Main Project README](../README.md)
- [MVP Guide](../MultiUser-VR-MVP-Guide.md)

---

Last Updated: Template alignment migration
