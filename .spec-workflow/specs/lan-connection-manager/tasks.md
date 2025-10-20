# Tasks Document

- [x] 1. Create core LANConnectionManager component in Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionManager.cs
  - File: Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionManager.cs
  - Implement main coordinator for direct IP-based network connections
  - Add methods: HostLAN(), JoinLAN(), GetLocalIPAddress()
  - Configure Unity Transport for host and client modes
  - Purpose: Provide core functionality for LAN connections bypassing UGS services
  - _Leverage: Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs, Assets/XRMP/Scripts/Network/NetworkManagers/NetworkManagerXRMultiplayer.cs_
  - _Requirements: 1.1, 1.2, 6.1_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Network Developer specializing in Netcode for GameObjects and Unity Transport | Task: Create LANConnectionManager component following requirements 1.1, 1.2, and 6.1, integrating with existing XRINetworkGameManager and NetworkManagerXRMultiplayer patterns from the Unity MR Multiplayer template | Restrictions: Must not modify existing UGS cloud functionality, maintain compatibility with existing NetworkBehaviours, do not bypass Unity Transport security features, follow template coding conventions | _Leverage: Existing network manager patterns, Unity Transport component configuration, NetworkManager state management | Unity-MCP Tools: Use Script_Read to examine XRINetworkGameManager.cs and NetworkManagerXRMultiplayer.cs patterns, grep_search to find existing NetworkManager implementations and Unity Transport configurations, Component_GetAll to identify network components, Script_CreateOrUpdate to create LANConnectionManager.cs, Assets_Refresh after script creation, and Console_GetLogs to verify compilation success | Success: Component successfully hosts and joins LAN sessions, integrates seamlessly with existing network architecture, all existing multiplayer features work identically. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._

- [x] 2. Create IPDiscoveryService utility in Assets/XRMP/Scripts/Network/Utils/IPDiscoveryService.cs
  - File: Assets/XRMP/Scripts/Network/Utils/IPDiscoveryService.cs
  - Implement static utility for network interface detection on Quest 3
  - Add methods: GetLocalIPAddress(), ValidateIPAddress(), GetWiFiInterface()
  - Handle Android/Quest 3 platform-specific network discovery
  - Purpose: Provide reliable IP address discovery and validation for LAN connections
  - _Leverage: System.Net.NetworkInformation, Unity platform detection patterns_
  - _Requirements: 4.1, 4.2, 4.3_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Platform Developer with expertise in Android networking and Meta Quest development | Task: Create IPDiscoveryService utility following requirements 4.1, 4.2, and 4.3, implementing WiFi interface detection optimized for Meta Quest 3 platform | Restrictions: Must prioritize WiFi over mobile interfaces, handle network permission exceptions gracefully, ensure Android compatibility, validate IP address formats properly | _Leverage: Unity conditional compilation directives, Android NetworkInformation APIs, existing platform detection patterns | Unity-MCP Tools: Use grep_search to find existing platform-specific code and conditional compilation patterns, Script_Read to examine Android networking implementations, Script_CreateOrUpdate to create IPDiscoveryService.cs utility class, Script_Execute to test IP discovery functionality dynamically, Console_GetLogs to monitor network permission warnings, and Assets_Refresh after creation | Success: Utility correctly detects Quest 3 WiFi IP addresses, validates IP formats accurately, handles network errors gracefully. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._

- [x] 3. Create ConnectionModeManager component in Assets/XRMP/Scripts/Network/NetworkManagers/ConnectionModeManager.cs
  - File: Assets/XRMP/Scripts/Network/NetworkManagers/ConnectionModeManager.cs
  - Implement central coordinator for switching between Cloud and LAN modes
  - Add mode selection logic with ConnectionMode enum
  - Coordinate between LANConnectionManager and existing LobbyManager
  - Purpose: Provide seamless switching between connection modes without breaking existing functionality
  - _Leverage: Assets/XRMP/Scripts/Network/NetworkManagers/LobbyManager.cs, existing state management patterns_
  - _Requirements: 3.1, 3.2, 3.3_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Architecture Developer specializing in state management and component coordination | Task: Create ConnectionModeManager following requirements 3.1, 3.2, and 3.3, coordinating between new LAN functionality and existing UGS cloud systems | Restrictions: Must not break existing cloud lobby functionality, ensure clean state transitions, avoid tight coupling between connection modes, maintain event-driven architecture | _Leverage: Existing LobbyManager patterns, XRINetworkGameManager state management, Unity event system | Unity-MCP Tools: Use Script_Read to examine LobbyManager.cs and existing state management code, grep_search to find event system patterns and state machine implementations, Component_GetAll to identify manager components, Script_CreateOrUpdate to create ConnectionModeManager.cs, Scene_GetHierarchy to verify component relationships in active scenes, and Console_GetLogs to check for state transition errors | Success: Mode switching works seamlessly, both connection modes function independently, existing cloud features remain unaffected. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._

- [x] 4. Create LANConnectionUI component in Assets/MRTabletopAssets/Scripts/UI/LAN/LANConnectionUI.cs
  - File: Assets/MRTabletopAssets/Scripts/UI/LAN/LANConnectionUI.cs
  - Implement user interface for connection mode selection and IP address entry
  - Add UI elements: mode toggle, IP input field, host/join buttons, status display
  - Integrate with existing LobbyUI styling and notification systems
  - Purpose: Provide intuitive user interface for LAN connection management
  - _Leverage: Assets/MRTabletopAssets/Scripts/UI/LobbyList/LobbyUI.cs, existing UI framework_
  - _Requirements: 2.1, 2.2, 2.3, 5.1_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity UI Developer with expertise in Canvas-based VR interfaces and user experience | Task: Create LANConnectionUI component following requirements 2.1, 2.2, 2.3, and 5.1, extending existing LobbyUI patterns with LAN-specific interface elements | Restrictions: Must follow existing UI styling patterns, ensure VR-friendly input methods, provide clear visual feedback, maintain accessibility standards | _Leverage: Existing LobbyUI component structure, TextMeshPro components, Unity UI event system, notification systems | Unity-MCP Tools: Use Script_Read to examine LobbyUI.cs implementation and UI patterns, grep_search to find UI styling and notification system code, Component_GetAll to identify Canvas and UI components, Script_CreateOrUpdate to create LANConnectionUI.cs, Assets_Find to locate UI prefab templates (t:Prefab), GameObject_Find to test UI hierarchy, and Console_GetLogs to check for UI errors | Success: UI provides clear mode selection, IP address input validation works properly, connection status updates are visible and helpful. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._

- [x] 5. Create LANConnectionPanel prefab in Assets/MRTabletopAssets/Prefabs/UI/LANConnectionPanel.prefab
  - File: Assets/MRTabletopAssets/Prefabs/UI/LANConnectionPanel.prefab
  - Create UI prefab with LANConnectionUI script and all required components
  - Configure Canvas, input fields, buttons, and text displays
  - Set up proper UI scaling and VR-friendly layout
  - Purpose: Provide reusable UI prefab for LAN connection interface
  - _Leverage: Existing UI prefab patterns from MRTabletopAssets/Prefabs/UI/_
  - _Requirements: 2.1, 2.4, 5.2_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity UI Designer with expertise in VR interface design and prefab creation | Task: Create LANConnectionPanel prefab following requirements 2.1, 2.4, and 5.2, implementing VR-optimized layout with proper scaling and accessibility | Restrictions: Must follow existing prefab naming conventions, ensure proper Canvas setup for VR, use TextMeshPro for all text elements, maintain consistent styling | _Leverage: Existing UI prefab structures, Canvas settings for VR, font and color schemes from template | Unity-MCP Tools: Use Assets_Find to locate existing UI prefabs in MRTabletopAssets for reference, Assets_Read to examine prefab structure and component configuration, GameObject_Create to build prefab UI hierarchy, GameObject_AddComponent to attach LANConnectionUI and other components, Assets_Prefab_Create to save the prefab, GameObject_Modify to configure Canvas and UI properties, Scene_Load to test prefab integration, and Assets_Refresh to update the asset database | Success: Prefab displays correctly in VR, all UI elements are properly sized and accessible, integrates smoothly with existing UI systems. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._

- [x] 6. Integrate LANConnectionManager with XRINetworkGameManager in Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs
  - File: Assets/XRMP/Scripts/Network/NetworkManagers/XRINetworkGameManager.cs (modify existing)
  - Add reference to LANConnectionManager component
  - Update connection flow to support dual modes
  - Ensure existing game state management works with LAN connections
  - Purpose: Integrate LAN functionality with existing game management systems
  - _Leverage: existing XRINetworkGameManager implementation, connection state patterns_
  - _Requirements: 6.2, 6.3, 6.4_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Integration Developer with expertise in game state management and network architecture | Task: Modify XRINetworkGameManager following requirements 6.2, 6.3, and 6.4, integrating LANConnectionManager while preserving all existing functionality | Restrictions: Must not break existing cloud connection flows, maintain backward compatibility, preserve all existing game state transitions, ensure proper error handling | _Leverage: Existing connection state machine, NetworkManager event handlers, game state management patterns | Unity-MCP Tools: Use Script_Read to examine current XRINetworkGameManager.cs implementation thoroughly, grep_search to find all references to XRINetworkGameManager, GameObject_Find to locate NetworkManager instances in scenes, Script_CreateOrUpdate to modify XRINetworkGameManager.cs, Scene_GetHierarchy to verify network manager setup, Console_GetLogs to monitor integration warnings, and TestRunner_Run to validate existing functionality remains intact | Success: LAN connections integrate seamlessly with game management, existing cloud functionality remains intact, all network events are properly handled. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._

- [x] 7. Update LobbyUI to include LAN connection options in Assets/MRTabletopAssets/Scripts/UI/LobbyList/LobbyUI.cs
  - File: Assets/MRTabletopAssets/Scripts/UI/LobbyList/LobbyUI.cs (modify existing)
  - Add connection mode toggle UI elements
  - Integrate LANConnectionPanel into existing lobby interface
  - Update UI flow to show appropriate options based on selected mode
  - Purpose: Provide unified entry point for both cloud and LAN connection modes
  - _Leverage: existing LobbyUI implementation, UI panel management patterns_
  - _Requirements: 3.4, 3.5_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity UI Integration Developer with expertise in complex UI state management | Task: Modify LobbyUI following requirements 3.4 and 3.5, integrating LAN connection options into existing cloud lobby interface seamlessly | Restrictions: Must not break existing lobby functionality, ensure smooth UI state transitions, maintain existing visual design language, preserve accessibility features | _Leverage: Existing UI state management, panel switching logic, lobby creation/join workflows | Unity-MCP Tools: Use Script_Read to examine current LobbyUI.cs implementation and UI state management, grep_search to find UI panel management and state transition code, GameObject_Find to locate LobbyUI instances and panel references, Script_CreateOrUpdate to modify LobbyUI.cs, Assets_Find to locate LANConnectionPanel prefab, Scene_GetHierarchy to verify UI hierarchy integration, and Console_GetLogs to check for UI state errors | Success: Mode selection is intuitive, UI transitions smoothly between modes, existing lobby features work unchanged. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._

- [x] 8. Add connection status and error handling in Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionStatus.cs
  - File: Assets/XRMP/Scripts/Network/NetworkManagers/LANConnectionStatus.cs
  - Implement comprehensive connection status monitoring
  - Add timeout handling, error classification, and user feedback
  - Create reconnection logic for temporary network interruptions
  - Purpose: Provide robust error handling and status reporting for LAN connections
  - _Leverage: existing error handling patterns, Unity networking callbacks_
  - _Requirements: 5.1, 5.2, 5.3, 5.4, 5.5_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Network Engineer with expertise in connection management and error handling | Task: Create LANConnectionStatus component following requirements 5.1, 5.2, 5.3, 5.4, and 5.5, implementing comprehensive status monitoring and error recovery | Restrictions: Must provide clear, actionable error messages, handle all network timeout scenarios, avoid infinite retry loops, maintain performance during error conditions | _Leverage: NetworkManager callback events, existing error notification systems, Unity coroutine patterns for timeouts | Unity-MCP Tools: Use grep_search to find existing error handling patterns and notification systems, Script_Read to examine NetworkManager callback implementations, Component_GetAll to identify network and UI components, Script_CreateOrUpdate to create LANConnectionStatus.cs, Script_Execute to test timeout and retry logic dynamically, Console_GetLogs to monitor connection status events and errors, and Reflection_MethodFind to discover NetworkManager callback APIs | Success: All connection errors are properly categorized and reported, timeout handling works reliably, reconnection logic functions correctly. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._

- [x] 9. Create unit tests for LANConnectionManager in Assets/Tests/EditMode/Network/LANConnectionManagerTests.cs
  - File: Assets/Tests/EditMode/Network/LANConnectionManagerTests.cs
  - Write comprehensive unit tests for LANConnectionManager functionality
  - Mock NetworkManager and UnityTransport components for isolated testing
  - Test host/client configuration, IP validation, and error scenarios
  - Purpose: Ensure reliability and catch regressions in core LAN functionality
  - _Leverage: Unity Test Framework, existing test utilities and patterns_
  - _Requirements: All LANConnectionManager requirements_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Test Engineer with expertise in network testing and Unity Test Framework | Task: Create comprehensive unit tests for LANConnectionManager covering all functionality, using proper mocking techniques for network components | Restrictions: Must test both success and failure scenarios, use proper mocking to isolate components, ensure tests run reliably in CI environment, do not test Unity framework code directly | _Leverage: Unity Test Framework patterns, mocking frameworks for Unity, existing test structure | Unity-MCP Tools: Use Assets_Find to locate existing test files and test assemblies, Script_Read to examine test patterns and mocking approaches, Script_CreateOrUpdate to create LANConnectionManagerTests.cs, TestRunner_Run to execute the test suite with EditMode configuration, Console_GetLogs to monitor test execution output, and Assets_Refresh to ensure test assembly is updated | Success: All critical LANConnectionManager methods are tested with good coverage, edge cases are covered, tests are reliable and fast. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._

- [x] 10. Create integration tests for LAN connection flow in Assets/Tests/PlayMode/Network/LANIntegrationTests.cs
  - File: Assets/Tests/PlayMode/Network/LANIntegrationTests.cs
  - Write integration tests for complete LAN connection workflow
  - Test UI integration, NetworkManager interaction, and connection establishment
  - Use Unity Multiplayer Play Mode for multi-instance testing
  - Purpose: Verify end-to-end LAN connection functionality works correctly
  - _Leverage: Unity Test Framework Play Mode, Multiplayer Play Mode testing_
  - _Requirements: All integration requirements_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Integration Test Engineer with expertise in multiplayer testing and Unity Play Mode tests | Task: Create integration tests covering complete LAN connection workflow, testing UI interactions and network establishment | Restrictions: Must test realistic user workflows, use proper test setup/teardown, handle async network operations correctly, ensure tests are deterministic | _Leverage: Unity Multiplayer Play Mode, existing integration test patterns, NetworkManager test utilities | Unity-MCP Tools: Use Assets_Find to locate existing integration test files, Script_Read to examine integration test patterns and async handling, Script_CreateOrUpdate to create LANIntegrationTests.cs, Scene_Load to set up test scenes, TestRunner_Run to execute PlayMode tests, Editor_SetApplicationState to control play mode during tests, Console_GetLogs to monitor test execution and network events, and Editor_GetApplicationInformation to verify test environment state | Success: Integration tests cover critical user journeys, tests run reliably in editor and CI, all component interactions are verified. Mark task as in-progress in tasks.md before starting using Unity-MCP Tools, mark as complete when finished._

- [x] 11. Update NetworkManager XR Multiplayer prefab to include LAN components in Assets/XRMP/Prefabs/Managers/Network Manager XR Multiplayer.prefab
  - File: Assets/XRMP/Prefabs/Managers/Network Manager XR Multiplayer.prefab (modify existing)
  - Add LANConnectionManager and ConnectionModeManager components to prefab
  - Configure component references and default settings
  - Ensure prefab maintains compatibility with existing scenes
  - Purpose: Provide pre-configured prefab with LAN functionality included
  - _Leverage: existing Network Manager prefab structure and configuration_
  - _Requirements: 6.1, 6.2_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: Unity Prefab Designer with expertise in network manager configuration and component relationships | Task: Modify Network Manager XR Multiplayer prefab following requirements 6.1 and 6.2, adding LAN components while maintaining existing functionality | Restrictions: Must not break existing prefab functionality, ensure proper component order and references, maintain serialized field values, preserve existing NetworkManager settings | _Leverage: Existing prefab configuration, component reference patterns, NetworkManager setup best practices | Unity-MCP Tools: Use Assets_Find to locate the Network Manager XR Multiplayer prefab, Assets_Prefab_Open to enter prefab edit mode, GameObject_Find to locate the prefab root, GameObject_AddComponent to attach LANConnectionManager and ConnectionModeManager, GameObject_Modify to configure component references and settings, Assets_Prefab_Save to persist changes, Assets_Prefab_Close to exit prefab mode, and Scene_GetHierarchy to verify prefab structure | Success: Prefab includes all necessary LAN components, existing functionality remains intact, component references are properly configured. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._

- [x] 12. Create device testing procedure document in docs/testing/lan-connection-testing.md
  - File: docs/testing/lan-connection-testing.md
  - Document step-by-step testing procedure for Quest 3 devices
  - Include network setup, troubleshooting, and validation steps
  - Provide testing checklist for different network scenarios
  - Purpose: Ensure consistent and thorough testing of LAN functionality on target hardware
  - _Leverage: existing documentation structure and testing procedures_
  - _Requirements: All testing and validation requirements_
  - _Prompt: Implement the task for spec lan-connection-manager, first run spec-workflow-guide to get the workflow guide then implement the task: Role: QA Documentation Specialist with expertise in VR device testing and network validation procedures | Task: Create comprehensive testing procedure document covering Quest 3 LAN connection testing with network setup and troubleshooting guidance | Restrictions: Must provide clear, actionable steps, include common failure scenarios and solutions, ensure procedures are repeatable by different team members | _Leverage: Existing testing documentation format, Quest 3 development knowledge, network troubleshooting best practices | Unity-MCP Tools: Use Script_Read to review LANConnectionManager and related components for accurate documentation, Assets_Find to identify all LAN-related assets and scripts, grep_search to find error messages and status codes for troubleshooting section, Scene_GetLoaded to document required scenes, Console_GetLogs to capture common error patterns, and Editor_GetApplicationInformation to document testing environment requirements | Success: Document enables consistent device testing, covers all critical test scenarios, provides clear troubleshooting guidance. Mark task as in-progress in tasks.md before starting using Unity-MCP tools, mark as complete when finished._