# Product Overview

## Foundation alignment (Unity MR Multiplayer Tabletop)

This product now builds on Unity’s MR Multiplayer Tabletop template as the baseline for multiplayer, XR interactions, and sample content. The Unity project resides at `mr-multiplayer/` and includes template-provided scenes, prefabs, and packages (XR Interaction Toolkit, Netcode for GameObjects, AR Foundation, and Unity Services such as Authentication, Multiplayer, and Vivox).

- Template docs: https://docs.unity3d.com/Packages/com.unity.template.mr-multiplayer@1.0/manual/index.html
- Sample references we leverage: XRMP Basic scene, Tabletop game samples (sandbox/slingshot/chess assets)

## Product Purpose

The Co-located Multi-user VR System addresses the critical gap in affordable VR solutions for shared physical spaces. While existing multi-user VR platforms connect users from different locations, this product solves the unique challenges of multiple users sharing the same physical room while collaborating in a virtual environment.

The system enables **3 users with Meta Quest 3 headsets to safely operate in the same physical space** while experiencing synchronized virtual environments, addressing problems like tracking interference, collision prevention, and avatar-to-body alignment that don't exist in remote VR scenarios. The Unity MR Multiplayer template accelerates this by providing a tested multiplayer/XR foundation.

## Target Users

### Primary Users
- **Training Facilities**: Organizations providing hands-on VR training programs requiring group interaction and collaboration
- **Educational Labs**: Schools and universities with VR programs needing multi-student experiences in controlled physical spaces  
- **Team-building Venues**: Corporate training centers and event spaces offering collaborative VR experiences

### User Needs and Pain Points
- **Cost Barrier**: Existing solutions like VIROO Rooms require expensive custom installations
- **Safety Concerns**: Users can't see each other physically while immersed, creating collision risks
- **Technical Complexity**: Managing multiple headsets in close proximity with potential tracking interference
- **Space Constraints**: Maximize use of limited physical room space while ensuring user safety
- **Scalability**: Flexible solutions that don't require permanent installations

## Key Features

1. **Template-aligned multiplayer**: NGO-based client/server foundation with Unity Transport and optional UGS Lobby/Relay
2. **XR interactions**: XR Interaction Toolkit for hand/controller interactions across MR/AR/VR modes (we target VR)
3. **Co-located Multi-user Support**: Exactly 3 Quest 3 users, synchronized in real time
4. **Collision Prevention System**: Proximity detection, alerting, and movement gating for user safety
5. **Shared Boundary Management**: Guardian system integration and room configuration guidance
6. **Avatar Synchronization**: Real-time avatar representation aligned to physical posture
7. **Voice Chat (optional)**: Vivox included via Unity Services for low-latency comms
8. **Affordable Hardware**: Consumer Quest 3 devices; no custom room hardware required

## Business Objectives

- **Cost Reduction**: 10x+ savings vs custom multi-user VR installations
- **Market Accessibility**: Lower barrier for labs and facilities to deploy multi-user VR
- **Research Advancement**: Document co-located VR constraints and solutions
- **Educational Impact**: Provide clear, template-aligned learning resources
- **Knowledge Sharing**: Publish reusable patterns and documentation

## Success Metrics

- **User Safety**: Zero physical collisions during multi-user sessions
- **Technical Performance**: <20ms perceived latency for avatar synchronization between users
- **Tracking Reliability**: Identify and mitigate tracking interference
- **Cost Effectiveness**: Total system cost <$2000 (3x Quest 3 + development)
- **Educational Goals**: Milestone completion with regular demos
- **Research Output**: Documented findings and mitigation strategies

## Product Principles

1. **Safety First**: Safety systems are first-class and override other features when needed
2. **Template-first Development**: Prefer template components/patterns; extend rather than reinvent
3. **Affordability Over Features**: Use consumer hardware and built-in services
4. **Co-location Advantages**: Design around in-room collaboration benefits
5. **Educational Transparency**: Document choices, trade-offs, and procedures

## Monitoring & Visibility

- **Dashboard Type**: Unity in-app monitoring (VR and desktop views)
- **Real-time Updates**: User proximity/safety, network state, tracking quality
- **Key Metrics**: Latency, packet loss, tracking status, boundary status, safety events
- **Sharing**: Research data export, demo capture, incident logging

## Future Vision

This project serves as a foundation for advancing affordable co-located VR experiences based on a standardized Unity template.

### Potential Enhancements

- **Remote Access**: Remote monitoring/guidance for co-located sessions
- **Analytics**: Historical metrics, safety incident trends
- **Collaboration**: Rich shared object manipulation and tools
- **Scalability**: 4+ users and larger rooms
- **Cross-Platform**: Beyond Quest 3
- **Commercialization**: Packaging/licensing options

### Research Contributions

- Tracking interference characterization and mitigation
- Co-location vs remote VR comparative analysis  
- Safety design patterns for shared physical VR
- Cost-benefit frameworks for adoption
- Open, template-aligned reference implementation
