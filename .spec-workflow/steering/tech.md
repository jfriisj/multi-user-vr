# Technology Stack

## Project Type

**Co-located Multi-user VR Game/Research Application** - Unity-based VR experience for Meta Quest 3 headsets with advanced networking, safety systems, and research instrumentation for investigating co-located VR challenges.

## Core Technologies

### Primary Language(s)
- **Language**: C# 9.0+ (.NET Standard 2.1 compatibility for Unity)
- **Runtime/Compiler**: Unity 2022.3+ LTS with IL2CPP for Android builds
- **Language-specific tools**: NuGet package management through Unity Package Manager, Unity Test Framework, Unity Analytics

### Key Dependencies/Libraries

**VR & Input Systems:**
- **XR Interaction Toolkit 2.5+**: Unity's official VR interaction framework for hand tracking, input handling, and UI interaction
- **Unity XR Hands 1.4+**: Advanced hand tracking support for Meta Quest 3 (already integrated based on project structure)
- **OpenXR 1.8+**: Cross-platform VR standard for headset compatibility and future-proofing

**Networking & Synchronization:**
- **Netcode for GameObjects 1.7+**: Unity's official multiplayer networking solution for real-time avatar synchronization
- **Unity Transport 2.0+**: Low-level networking transport layer optimized for real-time communication
- **Unity Relay/Lobby**: Optional cloud networking services for connection management (if needed for research flexibility)

**Safety & Physics:**
- **Unity Physics**: Built-in collision detection and proximity monitoring for safety systems
- **Unity XR Guardian**: Integration with Meta Quest Guardian system for physical boundary management
- **Custom Collision Prevention System**: Project-specific safety algorithms for co-located user protection

### Application Architecture

**Event-Driven Multi-user VR Architecture** with the following layers:

1. **VR Input Layer**: XR Interaction Toolkit handles all headset input, hand tracking, and controller events
2. **Networking Layer**: Netcode for GameObjects manages state synchronization between 3 clients
3. **Safety System Layer**: Real-time collision detection and prevention with escalating warning system
4. **Game Logic Layer**: Avatar management, scene interaction, and research data collection
5. **Research Instrumentation Layer**: Data logging, performance monitoring, and interference tracking

**Component Structure:**
- **Modular Safety-First Design**: Safety systems operate independently and can override all other systems
- **Client-Server Model**: One headset acts as host, others as clients (with server authority for safety)
- **Event Bus Pattern**: Decoupled communication between VR input, networking, and safety systems

### Data Storage

- **Primary storage**: Unity ScriptableObjects for configuration, local files for research data logging
- **Real-time data**: In-memory synchronization via Netcode NetworkVariables for avatar positions, rotations, states
- **Research data**: JSON/CSV exports for tracking interference analysis, safety incident logging, performance metrics
- **Configuration**: Unity's built-in serialization for project settings, safety thresholds, networking parameters

### External Integrations

- **Meta Quest Platform**: Native Android integration for Quest 3 hardware access, Guardian system, hand tracking
- **Unity Analytics**: Built-in telemetry for performance monitoring and crash reporting during research phases
- **Research Export Systems**: CSV/JSON data export for academic analysis of tracking interference and safety incidents
- **Development Tools**: Unity Remote for testing, Unity Profiler for performance optimization

### Monitoring & Dashboard Technologies

- **Dashboard Framework**: Unity's built-in UI system (Canvas-based) with both VR and desktop monitoring interfaces
- **Real-time Communication**: Netcode NetworkVariables for continuous synchronization of safety metrics, user positions, system status
- **Visualization**: Unity's built-in GUI system with custom safety alert overlays, proximity warnings, tracking quality indicators
- **State Management**: Centralized NetworkManager with safety system state taking precedence over all other states

## Development Environment

### Build & Development Tools

- **Build System**: Unity's integrated build pipeline targeting Android (Meta Quest 3)
- **Package Management**: Unity Package Manager for official packages, Git URLs for community packages
- **Development workflow**: Unity Play Mode testing with XR simulation, build-and-deploy cycle for Quest 3 testing
- **Version Control Integration**: Unity Smart Merge for scene and prefab collaboration

### Code Quality Tools

- **Static Analysis**: Unity Code Analysis package, Rider/Visual Studio Code analyzers for C#
- **Formatting**: Unity's C# formatting guidelines, EditorConfig for consistent team standards
- **Testing Framework**: Unity Test Framework for unit tests, NUnit for C# logic testing, integration tests for networking
- **Documentation**: XML documentation comments, Unity documentation tools, research logging for academic purposes

### Version Control & Collaboration

- **VCS**: Git with Git LFS for Unity assets (textures, models, audio)
- **Branching Strategy**: GitHub Flow with feature branches for research phases, main branch for stable builds
- **Code Review Process**: Pull request reviews with mandatory safety system validation, Unity build testing in CI/CD

### Dashboard Development

- **Live Reload**: Unity's domain reload system for script changes, play mode testing for immediate feedback
- **Multi-Instance Support**: Multiple Unity editor instances for multi-user development testing
- **Device Testing**: Unity Remote for wireless testing, direct Quest 3 deployment via ADB

## Deployment & Distribution

- **Target Platform**: Meta Quest 3 (Android-based VR headset)
- **Distribution Method**: Direct APK installation via Unity Build & Run, SideQuest for research sharing
- **Installation Requirements**: Meta Quest 3 headsets (exactly 3), sufficient physical room space (minimum 2m x 2m per user), stable WiFi network for networking
- **Update Mechanism**: Manual APK replacement during research phases, potential Oculus Store distribution for future public release

## Technical Requirements & Constraints

### Performance Requirements

- **Frame Rate**: Maintain 90 FPS per headset (Quest 3 requirement) even with 3 active users
- **Network Latency**: <20ms for avatar position synchronization to maintain safety system effectiveness
- **Memory Usage**: <4GB RAM per headset (Quest 3 hardware constraint)
- **Startup Time**: <30 seconds from launch to multi-user ready state
- **Safety Response Time**: <100ms for collision detection and warning system activation

### Compatibility Requirements

- **Platform Support**: Meta Quest 3 exclusively (Android 10+, Snapdragon XR2 Gen 2)
- **Unity Version**: 2022.3 LTS minimum for XR support and stability
- **XR Standards**: OpenXR 1.8+ for future headset compatibility research
- **Networking**: WiFi 5 (802.11ac) minimum for reliable real-time synchronization

### Security & Compliance

- **Privacy Requirements**: No personal data collection, local research data only, optional anonymized performance metrics
- **Physical Safety**: Primary security concern - collision prevention system must never fail
- **Network Security**: Local network operation preferred, encrypted communication for any cloud features
- **Research Ethics**: IRB approval for any human subjects research, informed consent for safety testing

### Scalability & Reliability

- **Expected Load**: Exactly 3 concurrent users (fixed constraint for research scope)
- **Availability**: 99% uptime during research sessions, graceful degradation if one headset disconnects
- **Growth Projections**: Research extensibility for 4+ users in future studies, multi-room scenarios

## Technical Decisions & Rationale

### Decision Log

1. **Unity + Meta Quest 3 Choice**: Unity provides mature VR development tools, C# enables rapid prototyping for research, Quest 3 offers consumer-grade affordability vs custom hardware, inside-out tracking enables investigation of interference scenarios
   
2. **Netcode for GameObjects over Mirror/Photon**: Official Unity support ensures long-term compatibility, built-in client-server authority model supports safety system requirements, lower barrier to entry for educational objectives

3. **Client-Server Architecture over Peer-to-Peer**: Server authority enables safety system override capabilities, single source of truth for collision detection reduces conflicts, simpler debugging for research purposes

4. **Safety-First System Design**: Physical user safety requires systems that can override all other functionality, independent collision detection system prevents interference from other subsystems, escalating warning system (visual → haptic → movement restriction) provides graduated response

5. **Research Instrumentation as Core Feature**: Academic/educational objectives require comprehensive data collection, tracking interference investigation is primary research goal, performance metrics essential for cost-benefit analysis vs commercial solutions

## Known Limitations

- **Quest 3 Hardware Constraints**: Limited to Snapdragon XR2 Gen 2 processing power, 4GB RAM constraint may limit complex scenes, battery life requires charging rotation during extended research sessions

- **Inside-Out Tracking Limitations**: Potential interference scenarios between multiple headsets not yet fully understood (primary research objective), tracking quality degrades in low-light conditions, limited tracking volume per headset

- **Unity Networking Complexity**: Netcode for GameObjects learning curve for team members, real-time networking debugging requires specialized tools, network issues can cascade into safety system false positives

- **Research vs Production Trade-offs**: Extensive logging and instrumentation may impact performance, research flexibility requirements add complexity vs streamlined production code, academic timeline constraints may limit optimization iterations

- **Physical Space Requirements**: Minimum room size requirements limit deployment flexibility, room setup and calibration required for each research session, physical furniture and obstacles must be manually managed