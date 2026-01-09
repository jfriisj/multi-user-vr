# Fusion 1 Introduction

## Overview

Fusion is a new high performance state synchronization networking library for Unity. With a single API, it supports two fundamentally different network topologies as well as a single player mode with no network connection.

It is built with simplicity in mind to integrate naturally into the common Unity workflow, while also offering advanced features like data compression, client-side prediction and lag compensation out of the box.

For example, RPCs and network state is defined with attributes on the methods and properties of the MonoBehaviour themselves with no need for explicit serialization code and network objects can be defined as prefabs using all of Unity's most recent prefab features like nesting and variants.

Behind the covers, Fusion relies on a state-of-the-art compression algorithm to reduce bandwidth requirements with minimal CPU overhead. Data is transferred either as complete compressed snapshots (only hosted mode) or as partial chunks with eventual consistency. In the latter case, a fully configurable area-of-interest system is supplied to allow support for very high player counts.

Fusion implements a robust tick-based simulation and operates in either Shared Mode or Hosted Mode. The main difference is in who has authority over (ability to change) network objects, but this in turn dictates which other SDK features are available.

### Hosted Mode / Server Mode

In hosted mode, whether running as a dedicated headless server or a combined client and server on the same device, the server has full and exclusive State Authority over all objects, no exceptions.

Clients can only modify networked objects by sending their input to the server (and have the server react to that input) or by requesting a change using an RPC.

Any changes a client makes directly to the networked state is only a local prediction, which will be overridden with actual authoritative snapshots from the server when those are received. This is known as reconciliation, as the client is rolled back to the server-provided state and re-simulated forward to the local (predicted) tick.

If previous predictions were accurate, this process is seamless. If not, the state will be updated and because the network state is separate from the rendering state, the rendering may either snap to this new state or use various forms of interpolation, error correction and smoothing to reduce the visual artifacts caused by the correction.

In hosted mode, Fusion supports lag compensated hit boxes to account for the fact that each client sees other clients in the past and regular ray cast on the server would not match what the player was seeing. Lag compensation effectively allow the server to see the world from the player's perspective at the time a given input was received.

When running hosted mode from behind a firewall or a router, the Photon cloud transparently provides UDP punch through or package relay as needed, but the session is owned by the host and will be lost if the host disconnects. Fusion does provide a host migration mechanism to allow transfer of network authority to a new client in the event that the current host is disconnected. Do note that, unlike Shared Mode, this requires special handling in client code.

### Shared Mode

In shared mode, authority over network objects is distributed among all clients. Specifically, each client initially has State Authority over objects they spawn, but are free to release that State Authority to other clients. Optionally, clients may be allowed to take State Authority at will.

In shared mode the data transfer mode is always Eventual Consistency, and features such as lag compensation, prediction and rollback are not available. Simulation always moves forward at the same tick rate on all clients, but beware that ticks are not guaranteed to be aligned between clients.

The Shared Mode network session is owned by the Photon cloud and remains alive as long as any client is connected to it. The Photon cloud serves as a package relay and has full access to the network state with no need to run Unity, allowing for lightweight server logic and data validation (e.g. cheat protection) to be implemented without the need to spin up dedicated server hardware.

Shared mode is in many ways similar to PUN, albeit more feature complete, faster, and with no run-time allocation overhead.

---

## PUN, Bolt & Fusion Comparison

Fusion was developed to evolve and replace the two existing Photon state-transfer products for Unity (Bolt and PUN); it includes all supported architectures and more!

Although PUN and Bolt are solid networking solutions, their architectures do not allow for further optimizations. Fusion merges all the best concepts of both PUN and Bolt, while being built from the ground up with a high-performance architecture to enable state of the art features right out of the box.

![PUN vs Bolt vs Fusion Comparison Table](https://doc.photonengine.com/docs/img/fusion/pun-bolt-fusion-comparison-table.jpg)

---

## The Basics

The two primary Fusion components that you will use are `NetworkRunner` and `NetworkObject`.

`NetworkRunner` can be thought of as the core of Fusion - There is a single runner in your scene managing both networking and simulation. This happens on both servers and clients.

Adding `NetworkObject` to an otherwise regular Unity prefab or scene object, will assign it a network identity in runtime, and let it be part of the synchronized tick-based simulation. It also lets you configure a few options (like for example making this particular object always part of the data transfers - global object).

Two other base behaviors are there to be inherited from and add the actual networking to Game Objects: `SimulationBehaviour` and `NetworkBehaviour`. Networked properties (that comprise the game state) are to be added to `NetworkBehaviour` (a specialized subclass of `SimulationBehaviour`), while the latter can be used to control simulation steps and callbacks without carrying networked properties.

`NetworkBehaviour` should be used to hold data that is automatically synchronized across the network. In order to define a network state value, simply create a property and mark it as `[Networked]`, like this:

```csharp
[Networked] public byte life { get; set; }
```

`[Networked]` works for all primitive types (except for `bool`, for which you should use `NetworkBool` instead since it will get properly serialized as a single bit). It also works for structs and references to other `NetworkObject`'s (even prefabs) since these can be safely identified on all clients.

It is also easy to register a callback to be triggered every time a networked property value changes:

```csharp
[Networked(OnChanged = "OnTypeChanged")] public Type type { get; set; }

public static void OnTypeChanged(Changed<TheClassWhichHasTheProperty> changed)
{
  // your code here - check API docs for more details
}
```

In addition to the actual callback name, you can also control where the callback is executed:
- `OnChangedLocal` (true/false) - set true to also have the event hook called on the machine that changed the property, for example a server (default false)
- `OnChangedRemote` (true/false) - set false to only have the event hook called on the machine that changed the property (default true)

---

## Built-in Network Behaviours

Fusion offers various prebuilt `NetworkBehaviour`s to get a game or prototype up and running quickly.

- **NetworkTransform**: Keeps an object transform synchronized (can also contain colliders). Rendering is automatically kept butter smooth with snapshot interpolation.
- **NetworkRigidbody**: Recommended for physics-controlled rigidbodies. Fusion can do full predict/rollback directly with Unity physics (PhysX).
- **NetworkCharacterController**: For objects controlled directly by players, like humanoid characters.

---

## Tick-based Callbacks

To write gameplay simulation code, `SimulationBehaviour` lifecycle comes into play. It is recommended to use the network-safe counterparts to Unity's built-in methods:

- **Spawned()**: Fusion's equivalent to `Start()`. Triggered when the object is brought to life on a specific machine.
- **FixedUpdateNetwork()**: The most important Fusion callback. Used to execute logic for the next network state and to resimulate during reconciliations. It is called at fixed intervals, independent of the rendering rate.
- **Render()**: Guaranteed to run after all calls to `FixedUpdateNetwork()` and before `LateUpdate()`.

Notice the use of `Runner.DeltaTime` within `FixedUpdateNetwork()` to keep simulation in accordance with the network tick rate.

---

## Input

Fusion splits input handling into two separate steps:
1. **Collect Input**: Done only on clients and a host, once per new tick. Polled via the `OnGetInput()` callback.
2. **Apply Input**: From `FixedUpdateNetwork()`, input is read via `GetInput()` or `TryGetInput()` to change the game state.

---

## Remote Procedure Calls (RPCs)

For rare complex interactions or one-time actions where reliable transmission is needed, Fusion supports RPCs.

Use the `[Rpc]` attribute on a `void` method. The method name must be prefixed or post-fixed with "rpc" (not case-sensitive).

```csharp
[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
public void RPC_Configure(string name, Color color)
{
    playerName = name;
    playerColor = color;
}
```

RPC types:
- **Channel**: Reliable (default) or Unreliable.
- **InvokeLocal**: Whether to invoke on the local client (default true).
- **InvokeResim**: Whether to invoke during re-simulations (default false).

Note: RPCs have no explicit state; they are not received by clients who join after the RPC was sent. Use `[Networked]` properties for persistent state.

---

## Where to go next

To get started with Fusion, we strongly recommend beginning with the tutorials:
- [Fusion Host Mode Basics tutorial](https://doc.photonengine.com/fusion/v1/tutorials/host-mode-basics/overview)
- [Fusion Shared Mode Basics tutorial](https://doc.photonengine.com/fusion/v1/tutorials/shared-mode-basics/overview)
