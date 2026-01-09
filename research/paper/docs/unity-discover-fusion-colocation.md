# Discover: How the Project Uses Photon Fusion

This document explains how the **Unity-Discover** project uses **Photon Fusion 1** for networking, what Fusion is responsible for in the app, and how it connects to the project’s **colocation + shared spatial anchor** flow.

## Why Fusion is in this project

Fusion is used to:
- Create/join a shared multiplayer session (room) in the Photon Cloud.
- Synchronize the presence of players and networked objects (icons, apps, gameplay objects).
- Provide authority transfer for interactive objects (grabbables, moveable icons).
- Provide RPC-based messaging for “request server/master does X” behaviors.

This project primarily runs Fusion in **Shared Mode** (not Hosted/Server mode).

## Network topology and authority model

### Shared Mode

The main connection path uses `GameMode.Shared`.

In Shared Mode:
- The Photon Cloud owns the session lifecycle and relays traffic.
- Authority is distributed among clients.
- The project treats the **Shared Mode master client** as the “host-like” authority for spawning scene-level network objects.

The helper extension `PhotonNetwork.IsMasterClient()` returns true when the runner is the Shared Mode master client (or in single-player mode).

## Entry point: session connect / join flow

### Connection orchestration

The top-level flow lives in [Unity-Discover/Assets/Discover/Scripts/DiscoverAppController.cs](../../Unity-Discover/Assets/Discover/Scripts/DiscoverAppController.cs).

Key steps:
1. **Host path** loads the MR scene first via `MRSceneLoader.LoadScene()`.
2. The app creates a `NetworkRunner` instance and enables input (`Runner.ProvideInput = true`).
3. It calls `Runner.StartGame(StartGameArgs)` with:
   - `GameMode = Shared` (default)
   - `SessionName = roomName` (generated if host did not supply one)
   - `DisableClientSessionCreation = !isHost` (joiners won’t create rooms)
   - `Scene = active scene build index`
   - Optional: `CustomPhotonAppSettings.FixedRegion` when the user selected a region

### Where “hosting” matters in Shared Mode

Even in Shared Mode, the project distinguishes “host” vs “join” mainly to decide:
- whether to **create** the room (host) vs only attempt to **join** (client)
- whether to pre-load the MR scene before connecting

Actual “host-like authority” during runtime is handled by:
- `runner.IsSharedModeMasterClient` (master client)

## What happens after connecting

### Scene-level network objects are spawned by the master client

When `OnConnectedToServer()` fires, the master client (Shared Mode master) does the session bootstrap:
- Calls all `PhotonInstantiator.Instances` to spawn pre-placed network objects.
- Spawns the `NetworkApplicationManager` prefab.
- Spawns the `ColocationDriverNetObj` prefab.
- Enables icon placement/moving for the session and loads saved icons.

This logic is in `DiscoverAppController.OnConnectedToServer()`.

### Each client spawns its player rig

Regardless of master status, each client spawns the player prefab and sets `DiscoverPlayer.IsRemote` based on whether they joined “Remote”:
- “Remote join” means no physical colocation attempt; the player is treated as remote in the app logic.

## Colocation: how it plugs into Fusion

Colocation is implemented as a Fusion `NetworkBehaviour` called `ColocationDriverNetObj`:
- [Unity-Discover/Assets/Discover/Scripts/Colocation/ColocationDriverNetObj.cs](../../Unity-Discover/Assets/Discover/Scripts/Colocation/ColocationDriverNetObj.cs)

How it works at a high level:
- The object initializes Oculus/Meta platform identity (logged-in user, device UID).
- It sets up a network messenger/adapter used by the Meta colocation package.
- If the local client has **State Authority** (`HasStateAuthority`), it creates the colocated space.
- If not authoritative:
  - If the session is a “remote join”, it skips colocation.
  - Otherwise it runs the automatic colocation alignment flow.

When colocation succeeds, it disables the default `AlignCameraToAnchor` behaviour (because it updates every frame and causes spikes), replaces it with `AlignCameraToAnchorManager`, and triggers a one-time realignment.

Practical implication:
- Fusion is used to ensure the colocation driver exists for everyone and is spawned consistently.
- The actual “spatial alignment” is handled via Meta colocation + shared spatial anchor logic.

## Avatar networking (Fusion + Meta Avatars)

Remote avatar motion/state is streamed with Fusion RPCs in:
- [Unity-Discover/Assets/Discover/Scripts/Networking/PhotonFusionAvatarNetworking.cs](../../Unity-Discover/Assets/Discover/Scripts/Networking/PhotonFusionAvatarNetworking.cs)

Key mechanics:
- A networked property `UserId` is synchronized via `[Networked]` and used to load the correct Meta Avatar.
- The authoritative instance periodically captures avatar stream data (`RecordStreamData_AutoBuffer`).
- It sends the data via an **unreliable RPC** to proxies (`RPC_SetStreamData(..., RpcChannel.Unreliable)`).
- Receivers apply the stream data and apply a smoothed playback delay based on observed timing.

Practical implication:
- The project uses Fusion for lightweight, frequent, low-latency avatar updates.

## Interactive objects and authority transfer

### Grabbables

Interactive objects can request authority on interaction.

Example:
- [Unity-Discover/Assets/Discover/Scripts/Networking/NetworkGrabbableObject.cs](../../Unity-Discover/Assets/Discover/Scripts/Networking/NetworkGrabbableObject.cs)

Behavior:
- On `Select`, if the object is being selected and the client does not have state authority, it calls `Object.RequestStateAuthority()`.

### Icons placement / moving

Icons are network-spawned and anchored.

The `AppsManager`:
- Spawns icon anchors as network objects (`IconAnchorNetworked`) when placing.
- Requests state authority when moving an existing icon to ensure the mover can update state.
- Persists placement using `OVRSpatialAnchor` saved to disk.

See:
- [Unity-Discover/Assets/Discover/Scripts/AppsManager.cs](../../Unity-Discover/Assets/Discover/Scripts/AppsManager.cs)

## Networked “apps” launching

Applications are spawned as networked prefabs managed by `NetworkApplicationManager`:
- [Unity-Discover/Assets/Discover/Scripts/NetworkApplicationManager.cs](../../Unity-Discover/Assets/Discover/Scripts/NetworkApplicationManager.cs)

Pattern:
- If the caller has state authority, it spawns the selected app prefab.
- Otherwise it calls an RPC targeting the state authority (`LaunchApplicationOnServerRPC`) to request spawning.

Practical implication:
- Any client can request launching/closing an app, but spawning is centralized to the authoritative instance.

## Related local Photon docs in this repo

A minimal offline snapshot of Photon Fusion docs (v1) is stored here:
- [Unity-Discover/Documentation/FusionV1/Introduction.md](../../Unity-Discover/Documentation/FusionV1/Introduction.md)
- [Unity-Discover/Documentation/FusionV1/AuthenticationOverview.md](../../Unity-Discover/Documentation/FusionV1/AuthenticationOverview.md)
- [Unity-Discover/Documentation/FusionV1/OculusAuthentication.md](../../Unity-Discover/Documentation/FusionV1/OculusAuthentication.md)

## Summary

In this project, Fusion primarily provides:
- **Shared Mode sessions** (room connect/join + master-client bootstrap)
- **NetworkObject spawning** (scene objects, app manager, colocation driver, apps)
- **Authority transfer** for interaction (grabbables, movable icons)
- **RPC-driven replication** for high-frequency avatar streaming

Colocation and shared anchors provide the **physical alignment layer**; Fusion ensures all participants share the same networked session and see consistent spawned objects and avatar/app state.
