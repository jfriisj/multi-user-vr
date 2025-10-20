[← Documentation Home](../README.md) · [← Architecture](README.md)

# Network Topology (3-User Client-Server, NGO)

Topology and session flow for 1 Host (server-authoritative) + 2 Clients using Unity Netcode for GameObjects (NGO) and Unity Transport (UDP).

## High-Level Topology (Req 4.1)
```mermaid
flowchart LR
  subgraph Host[Quest 3 A (Host/Server)]
    HNM[NetworkManager]
    HUTP["Unity Transport: UDP:7777"]
    HSpawn[Player/NetObject Spawner]
    HSafety[Safety Coordinator Authority]
  end
  subgraph ClientB[Quest 3 B (Client)]
    BNM[NetworkClient]
    BUTP[Unity Transport]
  end
  subgraph ClientC[Quest 3 C (Client)]
    CNM[NetworkClient]
    CUTP[Unity Transport]
  end

  HUTP <-.-> BUTP
  HUTP <-.-> CUTP
  HNM --> HSpawn
  HNM --> HSafety
```

- Default port: 7777 (configurable). Same WiFi network recommended for low latency.
- Server authority: host validates inputs, resolves conflicts (e.g., object grabs), and owns safety decisions.
- Discovery: LAN IP entry or Unity Relay/Lobby (optional) for NAT traversal.

## Session Lifecycle
```mermaid
sequenceDiagram
  participant Host as Host (Server)
  participant B as Client B
  participant C as Client C

  Host->>Host: StartHost() (NetworkManager)
  B->>Host: StartClient() → Connect
  Host-->>B: Approve + Spawn Player Prefab
  C->>Host: StartClient() → Connect
  Host-->>C: Approve + Spawn Player Prefab
  Note over Host,B,C: NGO replicates Player state + networked objects
```

## Authority and Ownership
- Player Prefab: owned by connecting client (for pose writes), but server validates and replicates.
- Interactable Objects: server-authoritative; ownership granted temporarily to grabber; server resolves contention.
- Safety: server makes final decisions; clients only request actions.

## Failure Modes
- Client disconnect: server despawns player and transfers object ownership back to server.
- Host lost: session ends (no host migration in MVP). Restart host and reconnect clients.
- Packet loss: interpolation and dwell timers prevent oscillations.

## Security Considerations
- Validate client-reported transforms (speed clamps, bounds checks) on server.
- Restrict ownership to allowed roles; reject illegal RPCs.
- LAN preferred; for cloud, use Unity Relay + auth tokens; enable encryption where available.
