using UnityEngine;
using System.Collections.Generic;

namespace Unity.VRTemplate.MultiUser
{
    /// <summary>
    /// Simple multi-user manager for local network or development testing
    /// MVP version that can be extended with Photon/Netcode later
    /// </summary>
    public class SimpleMultiUserManager : MonoBehaviour
    {
        [Header("Session Settings")]
        [SerializeField] private string roomName = "VRRoom01";
        [SerializeField] private int maxPlayers = 3;
        [SerializeField] private bool autoStart = true;
        
        [Header("Player Prefabs")]
        [SerializeField] private GameObject remotePlayerPrefab;
        
        [Header("Debug")]
        [SerializeField] private bool showDebugUI = true;
        
        [Header("Events")]
        public UnityEngine.Events.UnityEvent OnSessionStarted;
        public UnityEngine.Events.UnityEvent OnPlayerJoined;
        public UnityEngine.Events.UnityEvent OnPlayerLeft;
        
        // Singleton
        private static SimpleMultiUserManager _instance;
        public static SimpleMultiUserManager Instance => _instance;
        
        // State
        private bool _isSessionActive;
        private int _connectedPlayers = 1; // Local player counts as 1
        private Dictionary<string, GameObject> _remotePlayers = new Dictionary<string, GameObject>();
        
        // Local player reference
        private Transform _localPlayerHead;
        private Transform _localPlayerLeftHand;
        private Transform _localPlayerRightHand;
        
        public bool IsSessionActive => _isSessionActive;
        public int ConnectedPlayers => _connectedPlayers;
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            if (autoStart)
            {
                StartSession();
            }
        }
        
        public void StartSession()
        {
            if (_isSessionActive)
            {
                Debug.LogWarning("Session already active");
                return;
            }
            
            Debug.Log($"Starting multi-user session: {roomName}");
            _isSessionActive = true;
            
            // Find local player components
            InitializeLocalPlayer();
            
            OnSessionStarted?.Invoke();
        }
        
        private void InitializeLocalPlayer()
        {
            // Find XR Origin
            var xrOrigin = FindObjectOfType<Unity.XR.CoreUtils.XROrigin>();
            if (xrOrigin == null)
            {
                Debug.LogError("XR Origin not found! Make sure you have an XR Origin in the scene.");
                return;
            }
            
            // Get local player transforms
            _localPlayerHead = xrOrigin.Camera.transform;
            
            // Find controllers
            var leftController = xrOrigin.transform.Find("Camera Offset/LeftHand Controller") ?? 
                               xrOrigin.transform.Find("Camera Offset/Left Controller");
            var rightController = xrOrigin.transform.Find("Camera Offset/RightHand Controller") ?? 
                                xrOrigin.transform.Find("Camera Offset/Right Controller");
            
            _localPlayerLeftHand = leftController;
            _localPlayerRightHand = rightController;
            
            Debug.Log($"Local player initialized - Head: {_localPlayerHead != null}, " +
                     $"Left: {_localPlayerLeftHand != null}, Right: {_localPlayerRightHand != null}");
        }
        
        // Simulate adding a remote player (for testing)
        public void SimulatePlayerJoin(string playerId = null)
        {
            if (playerId == null)
                playerId = $"Player_{Random.Range(1000, 9999)}";
            
            if (_remotePlayers.ContainsKey(playerId))
            {
                Debug.LogWarning($"Player {playerId} already exists");
                return;
            }
            
            // Create remote player representation
            GameObject remotePlayer = CreateRemotePlayerAvatar(playerId);
            _remotePlayers[playerId] = remotePlayer;
            _connectedPlayers++;
            
            Debug.Log($"Player {playerId} joined. Total players: {_connectedPlayers}");
            OnPlayerJoined?.Invoke();
        }
        
        public void SimulatePlayerLeave(string playerId)
        {
            if (!_remotePlayers.ContainsKey(playerId))
            {
                Debug.LogWarning($"Player {playerId} not found");
                return;
            }
            
            Destroy(_remotePlayers[playerId]);
            _remotePlayers.Remove(playerId);
            _connectedPlayers--;
            
            Debug.Log($"Player {playerId} left. Total players: {_connectedPlayers}");
            OnPlayerLeft?.Invoke();
        }
        
        private GameObject CreateRemotePlayerAvatar(string playerId)
        {
            GameObject avatar = new GameObject($"RemotePlayer_{playerId}");
            
            // Create simple avatar representation
            GameObject head = CreateAvatarPart(avatar.transform, "Head", Vector3.zero, Color.red);
            GameObject leftHand = CreateAvatarPart(avatar.transform, "LeftHand", new Vector3(-0.3f, -0.2f, 0.1f), Color.green);
            GameObject rightHand = CreateAvatarPart(avatar.transform, "RightHand", new Vector3(0.3f, -0.2f, 0.1f), Color.blue);
            
            // Position avatar randomly in the space
            avatar.transform.position = new Vector3(
                Random.Range(-2f, 2f),
                0f,
                Random.Range(-2f, 2f)
            );
            
            // Add a simple animation to show it's "alive"
            var animator = avatar.AddComponent<SimpleAvatarAnimator>();
            animator.Initialize(head.transform, leftHand.transform, rightHand.transform);
            
            return avatar;
        }
        
        private GameObject CreateAvatarPart(Transform parent, string name, Vector3 localPos, Color color)
        {
            GameObject part = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            part.name = name;
            part.transform.SetParent(parent);
            part.transform.localPosition = localPos;
            part.transform.localScale = Vector3.one * 0.1f;
            
            var renderer = part.GetComponent<Renderer>();
            var material = new Material(Shader.Find("Standard"));
            material.color = color;
            material.SetFloat("_Metallic", 0.5f);
            renderer.material = material;
            
            return part;
        }
        
        public void StopSession()
        {
            _isSessionActive = false;
            
            // Clean up remote players
            foreach (var kvp in _remotePlayers)
            {
                if (kvp.Value != null)
                    Destroy(kvp.Value);
            }
            _remotePlayers.Clear();
            _connectedPlayers = 1;
            
            Debug.Log("Multi-user session stopped");
        }
        
        // Method to get local player transform data (for future networking)
        public (Vector3 headPos, Quaternion headRot, Vector3 leftPos, Vector3 rightPos) GetLocalPlayerTransforms()
        {
            Vector3 headPos = _localPlayerHead != null ? _localPlayerHead.position : Vector3.zero;
            Quaternion headRot = _localPlayerHead != null ? _localPlayerHead.rotation : Quaternion.identity;
            Vector3 leftPos = _localPlayerLeftHand != null ? _localPlayerLeftHand.position : Vector3.zero;
            Vector3 rightPos = _localPlayerRightHand != null ? _localPlayerRightHand.position : Vector3.zero;
            
            return (headPos, headRot, leftPos, rightPos);
        }
        
        private void OnGUI()
        {
            if (!showDebugUI) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 300, 250));
            GUILayout.Label("=== Multi-User VR Manager ===");
            GUILayout.Label($"Session: {(_isSessionActive ? "Active" : "Inactive")}");
            GUILayout.Label($"Players Connected: {_connectedPlayers}/{maxPlayers}");
            GUILayout.Space(10);
            
            if (!_isSessionActive)
            {
                if (GUILayout.Button("Start Session"))
                {
                    StartSession();
                }
            }
            else
            {
                if (GUILayout.Button("Add Test Player"))
                {
                    SimulatePlayerJoin();
                }
                
                if (GUILayout.Button("Remove Test Player"))
                {
                    var keys = new List<string>(_remotePlayers.Keys);
                    if (keys.Count > 0)
                        SimulatePlayerLeave(keys[0]);
                }
                
                if (GUILayout.Button("Stop Session"))
                {
                    StopSession();
                }
            }
            
            GUILayout.Space(10);
            GUILayout.Label("Ready for Photon/Netcode upgrade!");
            GUILayout.EndArea();
        }
    }
    
    /// <summary>
    /// Simple animation component for remote player avatars
    /// </summary>
    public class SimpleAvatarAnimator : MonoBehaviour
    {
        private Transform _head, _leftHand, _rightHand;
        private float _animTime;
        
        public void Initialize(Transform head, Transform leftHand, Transform rightHand)
        {
            _head = head;
            _leftHand = leftHand;
            _rightHand = rightHand;
        }
        
        private void Update()
        {
            _animTime += Time.deltaTime;
            
            // Simple floating animation
            if (_head != null)
            {
                _head.localPosition = new Vector3(0, Mathf.Sin(_animTime * 2f) * 0.02f, 0);
            }
            
            if (_leftHand != null)
            {
                _leftHand.localPosition = new Vector3(-0.3f, -0.2f + Mathf.Sin(_animTime * 3f) * 0.05f, 0.1f);
            }
            
            if (_rightHand != null)
            {
                _rightHand.localPosition = new Vector3(0.3f, -0.2f + Mathf.Sin(_animTime * 2.5f) * 0.05f, 0.1f);
            }
        }
    }
}