# Co-located Multi-user VR System

An affordable VR system that enables multiple users to share the same physical room while collaborating in a virtual environment using Meta Quest 3 headsets.

**🚀 Current Status:** This project now uses Unity's MR Multiplayer Tabletop template as the foundation, providing robust XR multiplayer capabilities out of the box.

## Overview

VR rooms are transforming physical spaces into shared multi-user VR environments where teams can train together, with companies like Virtualware deploying over 32 VIROO Rooms worldwide for industries from railways to military training. However, most solutions require expensive custom installations. 

This project develops an affordable alternative that addresses the unique challenges of co-located users - unlike typical multi-user VR platforms where users connect from different locations, this system focuses on users sharing the same physical space.

**Technical Foundation:** Built on Unity's MR Multiplayer Tabletop template (`com.unity.template.mr-multiplayer@1.0.3`), leveraging proven XR Interaction Toolkit and Netcode for GameObjects integration.

## Project Goals

- Create a stable multi-user VR system supporting 3 users in the same physical room
- Leverage Meta Quest 3's native shared space capabilities for co-located VR
- Implement safety measures to prevent collisions between users
- Develop synchronized virtual environments with proper avatar alignment
- Identify target customers in training facilities, educational labs, and team-building venues

## Technical Challenge

The main technical challenges include:

### Multi-headset Coordination
- Utilizing Meta Quest's built-in shared space feature for multiple headsets
- Managing shared physical boundaries and guardian systems
- Ensuring proper spatial alignment between virtual avatars and real user positions

### Safety & Collision Prevention
- Implementing collision avoidance for users who can't see each other in VR
- Managing shared physical space constraints
- Real-time awareness of other users' positions

### Networking & Synchronization
- Synchronizing virtual environments across multiple headsets
- Real-time avatar positioning and movement
- Shared object interactions and state management

## Technology Stack

### Development Environment
- **Unity LTS** - Using template-supported version
- **C#** - Programming language
- **Meta Quest 3** - Target VR hardware (3 devices provided)

### VR Frameworks (Template-Provided)
- **XR Interaction Toolkit 3.x** - Comprehensive XR interaction system
- **OpenXR + Meta OpenXR Plugin** - Cross-platform XR runtime
- **AR Foundation 6.x** - Mixed reality capabilities
- **XR Hands** - Hand tracking support

### Networking (Template-Integrated)
- **Netcode for GameObjects 2.x** - Unity's official multiplayer framework
- **Unity Transport** - Low-level networking transport
- **Unity Services** - Authentication, Lobby/Relay, Vivox voice chat
- **Unity Multiplayer Tools** - Debugging and profiling

### Development Tools
- **Multiplayer Play Mode** - Editor-based multiplayer testing
- **ParrelSync** - Alternative Editor clone testing
- **XR Device Simulator** - Editor VR simulation

## Architecture Overview

Built on Unity's proven MR Multiplayer template architecture:

- **Client/Server Topology** - Host acts as server, clients connect via Unity Transport
- **XR Interaction Framework** - Template-provided XR interaction patterns
- **Network Object Synchronization** - NGO-based avatar and object sync
- **Local & Cloud Support** - LAN testing and optional UGS cloud services
- **Safety Integration** - Collision detection and guardian boundary management

## Target Use Cases

### Training Facilities
- Corporate team training scenarios
- Safety procedure rehearsals
- Equipment operation training

### Educational Institutions
- Collaborative learning experiences
- Science and engineering labs
- Group problem-solving activities

### Team Building Venues
- Corporate retreats and workshops
- Interactive team exercises
- Communication skill development

## Getting Started

### Prerequisites
- Unity 2022.3 LTS or newer
- Meta Quest Developer account
- 3x Meta Quest 3 headsets
- Shared physical space (minimum 3x3 meters recommended)

### Initial Setup
1. Clone this repository
2. Open the Unity project at `mr-multiplayer/` using a template-supported Unity LTS version
3. Optional: Sign in to Unity Services for cloud features
4. Open a sample scene:
   - Basic: `Assets/XRMP/BasicScene.unity`
   - Tabletop demo: `Assets/MRTabletopAssets/Games/Chess/Scenes/SlicesChess.unity`
5. Test in Editor with XR Device Simulator or deploy to Quest 3

### Project Structure
```
├── mr-multiplayer/           # Unity project root (MR Multiplayer template)
│   ├── Assets/
│   │   ├── XRMP/            # Template XR multiplayer framework
│   │   ├── MRTabletopAssets/# Sample tabletop games
│   │   ├── Scenes/          # Unity scenes
│   │   └── Prefabs/         # Reusable game objects
│   └── Packages/            # Unity packages and dependencies
├── docs/                    # Comprehensive documentation
│   ├── architecture/        # System design and diagrams
│   ├── implementation/      # Technical guides
│   └── workflows/           # Development processes
├── MultiUser-VR-MVP-Guide.md # Template integration guide
└── README.md
```

## Development Status

### ✅ Completed (Template Integration)
- [x] Unity project setup with MR Multiplayer template
- [x] VR scene support for Quest 3 via OpenXR
- [x] Netcode for GameObjects integration
- [x] XR Interaction Toolkit implementation
- [x] Sample multiplayer scenes (Basic and Tabletop)
- [x] Editor-based multiplayer testing setup

### 🔄 In Progress
- [-] Avatar synchronization refinement
- [-] Safety system integration
- [-] Guardian boundary management
- [-] Performance optimization for 3+ users

### 📋 Next Phase
- [ ] Custom training scenario development
- [ ] Educational use case implementation
- [ ] Advanced safety protocols
- [ ] Research instrumentation integration

## Safety Considerations

- **Physical Space Management** - Ensure adequate room size for 3 users
- **Collision Prevention** - Real-time user position awareness
- **Emergency Protocols** - Quick VR exit procedures
- **Guardian Boundaries** - Properly configured shared boundaries

## Quick Start Guide

### Editor Testing
1. Open `mr-multiplayer/` in Unity
2. Install Multiplayer Play Mode package
3. Open `Assets/XRMP/BasicScene.unity`
4. Use Play Mode to test with multiple virtual players

### Device Testing (Quest 3)
1. Build Settings: Android, ARM64
2. Build and deploy APK to Quest 3 devices
3. Connect via Unity Transport on local network
4. Test multiplayer interactions

## Documentation

Comprehensive documentation available in `docs/`:
- **[Architecture](docs/architecture/README.md)** - System design and components
- **[Implementation](docs/implementation/README.md)** - Technical implementation guides  
- **[Workflows](docs/workflows/README.md)** - Development and testing procedures
- **[MVP Guide](MultiUser-VR-MVP-Guide.md)** - Template integration details

## Contributing

1. Fork the repository
2. Work within the `mr-multiplayer/` Unity project
3. Follow template-aligned architecture patterns
4. Test with Multiplayer Play Mode and Quest 3 devices
5. Document changes and submit pull requests

## Keywords

Virtual Reality, Co-located VR, Shared Physical Space, Multi-user Systems, Unity, Meta Quest 3, Shared Space, XR Development

## License

[Add your license information here]

## Contact

[Add project contact information]

---

*This project focuses on affordable co-located VR solutions, distinguishing itself from remote multi-user VR platforms by addressing the unique challenges and opportunities of users sharing the same physical space.*