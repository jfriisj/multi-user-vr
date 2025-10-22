# Multi-User VR System Development Instructions

## Project Overview
This is a Unity 6 project for developing an MVP of a co-located multi-user VR system enabling 3 Meta Quest 3 users to safely share the same physical room while collaborating in VR. The key differentiator is **physical safety** - preventing real-world collisions between users in shared space.

## Core Architecture Requirements

### Primary Safety-First Philosophy
- **Physical safety is paramount** - never compromise on collision prevention systems
- All networking and VR systems must prioritize real-world safety over performance
- Implement multiple redundant safety systems and graceful degradation patterns
- Emergency stop mechanisms must be accessible and reliable

### Essential Components to Implement
Based on `MVP_DEVELOPMENT_PROMPT.md`, implement these core systems:
```csharp
// Critical MVP Components (in priority order):
- VRNetworkManager (Unity Netcode for GameObjects) 
- UserTrackingSystem (XR Interaction Toolkit)
- CollisionPreventionSystem (Custom safety system)
- BasicAvatarController (Real-time position sync)
- SafetyManager (Emergency protocols)
```

## Technical Stack & Dependencies

### VR Framework
- **Meta Quest 3 SDK** v78.0.0 (com.meta.xr.sdk.all) - Primary VR platform
- **Unity XR OpenXR** v1.16.0 - Cross-platform XR management
- No XR Interaction Toolkit installed yet - **add when implementing interaction systems**
- No Unity Netcode for GameObjects installed yet - **add when implementing networking**

### AI Development Integration
- **Unity-MCP Server** v0.20.0 - AI coding assistant integration via Model Context Protocol
- MCP server runs at `Library/mcp-server/win-x64/unity-mcp-server.exe`
- Configuration in `.vscode/mcp.json` and `Assets/Resources/Unity-MCP-ConnectionConfig.json`
- Use MCP tools for rapid prototyping and AI-assisted development

### Performance Requirements
- Maintain 90fps on all 3 Quest 3 headsets simultaneously
- <50ms network latency between headsets  
- <100ms safety system response time
- <2cm position tracking accuracy per headset

## Development Workflow

### Phase-Based Implementation (8-week MVP)
```
Week 1-2: Foundation Setup (single headset VR environment)
Week 3-4: Networking & Sync (2-3 headset synchronization) 
Week 5-6: Safety & Polish (collision prevention, warnings)
Week 7-8: Validation & Documentation (testing, benchmarks)
```

### Essential Packages to Add
When implementing networking: `com.unity.netcode.gameobjects`
When implementing interactions: `com.unity.xr.interaction.toolkit`
For input handling: Unity Input System already installed (v1.14.2)

### Safety Testing Protocols
- Test tracking interference with multiple Quest 3 headsets
- Measure minimum safe distances between users  
- Validate collision detection accuracy in different lighting
- Benchmark network stability over 30+ minute sessions

## Key File Locations

### Configuration Files
- `Assets/Resources/Unity-MCP-ConnectionConfig.json` - AI assistant settings
- `ProjectSettings/XRSettings.asset` - VR configuration
- `Packages/manifest.json` - Package dependencies
- `MVP_DEVELOPMENT_PROMPT.md` - Complete project requirements and success metrics

### Scenes & Assets  
- `Assets/Scenes/SampleScene.unity` - Main development scene
- `Assets/XR/` - XR-specific settings and configurations
- Multiple XR settings folders suggest multi-device testing setup

## Development Conventions

### Safety-First Code Patterns
```csharp
// Always implement safety checks first
if (!SafetyManager.Instance.IsUserPositionSafe(userPosition))
{
    TriggerEmergencyStop();
    return;
}

// Graceful degradation for network issues
if (networkLatency > MAX_SAFE_LATENCY)
{
    FallbackToLocalMode();
}
```

### VR-Specific Considerations
- Use local network setup (same Wi-Fi) for minimal latency
- Implement Guardian boundary awareness and respect
- Test with actual Quest 3 hardware, not simulators
- Account for battery life optimization (1-2 hour sessions)

### AI-Assisted Development
- Leverage Unity-MCP for rapid prototyping of VR components
- Use MCP tools for automated testing and validation
- AI can help with reflection-based component manipulation
- Custom MCP tools can be added in project for domain-specific tasks

## Critical Success Metrics
- Zero physical collisions during testing
- 3 users can collaborate safely for 30+ minutes
- <5 minute setup time from start to collaborative experience  
- Real-time movement sync with no noticeable lag

## Scope Limitations
Out of MVP scope: Advanced haptic feedback, complex AI features, cloud networking, advanced avatar customization, voice chat (use Quest's built-in), eye tracking.

Focus on core safety and collaboration functionality first.