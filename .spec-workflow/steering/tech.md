# Technology Stack

## Project Type

Co-located Multi‑user VR application based on Unity’s MR Multiplayer Tabletop template (project at `mr-multiplayer/`). Template packages provide XR interactions, multiplayer (NGO), AR Foundation support, and Unity Services integrations (Authentication, Multiplayer, Vivox).

## Core Technologies

### Primary Language(s)
- Language: C# (.NET Standard 2.1 for Unity)
- Runtime/Compiler: Unity LTS supported by the template (see Unity docs) with IL2CPP for Android builds
- Test & tooling: Unity Test Framework; Unity Multiplayer Tools

### Key Dependencies/Libraries

VR & Input Systems
- XR Interaction Toolkit 3.x — interactions, UI, locomotion
- XR Hands 1.6.x — hand tracking support
- OpenXR + Meta OpenXR — headset runtime and platform support
- AR Foundation 6.x — template compatibility and MR features

Networking & Unity Services
- Netcode for GameObjects 2.x — multiplayer networking
- Unity Transport — low‑level transport for NGO
- Unity Services: Authentication, Multiplayer (Lobby/Relay), Vivox (voice)

Safety & Physics
- Unity Physics — collision and proximity checks
- Guardian integration — room boundary management
- Custom safety layer — proximity warnings and movement gating

### Application Architecture

Event‑driven architecture with layers:
1. VR Input (XRI, OpenXR/Meta XR)
2. Networking (NGO/Transport; optional UGS Relay/Lobby)
3. Safety (proximity, guardian, overrides)
4. Game Logic (avatar mgmt, interactions)
5. Research/Instrumentation (metrics, logging)

Component Principles
- Safety‑first: safety can override other subsystems
- Client‑server: host authority for safety decisions
- Decoupled messaging between VR, net, and safety layers

### Data Storage
- Config: ScriptableObjects
- Runtime state: NGO NetworkVariables
- Research data: JSON/CSV exports

### External Integrations
- Meta Quest 3 platform
- Unity Services (Authentication, Multiplayer, Vivox)
- Unity Profiler and Multiplayer Tools

### Monitoring & Dashboard
- In‑app Canvas UI for metrics and alerts
- NGO variables aggregated for live state

## Development Environment

Build & Development
- Target: Android (Quest 3), ARM64, OpenXR + Meta OpenXR
- Editor testing: XR Device Simulator; Multiplayer Play Mode (optional)
- Multi‑instance testing: ParrelSync (optional)
- Package management: Unity Package Manager (pinned in `mr-multiplayer/Packages/manifest.json`)

Code Quality
- Unity Test Framework (Edit/Play mode)
- Static analysis via IDE analyzers

Version Control & Collaboration
- Git + Git LFS; GitHub Flow; PR reviews with playtests

## Deployment & Distribution
- Platform: Meta Quest 3 (Android)
- Distribution: Build & Run / SideQuest during R&D; store later if needed
- Requirements: 3x Quest 3, Wi‑Fi 5+, ~2m x 2m per user

## Technical Requirements & Constraints

Performance Targets
- 90 FPS per headset
- <20 ms perceived avatar sync latency
- <100 ms safety response

Compatibility
- Unity version supported by template
- OpenXR + Meta OpenXR runtime

Security & Compliance
- Privacy‑respecting local research data; encrypted comms for cloud

Scalability & Reliability
- 3 concurrent users (scope)
- Graceful degradation on disconnects

## Technical Decisions & Rationale
1) Use Unity template to reduce risk and align with best practices
2) NGO client/server model provides server authority for safety
3) UGS optionality keeps LAN simple while enabling cloud scale later
4) Safety layer isolated for reliability
5) Instrumentation included to support research goals

## Known Limitations
- Quest 3 device limits (CPU/GPU/RAM, battery)
- Inside‑out tracking constraints in co‑located settings
- NGO learning curve; multi‑instance debugging complexity
- Room size and setup needs for safe co‑location

