[← Documentation Home](../README.md) · [← Implementation](README.md)

# Component Relationships (Scene-Level)

Unity component dependencies and message flow to guide implementation. Complements the architecture diagrams.

## Scene Integration Map
```mermaid
flowchart LR
  subgraph XR[XR Origin]
    Head[Camera (HMD)]
    LCtrl[Left Controller]
    RCtrl[Right Controller]
    Move[ContinuousMoveProvider]
    Tele[TeleportationProvider]
  end

  Pose[PoseProvider]
  VRS[VRPlayerSync]
  PM[ProximityMonitor]
  SC[SafetyCoordinator]
  MG[MovementGate]
  DC[DataCollector]
  RM[RollingMetrics]
  NM[NetworkManager]
  NI[NetworkedInteractable]

  Head --> Pose
  LCtrl --> Pose
  RCtrl --> Pose
  Pose --> VRS
  VRS -.Remote Poses.-> PM
  Pose --> PM
  PM --> SC
  SC <--> MG
  Move --> MG
  Tele --> MG
  SC --> DC
  VRS --> DC
  NM --> VRS
  NM --> NI
```

Notes:
- PoseProvider centralizes transforms for both networking and safety
- MovementGate toggles locomotion/teleport providers under Critical state
- DataCollector listens to Safety/Networking samples (opt-in research mode)

## Prefab/Asset Links
- Player Prefab contains: XR Origin, NetworkObject, VRPlayerSync, PoseProvider, TrackingMonitors, MovementGate, SafetyCoordinator
- Bootstrap GameObject contains: NetworkManager, MultiUserVRManager, optional Debug UI
- Interactables: grabbable objects with `NetworkObject` + `NetworkedInteractable`

## Accuracy Notes
- This reflects the planned Unity structure in steering/structure.md (vr/Assets/*) and the MVP guides
- Update diagrams if the concrete project structure diverges during implementation
