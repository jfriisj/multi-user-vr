# Requirements Document

## Introduction

Session Management UI enables dual-mode multiplayer session control for co‑located Quest 3 users: Cloud (UGS Lobby/Relay) and LAN Direct (Unity Transport without internet). It exposes clear Host/Join/Leave/Disconnect controls, live status, and a player list, working both in-Editor and on device.

## Alignment with Product Vision

- Template-first: Extends MR Multiplayer Tabletop template flows without breaking existing Cloud path.
- Co‑located focus: Adds LAN‑only operation so 3 local headsets can connect with no internet.
- Safety/Usability: Prominent in‑session Disconnect and clear state feedback to reduce confusion.

## Requirements

### R1: Dual connection mode selector (Cloud vs LAN Direct)

User Story: As a facilitator or player, I want to choose Cloud or LAN Direct from the lobby UI so I can connect either via UGS services or purely over local Wi‑Fi without internet.

Acceptance Criteria
1. WHEN the user opens the session UI THEN the UI SHALL present a selectable mode: “Cloud (UGS)” and “LAN Direct”.
2. WHEN the user toggles modes THEN the selection SHALL take effect without reloading/restarting the scene.
3. WHEN mode changes THEN subsequent Host/Join actions SHALL use the selected path (UGS Lobby/Relay vs Unity Transport direct).
4. The default mode SHALL be Cloud (UGS) to preserve current behavior; mode can be switched at any time prior to connecting.

### R2: Host/Join/Leave controls available in Editor and on device

User Story: As a user, I want Host, Join, and Leave buttons that work the same in the Unity Editor and on the Quest 3 so I can test locally and use in the field.

Acceptance Criteria
1. WHEN in Cloud mode AND pressing Host THEN the app SHALL authenticate (if needed), create Lobby and Relay allocation, and start Host via XRINetworkGameManager.
2. WHEN in LAN mode AND pressing Host THEN the app SHALL start a local server/host using Unity Transport direct, listening on the configured port.
3. WHEN Join is pressed in Cloud mode THEN the app SHALL list/join a lobby (quick join or code) and connect via Relay.
4. WHEN Join is pressed in LAN mode THEN the app SHALL connect directly to the provided IP:Port over LAN using Unity Transport without UGS.
5. WHEN Leave is pressed (not in active session) THEN the app SHALL return to the lobby panel; WHEN in active session THEN the app SHALL show prominent Disconnect control (see R3).
6. Controls SHALL be interactable in-world (VR) and via standard UI in Editor; input bindings SHALL be compatible with XR Device Simulator.

### R3: Prominent in‑session Disconnect/Leave control

User Story: As a user, I need an always‑visible and accessible Disconnect/Leave control during an active session so I can exit quickly if needed.

Acceptance Criteria
1. WHEN connected (Host or Client) THEN a prominent Disconnect/Leave control SHALL be visible in the main in‑session UI.
2. WHEN Disconnect is pressed THEN the app SHALL disconnect within 5 seconds and transition UI to the pre‑session (lobby) state.
3. The control SHALL be accessible via VR hand/controller interactions and in Editor UI.

### R4: Player list with role badges and local highlight

User Story: As a player, I want to see who’s in the session with Host/Client roles and a highlight for me, so I understand roles and presence.

Acceptance Criteria
1. WHEN on lobby panel (pre‑connect) or in‑session panel (post‑connect) THEN the player list UI SHALL show players and roles (Host/Client).
2. The local player entry SHALL be visually highlighted and labeled (e.g., “You”).
3. The list SHALL update within 500 ms of player join/leave events.

### R5: Connection status, busy/loading, and error/timeout feedback

User Story: As a user, I need clear status while connecting/disconnecting and actionable error messages if something fails.

Acceptance Criteria
1. WHEN starting Host/Join/Disconnect THEN the UI SHALL show a busy/loading state (spinner or disabled buttons) and status text (e.g., “Connecting…”, “Starting host…”, “Disconnecting…”).
2. WHEN an error occurs (auth failure, lobby join failure, transport failure) THEN the UI SHALL show an error message and enable actions to retry or change mode.
3. WHEN connection attempts exceed 5 seconds THEN the UI SHALL time out with an error message and reset relevant buttons.
4. Status and error messages SHALL be concise and non‑blocking; no modal dialogs that block core input.

### R6: Smooth manager handoff per mode

User Story: As a developer, I want the UI wired to route connection actions to XRINetworkGameManager for Cloud and to LANConnectionManager for LAN, so paths are clean and independent.

Acceptance Criteria
1. WHEN in Cloud mode THEN Host/Join/Leave SHALL call into XRINetworkGameManager (and existing Lobby/Relay flow) without regressions.
2. WHEN in LAN mode THEN Host/Join/Leave SHALL call into LANConnectionManager to start/stop Host/Client via Unity Transport direct.
3. Manager events (OnConnected, OnDisconnected, OnClientConnected/Disconnected, OnTransportFailure) SHALL update the UI state promptly (<100 ms UI reflection target).

### R7: LAN Direct requires no internet; Cloud flow remains intact

User Story: As a facilitator, I need LAN Direct to work completely offline while keeping Cloud flows available when internet is present.

Acceptance Criteria
1. WHEN LAN mode is selected AND device has no internet THEN Host/Join SHALL succeed over local Wi‑Fi using Unity Transport direct (given correct IP reachability).
2. WHEN Cloud mode is selected THEN the existing UGS Authentication/Lobby/Relay path SHALL continue to function as today.
3. Switching modes SHALL not require a scene reload nor affect the other path’s configuration.

## Non-Functional Requirements

### Code Architecture and Modularity
- Single Responsibility: UI panels/components isolate mode selection, connection actions, status display, and player list rendering.
- Clear Interfaces: XRINetworkGameManager and LANConnectionManager expose minimal, consistent methods/events for the UI to bind.
- Template Alignment: Reuse and extend existing LobbyUI and managers; avoid duplicating logic already present in the template.

### Performance
- Connect/Disconnect time: 5 seconds maximum end‑to‑end including UI state transitions (R3/R5).
- UI responsiveness: Status updates reflect network events within 100 ms on average.
- Memory/GC: No GC spikes during connect/disconnect; target <100 KB transient allocations and <2 ms GC on connect/disconnect operations (profiled on Quest 3 and in Editor). Reuse UI objects (pooling/disable‑enable) to avoid allocations.

### Security
- Cloud mode continues to use UGS auth/relay as configured by the template; no secrets in logs/UI.
- LAN mode avoids exposing sensitive info beyond local IP/port needed to join.

### Reliability
- Graceful handling of failures and timeouts with clear retry paths.
- Clean teardown on Disconnect: all network objects and subscriptions released; UI returns to a stable pre‑session state.

### Usability
- Works with VR controllers and XR Device Simulator.
- Prominent Disconnect in active session; clear “mode” indicator visible in lobby.
- Minimal text; concise actionable error messages.

## Assumptions & Dependencies
- Existing Cloud path is functional via XRINetworkGameManager and Lobby/Relay.
- A new LANConnectionManager will provide direct Unity Transport host/join and events suitable for UI binding.
- Files to be modified/integrated during implementation:
  - Modify: Assets/MRTabletopAssets/Scripts/UI/LobbyList/LobbyUI.cs
  - Modify: Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs
  - Read/Integrate: Assets/XRMP/Scripts/Network/NetworkManagers/NetworkManagerXRMultiplayer.cs
  - Integrate with: Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionManager.cs (for LAN mode)

## Out of Scope
- LAN host discovery and QR scan (future enhancement); initial join is manual IP:Port entry.
- Host migration; voice chat changes.
