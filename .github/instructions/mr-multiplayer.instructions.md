---
applyTo: '**'
---
# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview

This is a **co-located multi-user VR system** for Meta Quest 3 headsets that enables 3 users to collaborate in the same physical room while sharing a virtual environment. The project focuses on addressing the unique challenges of users sharing physical space, including collision prevention, tracking interference, and synchronized virtual environments.

**Key Technologies:**
- **Unity 2022.3 LTS** with C# development
- **Meta XR SDK** for Quest 3 hardware integration  
- **XR Interaction Toolkit** for VR interactions
- **Unity Netcode for GameObjects** for multi-user networking
- **OpenXR** for VR platform compatibility

## Mandatory reading (source of truth)

Before answering or making changes, always read these project docs first:
- `docs/README.md` (entrypoint linking to `docs/architecture`, `docs/implementation`, `docs/workflows`)
- `.spec-workflow/steering/product.md`, `.spec-workflow/steering/tech.md`, `.spec-workflow/steering/structure.md`

Use the sections as follows:
- Architecture: consult `docs/architecture/*` for system diagrams and relationships
- Implementation: follow `docs/implementation/*` for Unity/NGO patterns and code
- Workflows: use `docs/workflows/*` for processes, testing, deployment, troubleshooting

## Development Workflow

### Spec Workflow System
This project uses a sophisticated specification workflow located in `.spec-workflow/`:

**Important Commands:**
```bash
# View project status and progress
# Use MCP tool: spec-status with projectPath and specName parameters

# Load spec workflow guide (MUST USE FIRST for spec work)
# Use MCP tool: spec-workflow-guide

# Request approvals for documents
# Use MCP tool: approvals with action:"request", type:"document", category:"spec"
```

**Workflow Phases:**
1. **Requirements** → 2. **Design** → 3. **Tasks** → 4. **Implementation**

Always follow this sequence. Each phase must be approved before proceeding to the next.

### Unity Project Structure (Planned)
```
vr/                          # Unity project directory (not yet created)
├── Assets/
│   ├── Scripts/
│   │   ├── Networking/      # Unity Netcode for GameObjects
│   │   ├── VR/             # XR Interaction Toolkit extensions
│   │   ├── Safety/         # Collision prevention systems
│   │   ├── GameLogic/      # Core multi-user functionality
│   │   └── Research/       # Data logging and instrumentation
│   ├── Scenes/             # VR scenes
│   ├── Prefabs/            # Player avatars, networked objects
│   └── Settings/           # XR and input configurations
```

## Common Development Commands

### Unity Development
```bash
# Project setup (when Unity project is created)
# Open Unity 2022.3 LTS
# Install Meta XR SDK via Package Manager
# Configure build settings for Android/Quest platform

# Building for Quest 3
# Platform: Android, Architecture: ARM64, Target: Quest 3
```

### Testing Multi-User System
The project includes comprehensive single-headset testing approaches:

**Method 1: Unity Editor + VR Headset**
```bash
# 1. Build to VR headset
# 2. VR Headset: Start app → "Start Host"  
# 3. Unity Editor: Press Play → "Start as Client (Editor)"

# Desktop controls in editor:
# WASD - Move, Mouse - Look, Q/E - Up/Down
# Space - Grab objects, Tab - Switch hands
```

**Network Testing:**
```bash
# Unity Profiler for network monitoring
# Window → Analysis → Profiler → Add → Networking → Netcode for GameObjects
```

### Research & Documentation
```bash
# Generate project documentation
# Use MCP tools for systematic literature review and research analysis
```

## Architecture Highlights

### Safety-First Design
The system prioritizes physical user safety with independent collision detection that can override all other functionality:
- Real-time proximity monitoring between users
- Escalating warning system (visual → haptic → movement restriction)
- Guardian system integration with Meta Quest boundaries

### Networking Architecture
- **Client-Server Model**: Host acts as server with authority for safety systems
- **Synchronization**: Head/hand positions at 20fps, object interactions at 30fps
- **Data Models**: NetworkPlayerData, SafetyEventData, NetworkSessionData

### Component Organization
Following Unity namespace conventions with `MultiUserVR.*` namespacing:
```csharp
MultiUserVR.Networking    // Network management
MultiUserVR.VR.Input      // VR input handling  
MultiUserVR.Safety        // Safety systems
MultiUserVR.GameLogic     // Core functionality
MultiUserVR.Research      // Data collection
```

## Development Standards

### Code Organization
- **File Size**: Maximum 500 lines per C# script
- **Method Size**: Maximum 50 lines per method
- **MonoBehaviour**: Maximum 300 lines per component
- **Documentation**: XML documentation for all public APIs
- **Inspector Fields**: Use `[Tooltip]` attributes for SerializeField properties

### Unity-Specific Patterns
```csharp
public class ExampleVRComponent : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField, Tooltip("Detection radius for safety system")]
    private float detectionRadius = 2.0f;
    #endregion
    
    #region Unity Lifecycle  
    private void Awake() { /* Component initialization */ }
    private void Start() { /* Network and reference setup */ }
    #endregion
    
    #region Network Callbacks (if NetworkBehaviour)
    public override void OnNetworkSpawn() { /* Network init */ }
    #endregion
}
```

### Testing Strategy
- **Unit Tests**: Isolated component testing using Unity Test Framework
- **Integration Tests**: Multi-user networking validation  
- **Performance Tests**: 90 FPS maintenance with 3 users, <20ms network latency
- **Safety Tests**: Collision detection and prevention system validation

## Key Constraints & Requirements

### Performance Targets
- **Frame Rate**: 90 FPS per headset (Quest 3 requirement)
- **Network Latency**: <20ms for avatar synchronization
- **Memory Usage**: <4GB RAM per headset
- **Safety Response**: <100ms collision detection response time

### Physical Requirements
- **Room Size**: Minimum 2m x 2m per user (3x3m total recommended)
- **Hardware**: Exactly 3 Meta Quest 3 headsets
- **Network**: WiFi 5 (802.11ac) minimum for real-time sync
- **Setup**: Shared physical space with proper Guardian boundary configuration

## MCP Research Integration

This project includes MCP tools for systematic literature review and research analysis:
- Upload and analyze research papers related to VR collaboration
- Track citation networks in multi-user VR research
- Generate comprehensive research reports
- Validate research questions using PICO/SPIDER frameworks

Use MCP tools prefixed with research functionality when analyzing academic aspects of the project.