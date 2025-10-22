# Co-Located Multi-User VR System - AI Agent Instructions

## Project Context

This is a **Unity VR project** developing an MVP for **3 Meta Quest 3 headsets sharing the same physical room**. The unique challenge is co-located VR where users occupy the same physical space while immersed in a shared virtual environment—unlike traditional remote multi-user VR.

**Critical Safety Requirement**: Physical collision prevention is the #1 priority. Users cannot see each other in VR but share the same physical space.

## Architecture Overview

### Core Technology Stack
- **Unity 6000.2.3f1** (Unity 6) with URP pipeline
- **Meta XR All-in-One SDK v78.0.0** - Complete VR framework
- **Unity MCP (Model Context Protocol) v0.20.0** - AI-Unity bridge for development
- **Target Platform**: Meta Quest 3 (Android standalone builds)
- **Networking**: Unity Netcode for GameObjects (NOT YET INSTALLED - high priority)

### Meta XR SDK Package Structure
The project uses Meta's modular SDK architecture (all v78.0.0):
- `com.meta.xr.sdk.core` - OVRCameraRig, OVRManager, passthrough, tracking
- `com.meta.xr.sdk.interaction` - Hand/controller input, grabbing, locomotion
- `com.meta.xr.sdk.platform` - **CRITICAL for MVP**: LocalMatchmaking, ColocationController
- `com.meta.xr.mrutilitykit` - **CRITICAL for safety**: MRUK, RoomGuardian, spatial awareness
- `com.meta.xr.sdk.haptics` - Advanced haptics (optional)
- `com.meta.xr.sdk.audio` - Spatial audio (optional)
- `com.meta.xr.simulator` - Editor testing without device

## Development Workflows

### Unity MCP Integration
This project uses **Unity MCP server** for AI-assisted development. Access via:
- **Window > AI Connector (Unity-MCP)** in Unity Editor
- Server binaries in `Library/mcp-server/` (platform-specific)
- All MCP tools available through connected AI clients (Claude, Copilot, Cursor)

**Key MCP Tools for this project**:
```bash
# Component discovery
mcp_unity-mcp_Component_GetAll search:"Meta"
mcp_unity-mcp_Component_GetAll search:"OVR"

# Scene hierarchy inspection
mcp_unity-mcp_Scene_GetHierarchy

# GameObject manipulation
mcp_unity-mcp_GameObject_Find
mcp_unity-mcp_GameObject_Create
mcp_unity-mcp_GameObject_Modify

# Asset management
mcp_unity-mcp_Assets_Find filter:"t:Prefab"
mcp_unity-mcp_Assets_Find filter:"t:Scene"
```

### Building for Quest 3
1. **Build Target**: Android (Meta Quest 3 uses Android OS)
2. **XR Plugin Management**: OpenXR + Meta XR Plugin
3. **Build Location**: `building-block/` folder (workspace root)
4. No build scripts yet - manual builds via Unity Build Settings

### Testing Approach
- **Editor Testing**: Use Meta XR Simulator for single-user testing
- **Device Testing**: Requires 3 physical Quest 3 headsets for colocation validation
- **No automated tests yet** - Test framework installed but no test coverage

## Critical MVP Components (Phase-Ordered)

### Phase 1: Foundation (Weeks 1-2)
**Must implement**:
- `OVRCameraRig` + `OVRManager` setup in main scene
- `FromOVRHandDataSource` / `FromOVRControllerDataSource` for input
- Basic player prefab with tracking

### Phase 2: Networking & Colocation (Weeks 3-4) 
**Must implement** (highest complexity):
```csharp
// Meta Platform SDK components
LocalMatchmaking              // Auto-discover 3 Quest devices on LAN
ColocationController          // Manage shared physical space session
SharedSpatialAnchorCore       // THE KEY: synchronize coordinate systems
AlignCameraToAnchor          // Position each user's camera relative to shared anchor
ColocationSessionEventHandler // Connection status
```

**Critical workflow**: Host creates spatial anchor → Other users discover via LocalMatchmaking → All align to same anchor → Physical coordinates synchronized

**Missing dependency**: Install `com.unity.netcode.gameobjects` for gameplay networking (position sync, object states, RPCs). Meta Platform SDK handles discovery/colocation only.

### Phase 3: Safety Systems (Weeks 5-6)
**Must implement** (safety-critical):
```csharp
// Meta MR Utility Kit components  
MRUK                    // Main MR manager for room understanding
MRUKRoom                // Room boundary and layout data
RoomGuardian            // Boundary enforcement - prevents users from colliding
OVRPassthroughLayer     // Emergency "see-through" mode
```

**Safety pattern**: Continuously monitor all 3 `OVRCameraRig` positions → Calculate inter-user distances → Activate warnings/passthrough when users approach collision threshold.

## Project-Specific Conventions

### Scene Structure
- **Primary Scene**: `Assets/Scenes/SampleScene.unity` (default Unity scene, not yet configured)
- **Expected structure**: No multiplayer scenes exist yet - MVP development just starting

### Namespace Patterns for Meta XR
When searching for components, use these namespace prefixes:
- `Meta.XR.MultiplayerBlocks.*` - Colocation, matchmaking, multiplayer
- `Meta.XR.MRUtilityKit.*` - Room understanding, safety, spatial awareness
- `Meta.XR.BuildingBlocks.*` - Pre-built VR systems
- `Oculus.Interaction.*` - Input, hand tracking, locomotion
- `OVR*` - Core VR functionality (camera, manager, tracking)

### Asset Organization (Planned)
```
Assets/
├── Scripts/
│   ├── Networking/        # Unity Netcode components (not yet created)
│   ├── Safety/            # Collision prevention system (not yet created)
│   ├── VR/                # Player controllers, avatars (not yet created)
├── Prefabs/
│   ├── Player/            # Player rig with Meta XR components (not yet created)
├── Scenes/
│   └── SampleScene.unity  # Default scene - needs VR configuration
```

## Integration Points

### Meta XR Building Blocks
Access via **Window > Meta XR > Tools > Building Blocks**:
- Add pre-configured prefabs for common VR features
- Recommended for MVP: Spatial Anchor Core, Local Matchmaking, Colocation, Room Mesh Controller

### Unity Multiplayer Center
- Package installed: `com.unity.multiplayer.center` v1.0.0
- Use to add Unity Netcode for GameObjects package
- Access via **Window > Multiplayer > Multiplayer Center**

### OpenUPM Registry
Custom package source configured for Unity MCP and dependencies:
- Registry: `https://package.openupm.com`
- See `Packages/manifest.json` for scopes

## Known Constraints

### Project Path Limitation
**CRITICAL**: Unity MCP requires project path **without spaces**
- ✅ Current: `c:\github\multi-user-vr\building-block`
- ❌ Would fail: `c:\my projects\building-block`

### Networking Gap
**Unity Netcode for GameObjects NOT installed yet** - This is a high-priority blocker for Phase 2 networking implementation. Meta Platform SDK provides matchmaking/colocation but NOT gameplay synchronization.

### No Custom Code Yet
The project has ZERO custom VR/networking scripts. All current C# files are from:
- Unity tutorial boilerplate (`Assets/TutorialInfo/`)
- Unity MCP installer (`Assets/com.IvanMurzak/AI Game Dev Installer/`)

## Development Priorities (MVP Roadmap)

**Week 1-2**: VR Foundation
1. Configure `SampleScene.unity` with `OVRCameraRig`
2. Test single-user VR in Meta XR Simulator
3. Create basic player prefab with hand tracking

**Week 3-4**: Networking & Colocation
1. Install Unity Netcode for GameObjects via Multiplayer Center
2. Implement `LocalMatchmaking` + `ColocationController`
3. Set up `SharedSpatialAnchorCore` workflow
4. Test with 2-3 physical Quest 3 headsets

**Week 5-6**: Safety Systems
1. Integrate `MRUK` + `RoomGuardian`
2. Build collision detection using `OVRCameraRig` positions
3. Implement visual/haptic warnings
4. Add emergency `OVRPassthroughLayer` activation

**Week 7-8**: Polish & Validation
1. Performance optimization
2. Use case demonstrations
3. Safety testing with 3 concurrent users

## Quick Reference: Essential Meta XR Components

```csharp
// VR Core (Essential for all scenes)
OVRCameraRig                     // Main VR camera with tracked controllers
OVRManager                       // Core VR system manager

// Colocation (Critical for multi-user)
LocalMatchmaking                 // Discover nearby Quest devices
ColocationController             // Manage shared space session
SharedSpatialAnchorCore          // Shared coordinate system anchor
AlignCameraToAnchor             // Align user to shared anchor

// Safety (Critical for collision prevention)
MRUK                            // Mixed Reality Utility Kit manager
MRUKRoom                        // Room boundaries and layout
RoomGuardian                    // Safety boundary enforcement
OVRPassthroughLayer             // See real world through headset

// Input (Essential for interaction)
FromOVRHandDataSource           // Hand tracking input
FromOVRControllerDataSource     // Controller input
HandGrabInteractor              // Grab with hands
RayInteractor                   // Point and select
```

## When Writing Code

### Preferred Patterns
- Use Meta XR SDK components over custom implementations where available
- Always access Unity API from main thread when using MCP tools
- Leverage Meta XR Building Blocks for rapid prototyping

### Safety-First Design
- Every networking feature must consider physical collision scenarios
- Validate all 3 users' positions continuously during runtime
- Graceful degradation: system should fail safely, activating passthrough mode

### Component Discovery
Use MCP tools to discover available components before implementing custom solutions:
```bash
mcp_unity-mcp_Component_GetAll search:"Grab"      # Find grabbing components
mcp_unity-mcp_Component_GetAll search:"Teleport"  # Find locomotion components
```

Over 300+ Meta XR components available - search before building from scratch.

## Documentation References
- **MVP Requirements**: `MVP_DEVELOPMENT_PROMPT.md` (primary design document)
- **Meta XR Docs**: https://developer.oculus.com/documentation/unity/
- **Unity MCP Tools**: Use MCP component discovery tools to explore SDK
- **Project README**: Located at repository root (outside building-block workspace)

---

**Remember**: This is a SAFETY-CRITICAL application where users share physical space while immersed in VR. Collision prevention is not optional—it's the core technical challenge of co-located VR.
