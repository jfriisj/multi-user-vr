# 🧪 Testing Multi-User VR with One Headset

## **Overview**
You can thoroughly test multi-user functionality with just one VR headset using several approaches. I've added testing components to help you validate the multi-user system.

---

## **Method 1: Unity Editor + VR Headset** ⭐ **(Recommended)**

### **Setup:**
1. **Unity Editor** acts as desktop client (keyboard/mouse controls)
2. **VR Headset** acts as VR host/client
3. Both connect over local network

### **How to Test:**

#### **Option A: Editor as Client (Easier)**
1. **Build to VR headset** first
2. **VR Headset**: Start the app → Click "Start Host"  
3. **Unity Editor**: Press Play → Click "Start as Client (Editor)"
4. **Result**: See desktop player in VR, VR player in Editor

#### **Option B: Editor as Host**  
1. **Unity Editor**: Press Play → Click "Start as Host (Editor)"
2. **Build to VR headset**
3. **VR Headset**: Start app → Click "Join Session"
4. **Result**: Both connected, can test interactions

### **Desktop Controls (In Editor):**
- **WASD** - Move around
- **Mouse** - Look around  
- **Q/E** - Move up/down
- **Tab** - Switch between left/right hand
- **Space** - Grab/release objects
- **ESC** - Release mouse cursor

---

## **Method 2: Build Standalone Versions**

### **Setup:**
Build the same project twice to test on same PC:

1. **Build #1**: `MultiUserVR_Host.exe`
2. **Build #2**: `MultiUserVR_Client.exe`
3. Run both simultaneously on your PC
4. One acts as host, other as client

### **Steps:**
```
1. File → Build Settings → Build (Name: MultiUserVR_Host)
2. Copy build folder to MultiUserVR_Client
3. Run MultiUserVR_Host.exe → Start Host
4. Run MultiUserVR_Client.exe → Join Session  
5. Both windows show different perspectives
```

---

## **Method 3: Bot Simulation** 🤖

### **AI Bots for Testing:**
I've added AI bots that simulate other players automatically.

### **How to Use:**
1. **Unity Editor**: Press Play
2. **In Testing UI**: Click "Start Bot Simulation"  
3. **Result**: AI bots appear and move around, test interactions with them

### **Bot Features:**
- ✅ Simulate player movement
- ✅ Random movement patterns  
- ✅ Visual representation (magenta capsules)
- ✅ Network synchronization testing
- ⚠️ *Note: Bots don't grab objects yet (can be added)*

---

## **Method 4: Mobile Device Testing**

### **Use Phone/Tablet as Extra Client:**
1. **Build for Android** (same project)
2. **Install on phone/tablet**  
3. **VR Headset**: Start as host
4. **Mobile Device**: Join as client (touch controls)

### **Mobile Controls:**
- Touch screen for basic movement
- Test object synchronization
- Observe VR player from mobile perspective

---

## **Method 5: Network Debugging**

### **Using Built-in Network Tools:**

#### **Netcode Profiler:**
```
Window → Analysis → Profiler
Add → Networking → Netcode for GameObjects
```
**Shows:** Bandwidth, player sync, object updates

#### **Network Statistics:**
The testing UI shows real-time:
- Connection status (Host/Client/Server)
- Connected player count  
- Network role verification

---

## **🎯 What to Test & Verify**

### **✅ Basic Connectivity:**
- [ ] Host starts successfully
- [ ] Client connects to host
- [ ] Player count updates correctly
- [ ] Debug UI shows proper roles

### **✅ Player Synchronization:**
- [ ] See other player's head movement  
- [ ] See other player's hand movement
- [ ] Movement is smooth (not jerky)
- [ ] Position updates in real-time

### **✅ Object Interactions:**
- [ ] Grab object on one client
- [ ] Object turns yellow (grabbed state)
- [ ] Other client sees object move
- [ ] Release works correctly
- [ ] Only one person can grab at time

### **✅ Network Edge Cases:**
- [ ] Player disconnects gracefully
- [ ] Object ownership transfers correctly  
- [ ] No duplicate objects appear
- [ ] Performance stays smooth with multiple users

---

## **🐛 Troubleshooting**

### **Connection Issues:**
```
Problem: "Failed to connect"
Solution: Check firewall, ensure same WiFi network
```

### **Objects Not Syncing:**
```  
Problem: Objects don't move for other players
Solution: Ensure NetworkedInteractable component is attached
```

### **Poor Performance:**
```
Problem: Lag or stuttering
Solution: Reduce sync rate in VRPlayerSync (20fps → 10fps)
```

### **Desktop Controls Not Working:**
```
Problem: WASD doesn't work in editor
Solution: Click in Scene view first to focus
```

---

## **📊 Performance Testing**

### **Network Bandwidth:**
- **Expected**: ~2KB/sec per player  
- **Monitor**: Using Unity Profiler → Networking
- **Warning Signs**: >10KB/sec indicates issues

### **Frame Rate Impact:**
- **Expected**: No VR frame rate impact
- **Test**: Compare solo vs multi-user FPS
- **Target**: Maintain 72fps+ on Quest 3

---

## **🚀 Ready to Test!**

### **Quick Start Checklist:**
1. ✅ **Scene Setup**: MultiUserVRSetup + MultiUserTester added
2. ✅ **Testing UI**: Shows in top-left during play
3. ✅ **Desktop Controls**: WASD + Mouse + Space for grab
4. ✅ **Build Ready**: Project configured for Quest 3

### **Recommended Testing Flow:**
```
1. Test in Editor first (Method 1)
2. Build to VR headset  
3. Test Editor + VR connection
4. Verify all interactions work
5. Test edge cases (disconnect/reconnect)
```

### **Success Criteria:**
When you see:
- ✅ Two players connected
- ✅ Smooth head/hand tracking sync  
- ✅ Shared object interactions
- ✅ No lag or stuttering

**Your multi-user VR system is working perfectly!** 🎉

---

## **Next Steps After Testing:**

1. **Add More Features**: Voice chat, custom avatars, more interactions
2. **Optimize Performance**: Reduce network traffic, improve sync  
3. **Scale Up**: Test with real 3-headset setup when available
4. **Deploy**: Build production-ready multi-user VR experience

**Happy Testing!** 🧪🥽