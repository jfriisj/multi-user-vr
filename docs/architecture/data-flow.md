[← Documentation Home](../README.md) · [← Architecture](README.md)

# Data Flow Diagrams

Focused data flows for tracking, interaction, and safety pipelines. Diagrams are intentionally modular for readability.

## Head/Hand Tracking Synchronization
```mermaid
flowchart LR
  XR["XRI Input (Head/Hands)"] --> VRSync[VRPlayerSync]
  VRSync --> AvSys[Avatar System]
  AvSys --> NetMgr[NGO NetworkManager]
  NetMgr -->|Broadcast| Clients[Other Clients]
  Clients --> RemoteAvatar[Remote Avatar Renders]
```

## Networked Object Interaction
```mermaid
flowchart TD
  Grab[XR Grab Event] --> Request[Request Ownership]
  Request --> Host[Host Authority]
  Host --> Grant[Grant Ownership]
  Grant --> Move[Local Transform Updates]
  Move --> Replicate[NGO Replication]
  Replicate --> Others[Other Clients Update]
```

## Safety Event Pipeline
```mermaid
flowchart TD
  Pose["User Poses (3 users)"] --> Prox[Proximity Monitor]
  Prox --> SafetyCore[Safety Coordinator]
  SafetyCore --> Decision[Risk Assessment]
  Decision -->|Low| Visual[Visual Indicator]
  Decision -->|Medium| Haptic[Haptic Warning]
  Decision -->|High| Restrict[Movement Restriction]
  SafetyCore --> Metrics[Metrics Collector]
  Metrics --> Logger["Data Logger (JSON/CSV)"]
```
