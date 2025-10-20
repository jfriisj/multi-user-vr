[← Documentation Home](../README.md) · [← Workflows](../workflows/README.md)

# LAN Connection Testing for Quest 3

Comprehensive testing procedures for direct IP-based Local Area Network (LAN) connections on Meta Quest 3 devices. This guide covers network setup, connection validation, error scenarios, and troubleshooting for the LANConnectionManager system.

## Table of Contents
- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Network Environment Setup](#network-environment-setup)
- [Test Scenarios](#test-scenarios)
- [Testing Procedures](#testing-procedures)
- [Error Scenarios & Validation](#error-scenarios--validation)
- [Troubleshooting Guide](#troubleshooting-guide)
- [Performance Validation](#performance-validation)
- [Test Checklist](#test-checklist)

---

## Overview

### LAN Connection System Components

The LAN connection system consists of three primary components:

1. **LANConnectionManager** (`Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionManager.cs`)
   - Handles direct IP-based connections using Unity Transport
   - Manages host and client connection modes
   - Default port: **7777**
   - Connection timeout: **5 seconds**

2. **ConnectionModeManager** (`Assets/XRMP/Scripts/Network/NetworkManagers/ConnectionModeManager.cs`)
   - Coordinates switching between Cloud (UGS) and LAN Direct modes
   - Maintains connection state across mode transitions
   - Prevents mode switching during active connections

3. **LANConnectionStatus** (`Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionStatus.cs`)
   - Monitors connection health and timeout conditions
   - Provides automatic reconnection with configurable retry logic
   - Classifies errors into actionable categories
   - Default reconnection timeout: **30 seconds**
   - Maximum reconnection attempts: **10**

### Connection Modes

- **Cloud Mode**: Unity Gaming Services (UGS) Lobby + Relay (existing functionality)
- **LAN Direct Mode**: IP-based connections without internet requirement (new functionality)

---

## Prerequisites

### Hardware Requirements

- **3x Meta Quest 3 headsets** (minimum 2 for host-client testing)
- **WiFi 5 (802.11ac) router** or better
- Headsets must be on **same WiFi network** and **same subnet**
- Minimum 2m x 2m physical space per user

### Software Requirements

- Unity 2022.3 LTS or later
- Latest Meta XR SDK
- Project configured for Android/Quest 3 platform
- ADB (Android Debug Bridge) installed for debugging
- Unity Profiler for network monitoring

### Unity Project Setup

1. Open `mr-multiplayer/` project in Unity
2. Verify **Network Manager XR Multiplayer** prefab in scene
3. Confirm prefab has all LAN components:
   - `LANConnectionManager`
   - `ConnectionModeManager`
   - `LANConnectionStatus`
4. Build Settings: Android platform, ARM64 architecture, Quest 3 target

---

## Network Environment Setup

### WiFi Configuration

#### Recommended Router Settings

```
SSID: [Your Network Name]
Security: WPA2-PSK or WPA3
Band: 5GHz (preferred for lower latency)
Channel Width: 40MHz or 80MHz
Channel: Auto or manual (avoid congestion)
IPv4 DHCP: Enabled
Subnet Mask: 255.255.255.0 (Class C)
AP Isolation: DISABLED (critical!)
```

⚠️ **CRITICAL**: AP Isolation (Client Isolation) **MUST BE DISABLED**. This feature prevents devices on the same network from communicating directly, blocking all LAN connections.

#### Verify AP Isolation is Disabled

**Method 1: Ping Test (Before Quest Testing)**

From a computer on the same network:
```bash
# On Windows (PowerShell/CMD)
ping [quest_ip_address]

# On Mac/Linux
ping -c 4 [quest_ip_address]
```

If you receive replies, AP Isolation is disabled. If "Request timed out", enable direct communication in router settings.

**Method 2: Router Admin Interface**

1. Access router at `192.168.1.1` or `192.168.0.1`
2. Navigate to Wireless → Advanced Settings
3. Find "AP Isolation" or "Client Isolation"
4. Set to **OFF** or **DISABLED**
5. Save and reboot router

### Quest 3 Network Setup

#### Finding Quest 3 IP Address

**Method 1: In-Headset Settings**
1. Open Quest Settings
2. Navigate to: **Settings → WiFi**
3. Tap connected network name
4. Note IP address (e.g., `192.168.1.50`)

**Method 2: ADB Command**
```bash
adb shell ip addr show wlan0 | grep "inet "
```

**Method 3: Router DHCP Client List**
- Access router admin interface
- Check DHCP client list for Quest devices
- Note MAC address and assigned IP

#### Static IP Assignment (Recommended for Testing)

Assign static IPs to prevent address changes mid-session:

1. Access router DHCP settings
2. Reserve IP addresses by MAC address:
   - Quest 3 Host: `192.168.1.50`
   - Quest 3 Client 1: `192.168.1.51`
   - Quest 3 Client 2: `192.168.1.52`
3. Save and reboot devices

### Firewall Configuration

#### Windows Firewall (Development Machine)

If testing Unity Editor as client:
```powershell
# Allow Unity through firewall
netsh advfirewall firewall add rule name="Unity Editor LAN" dir=in action=allow program="C:\Program Files\Unity\Hub\Editor\[version]\Editor\Unity.exe" enable=yes
```

#### Quest 3 Firewall

Quest 3 typically allows local network traffic by default. If issues occur:
- Ensure Quest OS is updated to latest version
- Disable VPN apps running on headset
- Check Developer Mode settings don't restrict network access

---

## Test Scenarios

### Scenario 1: Basic Host-Client Connection
**Purpose**: Validate fundamental LAN connection establishment  
**Devices**: 2 Quest 3 headsets (1 host, 1 client)  
**Duration**: 5-10 minutes

### Scenario 2: Three-User Session
**Purpose**: Test full multi-user capacity  
**Devices**: 3 Quest 3 headsets (1 host, 2 clients)  
**Duration**: 10-15 minutes

### Scenario 3: Connection Mode Switching
**Purpose**: Verify seamless transition between Cloud and LAN modes  
**Devices**: 1 Quest 3 headset  
**Duration**: 5 minutes

### Scenario 4: Error Handling & Recovery
**Purpose**: Test timeout, invalid IP, and reconnection logic  
**Devices**: 2 Quest 3 headsets  
**Duration**: 15-20 minutes

### Scenario 5: Network Stress Testing
**Purpose**: Validate stability under poor network conditions  
**Devices**: 2-3 Quest 3 headsets  
**Duration**: 20-30 minutes

### Scenario 6: Long Session Stability
**Purpose**: Test sustained connection over extended period  
**Devices**: 2-3 Quest 3 headsets  
**Duration**: 60 minutes

---

## Testing Procedures

### Procedure 1: Initial Setup Validation

**Objective**: Confirm all components are properly configured

**Steps**:

1. **Build and Deploy**
   ```bash
   # Build APK from Unity (Android platform)
   # File → Build Settings → Build
   
   # Install to all Quest 3 devices
   adb devices  # Verify devices connected
   adb install -r mr-multiplayer.apk  # Install to each device
   ```

2. **Component Verification** (Unity Editor)
   - Open scene with Network Manager
   - Verify **Network Manager XR Multiplayer** GameObject exists
   - Inspect GameObject, confirm components:
     - ✓ NetworkManagerXRMultiplayer
     - ✓ UnityTransport
     - ✓ LANConnectionManager
     - ✓ ConnectionModeManager
     - ✓ LANConnectionStatus

3. **Network Connectivity Test**
   ```bash
   # From development machine, ping each Quest
   ping 192.168.1.50  # Host Quest
   ping 192.168.1.51  # Client Quest 1
   ping 192.168.1.52  # Client Quest 2
   
   # All should respond with <5ms latency
   ```

**Expected Results**:
- All components present on Network Manager prefab
- All Quest devices pingable from development machine
- Ping latency < 10ms for local network

---

### Procedure 2: Host Connection Test

**Objective**: Establish Quest 3 as LAN host and verify IP discovery

**Steps**:

1. **Start Host Application**
   - Put on Quest 3 headset (Host)
   - Launch application
   - Wait for UI to load

2. **Switch to LAN Mode**
   - In main menu, locate **Connection Mode Toggle**
   - Select **LAN Direct** mode
   - Verify mode switch confirmation message

3. **Start Hosting**
   - Press **Host LAN Session** button
   - Observe status messages:
     - "Detecting local IP address..."
     - "Hosting on [IP_ADDRESS]:7777"
     - "Waiting for clients..."

4. **Record Host Information**
   - Note displayed IP address (e.g., `192.168.1.50`)
   - Verify port is **7777** (default)
   - Check for any warning/error messages

5. **Verify Host Status Indicators**
   - Connection status: **Hosting** (green)
   - Local IP displayed correctly
   - Player count: **1/3**

**Expected Results**:
- ✓ Host starts successfully within 2 seconds
- ✓ Correct local IP address detected and displayed
- ✓ Status shows "Hosting" with green indicator
- ✓ No error messages in console

**Common Issues**:
- "Could not detect IP address" → See [Troubleshooting: IP Detection](#ip-detection-failures)
- "Port already in use" → See [Troubleshooting: Port Conflicts](#port-conflicts)

---

### Procedure 3: Client Connection Test

**Objective**: Connect Quest 3 client to LAN host

**Steps**:

1. **Start Client Application**
   - Put on Quest 3 headset (Client)
   - Launch application
   - Wait for UI to load

2. **Switch to LAN Mode**
   - Select **LAN Direct** mode
   - Wait for mode switch confirmation

3. **Enter Host IP Address**
   - Locate **IP Address Input Field**
   - Enter host IP (from Procedure 2, step 4)
   - Example: `192.168.1.50`
   - Verify input is correct (no typos)

4. **Initiate Connection**
   - Press **Join LAN Session** button
   - Observe connection sequence:
     - "Connecting to 192.168.1.50:7777..."
     - "Establishing connection..." (countdown timer)
     - "Connected!" (on success)

5. **Verify Connection Established**
   - Status indicator: **Connected** (green)
   - Host IP displayed
   - Client avatar visible in scene
   - Player count on host: **2/3**

6. **Test Basic Interactions**
   - Move head/hands, verify synchronization on host
   - From host perspective, verify client avatar moves smoothly
   - Grab networked objects, verify ownership transfer

**Expected Results**:
- ✓ Connection established within 5 seconds
- ✓ No timeout errors
- ✓ Client avatar spawns on host
- ✓ Head/hand positions sync smoothly (no stuttering)
- ✓ Network objects grabbable by both host and client

**Common Issues**:
- "Connection timeout" → See [Troubleshooting: Timeouts](#connection-timeouts)
- "Invalid IP address" → See [Troubleshooting: IP Validation](#ip-validation-errors)
- "Host not found" → See [Troubleshooting: Network Unreachable](#network-unreachable)

---

### Procedure 4: Three-User Session Test

**Objective**: Validate full capacity with 3 simultaneous users

**Steps**:

1. **Setup Host** (Quest 3 #1)
   - Follow [Procedure 2](#procedure-2-host-connection-test)
   - Confirm hosting status

2. **Connect First Client** (Quest 3 #2)
   - Follow [Procedure 3](#procedure-3-client-connection-test)
   - Verify player count: **2/3**

3. **Connect Second Client** (Quest 3 #3)
   - Repeat [Procedure 3](#procedure-3-client-connection-test) steps
   - Enter same host IP
   - Press **Join LAN Session**

4. **Verify All Users Connected**
   - Host sees 2 client avatars
   - Each client sees host + 1 other client
   - Player count: **3/3** on all devices

5. **Multi-User Interaction Test**
   - All users move simultaneously
   - Verify smooth avatar synchronization
   - Test object passing between all 3 users
   - Verify no collision detection false positives

6. **Session Duration Test**
   - Maintain connection for **10 minutes**
   - All users perform continuous interactions
   - Monitor for connection drops or degradation

**Expected Results**:
- ✓ All 3 users connect successfully
- ✓ Avatar synchronization smooth for all users
- ✓ No frame drops or stuttering
- ✓ Object interactions work correctly between all users
- ✓ Connection remains stable for full 10-minute duration

**Performance Metrics** (use Unity Profiler):
- Frame rate: **90 FPS** maintained on all devices
- Network latency: **<20ms** RTT
- Bandwidth: **<10 KB/s** per device

---

### Procedure 5: Connection Mode Switching

**Objective**: Test transition between Cloud and LAN modes

**Steps**:

1. **Start in Cloud Mode**
   - Launch application
   - Verify **Cloud** mode selected by default
   - Do NOT connect to any lobby

2. **Switch to LAN Mode**
   - Select **LAN Direct** from mode toggle
   - Observe UI changes:
     - Cloud lobby list disappears
     - LAN connection panel appears
     - IP input field visible
     - Host/Join buttons visible

3. **Switch Back to Cloud Mode**
   - Select **Cloud** from mode toggle
   - Verify smooth transition:
     - LAN panel disappears
     - Cloud lobby list reappears
     - No error messages

4. **Test Mode Switching During Connection**
   - Start hosting LAN session
   - Attempt to switch to Cloud mode
   - **Expected**: Mode switch **BLOCKED** with message:
     - "Cannot switch modes while connected. Please disconnect first."

5. **Disconnect and Switch**
   - Disconnect from LAN session
   - Switch to Cloud mode
   - **Expected**: Switch succeeds immediately

**Expected Results**:
- ✓ Mode switching works smoothly when disconnected
- ✓ UI updates appropriately for each mode
- ✓ Mode switching blocked during active connection
- ✓ Clear error message when switch is blocked
- ✓ No crashes or state corruption

---

### Procedure 6: Disconnection and Cleanup

**Objective**: Verify graceful disconnection and resource cleanup

**Steps**:

1. **Establish Connection** (host + client)
   - Follow [Procedure 2](#procedure-2-host-connection-test) and [Procedure 3](#procedure-3-client-connection-test)

2. **Client-Initiated Disconnect**
   - From client headset, press **Disconnect** button
   - Observe client status:
     - "Disconnecting..."
     - "Disconnected" (status returns to idle)
   - Observe host status:
     - "Client disconnected"
     - Player count: **1/3**
     - Client avatar removed from scene

3. **Client Reconnection**
   - From same client, rejoin using same IP
   - Verify connection re-establishes successfully
   - Player count: **2/3** again

4. **Host-Initiated Shutdown**
   - From host, press **Stop Hosting** button
   - Observe host status:
     - "Stopping server..."
     - "Disconnected"
   - Observe client status:
     - "Connection lost"
     - Automatic reconnection attempt (if enabled)
     - After timeout: "Host disconnected"

5. **Verify Complete Cleanup**
   - All network connections closed
   - No lingering network threads
   - Status indicators reset to disconnected
   - UI returns to initial state

**Expected Results**:
- ✓ Client disconnect is immediate and clean
- ✓ Host detects client disconnect within 1 second
- ✓ Client can reconnect after voluntary disconnect
- ✓ Host shutdown triggers client disconnect
- ✓ No memory leaks or stuck network states

---

## Error Scenarios & Validation

### Error Category 1: Invalid IP Address

**Test**: Enter malformed IP addresses and verify error handling

**Test Cases**:

1. **Empty IP Address**
   - Leave IP field blank
   - Press **Join LAN Session**
   - **Expected**: Error message "Please enter a valid IP address"
   - **Error Category**: `InvalidIPAddress`

2. **Malformed IP (Letters)**
   - Enter: `abc.def.ghi.jkl`
   - **Expected**: Input validation prevents entry OR error on join

3. **Malformed IP (Incomplete)**
   - Enter: `192.168.1`
   - **Expected**: Error "Invalid IP address format"

4. **Out of Range Values**
   - Enter: `256.300.400.500`
   - **Expected**: Error "IP address values out of range"

5. **Localhost**
   - Enter: `127.0.0.1`
   - **Expected**: Warning "Cannot connect to localhost on Quest"

**Validation**:
```csharp
// Expected error messages in LANConnectionStatus
- "Invalid IP address format. Please use format: XXX.XXX.XXX.XXX"
- "IP address cannot be empty"
- "IP address values must be between 0-255"
```

---

### Error Category 2: Network Unreachable

**Test**: Attempt connections to unreachable hosts

**Test Cases**:

1. **Wrong Subnet**
   - Host IP: `192.168.1.50`
   - Client enters: `192.168.2.50` (different subnet)
   - **Expected**: Error "Network unreachable or host not on same network"
   - **Error Category**: `NetworkUnreachable`

2. **Non-Existent IP**
   - Enter IP not assigned to any device: `192.168.1.250`
   - **Expected**: Connection timeout after 5 seconds
   - **Error Category**: `HostNotFound`

3. **Device Offline**
   - Enter IP of powered-off Quest
   - **Expected**: "Host not found. Please verify host is running and connected to network."

**Validation**:
- Timeout occurs at **5 seconds** (default `m_ConnectionTimeout`)
- Error message is actionable and user-friendly
- No crash or infinite connection attempt

---

### Error Category 3: Connection Timeout

**Test**: Validate timeout mechanism and user feedback

**Setup**:
1. Start host on Quest 3
2. Immediately put host to sleep or block network traffic
3. Attempt client connection

**Test Cases**:

1. **Standard Timeout**
   - Client connects to valid IP but host not responding
   - **Expected**: Timeout after **5 seconds**
   - **Error Message**: "Connection timed out. Please check network settings and try again."
   - **Error Category**: `ConnectionTimeout`

2. **Timeout with Countdown**
   - During connection, verify countdown timer visible:
     - "Connecting... (5s remaining)"
     - "Connecting... (4s remaining)"
     - ...
     - "Connection timeout"

3. **Multiple Timeout Attempts**
   - After first timeout, retry connection 3 times
   - Verify each attempt times out independently
   - Verify no cumulative delay issues

**Validation**:
```csharp
// LANConnectionStatus timeout configuration
m_ConnectionTimeout = 5.0f;  // Default timeout
m_ReconnectionTimeout = 30.0f;  // Reconnection window
m_MaxReconnectionAttempts = 10;  // Max retries
```

---

### Error Category 4: Port Conflicts

**Test**: Verify handling of blocked or occupied ports

**Test Cases**:

1. **Port Already in Use**
   - Start first host on port 7777
   - Attempt to start second host on same device (unlikely on Quest)
   - **Expected**: Error "Port 7777 already in use"

2. **Firewall Blocking Port**
   - Configure router to block port 7777
   - Attempt connection
   - **Expected**: Connection timeout
   - **Error Category**: `PortBlocked`

3. **Custom Port Testing**
   - Modify `m_DefaultPort` to 7778 in LANConnectionManager
   - Verify host uses new port
   - Client must match port for successful connection

**Validation**:
- Error clearly indicates port conflict
- Suggests using different port or stopping other application
- No system crash or undefined behavior

---

### Error Category 5: Connection Lost (Mid-Session)

**Test**: Validate reconnection logic after connection drop

**Setup**:
1. Establish stable host-client connection
2. Simulate network interruption

**Test Cases**:

1. **WiFi Disconnect (Client)**
   - Client: Disable WiFi for 5 seconds, then re-enable
   - **Expected Behavior**:
     - Client: "Connection lost. Attempting to reconnect..."
     - Reconnection attempt every 3 seconds (default `m_ReconnectionInterval`)
     - Success: "Reconnected successfully"
     - **Error Category**: `ConnectionLost`

2. **Host Shutdown (Unexpected)**
   - Force-close host application
   - **Expected Client Behavior**:
     - "Connection lost"
     - Reconnection attempts for 30 seconds (default `m_ReconnectionTimeout`)
     - After timeout: "Unable to reconnect. Host may have disconnected."

3. **Automatic Reconnection Success**
   - Client WiFi briefly drops (2 seconds)
   - **Expected**: Seamless reconnection within 5 seconds
   - Avatar state preserved (position, grabbed objects)

4. **Automatic Reconnection Failure**
   - Host permanently offline
   - **Expected**: After 10 attempts (default `m_MaxReconnectionAttempts`):
     - "Reconnection failed. Returning to main menu."
     - Clean disconnect, no hung state

**Validation**:
```csharp
// LANConnectionStatus reconnection settings
m_EnableAutoReconnect = true;  // Must be enabled
m_ReconnectionInterval = 3.0f;  // Time between attempts
m_MaxReconnectionAttempts = 10;  // Failure threshold
```

---

## Troubleshooting Guide

### IP Detection Failures

**Symptom**: Host shows "Could not detect IP address" or displays incorrect IP

**Possible Causes**:
1. Quest not connected to WiFi
2. WiFi network doesn't assign IPv4 addresses
3. Multiple network interfaces active (rare on Quest)
4. VPN app interfering with network detection

**Solutions**:

1. **Verify WiFi Connection**
   ```
   Quest Settings → WiFi → Confirm connected
   Note IP address (should be 192.168.x.x or 10.x.x.x)
   ```

2. **Restart Network**
   - Disable WiFi, wait 5 seconds, re-enable
   - Or restart Quest headset

3. **Check Router DHCP**
   - Access router settings
   - Verify DHCP enabled
   - Check for IP lease conflicts

4. **Test Connectivity**
   ```bash
   # From dev machine
   adb shell ping -c 4 8.8.8.8
   # Should receive replies if network working
   ```

5. **Disable VPN Apps**
   - Quest Settings → Apps
   - Force stop any VPN applications
   - Retry LAN connection

**Code Reference**:
```csharp
// LANConnectionManager.GetLocalIPAddress()
// Uses IPDiscoveryService.GetLocalIPAddress()
// Filters for IPv4, prefers 192.168.x.x or 10.x.x.x
```

---

### Connection Timeouts

**Symptom**: Client shows "Connection timeout" after 5 seconds

**Diagnostic Steps**:

1. **Verify Host is Running**
   - Check host Quest shows "Hosting" status
   - Confirm IP address displayed on host matches client input

2. **Test Network Connectivity**
   ```bash
   # From dev machine, ping host Quest
   ping [host_ip_address]
   
   # If successful, try from client Quest (via ADB)
   adb shell ping -c 4 [host_ip_address]
   ```

3. **Check AP Isolation**
   - Router settings → Wireless → Advanced
   - **AP Isolation MUST be DISABLED**
   - If enabled, devices cannot communicate

4. **Verify Firewall Settings**
   - Router: Check for blocked ports (7777)
   - Quest: Usually no firewall, but check VPN/security apps

5. **Test Different Port**
   - Temporarily change `m_DefaultPort` to 7778
   - Rebuild and test both host and client
   - Rules out port-specific blocking

**Common Fixes**:

| Issue | Solution |
|-------|----------|
| AP Isolation enabled | Disable in router → Wireless → Advanced |
| Wrong subnet | Ensure all devices on same network (e.g., 192.168.1.x) |
| Port blocked by ISP | Use alternative port (7778, 8080, etc.) |
| Host app not running | Verify host shows "Hosting" status |
| IPv6 confusion | Ensure using IPv4 addresses (not fe80::...) |

---

### IP Validation Errors

**Symptom**: Error messages like "Invalid IP address format"

**Validation Rules** (from `IPDiscoveryService.ValidateIPAddress`):
- Must be IPv4 format: `XXX.XXX.XXX.XXX`
- Each octet must be 0-255
- Cannot be empty or all zeros
- Cannot be localhost (127.0.0.1)
- Cannot be broadcast (255.255.255.255)

**Common Mistakes**:

1. **Typos in IP Entry**
   - Incorrect: `192.168.1.5O` (letter O instead of zero)
   - Correct: `192.168.1.50`

2. **Copy-Paste Artifacts**
   - Extra spaces: `192.168.1.50 `
   - Special characters from clipboard

3. **Incomplete IP**
   - Missing octets: `192.168.1`
   - Should be: `192.168.1.50`

**Fix**: Double-check IP on host device before entering on client

---

### Network Unreachable

**Symptom**: Error "Network unreachable or host not on same network"

**Diagnostic Flowchart**:

```
1. Check subnet match
   Host IP: 192.168.1.50
   Client IP: 192.168.1.51  ✓ Same subnet (192.168.1.x)
   
2. Ping test from dev machine
   ping 192.168.1.50  ✓ Success
   ping 192.168.1.51  ✓ Success
   
3. Ping between Quest devices
   adb shell ping -c 4 [other_quest_ip]
   
   ✓ Success → Software issue, check app logs
   ✗ Failure → Network issue, check AP Isolation
```

**Solutions**:

1. **Subnet Mismatch**
   - Ensure all devices connected to same WiFi network
   - Check router assigns sequential IPs from same range

2. **Router Configuration**
   - Access router admin (192.168.1.1)
   - Check DHCP range (e.g., 192.168.1.100-192.168.1.200)
   - Verify all Quests receive IPs in range

3. **Guest Network Issues**
   - If using guest WiFi, devices may be isolated
   - Connect all Quests to **main network**, not guest

---

### Port Conflicts

**Symptom**: "Port 7777 already in use" or connection refused

**Diagnostic**:

```bash
# On development machine (if testing Editor as host)
netstat -ano | findstr :7777

# Shows processes using port 7777
# PID at end of line identifies application
```

**Solutions**:

1. **Change Default Port**
   - Edit `LANConnectionManager`:
     ```csharp
     [SerializeField]
     private ushort m_DefaultPort = 7778;  // Changed from 7777
     ```
   - Rebuild application for all devices

2. **Stop Conflicting Application**
   - Identify process using port
   - Stop application or restart computer

3. **Router Port Forwarding**
   - If router blocks certain ports, try alternatives:
   - Common alternatives: 8080, 8888, 9999

---

### Reconnection Failures

**Symptom**: After connection drop, reconnection attempts fail repeatedly

**Diagnostic Steps**:

1. **Check Reconnection Settings**
   ```csharp
   // LANConnectionStatus configuration
   m_EnableAutoReconnect = true;  // Must be enabled
   m_MaxReconnectionAttempts = 10;  // Default
   m_ReconnectionInterval = 3.0f;  // Seconds between attempts
   ```

2. **Monitor Reconnection Attempts**
   - Check on-screen status messages:
     - "Reconnecting... Attempt 1/10"
     - "Reconnecting... Attempt 2/10"
     - ...
   - If reaching max attempts, host is truly unreachable

3. **Verify Host Still Running**
   - From dev machine:
     ```bash
     ping [host_ip_address]
     ```
   - If host unreachable, reconnection will always fail

**Solutions**:

1. **Increase Reconnection Window**
   - For unreliable networks:
     ```csharp
     m_ReconnectionTimeout = 60.0f;  // Extended to 60 seconds
     m_MaxReconnectionAttempts = 20;  // More attempts
     ```

2. **Manual Reconnection**
   - Disable auto-reconnect if causing issues
   - Use explicit "Reconnect" button in UI
   - Gives user more control over timing

3. **Network Stability**
   - Test with better WiFi router
   - Move closer to access point
   - Reduce interference (move away from microwaves, Bluetooth devices)

---

### Performance Degradation

**Symptom**: Frame drops, stuttering avatars, or high latency after initial connection

**Diagnostic Tools**:

1. **Unity Profiler (Networked)**
   - In-headset profiling not practical
   - Build with **Development Build + Autoconnect Profiler**
   - Connect Unity Profiler from dev machine
   - Monitor:
     - CPU: Should be <11ms per frame (90 FPS)
     - Network: <10 KB/s bandwidth per device
     - Rendering: No major spikes

2. **ADB Logcat**
   ```bash
   adb logcat -s Unity:V | grep "LAN Connection"
   ```
   Look for warning messages about network congestion

**Common Issues**:

| Symptom | Likely Cause | Solution |
|---------|--------------|----------|
| Stuttering avatars | High network latency | Move closer to router; reduce WiFi interference |
| Frame drops | CPU overload | Optimize scene; reduce draw calls; check for infinite loops |
| Bandwidth spikes | Too frequent network updates | Reduce network sync frequency from 30Hz to 20Hz |
| Connection drops | WiFi instability | Use 5GHz band; check router QoS settings |

**Performance Targets**:
- Frame rate: **90 FPS** (consistent)
- Network latency: **<20ms** RTT
- Bandwidth per device: **<10 KB/s**
- Avatar sync rate: **20Hz** (default)
- Object sync rate: **30Hz** (on interaction)

---

## Performance Validation

### Network Performance Metrics

**Measurement Method**: Unity Profiler → Networking module

**Target Metrics**:

| Metric | Target | Acceptable | Poor |
|--------|--------|------------|------|
| Round-Trip Time (RTT) | <10ms | <20ms | >50ms |
| Bandwidth (per device) | <5 KB/s idle | <10 KB/s active | >20 KB/s |
| Packet Loss | 0% | <1% | >3% |
| Connection Setup Time | <2s | <5s | >10s |

**Testing Procedure**:

1. **Baseline Measurement** (2 users, idle)
   - Both users stationary for 30 seconds
   - Record average bandwidth
   - **Expected**: 2-3 KB/s per device

2. **Active Interaction** (2 users, moving)
   - Both users moving head/hands continuously
   - Record peak bandwidth
   - **Expected**: 5-8 KB/s per device

3. **Three-User Stress Test**
   - All 3 users moving simultaneously
   - Grabbing and passing objects
   - **Expected**: 8-10 KB/s per device

4. **Latency Measurement**
   - Use Unity Profiler to record RTT
   - Test during peak activity
   - **Expected**: <20ms consistently

### Frame Rate Validation

**Requirement**: Maintain **90 FPS** on Quest 3 at all times

**Testing Procedure**:

1. **Enable FPS Display**
   - In-headset: Quest Settings → Developer → Show Performance Overlay
   - Or use Unity Profiler remote connection

2. **Baseline Test** (Single user)
   - No network activity
   - **Expected**: 90 FPS locked

3. **Two-User Test**
   - Host + 1 client connected
   - Both users moving
   - **Expected**: 90 FPS maintained on both devices

4. **Three-User Test**
   - All 3 users connected and active
   - **Expected**: 90 FPS maintained on all devices
   - **Acceptable**: Brief drops to 80-85 FPS during complex interactions

**Performance Failure Indicators**:
- ⚠️ FPS drops below 80
- ⚠️ Stuttering or judder in avatar movement
- ⚠️ Latency exceeds 50ms
- ⚠️ Frequent packet loss (>1%)

**Optimization If Performance Poor**:
- Reduce avatar sync rate from 20Hz to 15Hz
- Implement LOD for distant avatars
- Reduce physics simulation frequency
- Check for network congestion (other devices on WiFi)

---

## Test Checklist

### Pre-Test Setup Checklist

- [ ] All Quest 3 devices updated to latest OS
- [ ] WiFi router configured (AP Isolation **disabled**)
- [ ] Static IPs assigned or DHCP reservations set
- [ ] Application built and installed on all devices
- [ ] Unity Profiler configured for network monitoring
- [ ] ADB installed and devices accessible
- [ ] Test environment prepared (adequate physical space)

### Connection Establishment Checklist

- [ ] Host starts successfully and displays correct IP
- [ ] Host shows "Hosting" status with green indicator
- [ ] Client can enter IP address without UI errors
- [ ] Client connection completes within 5 seconds
- [ ] Client avatar spawns on host within 1 second
- [ ] Head/hand synchronization smooth (no stuttering)
- [ ] Player count updates correctly on all devices

### Multi-User Session Checklist

- [ ] Second client can connect to existing session
- [ ] All 3 users see each other's avatars
- [ ] Avatar positions sync smoothly for all users
- [ ] Object interactions work between all users
- [ ] Collision detection functions correctly
- [ ] Frame rate maintains 90 FPS on all devices
- [ ] Network latency remains <20ms

### Error Handling Checklist

- [ ] Invalid IP address rejected with clear error message
- [ ] Connection timeout occurs at 5 seconds (not sooner/later)
- [ ] Timeout displays countdown timer to user
- [ ] Network unreachable shows actionable error message
- [ ] Host disconnect triggers client reconnection attempt
- [ ] Reconnection succeeds after brief network interruption
- [ ] Reconnection fails gracefully after extended outage
- [ ] Max reconnection attempts enforced (10 by default)

### Mode Switching Checklist

- [ ] Can switch from Cloud to LAN when disconnected
- [ ] Can switch from LAN to Cloud when disconnected
- [ ] Mode switching **blocked** during active connection
- [ ] Clear error message when switch blocked
- [ ] UI updates appropriately for each mode
- [ ] No state corruption after multiple mode switches

### Disconnection Checklist

- [ ] Client disconnect is immediate (<1 second)
- [ ] Host detects client disconnect within 1 second
- [ ] Client avatar removed from host scene
- [ ] Player count updates correctly
- [ ] Client can reconnect after voluntary disconnect
- [ ] Host shutdown triggers all clients to disconnect
- [ ] All network resources released after disconnect

### Performance Validation Checklist

- [ ] Frame rate: 90 FPS maintained (all devices)
- [ ] Network latency: <20ms RTT
- [ ] Bandwidth: <10 KB/s per device during active use
- [ ] Packet loss: <1%
- [ ] No memory leaks over 60-minute session
- [ ] No CPU/GPU thermal throttling

### Long-Term Stability Checklist

- [ ] 60-minute session without disconnections
- [ ] No degradation in frame rate over time
- [ ] No increase in network latency over time
- [ ] Reconnection works after 30-minute stable connection
- [ ] Multiple disconnect/reconnect cycles without issues
- [ ] No crashes or freezes over extended use

---

## Additional Resources

### Related Documentation

- [Network Architecture](../architecture/network-topology.md) — Overall multiplayer system design
- [Unity Testing Procedures](../workflows/testing-procedures.md) — General VR/networking test guide
- [Debugging Guide](../workflows/debugging-guide.md) — Troubleshooting Unity/networking issues
- [Performance Testing](../workflows/performance-testing.md) — Frame rate and optimization validation

### Component Documentation

- `LANConnectionManager.cs` — Host/client connection management
- `ConnectionModeManager.cs` — Cloud/LAN mode coordination
- `LANConnectionStatus.cs` — Error handling and reconnection
- `IPDiscoveryService.cs` — Network interface detection (Quest 3)

### Unity Documentation

- [Unity Netcode for GameObjects](https://docs-multiplayer.unity3d.com/netcode/current/about/)
- [Unity Transport](https://docs.unity3d.com/Packages/com.unity.transport@2.0/manual/index.html)
- [Meta Quest Development](https://developer.oculus.com/documentation/unity/)

### Testing Tools

- **Unity Profiler**: Network performance monitoring
- **ADB (Android Debug Bridge)**: Quest 3 debugging
- **Wireshark**: Packet capture and analysis (advanced)
- **ParrelSync**: Multi-instance Unity Editor testing (pre-device testing)

---

## Revision History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0 | 2025-10-20 | QA Team | Initial LAN connection testing procedures |

---

[← Back to Top](#lan-connection-testing-for-quest-3)
