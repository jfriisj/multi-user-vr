# Requirements Document

## Introduction

The LAN Connection Manager feature enables direct local area network connections for Unity VR multiplayer sessions without requiring internet connectivity or Unity Gaming Services (UGS). This addresses the critical MVP requirement for "Unity Transport over LAN by default" with manual IP entry for direct device-to-device connection between Meta Quest 3 headsets in the same physical space.

This feature provides an alternative connection mode to the existing cloud-based UGS Lobby/Relay system, allowing 3 users with Meta Quest 3 devices to connect and synchronize in a shared VR environment using only local WiFi infrastructure.

## Alignment with Product Vision

This feature directly supports the core product goals outlined in product.md:

- **Affordability Over Features**: Eliminates dependency on Unity Services cloud infrastructure and associated costs
- **Co-location Advantages**: Optimizes for users sharing the same physical room and WiFi network
- **Safety First**: Provides reliable, low-latency connections essential for proximity detection and collision prevention
- **Template-first Development**: Integrates with existing Unity MR Multiplayer template architecture

The feature enables the primary use cases of training facilities, educational labs, and team-building venues that require local multiplayer experiences without internet dependency.

## Requirements

### Requirement 1: Direct LAN Connection Configuration

**User Story:** As a VR session host, I want to start a local server on my device so that other users in my room can connect directly to me without needing internet access.

#### Acceptance Criteria

1. WHEN the host selects "LAN Direct" mode THEN the system SHALL configure Unity Transport for local IP binding on port 7777 (default)
2. WHEN the server starts successfully THEN the system SHALL display the host's local IP address prominently in the UI
3. WHEN Unity Transport is configured for LAN THEN the system SHALL bind to "0.0.0.0" for server listen address and the device's WiFi IP for connection address
4. IF no internet connection is available THEN the LAN connection SHALL still function normally
5. WHEN the host starts a session THEN the connection SHALL be established within 5 seconds or display appropriate error messaging

### Requirement 2: Manual IP Address Entry

**User Story:** As a VR session participant, I want to enter the host's IP address manually so that I can join their local session directly.

#### Acceptance Criteria

1. WHEN I select "LAN Direct" mode THEN the system SHALL provide an input field for IP address entry
2. WHEN I enter a valid IP address (e.g., "192.168.1.10") THEN the system SHALL validate the format and store it for connection
3. WHEN I enter an invalid IP format THEN the system SHALL display clear error messaging without attempting connection
4. WHEN I click "Join" with a valid IP THEN the system SHALL attempt connection within 5 seconds
5. IF the connection fails THEN the system SHALL display specific error messages (timeout, host unreachable, etc.)

### Requirement 3: Dual Connection Mode Support

**User Story:** As a system administrator, I want users to be able to choose between Cloud (UGS) and LAN connection modes so that the system can work in various deployment scenarios.

#### Acceptance Criteria

1. WHEN the connection UI loads THEN the system SHALL display mode selector with "Cloud (Internet)" and "LAN Direct" options
2. WHEN "Cloud" mode is selected THEN the system SHALL use existing UGS Lobby/Relay functionality
3. WHEN "LAN Direct" mode is selected THEN the system SHALL bypass all UGS services and use direct Unity Transport
4. WHEN switching modes THEN the UI SHALL update to show appropriate options (lobby codes vs IP address entry)
5. IF cloud services are unavailable THEN the system SHALL automatically suggest LAN Direct mode

### Requirement 4: Local IP Discovery

**User Story:** As a host, I want the system to automatically discover and display my local IP address so that I can share it with other participants easily.

#### Acceptance Criteria

1. WHEN the app starts THEN the system SHALL detect the device's WiFi interface IP address
2. WHEN hosting a LAN session THEN the system SHALL display the IP address in large, readable text
3. WHEN on Meta Quest 3 THEN the system SHALL prioritize WiFi over any other network interfaces
4. IF multiple network interfaces exist THEN the system SHALL use the interface connected to the local router
5. WHEN IP address cannot be determined THEN the system SHALL display helpful instructions for manual discovery

### Requirement 5: Connection Status and Error Handling

**User Story:** As a user, I want clear feedback about connection status and errors so that I can troubleshoot problems effectively.

#### Acceptance Criteria

1. WHEN initiating a connection THEN the system SHALL display loading indicators with timeout countdown
2. WHEN connection succeeds THEN the system SHALL display success message and transition to game state
3. WHEN connection fails THEN the system SHALL display specific error codes and suggested remedies
4. WHEN connection is lost during gameplay THEN the system SHALL attempt automatic reconnection for 30 seconds
5. WHEN timeout occurs (>5 seconds) THEN the system SHALL abort connection attempt and return to connection UI

### Requirement 6: Integration with Existing Network Architecture

**User Story:** As a developer, I want the LAN connection system to integrate seamlessly with existing XRINetworkGameManager so that all other multiplayer features continue to work normally.

#### Acceptance Criteria

1. WHEN LAN connection is established THEN avatar synchronization SHALL work identically to cloud connections
2. WHEN objects are networked THEN they SHALL sync with the same authority model and update rates as cloud mode
3. WHEN voice chat is enabled THEN it SHALL work over LAN connections (if Vivox supports local routing)
4. WHEN existing NetworkBehaviours spawn THEN they SHALL function identically regardless of connection mode
5. IF NetworkManager settings need modification THEN changes SHALL be applied dynamically without scene reloading

## Non-Functional Requirements

### Code Architecture and Modularity
- **Single Responsibility Principle**: LANConnectionManager handles only direct IP connections, existing LobbyManager handles UGS
- **Modular Design**: Connection mode selection isolates LAN logic from cloud logic
- **Dependency Management**: Minimal coupling to UGS services, direct integration with Unity Transport
- **Clear Interfaces**: Well-defined API for connection mode switching and status reporting

### Performance
- **Connection Establishment**: Maximum 5 seconds from user action to connected state
- **Avatar Synchronization**: Maintain <20ms perceived latency identical to cloud connections
- **Network Bandwidth**: No additional overhead compared to UGS Relay connections
- **Memory Usage**: <10MB additional RAM usage for LAN management components

### Security
- **Local Network Only**: Connections limited to same subnet for security
- **No Authentication**: Rely on physical access control (same room, same WiFi)
- **Data Encryption**: Use Unity Transport's built-in encryption if available

### Reliability
- **Connection Stability**: Maintain connection for minimum 30 minutes without intervention
- **Reconnection**: Automatic reconnection attempts on temporary network interruption
- **Graceful Degradation**: Clear error messages and fallback suggestions when connections fail
- **Device Support**: 100% compatibility with Meta Quest 3 WiFi networking

### Usability
- **Setup Time**: Users can establish connections within 60 seconds of starting application
- **Error Recovery**: Clear instructions for common problems (firewall, wrong IP, device offline)
- **Visual Clarity**: IP addresses displayed in large, easily readable fonts
- **Accessibility**: Voice-over friendly text and high contrast UI elements