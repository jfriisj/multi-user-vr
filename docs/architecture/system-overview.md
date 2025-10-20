[← Documentation Home](../README.md) · [← Architecture](README.md)

# System Overview

This page provides layered architecture views and high-level relationships between major subsystems for the co-located multi-user VR system (3 users, Meta Quest 3).

## Layered Architecture (Requirements 1.3)
```mermaid
flowchart TD
    subgraph L1[VR Input Layer]
      XRI[XR Interaction Toolkit]
      MXR[Meta XR SDK / OpenXR]
      XRHands[Unity XR Hands]
    end

    subgraph L2[Networking Layer]
      NGO[Netcode for GameObjects]
      UTP["Unity Transport (UDP)"]
    end

    subgraph L3[Safety System Layer]
      SafetyCore[Safety Coordinator]
      Proximity["Proximity / Collision Detection"]
      Guardian[Quest Guardian Integration]
      Alerts["Visual/Haptic Alerts"]
    end

    subgraph L4[Game Logic Layer]
      Avatar[Avatar Management]
      Interact[Object Interaction]
      Session[Session State]
    end

    subgraph L5[Research Instrumentation]
      Metrics[Metrics Tracking]
      Logging["Data Logging (JSON/CSV)"]
    end

    %% Relationships
    XRI --> Avatar
    MXR --> Avatar
    XRHands --> Avatar

    Avatar <--> NGO
    Interact <--> NGO
    NGO --- UTP

    Avatar --> SafetyCore
    Interact --> SafetyCore
    SafetyCore --> Proximity
    SafetyCore --> Guardian
    SafetyCore --> Alerts

    Avatar --> Metrics
    Interact --> Metrics
    Metrics --> Logging

    L1 --> L2
    L1 --> L3
    L2 --> L4
    L3 --> L4
    L4 --> L5
```

## Subsystem Overview
- VR Input Layer: Head/hand tracking, controller input via XR Interaction Toolkit and Meta XR/OpenXR.
- Networking Layer: Client-server model using Unity Netcode for GameObjects with Unity Transport.
- Safety System Layer: Independent collision prevention with Guardian integration and alerts.
- Game Logic Layer: Avatars, interactions, and session management.
- Research Layer: Metrics capture and local data logging for analysis.
