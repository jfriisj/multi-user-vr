# Spec Features (MVP, Template‑Aligned) - REVISED

This file enumerates the minimal feature specs required to deliver the MVP using Unity's MR Multiplayer Tabletop template (`mr-multiplayer/`) and our guide in `MultiUser-VR-MVP-Guide.md`.

**REVISION DATE:** October 20, 2025
**ANALYSIS METHOD:** Unity-MCP tools analysis of current implementation

Scope: focus on co‑located local multiplayer (same room), player visualization, and basic NGO/XRI integration. Post‑MVP items are excluded here.

Read first: MultiUser-VR-MVP-Guide.md, docs/README.md, .spec-workflow/steering/product.md, tech.md, structure.md

---

## Implementation Status Summary

**Current Scene:** `Assets/Scenes/SampleScene.unity` (derived from template)
**Network Manager:** `Network Manager XR Multiplayer` (NetworkManagerXRMultiplayer.cs)
**Game Manager:** `XRI_Network_Game_Manager` (XRINetworkGameManager.cs)
**Lobby System:** LobbyManager.cs with UGS Lobby/Relay integration
**Player System:** XRINetworkPlayer.cs with avatar sync via ClientNetworkTransform

### ✅ Implemented Features (from template)
- UGS Authentication (AuthenticationManager)
- Lobby creation/join via UGS Lobby service (LobbyUI, LobbyManager)
- Room code-based joining
- Quick join functionality
- Player list UI (PlayerListUI) with role indicators
- Player name tags (PlayerNameTag) with LOD system
- Avatar color/name customization (PlayerAppearanceMenu, XRAvatarVisuals)
- Voice chat integration (VoiceChatManager, Vivox)
- Networked objects with ownership (NetworkBaseInteractable, NetworkPhysicsInteractable)
- XR Hands integration (XRHandPoseReplicator, JointBasedHand)
- Basic player notification system (PlayerHudNotification)

### ❌ Missing/Incomplete Features per MVP Guide

---

## Core MVP feature specs

### 1) session-management-ui ⚠️ PARTIALLY IMPLEMENTED
**Status:** Cloud-based (UGS) working, LAN-only mode missing

**What exists:**
- ✅ LobbyUI with room creation, join by code, quick join
- ✅ PlayerListUI showing connected players with Host/Client indicators
- ✅ Connection status notifications
- ✅ Disconnect functionality via XRINetworkGameManager
- ✅ Player count display (e.g., "3/4 players")
- ✅ Room code display and copy functionality

**What's MISSING to meet MVP:**
- ❌ **Manual IP entry for direct LAN connection** (currently requires UGS Relay)
- ❌ **LAN-only mode without Unity Services** (template assumes UGS)
- ❌ **Host/Join toggle for pure Unity Transport** (no Relay fallback)
- ❌ **Connection status UI for LAN discovery**
- ⚠️ **Leave/Disconnect button in active session UI** (exists but needs prominent placement)

**Files involved:**
- `Assets/MRTabletopAssets/Scripts/UI/LobbyList/LobbyUI.cs`
- `Assets/MRTabletopAssets/Scripts/UI/PlayerList/PlayerListUI.cs`
- `Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs`
- `Assets/XRMP/Scripts/Network/NetworkManagers/LobbyManager.cs`

**Required Implementation:**
- Add UI for direct IP address input (e.g., "192.168.1.10:7777")
- Create LAN-only connection path bypassing LobbyManager
- Add Unity Transport configuration for direct connections
- Update NetworkManagerXRMultiplayer to support both modes
- Add connection mode selector: "Cloud" vs "LAN Direct"

**Priority:** 🔴 **CRITICAL** - MVP cannot function without internet currently

---

### 2) avatar-synchronization ✅ IMPLEMENTED
**Status:** Fully working via template

**Implementation:**
- ✅ XRINetworkPlayer spawns per client (NetworkObject)
- ✅ Head/hands synced via ClientNetworkTransform (~20Hz)
- ✅ XRAvatarIK handles IK calculations
- ✅ XRAvatarVisuals manages appearance (color, materials)
- ✅ XRHandPoseReplicator syncs hand poses
- ✅ Owner-write authority model
- ✅ Interpolation and smoothing working

**Files involved:**
- `Assets/XRMP/Scripts/Network/NetworkPlayer/XRINetworkPlayer.cs`
- `Assets/XRMP/Scripts/Network/NetworkPlayer/XRAvatarIK.cs`
- `Assets/XRMP/Scripts/Network/NetworkPlayer/XRAvatarVisuals.cs`
- `Assets/XRMP/Scripts/Network/NetworkPlayer/XRHandPoseReplicator.cs`
- `Assets/XRMP/Scripts/Network/NetworkUtilityComponents/ClientNetworkTransform.cs`
- `Assets/XRMP/Prefabs/PlayerPrefabs/XRI_Network_Player_Avatar.prefab`

**Meets MVP requirements:** ✅ **COMPLETE**

---

### 3) networked-interactables ✅ IMPLEMENTED
**Status:** Fully working via template

**Implementation:**
- ✅ NetworkBaseInteractable base class
- ✅ NetworkPhysicsInteractable for physics objects
- ✅ ClaimableNetworkBehaviour for ownership transfer
- ✅ NetworkSocketInteractor for socket interactions
- ✅ Object pose sync via CustomNetworkTransform
- ✅ Idle optimization present
- ✅ ~30Hz sync when grabbed

**Files involved:**
- `Assets/XRMP/Scripts/Network/NetworkInteractions/NetworkBaseInteractable.cs`
- `Assets/XRMP/Scripts/Network/NetworkInteractions/NetworkPhysicsInteractable.cs`
- `Assets/XRMP/Scripts/Network/NetworkInteractions/NetworkSocketInteractor.cs`
- `Assets/MRTabletopAssets/Scripts/ClaimableNetworkBehaviour.cs`
- `Assets/MRTabletopAssets/Scripts/ClaimableNetworkTransform.cs`
- `Assets/MRTabletopAssets/Scripts/CustomNetworkTransform.cs`

**Meets MVP requirements:** ✅ **COMPLETE**

---

### 4) lan-join-and-manual-ip ❌ NOT IMPLEMENTED
**Status:** Critical gap for MVP

**What exists:**
- ✅ Unity Transport configured in NetworkManager
- ✅ Works over LAN when using UGS Relay allocation

**What's MISSING:**
- ❌ **Direct Unity Transport over LAN without UGS** (template requires cloud services)
- ❌ **Manual IP address input field**
- ❌ **Host discovery on local network**
- ❌ **Fallback connection when internet unavailable**
- ❌ **Documentation for LAN-only setup**
- ❌ **No LANConnectionManager or equivalent**

**Required Implementation:**
1. Create `LANConnectionManager.cs` for direct connections
2. Add UI panel with:
   - IP address input field
   - Port input field (default 7777)
   - "Host" button (starts server)
   - "Join" button (connects as client)
   - Connection status display
3. Configure UnityTransport for local connections:
   ```csharp
   transport.ConnectionData.Address = ipAddress;
   transport.ConnectionData.Port = port;
   transport.ConnectionData.ServerListenAddress = "0.0.0.0";
   ```
4. Update XRINetworkGameManager to support dual connection modes
5. Add mode selector in UI: "Internet (Unity Services)" vs "Local Network (Direct)"

**Priority:** 🔴 **CRITICAL** - Explicitly required by MVP guide

---

### 5) local-co-located-visualization ⚠️ PARTIALLY IMPLEMENTED
**Status:** Basic name tags exist, spatial awareness missing

**What exists:**
- ✅ PlayerNameTag with color-coded avatars
- ✅ LOD system (min 1m, max 3m distance thresholds)
- ✅ Name text scales with distance
- ✅ Voice chat visualization particles
- ✅ Name tags show player name and initials
- ✅ Mute/unmute indicators

**What's MISSING per MVP:**
- ❌ **Distance indicators between players** (e.g., "2.5m away")
- ❌ **Spatial awareness overlay/minimap** showing relative positions
- ❌ **Floor-space visualization** (tabletop boundary, play area markers)
- ❌ **Collision proximity warnings** for co-located users
- ❌ **Guardian/boundary integration** (mentioned in MVP, not implemented)
- ❌ **"Tabletop map" overlay anchored to play area**

**Files involved:**
- `Assets/XRMP/Scripts/Network/NetworkPlayer/PlayerNameTag.cs`
- `Assets/MRTabletopAssets/Scripts/Player/OfflinePlayerAvatar.cs`

**Required Implementation:**
1. Create `PlayerProximityIndicator.cs`:
   - Calculate distances between local player and others
   - Display distance on name tag or separate UI
   - Color-code by proximity (green > 2m, yellow 1-2m, red < 1m)
   
2. Create `TabletopMinimap.cs`:
   - Top-down view of play area
   - Show player positions relative to table
   - Update in real-time
   - Anchored to Virtual Table object
   
3. Create `CoLocatedVisualizationManager.cs`:
   - Coordinate distance indicators
   - Manage minimap visibility
   - Handle spatial awareness features
   
4. Guardian integration:
   - Access Meta Guardian boundaries (if available in SDK)
   - Share boundary data between clients
   - Visualize overlapping play areas
   - Warn when players get too close physically

**Priority:** 🟡 **HIGH** - Important for co-located safety and awareness

---

### 6) appearance-customization ✅ IMPLEMENTED
**Status:** Fully working

**Implementation:**
- ✅ PlayerAppearanceMenu for color/name selection
- ✅ XRINetworkGameManager.LocalPlayerColor/LocalPlayerName bindable variables
- ✅ NetworkVariables sync appearance to all clients
- ✅ Random color selection available
- ✅ Persists for session duration
- ✅ UI shows current color/name
- ✅ Input field for custom names

**Files involved:**
- `Assets/XRMP/Scripts/LocalPlayer/PlayerAppearanceMenu.cs`
- `Assets/XRMP/Scripts/Network/NetworkPlayer/XRAvatarVisuals.cs`
- `Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs`

**Meets MVP requirements:** ✅ **COMPLETE**

---

## Critical Gaps Analysis

### 🔴 High Priority (Blocking MVP)
1. **LAN-only connection mode** - Users cannot connect without internet/UGS (Feature #4)
2. **Manual IP entry** - No direct device-to-device connection option (Feature #4)
3. **Session management UI refinement** - Missing prominent disconnect, needs LAN toggle (Feature #1)

### 🟡 Medium Priority (Important for MVP)
4. **Distance indicators** - Limited awareness of other players' positions (Feature #5)
5. **Minimap/spatial overlay** - No tabletop-relative positioning aid (Feature #5)
6. **Proximity warnings** - Safety feature for co-located users (Feature #5)

### 🟢 Low Priority (Post-MVP acceptable)
7. Guardian integration - Full boundary sharing system
8. Voice chat improvements - Already functional with Vivox
9. Advanced appearance options - Basic customization works
10. Research instrumentation hooks - Out of MVP scope

---

## Implementation Roadmap to MVP

### Phase 1: LAN Connectivity 🔴 (2-3 days)
**Critical for MVP**

- [ ] Create `LANConnectionManager.cs` component
  - Direct UnityTransport configuration
  - Host/Client connection logic
  - Error handling for connection failures
  
- [ ] Add LAN connection UI panel
  - IP address input field
  - Port input field
  - Host/Join buttons
  - Connection status display
  - Add to existing LobbyUI or create separate panel
  
- [ ] Update `XRINetworkGameManager.cs`
  - Add connection mode enum (Cloud/LAN)
  - Dual connection path support
  - Fallback to LAN if cloud unavailable
  
- [ ] Configure `UnityTransport` for direct connections
  - Test with 2 Quest 3 devices on same WiFi
  - Verify no internet required
  
- [ ] Update documentation
  - Add LAN setup guide
  - Include IP address discovery instructions
  - Document port requirements

**Success Criteria:**
- Two Quest 3 devices can connect with WiFi but no internet
- Manual IP entry works reliably
- Host can see Join IP address displayed
- Connection persists after internet disconnect

---

### Phase 2: Spatial Awareness 🟡 (2-3 days)
**Important for co-located safety**

- [ ] Create `PlayerProximityIndicator.cs`
  - Calculate distances to all remote players
  - Update every frame or on timer
  - Color-code based on thresholds
  
- [ ] Update `PlayerNameTag.cs`
  - Add distance text display
  - Toggle distance visibility based on settings
  - Format: "2.5m" or "Close"
  
- [ ] Create `TabletopMinimap.cs` prefab
  - Canvas anchored to Virtual Table
  - Top-down orthographic view
  - Player position icons with colors
  - Toggle visibility with button
  
- [ ] Create `CoLocatedVisualizationManager.cs`
  - Central coordinator for spatial features
  - Settings for enabling/disabling features
  - Proximity warning thresholds
  
- [ ] Add proximity warnings
  - Audio/visual alert when < 1m apart
  - Haptic feedback on controllers
  - Optional: floor projections showing "danger zones"

**Success Criteria:**
- Distance to each player visible on name tag
- Minimap shows accurate relative positions
- Warnings activate when players too close
- No performance impact on 90 FPS target

---

### Phase 3: Safety Integration 🟢 (1-2 days)
**Optional enhancement for MVP**

- [ ] Investigate Meta Guardian API access
  - Check if boundary data accessible
  - Determine if shareable over network
  
- [ ] Implement boundary visualization (if feasible)
  - NetworkVariable to share boundary points
  - Render boundaries for remote players
  - Highlight overlapping areas
  
- [ ] Add collision warnings
  - Detect when guardian boundaries overlap
  - Visual warning in headset
  - Audio cue for imminent collision
  
- [ ] Test co-located scenarios
  - 2-3 players in same room
  - Verify warnings work accurately
  - Ensure no false positives

**Success Criteria:**
- Guardian boundaries visible (if API available)
- Collision warnings accurate within 30cm
- No user confusion from excessive warnings

---

### Phase 4: Polish & Testing 🟢 (1-2 days)
**Final validation**

- [ ] UI/UX improvements
  - Prominent disconnect button in active session
  - Clear connection mode indicator
  - Better error messages
  - Loading indicators
  
- [ ] Edge case testing
  - Disconnect and reconnect
  - Host migration (if supported)
  - Network interruption recovery
  - Mixed cloud/LAN players (if applicable)
  
- [ ] Performance validation
  - 90 FPS with 3 players
  - Network bandwidth monitoring
  - Avatar sync latency measurement
  - Memory usage profiling
  
- [ ] Documentation updates
  - Update spec-features.md with completion status
  - Create user guide for LAN connections
  - Document known limitations
  - Add troubleshooting section

**Success Criteria:**
- All MVP features functional
- 90 FPS maintained on Quest 3
- <20ms avatar sync latency
- Documentation complete and accurate

---

## Technical Notes

### Current Architecture
**Scene:** `Assets/Scenes/SampleScene.unity`
**Network Stack:** Netcode for GameObjects 2.x + Unity Transport
**XR Stack:** XR Interaction Toolkit 3.x, XR Hands, OpenXR, Meta OpenXR
**Services:** UGS Authentication, Lobby, Relay, Vivox (voice)

### Key Scripts Locations
| Component | Script Path |
|-----------|------------|
| Main Game Manager | `Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs` |
| Network Manager | `Assets/XRMP/Scripts/Network/NetworkManagers/NetworkManagerXRMultiplayer.cs` |
| Lobby Management | `Assets/XRMP/Scripts/Network/NetworkManagers/LobbyManager.cs` |
| Player Avatar | `Assets/XRMP/Scripts/Network/NetworkPlayer/XRINetworkPlayer.cs` |
| Session UI | `Assets/MRTabletopAssets/Scripts/UI/LobbyList/LobbyUI.cs` |
| Player List UI | `Assets/MRTabletopAssets/Scripts/UI/PlayerList/PlayerListUI.cs` |
| Name Tags | `Assets/XRMP/Scripts/Network/NetworkPlayer/PlayerNameTag.cs` |
| Appearance | `Assets/XRMP/Scripts/LocalPlayer/PlayerAppearanceMenu.cs` |

### Scene Hierarchy (Relevant Objects)
```
SampleScene
├── XRI_Network_Game_Manager (XRINetworkGameManager)
├── Network Manager XR Multiplayer (NetworkManagerXRMultiplayer, UnityTransport)
├── MRInteractionSetup
│   └── XR Origin (XR Rig)
│       ├── Camera Offset/Main Camera
│       ├── Left/Right Controllers
│       ├── Left/Right Hand
│       ├── Offline_Player_Avatar (local avatar preview)
│       └── Local Player Canvas (PlayerAppearanceMenu)
├── Virtual Table
│   ├── NetworkTableTopManager
│   └── TableSystem
└── UI
    └── World Space Canvas
        ├── Coaching UI
        ├── PlayerNameTags (Container)
        └── Table UI
```

### Performance Targets
- **Frame Rate:** 90 FPS (Quest 3 native)
- **Avatar Sync:** <20ms perceived latency
- **Pose Updates:** ~20Hz (head/hands)
- **Object Sync:** ~30Hz when grabbed, <5Hz idle
- **Network Bandwidth:** <100 KB/s per player typical
- **Memory:** <4GB total (Quest 3 has 8GB)

### Testing Recommendations
- **Editor Testing:** Use Multiplayer Play Mode or ParrelSync
- **Device Testing:** 3x Meta Quest 3 on same WiFi network
- **LAN Testing:** Disable internet, test direct connections
- **Performance:** Use Unity Profiler + Meta Quest Developer Hub

---

## Spec Implementation Status

| Spec ID | Status | Priority | Days Estimate |
|---------|--------|----------|---------------|
| session-management-ui | ⚠️ Partial | 🔴 Critical | 2-3 |
| avatar-synchronization | ✅ Complete | ✅ Done | 0 |
| networked-interactables | ✅ Complete | ✅ Done | 0 |
| **lan-join-and-manual-ip** | ❌ **Missing** | 🔴 **Critical** | **2-3** |
| local-co-located-visualization | ⚠️ Partial | 🟡 High | 2-3 |
| appearance-customization | ✅ Complete | ✅ Done | 0 |

### NEW SPECS NEEDED
| Spec ID | Description | Priority | Days Estimate |
|---------|-------------|----------|---------------|
| **lan-connection-manager** | Direct IP connection without UGS | 🔴 Critical | 2-3 |
| **spatial-awareness-system** | Distance indicators and minimap | 🟡 High | 2-3 |
| guardian-integration | Safety boundary sharing | 🟢 Low | 1-2 |

### Total Estimated Effort
- **Critical (Blocking MVP):** 2-3 days
- **High Priority (Important):** 2-3 days
- **Total to MVP:** 4-6 days
- **With Polish/Testing:** 5-8 days

---

## Spec IDs for .spec-workflow/specs

### Existing (to revise)
- `session-management-ui` - Update with LAN requirements
- ~~`avatar-synchronization`~~ - Complete, archive
- ~~`networked-interactables`~~ - Complete, archive
- `lan-join-and-manual-ip` - **Create detailed spec (CRITICAL)**
- `local-co-located-visualization` - Update with spatial awareness requirements
- ~~`appearance-customization`~~ - Complete, archive

### New Specs to Create
- `lan-connection-manager` - Direct IP networking
- `spatial-awareness-system` - Distance indicators and minimap
- `guardian-integration` - Boundary sharing (optional)

---

## Conclusion

**MVP Status:** ~60% complete (3 of 6 features fully working)

**Critical Blockers:**
1. LAN-only connection mode (feature #4)
2. Manual IP entry UI (feature #4)

**Key Insight:** The Unity template provides excellent cloud-based multiplayer infrastructure, but **lacks the local network direct connection capability** explicitly required by the MVP guide. This is the #1 priority to address.

**Recommendation:** Focus Phase 1 (LAN Connectivity) immediately, as it's a hard requirement for MVP delivery. Phase 2 (Spatial Awareness) can follow once basic connectivity works. Phase 3 (Guardian) is optional enhancement.

**Next Steps:**
1. Create detailed spec for `lan-join-and-manual-ip` feature
2. Begin implementation of LANConnectionManager
3. Test LAN connectivity with 2 Quest 3 devices
4. Update this document with progress

---

**Analysis Performed By:** Unity-MCP Tools
**Date:** October 20, 2025
**Analyst:** GitHub Copilot
**Confidence Level:** High (direct Unity project inspection)
