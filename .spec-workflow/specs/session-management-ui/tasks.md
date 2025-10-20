# Tasks Document

Spec: session-management-ui

- [ ] 1. Wire connection mode toggle in UI and scene
  - Files: Assets/MRTabletopAssets/Scripts/UI/LobbyList/LobbyUI.cs, Assets/MRTabletopAssets/Scripts/UI/LAN/LANConnectionUI.cs, Assets/XRMP/Scripts/Network/NetworkManagers/ConnectionModeManager.cs
  - Scene/Prefab: Ensure Lobby panel contains a Toggle bound to ConnectionModeManager and a label; confirm LAN sub-panel exists and is mapped via m_ConnectionSubPanels (LANPanelIndex)
  - Ensure mode switch does not reload scene; verify disconnect-before-switch behavior
  - _Leverage: ConnectionModeManager.SetConnectionMode, LobbyUI.OnConnectionModeToggleChanged, LANConnectionUI.OnConnectionModeToggled_
  - _Requirements: R1, Constraints (toggle without restart)_
  - _Prompt: Implement the task for spec session-management-ui, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity XR/NGO UI Engineer | Task: Bind a UI Toggle to ConnectionModeManager so users can switch Cloud/LAN without scene reload; ensure correct sub-panel activation and label updates; verify event subscriptions and unsubscriptions | Restrictions: Do not duplicate mode logic in UI; use ConnectionModeManager events; avoid GC allocations in hot paths | _Leverage: LobbyUI.cs, LANConnectionUI.cs, ConnectionModeManager.cs | _Requirements: R1, toggle without restart | Success: Toggle switches modes instantly; UI panels/labels update; no leaks; switching while disconnected is safe; logs show mode change | Tools: Assets_Find, Script_Read, Script_CreateOrUpdate, Assets_Modify, Scene_GetHierarchy, Editor_Selection_Get, Assets_Refresh, Console_GetLogs, TestRunner_Run

- [ ] 2. Prominent in-session Disconnect/Leave control
  - Files: LANConnectionUI.cs, LobbyUI.cs, XRINetworkGameManager.cs, ConnectionModeManager.cs
  - Add a persistent, prominent Disconnect button visible during Connected; route to ConnectionModeManager.Disconnect()
  - Ensure disconnect completes <5s and UI returns to lobby panel; show status "Disconnecting..."
  - _Leverage: XRINetworkGameManager.DisconnectAsync, ConnectionModeManager.Disconnect, LANConnectionManager.Disconnect_
  - _Requirements: R3, Constraints (5s disconnect)_
  - _Prompt: Implement the task for spec session-management-ui, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity UI Engineer | Task: Add a clearly visible Disconnect button in active session canvases (VR + Editor) and wire to ConnectionModeManager.Disconnect; show progress and handle completion/timeout within 5s | Restrictions: No modal blocking dialogs; keep interaction accessible in VR; avoid per-frame allocations | _Leverage: LANConnectionUI.OnDisconnectButtonClicked, XRINetworkGameManager.DisconnectAsync | _Requirements: R3 | Success: Button visible and usable in VR and Editor; disconnects under 5s; UI returns to lobby; no dangling subscriptions | Tools: Script_Read, Script_CreateOrUpdate, Assets_Find, Assets_Modify, Scene_GetHierarchy, Editor_Selection_Get, Assets_Refresh, Console_GetLogs, TestRunner_Run

- [ ] 3. Host/Join/Leave handlers for both modes
  - Files: LobbyUI.cs (Cloud path), LANConnectionUI.cs (LAN path), XRINetworkGameManager.cs
  - Ensure Cloud buttons call XRINetworkGameManager (Create/Join/QuickJoin/Cancel); ensure LAN buttons call LANConnectionManager.HostLAN/JoinLAN/Disconnect
  - Add input validation for IP:Port; default port 7777; in Editor and device
  - _Leverage: XRINetworkGameManager cloud methods, LANConnectionManager HostLAN/JoinLAN_
  - _Requirements: R2, R6, R7_
  - _Prompt: Implement the task for spec session-management-ui, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Networking Engineer | Task: Hook up Host/Join/Leave for Cloud and LAN paths using existing managers; add IP validation and feedback; maintain identical behavior on device and in-Editor | Restrictions: Do not block Cloud flow; no UGS calls in LAN path; keep UI responsive | _Leverage: LobbyUI, LANConnectionUI, XRINetworkGameManager, LANConnectionManager | _Requirements: R2, R6, R7 | Success: All buttons work per mode; validation errors shown; LAN path works offline | Tools: Script_Read, Script_CreateOrUpdate, Assets_Refresh, Console_GetLogs, TestRunner_Run

- [ ] 4. Status, loading, and error feedback wiring
  - Files: LobbyUI.cs, LANConnectionUI.cs, XRINetworkGameManager.cs, LANConnectionManager.cs
  - Display Connecting/Starting host/Disconnecting; show timeout at 5s; surface error messages from both managers
  - Ensure UI reflects BindableVariables and events within ~100 ms
  - _Leverage: XRINetworkGameManager.connectionUpdated/connectionFailedAction, LANConnectionManager.OnStatusChanged/OnConnectionFailed_
  - _Requirements: R5, Constraints (5s)_
  - _Prompt: Implement the task for spec session-management-ui, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity UI Engineer | Task: Bind status text/spinner to manager events for both modes; add 5s timeout handling; ensure prompt UI updates | Restrictions: No GC spikes; reuse TMP components; avoid allocations in event handlers | _Leverage: existing events in managers | _Requirements: R5 | Success: Status transitions visible; timeout handled; errors actionable; updates within 100 ms | Tools: Script_Read, Script_CreateOrUpdate, Console_GetLogs, TestRunner_Run

- [ ] 5. Player list with role badges and local highlight
  - Files: Assets/MRTabletopAssets/Scripts/UI/PlayerList/PlayerListUI.cs (modify if needed), XRINetworkGameManager.cs
  - Ensure Host/Client badges, highlight local entry ("You"), updates within 500 ms on join/leave
  - _Leverage: XRINetworkGameManager.playerStateChanged, Connected bindable, existing PlayerListUI patterns_
  - _Requirements: R4_
  - _Prompt: Implement the task for spec session-management-ui, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity UI Engineer | Task: Verify/extend PlayerList UI to show roles and local highlight, update reactively | Restrictions: No per-frame polling; event-driven updates only; minimize allocations | _Leverage: PlayerListUI.cs, XRINetworkGameManager events | _Requirements: R4 | Success: Correct roles shown; local player highlighted; list updates promptly | Tools: Script_Read, Script_CreateOrUpdate, Console_GetLogs, TestRunner_Run

- [ ] 6. Ensure smooth handoff between managers
  - Files: XRINetworkGameManager.cs, ConnectionModeManager.cs, LANConnectionManager.cs
  - Verify Cloud path remains intact; LAN events update connection state and UI; mode switch triggers disconnect then new path
  - _Leverage: OnLANConnectionSuccess/Failed/StatusChanged in XRINetworkGameManager, ConnectionModeManager.OnModeChanged_
  - _Requirements: R6, R7_
  - _Prompt: Implement the task for spec session-management-ui, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Networking Engineer | Task: Validate and refine manager interactions so mode handoff is clean with no regressions | Restrictions: Do not introduce cross‑mode side effects; keep responsibilities separated | _Leverage: Existing event handlers | _Requirements: R6, R7 | Success: Both modes operate independently; switching works reliably | Tools: Script_Read, Script_CreateOrUpdate, Console_GetLogs, TestRunner_Run

- [ ] 7. Performance pass: allocations and time-to-connect/disconnect
  - Files: LobbyUI.cs, LANConnectionUI.cs, XRINetworkGameManager.cs, LANConnectionManager.cs
  - Profile connect/disconnect for GC; pool temporary strings/UI where feasible; verify <5s connect/disconnect on LAN and Cloud
  - _Leverage: Unity Profiler, Multiplayer Tools; reuse TMP components; avoid string.Format in hot paths_
  - _Requirements: NFR Performance, Constraints_
  - _Prompt: Implement the task for spec session-management-ui, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Performance Engineer | Task: Profile and eliminate avoidable allocations during connect/disconnect; confirm <5s transitions | Restrictions: No behavior regressions; keep code readability | _Leverage: Profiler; existing events | _Requirements: NFR Performance | Success: No GC spikes; time limits met on Editor and Quest 3 | Tools: Console_GetLogs, Script_Execute, TestRunner_Run

- [ ] 8. Editor and device test matrix
  - Steps: ParrelSync or Multiplayer Play Mode (Editor), 2–3 Quest 3 devices (LAN with no internet), Cloud path sanity
  - Record results and issues; ensure parity between Editor and device UX
  - _Leverage: MultiUser-VR-MVP-Guide.md, docs/mvp-action-plan.md_
  - _Requirements: R2, R7_
  - _Prompt: Implement the task for spec session-management-ui, first run spec-workflow-guide to get the workflow guide then implement the task: Role: QA Engineer | Task: Execute test matrix for both modes across Editor and devices; capture timing and errors; verify offline LAN works | Restrictions: Use same port; same Wi‑Fi; disable internet for LAN tests | _Leverage: Guide/docs | _Requirements: R2, R7 | Success: All tests pass; offline LAN functional; timings within limits; issues logged | Tools: TestRunner_Run, Console_GetLogs, Editor_GetApplicationInformation

- [ ] 9. Scene/prefab wiring verification checklist
  - Ensure: ConnectionModeManager, XRINetworkGameManager, LANConnectionManager, NetworkManagerXRMultiplayer, LANConnectionUI, LobbyUI are present in scene
  - Ensure: UnityTransport on NetworkManagerXRMultiplayer; default port 7777; LAN panel registered in LobbyUI
  - _Leverage: Scene hierarchy in docs/spec-features-revised.md_
  - _Requirements: R1–R7_
  - _Prompt: Implement the task for spec session-management-ui, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Integrator | Task: Verify scene composition and prefabs include all required managers and UI with correct references | Restrictions: Do not duplicate managers; prefer singletons where designed | _Leverage: Scene hierarchy, prefab inspectors | _Requirements: All | Success: Scene loads with all components; no missing refs/warnings | Tools: Scene_GetLoaded, Scene_GetHierarchy, Assets_Find, GameObject_Find, Editor_Selection_Get
