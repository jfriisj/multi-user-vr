# Multi-User VR MVP Solution

A clean, simple multi-user VR solution built with **Unity Netcode for GameObjects** that enables up to 3 users to collaborate in VR.

## 🚀 Quick Start

### 1. **Automatic Setup** (Recommended)
The `MultiUserVRSetup` GameObject in the scene will automatically configure everything on startup:

```
Scene Hierarchy:
├── MultiUserVRSetup (Auto-configures networking)
├── XR Origin (XR Rig) (Your VR player)
└── Interactables (Network-synced grabbable objects)
```

### 2. **Manual Setup** (Alternative)
Right-click the `MultiUserVRSetup` component and select **"Setup Multi-User VR"** from the context menu.

## 🎮 How to Use

### **Host a Session:**
1. Build and run on Quest 3 (Device A)
2. The app automatically starts as **Host**
3. Other players can join this session

### **Join a Session:**  
1. Build and run on Quest 3 (Device B & C)
2. In the debug UI, click **"Join Session"**
3. All 3 users now see each other's head and hand positions

### **Interact with Objects:**
- Grab any object in the **Interactables** group
- Other users see the object move in real-time
- Only one person can grab an object at a time

## 🔧 Components Overview

### **MultiUserVRManager**
- Manages network sessions (Host/Client)
- Handles player connections/disconnections
- Shows debug UI for testing

### **VRPlayerSync**  
- Synchronizes head and hand positions
- Creates visual representations of remote players
- Automatically finds VR components in XR Origin

### **NetworkedInteractable**
- Syncs grabbable objects across network
- Handles ownership transfer when grabbing
- Prevents conflicts (only one person can grab at a time)

### **MultiUserVRSetup**
- One-click setup for entire multi-user system
- Automatically configures all necessary components
- Adds NetworkObjects and Rigidbodies where needed

## 🎯 What This MVP Provides

✅ **3-User VR Sessions** - Host + 2 Clients  
✅ **Head & Hand Tracking Sync** - See other users' movements  
✅ **Object Synchronization** - Shared grabbable objects  
✅ **Simple Setup** - One-click configuration  
✅ **Built-in Debug UI** - Easy testing and troubleshooting  

## 🌐 Network Architecture

### **Unity Netcode for GameObjects**
- **Transport**: Unity Transport (UDP)
- **Port**: 7777 (configurable)
- **Architecture**: Client-Server (Host acts as server)
- **Max Players**: 3 (1 Host + 2 Clients)

### **Synchronization:**
- **Player Tracking**: 20 updates/second
- **Object Movement**: 30 updates/second  
- **Grab States**: Event-based (immediate)

## 📱 Building for Quest 3

1. **Build Settings**:
   - Platform: Android
   - Architecture: ARM64
   - Target Device: Quest 3

2. **XR Settings**:
   - Already configured in project
   - OpenXR + Meta XR SDK integration

3. **Network Settings**:
   - Local network discovery (same WiFi)
   - IP address auto-detection

## 🔍 Testing Locally

### **In Unity Editor:**
1. Play the scene
2. Click **"Start Host"** in debug UI
3. Use Unity's "Multiple Displays" to simulate clients

### **On Device:**
1. Build to 3 Quest 3 devices
2. Ensure all are on the same WiFi network
3. Start Host on one device, Join on others

## 🛠️ Extending the MVP

### **Add More Interactables:**
```csharp
// Any GameObject with XRGrabInteractable automatically gets networked
gameObject.AddComponent<XRGrabInteractable>();
// MultiUserVRSetup will auto-configure networking components
```

### **Custom Player Avatars:**
```csharp
// Assign custom prefab to VRPlayerSync.remotePlayerPrefab
public GameObject customAvatarPrefab;
```

### **Voice Chat Integration:**
Ready to integrate with Unity's Vivox or other voice solutions.

## 📋 Dependencies

- ✅ **Unity Netcode for GameObjects** (Already installed)
- ✅ **XR Interaction Toolkit** (Already configured) 
- ✅ **Meta XR SDK** (Already integrated)
- ✅ **Unity Transport** (Included with Netcode)

## 🎉 Ready to Go!

Your multi-user VR MVP is complete! The solution is:

- **Simple**: 4 core scripts, automatic setup
- **Clean**: Extends existing XR template without breaking it  
- **Scalable**: Easy to add features and upgrade to Photon later
- **Production-Ready**: Uses Unity's official networking solution

**Next Steps**: Test with 3 Quest 3 devices and start building your collaborative VR experience! 🚀