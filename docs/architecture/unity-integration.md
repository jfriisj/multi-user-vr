[← Documentation Home](../README.md) · [← Architecture](README.md)

# Unity Integration (Prefab · Scenes · Assets)

Unity-focused diagrams reflecting the established project conventions and planned structure (Req 1.4, 2.4). These support developer navigation and setup.

## Asset Organization (planned `vr/Assets/`)
```mermaid
flowchart TD
  A[Assets/] --> S[Scripts/]
  A --> P[Prefabs/]
  A --> SC[Scenes/]
  A --> M[Materials/]
  A --> ST[Settings/]
  S --> SNet[Networking/]
  S --> SVR[VR/]
  S --> SSaf[Safety/]
  S --> SGame[GameLogic/]
  S --> SRes[Research/]
  S --> SUtil[Utils/]
  P --> PPlayer[Player/]
  P --> PEnv[Environment/]
  P --> PUI[UI/]
  P --> PNet[Networking/]
  SC --> Main[MainScene.unity]
  SC --> Tests[TestScenes/]
```

Notes:
- Scripts namespaces follow `MultiUserVR.*` (Networking, VR.Input, Safety, GameLogic, Research, Utils)
- Player Prefab lives under `Prefabs/Player/`; networked props under `Prefabs/Networking/`

## Player Prefab Composition (XR Origin extended)
```mermaid
flowchart TD
  subgraph PlayerPrefab[Player Prefab]
    XRO["XR Origin (Action-based)"]
    NO[NetworkObject]
    VRS[VRPlayerSync]
    PP[PoseProvider]
    TMH["TrackingMonitor: Head"]
    TML["TrackingMonitor: Left"]
    TMR["TrackingMonitor: Right"]
    MG[MovementGate]
    SCH[SafetyCoordinator]
  end
  subgraph Avatar[Avatar Mesh Targets]
    RH[remoteHead]
    RL[remoteLeft]
    RR[remoteRight]
  end
  VRS --> RH
  VRS --> RL
  VRS --> RR
  PP --> VRS
  TMH --> SCH
  TML --> SCH
  TMR --> SCH
  MG <--> SCH
  NO --> VRS
```

Key points:
- XR Origin provides local head/hand transforms to PoseProvider and VRPlayerSync
- NetworkObject enables NGO spawning/ownership; VRPlayerSync writes/reads pose data
- SafetyCoordinator subscribes to Proximity/Safety events and controls MovementGate

## Bootstrap & Spawn Flow (MainScene)
```mermaid
sequenceDiagram
  participant Boot as Bootstrap(GameObject)
  participant NM as NetworkManager
  participant Mgr as MultiUserVRManager
  participant Host as Host Device
  participant Client as Client Device

  Boot->>NM: Configure Transport + Player Prefab
  Boot->>Mgr: Initialize UI (Start Host/Join)
  Host->>NM: StartHost()
  NM-->>Host: Spawn Player Prefab (owner=Host)
  Client->>NM: StartClient()
  NM-->>Client: Spawn Player Prefab (owner=Client)
  Note over Host,Client: VRPlayerSync streams poses<br/>Safety monitors proximity
```
