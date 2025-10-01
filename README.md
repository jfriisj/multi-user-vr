# Co-located Multi-user VR System

An affordable VR system that enables multiple users to share the same physical room while collaborating in a virtual environment, addressing the unique challenges of co-located VR experiences.

## 🎯 Project Overview

### Case Description Enterprise

VR rooms are transforming physical spaces into shared multi-user VR environments where teams can train together, with companies like Virtualware deploying over 32 VIROO Rooms worldwide for industries from railways to military training. However, most solutions require expensive custom installations. 

This project aims to develop an affordable VR system that enables multiple users to share the same physical room while collaborating in a virtual environment. Unlike typical multi-user VR platforms where users connect from different locations, this system addresses the unique challenges of co-located users. 

**Target Customers:**
- Training facilities
- Educational labs
- Team-building venues
- Organizations seeking shared-space VR experiences

### The Challenge

The main challenge is creating a stable multi-user VR system where **3 users can operate in the same physical room**. While controllers are paired to individual headsets so there's no signal mixing, we must investigate whether Meta Quest 3's inside-out tracking handles multiple headsets in close proximity without interference.

**Key Technical Challenges:**
- Managing shared physical boundaries
- Preventing collisions between users who can't see each other
- Ensuring virtual avatars properly align with users' actual positions
- Implementing basic networking to synchronize the virtual environment
- Maintaining awareness of real-world space constraints
- Identifying use cases that benefit from physical co-location vs. remote connection

## 🔧 Technology Stack

- **Platform:** Meta Quest 3 (3 devices)
- **Game Engine:** Unity
- **Programming Language:** C#
- **Networking:** Unity's Netcode for GameObjects
- **VR Framework:** XR Interaction Toolkit or OpenXR
- **Version Control:** GitHub

## 🚀 Getting Started

### Prerequisites

- Unity 2022.3+ LTS
- Meta Quest 3 headsets (3 units)
- Development PC with VR capabilities
- Git for version control

### Installation

1. Clone the repository:
```bash
git clone https://github.com/[username]/co-located-vr-system.git
cd co-located-vr-system
```

2. Open the project in Unity Hub

3. Install required packages:
   - XR Interaction Toolkit
   - Netcode for GameObjects
   - Meta XR SDK (if using Meta-specific features)

4. Configure build settings for Android (Meta Quest 3)

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── Networking/
│   ├── VR/
│   └── Utils/
├── Prefabs/
│   ├── Player/
│   ├── Environment/
│   └── UI/
├── Scenes/
├── Materials/
├── Audio/
└── Documentation/
```

## 🎮 Features

- [ ] **Multi-headset Tracking**: Investigation and implementation of interference-free tracking
- [ ] **Collision Prevention**: Safety systems to prevent physical collisions
- [ ] **Avatar Synchronization**: Real-time avatar positioning and movement sync
- [ ] **Shared Boundaries**: Management of physical room constraints
- [ ] **Network Architecture**: Stable multi-user networking solution
- [ ] **Safety Protocols**: User safety in shared physical space

## 🧪 Research Areas

### Tracking Interference Investigation
- Test Meta Quest 3's inside-out tracking with multiple headsets
- Document any signal interference or tracking degradation
- Develop mitigation strategies if issues are found

### Use Case Analysis
- Identify scenarios where co-location provides advantages over remote VR
- Document training applications and educational use cases
- Analyze cost-benefit compared to existing solutions

## 📊 Development Milestones

1. **Phase 1: Setup & Research**
   - Unity project initialization
   - Tracking interference testing
   - Basic networking setup

2. **Phase 2: Core Implementation**
   - Multi-user synchronization
   - Collision prevention system
   - Avatar alignment

3. **Phase 3: Testing & Optimization**
   - Multi-headset testing
   - Performance optimization
   - Safety validation

4. **Phase 4: Documentation & Demos**
   - Use case documentation
   - Demo preparation
   - Final presentation

## 🔍 Keywords

Virtual Reality, Co-located VR, Shared Physical Space, Multi-user Systems, Unity, Meta Quest 3, Inside-out Tracking, Netcode for GameObjects, XR Interaction Toolkit

## 🤝 Contributing

This is an educational project developed as part of [Course/Program Name]. Contributions should follow the established development workflow with regular demonstrations throughout the semester.

### Development Workflow
1. Create feature branches for new functionality
2. Test with physical hardware when possible
3. Document findings and decisions
4. Regular code reviews and demonstrations

## 📝 Documentation

- [Technical Documentation](docs/technical.md)
- [Testing Protocols](docs/testing.md)
- [Safety Guidelines](docs/safety.md)
- [Use Case Studies](docs/use-cases.md)

## ⚠️ Safety Considerations

When working with multiple users in the same physical space:
- Establish clear physical boundaries
- Implement guardian/boundary systems
- Test collision prevention thoroughly
- Maintain awareness of real-world obstacles

## 📄 License

[Add appropriate license for your project]

## 📞 Contact

[Your contact information or team details]

---

**Note:** This project is part of an educational initiative to explore affordable alternatives to expensive commercial VR room solutions. All testing should prioritize user safety in shared physical environments.
