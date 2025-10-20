# Requirements Document

> Template alignment: The project now uses Unity’s MR Multiplayer Tabletop template (`mr-multiplayer/`). Any auto‑configure tooling must extend or integrate with the template’s prefabs/managers (e.g., XRMP Network Manager) rather than duplicating functionality. Prefer configuring existing template assets and settings.

## Introduction

Provide a one-click Auto-Configure capability that prepares a Unity scene for multi-user VR using Unity Netcode for GameObjects (NGO) and XR Interaction Toolkit. The tool wires required GameObjects/components, applies safe defaults, and validates the scene so developers can start a 3‑user co-located session rapidly and safely.

## Alignment with Product Vision

This feature accelerates setup for the co‑located multi-user MVP by enforcing the tech stack (Unity 2022.3 LTS, XRI, NGO with Unity Transport) and safety-first architecture. It reduces technical complexity (a key pain point) and supports educational/research objectives by making the baseline scene trivial to create consistently.

## Requirements

### Requirement 1 — Scene Auto-Configuration Command

**User Story:** As a Unity developer, I want a single command to auto‑configure my scene for multi‑user VR so that I can start host/client sessions without manual wiring.

#### Acceptance Criteria

1. WHEN the developer triggers "Tools/MultiUserVR/Auto‑Configure Scene" THEN the system SHALL create a Bootstrap GameObject (if missing) with NetworkManager and MultiUserVRManager components.
2. IF NetworkManager exists THEN the system SHALL configure Unity Transport (UDP) with default port 7777 (configurable).
3. WHEN auto‑configure runs THEN the system SHALL be idempotent (no duplicate components/objects on repeated runs) and SHALL support Unity Undo for all changes.

### Requirement 2 — Player/XR Origin Wiring

**User Story:** As a Unity developer, I want my XR Origin wired for networking and safety so that avatars synchronize and safety systems run consistently.

#### Acceptance Criteria

1. IF an XR Origin is present AND lacks required components THEN the system SHALL add: NetworkObject, VRPlayerSync, PoseProvider, TrackingMonitors (Head, Left, Right), MovementGate, SafetyCoordinator.
2. WHEN components are added THEN the system SHALL auto‑link references between PoseProvider → VRPlayerSync and TrackingMonitors → SafetyCoordinator.
3. IF multiple XR Origins exist THEN the system SHALL prompt/choose one for local player wiring and SHALL not modify others.

### Requirement 3 — Player Prefab and Network Settings

**User Story:** As a Unity developer, I want correct network/player prefab settings so that NGO spawns players correctly for host and clients.

#### Acceptance Criteria

1. WHEN auto‑configure completes THEN NetworkManager.PlayerPrefab SHALL reference the Player Prefab (XR Origin + required components) or a newly created default under Assets/Prefabs/Player/ if none exists.
2. IF a default Player Prefab is created THEN the system SHALL place it under Assets/Prefabs/Player/ using project naming conventions and SHALL not overwrite an existing asset.
3. WHEN starting Host/Client from the debug UI THEN Player Prefabs SHALL spawn with ownership assigned to the connecting client.

### Requirement 4 — Session Management UI (Debug)

**User Story:** As a developer, I want a basic in‑scene UI to start/stop host/client so that I can validate networking quickly.

#### Acceptance Criteria

1. WHEN auto‑configure runs THEN the system SHALL add a lightweight debug UI (if missing) with Start Host, Start Client, and Disconnect actions wired to NetworkManager.
2. IF the project defines a custom UI prefab THEN the system SHALL use it; ELSE it SHALL create a minimal Canvas‑based UI under Bootstrap.

### Requirement 5 — Scene Validation and Reporting

**User Story:** As a developer, I want validation and a summary of actions so that I can trust the scene state.

#### Acceptance Criteria

1. WHEN auto‑configure finishes THEN the system SHALL display a summary of created/updated objects and any warnings for missing safety‑critical items.
2. WHEN validation detects conflicts (e.g., multiple NetworkManagers, missing XR Origin) THEN the system SHALL provide actionable guidance without making destructive changes.

## Non-Functional Requirements

### Code Architecture and Modularity
- Single Responsibility: Editor tooling isolated from runtime components.
- Modular Design: Auto‑configure steps implemented as composable validators/fixers.
- Clear Interfaces: Editor API for detection, creation, and wiring with testable units.

### Performance
- Editor‑only operations SHALL complete in < 2 seconds on a clean scene on a typical development machine.

### Security
- No runtime network secrets are created; default transport uses localhost/LAN with developer‑set IP.

### Reliability
- Idempotent operations; safe to re‑run any time.
- Full Undo support for all changes.

### Usability
- Menu entry: Tools/MultiUserVR/Auto‑Configure Scene.
- Clear console/log output summarizing actions and validation results.
