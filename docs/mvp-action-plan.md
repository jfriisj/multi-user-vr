# MVP Implementation Action Plan

**Date:** October 20, 2025  
**Status:** Ready to Begin  
**Critical Path:** 4-6 days to MVP

---

## 🎯 Mission

Implement **LAN-only connection mode** to meet MVP requirements from MultiUser-VR-MVP-Guide.md.

Current blocker: Template requires UGS cloud services; MVP requires "Unity Transport over LAN by default."

---

## 🔴 CRITICAL: Phase 1 - LAN Connectivity (2-3 days)

### Day 1: LANConnectionManager Core

**Morning: Setup & Architecture**
- [ ] Create `Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionManager.cs`
- [ ] Add connection mode enum: `Cloud`, `LANDirect`
- [ ] Reference UnityTransport component
- [ ] Add methods: `HostLAN()`, `JoinLAN(string ip, ushort port)`

**Code Template:**
```csharp
public class LANConnectionManager : MonoBehaviour
{
    private UnityTransport transport;
    private NetworkManager networkManager;
    
    public void HostLAN(ushort port = 7777)
    {
        transport.ConnectionData.Address = "0.0.0.0";
        transport.ConnectionData.Port = port;
        transport.ConnectionData.ServerListenAddress = "0.0.0.0";
        networkManager.StartHost();
    }
    
    public void JoinLAN(string ipAddress, ushort port = 7777)
    {
        transport.ConnectionData.Address = ipAddress;
        transport.ConnectionData.Port = port;
        networkManager.StartClient();
    }
    
    public string GetLocalIPAddress()
    {
        // Implementation: Get local IP from NetworkInterface
    }
}
```

**Afternoon: Integration**
- [ ] Attach LANConnectionManager to XRI_Network_Game_Manager
- [ ] Update XRINetworkGameManager to reference LANConnectionManager
- [ ] Add connection mode property
- [ ] Test basic host/client in Editor (ParrelSync)

**Success Criteria:**
- Two Editor instances connect via 127.0.0.1
- No UGS services used
- Connection stable

---

### Day 2: LAN UI Implementation

**Morning: UI Design**
- [ ] Create `Assets/MRTabletopAssets/Prefabs/UI/LANConnectionPanel.prefab`
- [ ] Add UI elements:
  - Connection mode toggle (Cloud/LAN)
  - IP address input field (TMP_InputField)
  - Port input field (default 7777)
  - Host button
  - Join button
  - Status text
  - Local IP display (for host)
- [ ] Create script `LANConnectionUI.cs`

**Afternoon: UI Integration**
- [ ] Add LANConnectionPanel to LobbyUI or create separate panel
- [ ] Wire up buttons to LANConnectionManager methods
- [ ] Add connection status callbacks
- [ ] Display local IP when hosting
- [ ] Test UI functionality in Editor

**Success Criteria:**
- UI allows toggling between Cloud/LAN modes
- Host button shows local IP
- Join button accepts manual IP
- Status updates visible

---

### Day 3: Device Testing & Refinement

**Morning: Quest 3 Testing**
- [ ] Build APK with LAN functionality
- [ ] Deploy to 2 Quest 3 devices
- [ ] Disconnect from internet
- [ ] Test host on Device A
- [ ] Note Device A's IP address
- [ ] Test join from Device B with manual IP
- [ ] Verify avatar sync works
- [ ] Test object interaction sync

**Afternoon: Bug Fixes & Polish**
- [ ] Fix any connection issues found
- [ ] Add error messages for failed connections
- [ ] Add timeout handling
- [ ] Add reconnect logic
- [ ] Test disconnect/reconnect scenarios
- [ ] Document IP discovery process (settings -> WiFi)

**Success Criteria:**
- Two Quest 3 devices connect without internet
- Avatar sync maintains <20ms latency
- Objects sync correctly
- Connection stable for 5+ minutes

---

## 🟡 OPTIONAL: Phase 2 - Spatial Awareness (2-3 days)

### Day 4: Distance Indicators

**Morning:**
- [ ] Update `PlayerNameTag.cs`
- [ ] Add distance calculation to local player
- [ ] Add TMP_Text field for distance display
- [ ] Format: "2.5m" or "Close"
- [ ] Update every 0.1 seconds (not every frame)

**Afternoon:**
- [ ] Add color coding based on distance
  - Green: >2m
  - Yellow: 1-2m
  - Red: <1m
- [ ] Add toggle for showing/hiding distance
- [ ] Test with 2-3 players

**Code Snippet:**
```csharp
void UpdateDistance()
{
    if (Camera.main == null) return;
    float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
    m_DistanceText.text = $"{distance:F1}m";
    m_DistanceText.color = GetColorForDistance(distance);
}
```

---

### Day 5: Minimap Implementation

**Morning:**
- [ ] Create `TabletopMinimap.cs`
- [ ] Create canvas prefab anchored to Virtual Table
- [ ] Add orthographic camera for top-down view
- [ ] Add player position icons (colored dots)

**Afternoon:**
- [ ] Update icon positions every frame
- [ ] Match colors to player avatars
- [ ] Add toggle button to show/hide
- [ ] Add zoom controls (optional)
- [ ] Test with multiple players

---

### Day 6: Proximity Warnings

**Morning:**
- [ ] Create `ProximityWarningSystem.cs`
- [ ] Check distances between all players
- [ ] Trigger warning when <1m apart
- [ ] Visual: Red border flash
- [ ] Audio: Gentle beep

**Afternoon:**
- [ ] Test in co-located scenario
- [ ] Adjust warning thresholds
- [ ] Add haptic feedback (optional)
- [ ] Ensure no performance impact

---

## 🟢 Phase 3: Polish (1-2 days)

### Day 7-8: Final Validation

**Tasks:**
- [ ] Add prominent disconnect button in active session
- [ ] Improve error messages
- [ ] Add loading indicators
- [ ] Test edge cases:
  - Network interruption
  - Host disconnects
  - Client reconnects
- [ ] Performance profiling:
  - Confirm 90 FPS with 3 players
  - Check network bandwidth
  - Monitor memory usage
- [ ] Update documentation:
  - User guide for LAN connections
  - Troubleshooting section
  - Known limitations
- [ ] Update spec-features.md with completion status

---

## 📋 Daily Checklist Template

**Start of Day:**
- [ ] Review previous day's progress
- [ ] Check for any overnight issues
- [ ] Confirm Quest 3 devices charged
- [ ] Ensure ParrelSync/Multiplayer Play Mode working

**End of Day:**
- [ ] Commit code changes
- [ ] Update progress in spec-features.md
- [ ] Document any blockers
- [ ] Plan next day's tasks

---

## 🚨 Blockers & Mitigation

| Potential Blocker | Mitigation Strategy |
|-------------------|---------------------|
| UnityTransport doesn't work without Relay | Research NetworkManager.Singleton.StartHost/Client with transport config |
| IP discovery difficult on Quest 3 | Add QR code scanning for IP, or manual typing |
| Connection unstable | Add heartbeat system, reconnect logic |
| Performance impact | Profile early, optimize calculations, use object pooling |
| Quest 3 Meta OpenXR conflicts | Test early on device, use Unity 2022.3 LTS+ |

---

## 📊 Success Metrics

### Technical Metrics
- [ ] 90 FPS on Quest 3 with 3 players
- [ ] <20ms avatar sync latency
- [ ] <100ms connection establishment
- [ ] <5% packet loss
- [ ] Connection stable for 30+ minutes

### Feature Metrics
- [ ] LAN connection works without internet
- [ ] Manual IP entry succeeds 95%+ of time
- [ ] Distance indicators accurate to ±0.2m
- [ ] All 6 MVP features functional
- [ ] No critical bugs

### User Experience Metrics
- [ ] User can connect in <60 seconds
- [ ] Clear error messages guide troubleshooting
- [ ] UI intuitive without documentation
- [ ] No user confusion about connection mode

---

## 🎓 Learning Resources

### Unity Documentation
- [Netcode for GameObjects - Getting Started](https://docs-multiplayer.unity3d.com/netcode/current/about/)
- [Unity Transport Manual](https://docs-multiplayer.unity3d.com/transport/current/about/)
- [XR Interaction Toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.0/manual/index.html)

### Code Examples
- Check `LobbyManager.cs` for Relay setup (lines 200-240)
- Check `NetworkManagerXRMultiplayer.cs` for NetworkManager config
- Check `XRINetworkGameManager.cs` for connection state management

### Testing Tools
- ParrelSync for Editor testing
- Unity Multiplayer Play Mode
- Meta Quest Developer Hub for device profiling
- Unity Profiler for performance analysis

---

## 📝 Notes & Observations

### From Unity-MCP Analysis:
- Template heavily relies on UGS (Authentication, Lobby, Relay)
- UnityTransport is present but configured for Relay
- NetworkManager already has StartHost/StartClient methods
- LobbyManager bypasses direct transport configuration
- No existing LANConnectionManager or equivalent

### Key Insight:
The template assumes cloud-first architecture. We need to add LAN path **alongside** cloud, not replace it. Both modes should coexist.

### Architecture Decision:
- Keep existing cloud path intact
- Add new LAN path via LANConnectionManager
- XRINetworkGameManager routes to appropriate manager based on mode
- UI allows user to choose mode at connection time

---

## 🏁 Definition of Done

**MVP is complete when:**
1. ✅ All 6 features from spec-features.md implemented
2. ✅ Two Quest 3 devices connect via LAN without internet
3. ✅ Avatar synchronization works with <20ms latency
4. ✅ Objects sync correctly when grabbed/released
5. ✅ Distance indicators show accurate measurements
6. ✅ Performance maintains 90 FPS target
7. ✅ Documentation updated and accurate
8. ✅ Known issues documented
9. ✅ Code committed and reviewed
10. ✅ Tested by 2+ people successfully

---

**Next Step:** Begin Day 1 - Create LANConnectionManager.cs

**Questions/Issues:** Document in spec-features-revised.md

**Progress Tracking:** Update mvp-gap-analysis.md daily
