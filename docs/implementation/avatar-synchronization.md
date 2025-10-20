[← Documentation Home](../README.md) · [← Implementation](README.md)

# Avatar Synchronization (Unity Netcode for GameObjects)

Implement 3-user head/hand sync with owner-authoritative updates at ~20 Hz.

## Overview
- Each player uses an XR Origin (Action-based) locally.
- A Player Prefab (with NetworkObject) is spawned by NetworkManager for each client.
- Owners publish head/hand poses; non-owners render remote avatars.

## Player Data Model (owner-writable)
```csharp
namespace MultiUserVR.Networking
{
    using Unity.Netcode;
    using Unity.Collections;
    using UnityEngine;

    [System.Serializable]
    public struct NetworkPlayerData : INetworkSerializable
    {
        public Vector3 HeadPos; public Quaternion HeadRot;
        public Vector3 LeftPos; public Quaternion LeftRot;
        public Vector3 RightPos; public Quaternion RightRot;
        public bool IsTracking;
        public FixedString32Bytes PlayerName;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref HeadPos);
            serializer.SerializeValue(ref HeadRot);
            serializer.SerializeValue(ref LeftPos);
            serializer.SerializeValue(ref LeftRot);
            serializer.SerializeValue(ref RightPos);
            serializer.SerializeValue(ref RightRot);
            serializer.SerializeValue(ref IsTracking);
            serializer.SerializeValue(ref PlayerName);
        }
    }
}
```

## VRPlayerSync (attach to Player Prefab root)
```csharp
namespace MultiUserVR.Networking
{
    using UnityEngine;
    using Unity.Netcode;

    public class VRPlayerSync : NetworkBehaviour
    {
        [Header("Local XR Rig References (Owner)")]
        public Transform head; public Transform leftHand; public Transform rightHand;

        [Header("Remote Avatar Targets (Non-Owner)")]
        public Transform remoteHead; public Transform remoteLeft; public Transform remoteRight;

        [Header("Config")]
        [Range(5, 30)] public int sendRateHz = 20;
        [Range(0f, 1f)] public float remoteLerp = 0.25f;

        private float _accum;
        private NetworkVariable<NetworkPlayerData> _state = new NetworkVariable<NetworkPlayerData>(
            writePerm: NetworkVariableWritePermission.Owner);

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                // Ensure remote avatar targets are active for non-owners
                if (remoteHead) remoteHead.gameObject.SetActive(true);
                if (remoteLeft) remoteLeft.gameObject.SetActive(true);
                if (remoteRight) remoteRight.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (IsOwner)
            {
                _accum += Time.deltaTime;
                var interval = 1f / Mathf.Max(1, sendRateHz);
                if (_accum >= interval)
                {
                    _accum = 0f;
                    var data = new NetworkPlayerData
                    {
                        HeadPos = head.position,
                        HeadRot = head.rotation,
                        LeftPos = leftHand.position,
                        LeftRot = leftHand.rotation,
                        RightPos = rightHand.position,
                        RightRot = rightHand.rotation,
                        IsTracking = true,
                        PlayerName = new Unity.Collections.FixedString32Bytes("Player")
                    };
                    _state.Value = data;
                }
            }
            else
            {
                // Smoothly render remote avatars
                var s = _state.Value;
                if (remoteHead)
                {
                    remoteHead.position = Vector3.Lerp(remoteHead.position, s.HeadPos, remoteLerp);
                    remoteHead.rotation = Quaternion.Slerp(remoteHead.rotation, s.HeadRot, remoteLerp);
                }
                if (remoteLeft)
                {
                    remoteLeft.position = Vector3.Lerp(remoteLeft.position, s.LeftPos, remoteLerp);
                    remoteLeft.rotation = Quaternion.Slerp(remoteLeft.rotation, s.LeftRot, remoteLerp);
                }
                if (remoteRight)
                {
                    remoteRight.position = Vector3.Lerp(remoteRight.position, s.RightPos, remoteLerp);
                    remoteRight.rotation = Quaternion.Slerp(remoteRight.rotation, s.RightRot, remoteLerp);
                }
            }
        }
    }
}
```

## Setup Steps
1) Create a Player Prefab:
- Root GameObject with NetworkObject, VRPlayerSync, and (optional) CharacterController
- For local owner: assign head/leftHand/rightHand (XR Origin Camera + Controller transforms)
- For remote: assign remoteHead/remoteLeft/remoteRight to avatar mesh targets

2) NetworkManager:
- Add NetworkManager to a bootstrap object
- Assign Player Prefab in NetworkManager → Player Prefab
- Choose Unity Transport (default)

3) Spawning:
- Start Host on one device; Join as Client on others
- NGO will auto-spawn Player Prefab per connection

## Alternative: NetworkTransform
For simpler sync, add NetworkTransform/ClientNetworkTransform to each tracked object (head, hands). This increases object count but reduces custom code.

## Verification
- Three devices: host + 2 clients
- Head/hand movement of one user appears smoothly on others
- Ownership transfer for grabbables handled separately (see interaction docs)
