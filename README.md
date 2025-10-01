# Co-located Multi-user VR System

An affordable VR system that enables multiple users to share the same physical room while collaborating in a virtual environment using Meta Quest 3 headsets.

## Overview

VR rooms are transforming physical spaces into shared multi-user VR environments where teams can train together, with companies like Virtualware deploying over 32 VIROO Rooms worldwide for industries from railways to military training. However, most solutions require expensive custom installations. 

This project develops an affordable alternative that addresses the unique challenges of co-located users - unlike typical multi-user VR platforms where users connect from different locations, this system focuses on users sharing the same physical space.

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
- **Unity 2022.3+ LTS** - Primary development platform
- **C#** - Programming language
- **Meta Quest 3** - Target VR hardware (3 devices provided)

### VR Frameworks
- **Meta XR SDK** - Native Quest development support
- **XR Interaction Toolkit** or **OpenXR** - VR interaction systems
- **Meta Quest Shared Space API** - Multi-user co-location features

### Networking
- **Unity Netcode for GameObjects** - Networking solution
- **Meta Quest Platform SDK** - Social features and user management

### Version Control
- **GitHub** - Code management and collaboration
- Regular demonstrations throughout development

## Meta Quest Shared Space Integration

This project leverages Meta Quest's native shared space capabilities:

- **Automatic Space Sharing** - Quest headsets can automatically detect and share the same physical space
- **Multi-user Guardian** - Shared boundary system for multiple users
- **Spatial Anchors** - Persistent virtual object placement across sessions
- **User Awareness** - Visual indicators of other users' positions and boundaries

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
2. Open project in Unity
3. Install Meta XR SDK via Package Manager
4. Configure build settings for Android/Quest platform
5. Set up developer mode on Quest headsets

### Project Structure
```
├── Assets/
│   ├── Scripts/           # C# scripts for VR interactions
│   ├── Prefabs/          # Reusable game objects
│   ├── Scenes/           # Unity scenes
│   ├── Materials/        # Visual materials and textures
│   └── Networking/       # Network-related components
├── Documentation/        # Technical documentation
├── Tests/               # Unit and integration tests
└── README.md
```

## Development Milestones

### Phase 1: Foundation
- [ ] Unity project setup with Meta XR SDK
- [ ] Basic VR scene with Quest 3 support
- [ ] Shared space detection implementation

### Phase 2: Multi-user Core
- [ ] Multiple headset tracking validation
- [ ] Basic networking with Netcode for GameObjects
- [ ] Avatar synchronization system

### Phase 3: Safety & UX
- [ ] Collision prevention system
- [ ] Shared guardian boundary management
- [ ] User awareness indicators

### Phase 4: Applications
- [ ] Training scenario prototypes
- [ ] Educational use case development
- [ ] Performance optimization

## Safety Considerations

- **Physical Space Management** - Ensure adequate room size for 3 users
- **Collision Prevention** - Real-time user position awareness
- **Emergency Protocols** - Quick VR exit procedures
- **Guardian Boundaries** - Properly configured shared boundaries

## Contributing

1. Fork the repository
2. Create feature branches for new development
3. Follow C# coding standards
4. Test with multiple Quest headsets when possible
5. Document changes and submit pull requests

## Keywords

Virtual Reality, Co-located VR, Shared Physical Space, Multi-user Systems, Unity, Meta Quest 3, Shared Space, XR Development

## License

[Add your license information here]

## Contact

[Add project contact information]

---

*This project focuses on affordable co-located VR solutions, distinguishing itself from remote multi-user VR platforms by addressing the unique challenges and opportunities of users sharing the same physical space.*