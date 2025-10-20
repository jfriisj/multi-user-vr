# Spec Features (MVP, Template‑Aligned)

> ⚠️ **UPDATED:** See [spec-features-revised.md](spec-features-revised.md) for detailed Unity-MCP analysis (Oct 20, 2025)  
> 📊 **SUMMARY:** See [mvp-gap-analysis.md](mvp-gap-analysis.md) for executive summary and implementation roadmap

This file enumerates the minimal feature specs required to deliver the MVP using Unity's MR Multiplayer Tabletop template (`mr-multiplayer/`) and our guide in `MultiUser-VR-MVP-Guide.md`.

Scope: focus on co‑located local multiplayer (same room), player visualization, and basic NGO/XRI integration. Post‑MVP items are excluded here.

Read first: MultiUser-VR-MVP-Guide.md, docs/README.md, .spec-workflow/steering/product.md, tech.md, structure.md

## Current Implementation Status (Oct 20, 2025)

**MVP Completion:** ~60% (3 of 6 features complete)  
**Critical Blocker:** LAN-only connection mode (feature #4)  
**Days to MVP:** 4-6 days estimated

| Feature | Status | Priority |
|---------|--------|----------|
| session-management-ui | ⚠️ Partial (cloud works, LAN missing) | 🔴 Critical |
| avatar-synchronization | ✅ Complete | ✅ Done |
| networked-interactables | ✅ Complete | ✅ Done |
| **lan-join-and-manual-ip** | ❌ **Missing** | 🔴 **CRITICAL** |
| local-co-located-visualization | ⚠️ Partial (name tags only) | 🟡 High |
| appearance-customization | ✅ Complete | ✅ Done |

## Core MVP feature specs

1) session-management-ui
- Host/Join/Leave actions (Editor + device)
- Player list with display names and role (Host/Client)
- Minimal in‑scene UI; works with XRMP Network Manager prefab

2) avatar-synchronization
- Owner‑write head/hands, remote avatar visualization
- Update rates: ~20 Hz poses, interpolation/smoothing
- Spawn via NGO PlayerPrefab; OpenXR + Meta plugin

3) networked-interactables
- XRI interactables with NGO ownership transfer
- Object pose sync while grabbed (~30 Hz); idle optimization

4) lan-join-and-manual-ip
- Unity Transport over LAN by default
- Manual IP entry fallback; optional UGS Lobby/Relay later

5) local-co-located-visualization
- In‑room visualization of players (name tags, simple color coding)
- Optional mini “tabletop map” overlay anchored to play area
- Basic distance indicators for awareness (non‑blocking)

6) appearance-customization
- Per‑player color/material + display name selection
- Synced to all clients; persists for session

## Non‑goals (out of MVP scope)
- Full safety/guardian systems, research instrumentation, advanced analytics

## Notes
- Use template assets/scenes where possible (XRMP/BasicScene, MRTabletopAssets/Chess)
- Favor ParrelSync or Multiplayer Play Mode for Editor testing

## Spec IDs to create under .spec-workflow/specs

### Original Specs (Status Updated)
- `session-management-ui` - ⚠️ Needs LAN mode enhancement
- ~~`avatar-synchronization`~~ - ✅ Complete
- ~~`networked-interactables`~~ - ✅ Complete
- `lan-join-and-manual-ip` - ❌ **MUST CREATE (CRITICAL)**
- `local-co-located-visualization` - ⚠️ Needs distance indicators/minimap
- ~~`appearance-customization`~~ - ✅ Complete

### New Specs Needed
- `lan-connection-manager` - Direct IP networking system (CRITICAL)
- `spatial-awareness-system` - Distance indicators and minimap
- `guardian-integration` - Boundary sharing (optional)

---

## Agent Prompts (Spec Workflow MCP)

Use these prompts with the Spec Workflow MCP tools to create detailed specifications for each new feature.

### 1. LAN Connection Manager (CRITICAL)

```
Create a complete spec-workflow for the "lan-connection-manager" feature for a Unity VR multiplayer project using Netcode for GameObjects.

Context:
- Project: Multi-user VR system using Unity MR Multiplayer Tabletop template (mr-multiplayer/)
- Current Issue: Template only supports cloud-based networking via Unity Gaming Services (Lobby/Relay)
- MVP Requirement: "Unity Transport over LAN by default" with manual IP entry for direct device-to-device connection
- Target Platform: Meta Quest 3 headsets (3 devices in same room, same WiFi network)
- Network Stack: Netcode for GameObjects 2.x + Unity Transport

Requirements to specify:
1. LANConnectionManager component that configures Unity Transport for direct local connections
2. UI panel for manual IP address and port entry
3. Host mode: Start server on local IP, display IP address for others to join
4. Client mode: Enter host IP address, connect directly without internet/UGS
5. Dual-mode support: Toggle between Cloud (UGS Relay) and LAN (Direct IP)
6. Connection status feedback, error handling, timeout management
7. IP address discovery helper (get local WiFi IP on Quest 3)
8. Integration with existing XRINetworkGameManager and NetworkManagerXRMultiplayer

Technical constraints:
- Must work without internet connection (LAN only)
- Must coexist with existing cloud-based lobby system
- Must support 3 simultaneous connections (1 host + 2 clients)
- Connection establishment should complete within 5 seconds
- Avatar sync must maintain <20ms latency after connection

File locations:
- New script: Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionManager.cs
- UI prefab: Assets/MRTabletopAssets/Prefabs/UI/LANConnectionPanel.prefab
- Modify: Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs
- Integrate with: Assets/MRTabletopAssets/Scripts/UI/LobbyList/LobbyUI.cs

Deliverables:
- Requirements document with user stories and acceptance criteria
- Design document with architecture, class diagrams, and Unity integration patterns
- Tasks document with step-by-step implementation checklist
- Code examples for UnityTransport configuration and NetworkManager integration

Reference documents in repo:
- docs/mvp-action-plan.md (Day 1-3 implementation details)
- docs/spec-features-revised.md (Feature #4 detailed analysis)
- MultiUser-VR-MVP-Guide.md (MVP requirements and testing approaches)

Deveoplopment:
- Use Unity-MCP interface when developing in unity editor 
```

---

### 2. Spatial Awareness System

```
Create a complete spec-workflow for the "spatial-awareness-system" feature for a co-located multi-user VR environment.

Context:
- Project: Multi-user VR system with 3 Meta Quest 3 users in the same physical room
- Current State: Basic color-coded name tags with LOD system exist (PlayerNameTag.cs)
- Gap: Missing distance indicators, spatial minimap, and proximity warnings for co-located safety
- Purpose: Enhance user awareness of other players' positions to prevent physical collisions
- Technology: Unity XR Interaction Toolkit 3.x, Netcode for GameObjects 2.x

Requirements to specify:
1. Distance indicators on player name tags
   - Show distance from local player to each remote player (e.g., "2.5m")
   - Update at 10Hz (not every frame for performance)
   - Color-coded by proximity: Green (>2m), Yellow (1-2m), Red (<1m)
   - Toggle visibility on/off

2. Tabletop minimap overlay
   - Top-down orthographic view of play area
   - Anchored to Virtual Table GameObject
   - Show player positions as colored icons matching avatar colors
   - Real-time position updates
   - Toggle visibility with button
   - Optional zoom controls

3. Proximity warning system
   - Audio/visual alerts when players <1m apart physically
   - Haptic feedback on controllers (optional)
   - Non-intrusive warnings (gentle beep, subtle visual border)
   - Adjustable warning thresholds

4. CoLocatedVisualizationManager
   - Central coordinator for all spatial awareness features
   - Settings panel for enabling/disabling features
   - Performance monitoring to maintain 90 FPS

Technical constraints:
- Must maintain 90 FPS on Quest 3 with all features active
- Distance calculations must be efficient (avoid N² every frame)
- Minimap rendering should use separate lightweight render pipeline
- Warnings must not interfere with gameplay or cause alarm
- All features must work with Netcode for GameObjects synchronization

File locations:
- Modify: Assets/XRMP/Scripts/Network/NetworkPlayer/PlayerNameTag.cs
- New script: Assets/XRMP/Scripts/LocalPlayer/TabletopMinimap.cs
- New script: Assets/XRMP/Scripts/LocalPlayer/PlayerProximityIndicator.cs
- New script: Assets/XRMP/Scripts/LocalPlayer/CoLocatedVisualizationManager.cs
- New prefab: Assets/MRTabletopAssets/Prefabs/UI/TabletopMinimapCanvas.prefab

Deliverables:
- Requirements document with user stories focused on co-located safety
- Design document with spatial calculation algorithms and UI mockups
- Tasks document with phased implementation (distance → minimap → warnings)
- Performance optimization strategies and testing procedures

Reference documents in repo:
- docs/mvp-action-plan.md (Day 4-6 implementation details)
- docs/spec-features-revised.md (Feature #5 detailed analysis)
- docs/implementation/safety-protocols.md (Safety system context)

Deveoplopment:
- Use Unity-MCP interface when developing in unity editor 
```

---

### 3. Guardian Integration (Optional)

```
Create a complete spec-workflow for the "guardian-integration" feature for Meta Quest 3 boundary sharing in multi-user VR.

Context:
- Project: Co-located multi-user VR with 3 Meta Quest 3 users in same physical room
- Challenge: Each user has own Guardian boundary, but users don't know where others' boundaries are
- Risk: Users may collide physically even if respecting their own Guardian boundaries
- Goal: Share and visualize Guardian boundary data between networked clients for collision avoidance
- Platform: Meta Quest 3 with Meta XR SDK, Unity OpenXR, Netcode for GameObjects

Requirements to specify:
1. Guardian API access investigation
   - Research if Meta XR SDK exposes Guardian boundary point data
   - Determine if boundary geometry can be read programmatically
   - Check for privacy/security restrictions on boundary sharing
   - Fallback options if API unavailable

2. Boundary data synchronization
   - NetworkVariable to share boundary points between clients
   - Efficient serialization of boundary geometry (minimize bandwidth)
   - Update only when boundary changes (not continuous sync)
   - Handle different room sizes/shapes across users

3. Visual boundary rendering
   - Render other users' Guardian boundaries in VR
   - Visual style: Semi-transparent walls/mesh at boundary edges
   - Color-code by user (match avatar colors)
   - Highlight overlapping danger zones in red

4. Collision warning integration
   - Detect when user approaches another user's boundary
   - Visual warning when <0.5m from someone else's boundary
   - Audio cue for boundary proximity
   - Optional: Prevent movement past certain threshold

5. Privacy and safety considerations
   - Allow users to opt-out of boundary sharing
   - Clear visual indication when boundaries are being shared
   - Ensure boundary data only used for collision prevention
   - Graceful degradation if some users don't share

Technical constraints:
- Dependent on Meta XR SDK Guardian API availability (may be blocked)
- Must work with existing safety systems (don't replace, augment)
- Boundary visualization should not obstruct gameplay view
- Network bandwidth must be minimal (<1KB per boundary update)
- Performance: Boundary rendering must not impact 90 FPS target

File locations:
- New script: Assets/XRMP/Scripts/Safety/GuardianBoundaryManager.cs
- New script: Assets/XRMP/Scripts/Safety/GuardianVisualization.cs
- New script: Assets/XRMP/Scripts/Safety/BoundaryCollisionDetector.cs
- Integration with: Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs

Deliverables:
- Requirements document with safety-focused user stories
- Design document with Guardian API research findings and architecture
- Tasks document with API investigation phase followed by implementation
- Fallback design if Guardian API inaccessible
- Privacy and ethical considerations documentation

Reference documents in repo:
- docs/mvp-action-plan.md (Phase 3 implementation details)
- docs/spec-features-revised.md (Feature #5 Guardian section)
- docs/implementation/guardian-integration.md (Existing Guardian notes)
- docs/implementation/safety-protocols.md (Safety system integration)

Note: This is marked as OPTIONAL for MVP. If Meta Guardian API is not accessible, document the limitation and propose alternative proximity detection methods using XR rig positions only.

Deveoplopment:
- Use Unity-MCP interface when developing in unity editor 
```

---

### 4. Session Management UI

```
Create a complete spec-workflow for the "session-management-ui" feature aligning the Lobby/Session flows with dual connection modes (Cloud via UGS and LAN Direct via Unity Transport).

Context:
- Project: Multi-user VR using Unity MR Multiplayer Tabletop template (mr-multiplayer/)
- Current State: Cloud (UGS) flows work; LAN-only path and clearer session controls are missing
- Target: Meta Quest 3, 1 host + 2 clients, co-located
- Network Stack: Netcode for GameObjects 2.x + Unity Transport; optional UGS Lobby/Relay

Requirements to specify:
1. Dual-mode session flow (Cloud/LAN) exposed in UI
2. Host/Join/Leave buttons usable on device and in-Editor
3. Prominent Disconnect/Leave control inside active session
4. Player list with role badges (Host/Client), local player highlight
5. Connection status, busy/loading, and error/timeout feedback
6. Smooth handoff to XRINetworkGameManager and new LANConnectionManager
7. No internet required for LAN Direct; Cloud flow remains intact

Technical constraints:
- Toggle between modes without restarting scene
- Complete connect/disconnect within 5s, UI must reflect state transitions
- Minimum allocations and no GC spikes during connect/disconnect

File locations:
- Modify: Assets/MRTabletopAssets/Scripts/UI/LobbyList/LobbyUI.cs
- Modify: Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs
- Read/Integrate: Assets/XRMP/Scripts/Network/NetworkManagers/NetworkManagerXRMultiplayer.cs
- Integrate with: Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionManager.cs (when in LAN mode)

Deliverables:
- Requirements doc with user stories and acceptance criteria
- Design doc with UI state machine and wiring to managers
- Tasks doc with step-by-step UI integration checklist
- Code examples for mode toggle, button handlers, and status updates

Reference documents in repo:
- docs/spec-features-revised.md (Feature #1 analysis)
- docs/mvp-action-plan.md (UI day plan)
- MultiUser-VR-MVP-Guide.md (MVP flows)

Deveoplopment:
- Use Unity-MCP interface when developing in unity editor 
```

---

### 5. Avatar Synchronization

```
Create a complete spec-workflow for the "avatar-synchronization" feature documenting the as-built implementation and defining validation criteria.

Context:
- Project: Unity MR Multiplayer Tabletop template baseline
- Current State: Implemented with XRINetworkPlayer and ClientNetworkTransform
- Target: Meta Quest 3, 90 FPS, <20ms perceived latency

Requirements to specify:
1. Authority model (owner-write), update rates (head/hands ~20Hz), interpolation/smoothing
2. IK setup (XRAvatarIK) and visual pipeline (XRAvatarVisuals)
3. Spawn/despawn via NGO PlayerPrefab lifecycle
4. Performance targets and profiling plan on Quest 3
5. Edge cases (late join, reconnect, host migration if any)

Technical constraints:
- Maintain 90 FPS, minimize bandwidth (<100 KB/s per player typical)
- Robust to short packet loss without visible jitter

File locations:
- Read: Assets/XRMP/Scripts/Network/NetworkPlayer/XRINetworkPlayer.cs
- Read: Assets/XRMP/Scripts/Network/NetworkUtilityComponents/ClientNetworkTransform.cs
- Read: Assets/XRMP/Scripts/Network/NetworkPlayer/XRAvatarIK.cs, XRAvatarVisuals.cs
- Prefab: Assets/XRMP/Prefabs/PlayerPrefabs/XRI_Network_Player_Avatar.prefab

Deliverables:
- Requirements doc (user stories, latency/quality acceptance criteria)
- Design doc (data flow, timing diagram, network variables)
- Tasks doc (validation tests, perf measurements, QA checklist)
- Code snippets showing configuration and smoothing parameters

References:
- docs/spec-features-revised.md (Feature #2)
- MultiUser-VR-MVP-Guide.md (testing approaches)

Deveoplopment:
- Use Unity-MCP interface when developing in unity editor 
```

---

### 6. Networked Interactables

```
Create a complete spec-workflow for the "networked-interactables" feature documenting ownership transfer, pose sync, and performance behaviors.

Context:
- Project: Uses XRI interactables integrated with NGO
- Current State: Implemented (NetworkBaseInteractable, NetworkPhysicsInteractable, CustomNetworkTransform)

Requirements to specify:
1. Ownership transfer rules (ClaimableNetworkBehaviour) and contention handling
2. Pose sync rates (~30 Hz while grabbed, <5 Hz idle), compression, and snapping rules
3. Physics authority setup and reconciliation constraints
4. Interaction latency and visual smoothing targets
5. Test scenarios (grab/release, throw, sockets)

Technical constraints:
- Deterministic authority to avoid tug-of-war
- Stable at 3 players with minimal bandwidth

File locations:
- Read: Assets/XRMP/Scripts/Network/NetworkInteractions/*.cs
- Read: Assets/MRTabletopAssets/Scripts/ClaimableNetworkBehaviour.cs, ClaimableNetworkTransform.cs, CustomNetworkTransform.cs

Deliverables:
- Requirements doc with interaction user stories
- Design doc with ownership state machine and sync strategy
- Tasks doc for verification and perf tuning
- Code examples for claiming/releasing and sync configuration

References:
- docs/spec-features-revised.md (Feature #3)
- MultiUser-VR-MVP-Guide.md

Deveoplopment:
- Use Unity-MCP interface when developing in unity editor 
```

---

### 7. LAN Join and Manual IP

```
Create a complete spec-workflow for the "lan-join-and-manual-ip" feature enabling direct device-to-device connection without UGS.

Context:
- Project: Same-room Quest 3 devices on the same WiFi
- Current Gap: No manual IP entry; all paths go through Lobby/Relay

Requirements to specify:
1. IP address + port inputs and validation (default 7777)
2. Host start on local IP, display joinable IP to others
3. Client join via entered IP; timeout and error handling
4. Connection status feedback and retry/cancel flows
5. Dual-mode toggle (Cloud vs LAN Direct) and persisted last choice
6. Integration with LANConnectionManager for transport setup

Technical constraints:
- Connect within 5 seconds
- Works with no internet

File locations:
- New prefab: Assets/MRTabletopAssets/Prefabs/UI/LANConnectionPanel.prefab
- Modify: Assets/MRTabletopAssets/Scripts/UI/LobbyList/LobbyUI.cs
- Modify: Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs
- Integrate: Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionManager.cs

Deliverables:
- Requirements, Design, Tasks documents
- Code examples for UnityTransport configuration and input validation

References:
- docs/spec-features-revised.md (Feature #4)
- docs/mvp-action-plan.md (Day 1–3)

Deveoplopment:
- Use Unity-MCP interface when developing in unity editor 
```

---

### 8. Local Co-located Visualization

```
Create a complete spec-workflow for the "local-co-located-visualization" feature to improve in-room awareness.

Context:
- Current State: Name tags exist; spatial awareness missing

Requirements to specify:
1. Distance indicators on name tags (10 Hz updates; color-coded thresholds)
2. Optional tabletop minimap overlay anchored to Virtual Table
3. Basic proximity warnings (visual/audio), non-intrusive
4. Performance safeguards to maintain 90 FPS

Technical constraints:
- Efficient distance calculations (avoid N^2 every frame)
- Lightweight minimap rendering

File locations:
- Modify: Assets/XRMP/Scripts/Network/NetworkPlayer/PlayerNameTag.cs
- New: Assets/XRMP/Scripts/LocalPlayer/TabletopMinimap.cs
- New: Assets/XRMP/Scripts/LocalPlayer/PlayerProximityIndicator.cs
- New: Assets/XRMP/Scripts/LocalPlayer/CoLocatedVisualizationManager.cs
- New prefab: Assets/MRTabletopAssets/Prefabs/UI/TabletopMinimapCanvas.prefab

Deliverables:
- Requirements, Design (algorithms/UI), Tasks
- Performance testing plan

References:
- docs/spec-features-revised.md (Feature #5)
- docs/mvp-action-plan.md (Days 4–6)

Deveoplopment:
- Use Unity-MCP interface when developing in unity editor 
```

---

### 9. Appearance Customization

```
Create a complete spec-workflow for the "appearance-customization" feature documenting current behavior and validation tests.

Context:
- Current State: Implemented (color/material + display name; session-scoped)

Requirements to specify:
1. Local UI for name entry and color/material selection
2. Network synchronization of appearance to all clients
3. Persistence scope (session-only) and reset rules
4. UX guidelines for readability and accessibility

Technical constraints:
- Low bandwidth updates; change events only

File locations:
- Read: Assets/XRMP/Scripts/LocalPlayer/PlayerAppearanceMenu.cs
- Read: Assets/XRMP/Scripts/Network/NetworkPlayer/XRAvatarVisuals.cs
- Read: Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs

Deliverables:
- Requirements, Design, Tasks
- QA checklist and test matrix

References:
- docs/spec-features-revised.md (Feature #6)
- MultiUser-VR-MVP-Guide.md

Deveoplopment:
- Use Unity-MCP interface when developing in unity editor 
```

---

## Using These Prompts

1. **Load the Spec Workflow Guide first:**
   ```
   Use MCP tool: spec-workflow-guide
   ```

2. **Provide the appropriate Agent Prompt** to generate requirements, design, and tasks documents

3. **Create spec directories** under `.spec-workflow/specs/`:
   - `.spec-workflow/specs/lan-connection-manager/`
   - `.spec-workflow/specs/spatial-awareness-system/`
   - `.spec-workflow/specs/guardian-integration/`

4. **Follow the spec workflow phases:**
   - Phase 1: Requirements → Review & Approve
   - Phase 2: Design → Review & Approve
   - Phase 3: Tasks → Review & Approve
   - Phase 4: Implementation

5. **Reference the detailed implementation plans:**
   - See `docs/mvp-action-plan.md` for day-by-day implementation details
   - See `docs/spec-features-revised.md` for complete feature analysis
   - See `docs/mvp-gap-analysis.md` for priority and risk assessment

---

**For detailed analysis, see:**
- [spec-features-revised.md](spec-features-revised.md) - Complete feature-by-feature analysis
- [mvp-gap-analysis.md](mvp-gap-analysis.md) - Executive summary and roadmap
- [mvp-action-plan.md](mvp-action-plan.md) - Day-by-day implementation guide
