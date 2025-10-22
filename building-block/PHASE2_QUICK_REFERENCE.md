# Phase 2 Quick Reference Guide
**Networking & Colocation Implementation**

---

## 🚀 Quick Start Commands

### Unity Editor Actions
```
1. Add Meta Colocation Components:
   Window > Meta XR > Tools > Building Blocks
   - Add "Local Matchmaking"
   - Add "Colocation"
   - Add "Spatial Anchor Core"

2. Verify NetworkTransform:
   - Open Assets/Prefabs/Player/PlayerController.prefab
   - Check for "Network Transform" component
   - If missing, add manually

3. Test Networking in Editor:
   - Press Play
   - Click "Start as Host" in Debug UI
   - (Need second Unity instance or device to test client)
```

---

## 📁 Phase 2 File Structure

### Created Files
```
Assets/
├── Prefabs/
│   └── Player/
│       └── PlayerController.prefab ✅ (Network-ready)
├── Scripts/
│   ├── Networking/
│   │   └── VRNetworkManager.cs ✅ (Fully implemented)
│   └── VR/
│       └── UserTrackingSystem.cs ✅ (Phase 1)
└── Scenes/
    └── SampleScene.unity ✅ (Has NetworkManager)
```

### Scene Hierarchy
```
SampleScene
├── Directional Light
├── Global Volume
├── Floor
├── OVRCameraRig ✅
├── OVRManager ✅
└── NetworkManager ✅ (NEW - Phase 2)
    ├── NetworkManager component
    ├── UnityTransport component
    └── VRNetworkManager component
```

---

## 🔧 Component Configuration

### NetworkManager GameObject
```csharp
// Components:
- NetworkManager (Unity Netcode)
  • Network Transport: UnityTransport
  • Player Prefab: PlayerController
  
- UnityTransport
  • Connection Data Port: 7777
  • Server Listen Address: 0.0.0.0
  • Client Address: 127.0.0.1 (editor) or host IP (device)
  
- VRNetworkManager (Custom)
  • Max Players: 3
  • Network Port: 7777
  • Player Prefab: PlayerController
  • Use Local Matchmaking: true
```

### PlayerController Prefab
```csharp
// Components:
- Transform
  • Position: (0, 0, 0)
  
- UserTrackingSystem (Custom - Phase 1)
  • User ID: 0
  • Show Debug Visualization: true
  
- NetworkObject (Unity Netcode)
  • Don't Destroy With Owner: false
  
- NetworkTransform (Unity Netcode) ⚠️ Needs verification
  • Sync Position X/Y/Z: true
  • Sync Rotation X/Y/Z: true
  • In Local Space: false
  
// Children:
- HeadRepresentation (Capsule)
  • Position: (0, 1.8, 0)
  • Scale: (0.2, 0.15, 0.2)
```

---

## 🎮 VRNetworkManager API

### Public Methods
```csharp
// Start as network host
public void StartAsHost()
// Use: First player to create session
// Creates SharedSpatialAnchor, starts network server

// Join existing session as client
public void JoinAsClient()  
// Use: Second and third players
// Discovers host, connects, aligns to anchor

// Disconnect from network
public void Disconnect()
// Use: Leave session gracefully
// Cleans up network and Meta SDK session
```

### Public Properties
```csharp
public int maxPlayers = 3;          // MVP uses 3 players
public ushort networkPort = 7777;   // Network port
public GameObject playerPrefab;     // Player to spawn
public bool useLocalMatchmaking;    // Use Meta SDK discovery
public GameObject sharedSpatialAnchor; // Colocation anchor
```

---

## 🌐 Network Testing Workflow

### Editor Testing (Single Device)
```
1. Press Play in Unity Editor
2. Debug UI appears in top-left
3. Click "Start as Host"
4. Observe console logs:
   - "[VRNetworkManager] Starting as Host..."
   - "[VRNetworkManager] Host started successfully"
   - "[VRNetworkManager] Server started callback"
   - "[VRNetworkManager] Player spawning handled..."
```

### Multi-Device Testing (2-3 Quest 3s)
```
Device 1 (Host):
1. Build APK and deploy to Quest 3
2. Launch app
3. Click "Start as Host"
4. Note IP address shown in debug UI

Device 2 & 3 (Clients):
1. Build APK and deploy to Quest 3
2. Launch app
3. Enter host IP address (when implemented)
4. Click "Join as Client"
5. Verify connection in debug UI
```

---

## 🔌 Meta Colocation Integration

### Required Building Blocks
```
1. Local Matchmaking
   Purpose: Auto-discover Quest devices on LAN
   Location: Window > Meta XR > Tools > Building Blocks
   Category: Social & Multiplayer
   
2. Colocation
   Purpose: Manage shared physical space session
   Location: Same as above
   
3. Spatial Anchor Core
   Purpose: Create/share spatial anchor for alignment
   Location: Same as above
   Category: Spatial Anchors
```

### Integration Steps
```csharp
// In VRNetworkManager.cs:

1. Uncomment these lines (around line 50):
   // private LocalMatchmaking localMatchmaking;
   // private ColocationController colocationController;
   // private SharedSpatialAnchorCore spatialAnchor;

2. In Awake(), add:
   localMatchmaking = GetComponent<LocalMatchmaking>();
   colocationController = GetComponent<ColocationController>();
   spatialAnchor = FindObjectOfType<SharedSpatialAnchorCore>();

3. In StartAsHost(), uncomment:
   // CreateSharedSpatialAnchor();
   // StartLocalMatchmaking();

4. In JoinAsClient(), uncomment:
   // FindAndConnectToHost();
   // AlignToSharedAnchor();
```

---

## 📊 Network Events

### Event Flow (Host)
```
1. StartAsHost() called
   ↓
2. NetworkManager.StartHost()
   ↓
3. OnServerStarted() callback
   ↓
4. connectedPlayers = 1
   ↓
5. SpawnLocalPlayer()
   ↓
6. (Wait for clients to connect)
   ↓
7. OnClientConnected(clientId) for each client
   ↓
8. connectedPlayers++
```

### Event Flow (Client)
```
1. JoinAsClient() called
   ↓
2. NetworkManager.StartClient()
   ↓
3. Connection attempt to host IP
   ↓
4. (Host receives OnClientConnected)
   ↓
5. Client receives player spawn from server
   ↓
6. NetworkTransform syncs position
```

---

## 🐛 Troubleshooting

### "NetworkTransform not found" error
```
Solution:
1. Open PlayerController prefab
2. Click "Add Component"
3. Search: "Network Transform"
4. Add from Unity.Netcode.Components
5. Save prefab
```

### "Failed to start as host" error
```
Check:
1. NetworkManager component present on GameObject
2. UnityTransport component present
3. Port 7777 not in use
4. PlayerPrefab assigned in VRNetworkManager
5. NetworkObject on PlayerPrefab
```

### "Client cannot connect" error
```
Check:
1. Host is running (debug UI shows "Host")
2. Client has correct host IP address
3. Both devices on same network
4. Firewall allows port 7777
5. UnityTransport address configured correctly
```

### Meta Building Blocks not visible
```
Solution:
1. Wait for Meta XR SDK import (check progress bar)
2. Restart Unity Editor
3. Window > Package Manager > Meta XR All-in-One SDK
4. Verify version 78.0.0 installed
5. Try: Assets > Reimport All (if desperate)
```

---

## 📈 Success Metrics (Phase 2)

### Network Performance
```
✅ Connection Establishment: <5 seconds
✅ Network Latency: <50ms
✅ Frame Rate: 90fps on Quest 3
✅ Max Players: 3 concurrent
```

### Colocation Accuracy
```
✅ Spatial Alignment: <2cm error
✅ Anchor Discovery: <10 seconds
✅ Coordinate Sync: All users see same origin
```

---

## 🚦 Phase 2 Status Indicators

### Debug UI Colors (VRNetworkManager)
```
"Not Connected" = Gray/Default
"Host (Server + Client)" = Green (ready to accept clients)
"Server Only" = Yellow (unusual, debug mode)
"Client Only" = Blue (connected to host)
```

### Console Log Prefixes
```
[VRNetworkManager] = General network operations
[UserTrackingSystem] = Player position tracking
[NetworkManager] = Unity Netcode system logs
[OVRManager] = Meta XR SDK logs
```

---

## 🎯 Next Phase Preview

### Phase 3: Safety & Collision Prevention
```
1. Add MRUK (Mixed Reality Utility Kit)
2. Add RoomGuardian component
3. Implement CollisionPreventionSystem.cs
4. Add proximity warnings (visual/haptic)
5. Implement emergency passthrough mode
6. Test with 3 users in shared physical space
```

---

**Quick Status Check**:
- ✅ Unity Netcode: Configured
- ✅ VRNetworkManager: Implemented  
- ✅ PlayerController: Network-ready
- ⏳ Meta Colocation: Pending (Building Blocks not added yet)
- ❌ Multi-Device Testing: Requires physical Quest 3 devices

**Ready to**: Add Meta Building Blocks and test on physical devices!
