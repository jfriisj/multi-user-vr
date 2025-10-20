[← Documentation Home](../README.md) · [← Implementation](README.md)

# Synchronization Patterns (NGO for VR)

Patterns for stable, low-latency synchronization in 3-user VR with Unity Netcode for GameObjects (Req 4.2).

## Choosing Replication Mechanisms
- Continuous state (head/hands): NetworkVariable<struct> at ~20 Hz (owner write, everyone read) with client-side interpolation.
- Event/state changes (grab/release): RPCs (ServerRpc/ClientRpc) or NetworkVariable flags.
- Transforms for props: NetworkTransform or custom state struct at ~30 Hz for active objects only.

## Player Pose Stream (reference)
See [Avatar Synchronization](avatar-synchronization.md) for owner-auth pose stream using NetworkVariable<NetworkPlayerData>.

## Networked Interactable (ownership + state)
```csharp
namespace MultiUserVR.Networking
{
    using UnityEngine;
    using Unity.Netcode;

    public class NetworkedInteractable : NetworkBehaviour
    {
        public NetworkVariable<bool> IsGrabbed = new(writePerm: NetworkVariableWritePermission.Server);

        [ServerRpc(RequireOwnership = false)]
        public void RequestGrabServerRpc(ServerRpcParams p = default)
        {
            if (IsGrabbed.Value) return; // already owned
            // Optional: server-side distance/permission checks here
            var sender = p.Receive.SenderClientId;
            NetworkObject.ChangeOwnership(sender);
            IsGrabbed.Value = true;
        }

        [ServerRpc(RequireOwnership = true)]
        public void ReleaseServerRpc()
        {
            // Return ownership to server for determinism
            NetworkObject.ChangeOwnership(NetworkManager.ServerClientId);
            IsGrabbed.Value = false;
        }
    }
}
```

Usage (pseudo): on local grab event → RequestGrabServerRpc(); on release → ReleaseServerRpc().

## Interest Management (proximity-based)
- Update only when peers within X meters (e.g., 10–15 m). Outside range, reduce send rate or suspend.
- Partition scene into zones; update zone members only.

## Tick Rates and Smoothing
- Head/hands: 20 Hz send, client interpolation (lerp/slerp 0.2–0.3).
- Props while grabbed: up to 30 Hz; idle props: 0–5 Hz.
- Use Time.deltaTime-aware smoothing; avoid physics extrapolation for avatars to prevent discomfort.

## Reliability & Channels
- Prefer UnreliableSequenced for continuous pose streams via Custom Messaging when needed; use Reliable for important events (grabs, spawns).
- With NetworkVariables, rely on NGO delta updates; avoid sending unchanged data.

## Validation on Server (security)
- Clamp max linear/angular speed; ignore impossible deltas.
- Reject ownership requests outside reach distance.
- Force ownership reset on disconnect/timeouts.

## Testing Checklist
- Three-headset test: smooth remote avatar motion with no rubber-banding at 20 Hz.
- Grab conflict: two clients request at once → server grants one, others rejected; no duplicate owners.
- Idle scene bandwidth: ≤ ~2–5 KB/s per client; with interaction spikes still < 10 KB/s.
