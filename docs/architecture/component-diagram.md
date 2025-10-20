[← Documentation Home](../README.md) · [← Architecture](README.md)

# Component Relationships

Readable component-level interactions grouped by responsibility (Requirements 1.1). Diagrams are split to avoid excessive complexity.

## Core Runtime Components
```mermaid
flowchart LR
  subgraph VR[VR Subsystem]
    XROrigin[XR Origin]
    InputMgr[Controller/Input Manager]
    VRPlayerSync[VRPlayerSync]
  end

  subgraph NET[Networking]
    NetworkManager[NGO NetworkManager]
    Spawn[Network Spawn System]
    NetObj[NetworkObject Components]
  end

  subgraph SAFETY[Safety]
    SafetyCoord[Safety Coordinator]
    Prox[Proximity Monitor]
    Warn[Warning UI/Haptics]
  end

  subgraph GAME[Game Logic]
    MultiUserMgr[MultiUserVRManager]
    Interactable[NetworkedInteractable]
    AvatarSys[Avatar System]
  end

  subgraph RESEARCH[Research]
    Metrics[Metrics Collector]
    Logger[Data Logger]
  end

  %% Links
  XROrigin --> VRPlayerSync
  InputMgr --> VRPlayerSync
  VRPlayerSync --> AvatarSys

  AvatarSys <--> NetworkManager
  Interactable <--> NetObj
  NetObj --> Spawn
  NetworkManager --> Spawn

  AvatarSys --> SafetyCoord
  Interactable --> SafetyCoord
  SafetyCoord --> Prox
  SafetyCoord --> Warn

  AvatarSys --> Metrics
  Interactable --> Metrics
  Metrics --> Logger
```

## Ownership and Authority (Object Interaction)
```mermaid
sequenceDiagram
  participant Local as Local Client
  participant Host as Host/Server
  participant Remote as Remote Client

  Local->>Local: Grab request on Interactable
  Local->>Host: NGO Ownership Request
  Host-->>Local: Ownership Granted
  Local->>Host: Transform updates (NetVar/RPC)
  Host-->>Remote: Replicate state
  Remote->>Remote: Render movement
```
