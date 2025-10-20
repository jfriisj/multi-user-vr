using UnityEngine;
using Unity.Netcode;

namespace Unity.VRTemplate.MultiUser
{
    /// <summary>
    /// Testing configuration for single-headset multi-user development
    /// Provides various testing modes and simulation options
    /// </summary>
    public class MultiUserTester : MonoBehaviour
    {
        [Header("Testing Mode")]
        [SerializeField] private TestingMode mode = TestingMode.EditorAsClient;
        [SerializeField] private bool enableDesktopSimulation = true;
        [SerializeField] private bool enableBots = false;
        
        [Header("Network Testing")]
        [SerializeField] private bool autoStartTesting = true;
        [SerializeField] private float botUpdateRate = 10f;
        [SerializeField] private string testServerIP = "127.0.0.1";
        
        [Header("Simulation Settings")]
        [SerializeField] private int numberOfBots = 2;
        [SerializeField] private float botMovementRadius = 3f;
        [SerializeField] private float botMovementSpeed = 1f;
        
        public enum TestingMode
        {
            EditorAsHost,      // Unity Editor acts as host, VR headset connects as client
            EditorAsClient,    // VR headset acts as host, Unity Editor connects as client  
            StandaloneTest,    // Both run on same PC (different processes)
            BotSimulation      // AI bots simulate other players
        }
        
        private NetworkManager networkManager;
        private DesktopPlayerSimulator desktopSim;
        private TestBot[] bots;
        
        private void Start()
        {
            networkManager = FindObjectOfType<NetworkManager>();
            
            if (enableDesktopSimulation)
            {
                SetupDesktopSimulation();
            }
            
            if (autoStartTesting)
            {
                StartTesting();
            }
        }
        
        private void SetupDesktopSimulation()
        {
            // Add desktop simulator if not in VR
            if (!UnityEngine.XR.XRSettings.enabled)
            {
                desktopSim = gameObject.AddComponent<DesktopPlayerSimulator>();
                Debug.Log("Desktop simulation enabled for testing");
            }
        }
        
        [ContextMenu("Start Testing")]
        public void StartTesting()
        {
            switch (mode)
            {
                case TestingMode.EditorAsHost:
                    StartEditorAsHost();
                    break;
                case TestingMode.EditorAsClient:
                    StartEditorAsClient();
                    break;
                case TestingMode.BotSimulation:
                    StartBotSimulation();
                    break;
                default:
                    StartEditorAsClient();
                    break;
            }
        }
        
        private void StartEditorAsHost()
        {
            if (networkManager != null)
            {
                networkManager.StartHost();
                Debug.Log("Editor started as HOST - VR headset should connect as client");
                ShowInstructions("HOST", "VR headset should select 'Join Session'");
            }
        }
        
        private void StartEditorAsClient()
        {
            if (networkManager != null)
            {
                networkManager.StartClient();
                Debug.Log("Editor started as CLIENT - VR headset should be host");
                ShowInstructions("CLIENT", "VR headset should select 'Start Host' first");
            }
        }
        
        private void StartBotSimulation()
        {
            if (enableBots)
            {
                CreateTestBots();
            }
            
            if (networkManager != null)
            {
                networkManager.StartHost();
                Debug.Log("Bot simulation started - AI players will join automatically");
            }
        }
        
        private void CreateTestBots()
        {
            bots = new TestBot[numberOfBots];
            
            for (int i = 0; i < numberOfBots; i++)
            {
                var botObj = new GameObject($"TestBot_{i}");
                var bot = botObj.AddComponent<TestBot>();
                bot.Initialize(i, botMovementRadius, botMovementSpeed, botUpdateRate);
                bots[i] = bot;
            }
            
            Debug.Log($"Created {numberOfBots} test bots");
        }
        
        private void ShowInstructions(string role, string instruction)
        {
            Debug.Log($"=== MULTI-USER TESTING ===");
            Debug.Log($"Editor Role: {role}");
            Debug.Log($"Instruction: {instruction}");
            Debug.Log($"Testing Mode: {mode}");
        }
        
        private void OnGUI()
        {
            try
            {
                GUILayout.BeginArea(new Rect(10, 220, 400, 300));
                GUILayout.Label("=== Multi-User Testing ===");
                GUILayout.Label($"Mode: {mode}");
                
                if (networkManager != null)
                {
                    GUILayout.Label($"Network Status: {GetNetworkStatus()}");
                    
                    // Only show client count if we're server/host
                    if (networkManager.IsServer || networkManager.IsHost)
                    {
                        GUILayout.Label($"Connected Clients: {networkManager.ConnectedClients.Count}");
                    }
                    else if (networkManager.IsClient)
                    {
                        GUILayout.Label("Connected as Client");
                    }
                }
                else
                {
                    GUILayout.Label("Network Status: No NetworkManager");
                }
            
            GUILayout.Space(10);
            
            if (!networkManager.IsListening)
            {
                if (GUILayout.Button("Start as Host (Editor)"))
                {
                    mode = TestingMode.EditorAsHost;
                    StartEditorAsHost();
                }
                
                if (GUILayout.Button("Start as Client (Editor)"))
                {
                    mode = TestingMode.EditorAsClient;
                    StartEditorAsClient();
                }
                
                if (GUILayout.Button("Start Bot Simulation"))
                {
                    mode = TestingMode.BotSimulation;
                    StartBotSimulation();
                }
            }
            else
            {
                if (GUILayout.Button("Stop Testing"))
                {
                    networkManager.Shutdown();
                }
                
                GUILayout.Space(10);
                GUILayout.Label("=== Testing Instructions ===");
                
                switch (mode)
                {
                    case TestingMode.EditorAsHost:
                        GUILayout.Label("1. Editor is HOST");
                        GUILayout.Label("2. Build to VR headset");
                        GUILayout.Label("3. In VR: Click 'Join Session'");
                        break;
                        
                    case TestingMode.EditorAsClient:
                        GUILayout.Label("1. Build to VR headset");
                        GUILayout.Label("2. In VR: Click 'Start Host'");
                        GUILayout.Label("3. Editor will connect as client");
                        break;
                        
                    case TestingMode.BotSimulation:
                        GUILayout.Label("1. AI bots simulate other players");
                        GUILayout.Label("2. Test interactions with bots");
                        GUILayout.Label("3. Observe networked behavior");
                        break;
                }
            }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"GUI Error in MultiUserTester: {ex.Message}");
            }
            finally
            {
                GUILayout.EndArea();
            }
        }
        
        private string GetNetworkStatus()
        {
            if (networkManager.IsHost) return "Host";
            if (networkManager.IsClient) return "Client";
            if (networkManager.IsServer) return "Server";
            return "Disconnected";
        }
    }
    
    /// <summary>
    /// Simple AI bot for testing multi-user interactions
    /// </summary>
    public class TestBot : MonoBehaviour
    {
        private int botId;
        private float movementRadius;
        private float movementSpeed;
        private float updateRate;
        private float updateTimer;
        
        private Vector3 centerPosition;
        private Vector3 targetPosition;
        
        public void Initialize(int id, float radius, float speed, float rate)
        {
            botId = id;
            movementRadius = radius;
            movementSpeed = speed;
            updateRate = rate;
            centerPosition = transform.position;
            
            // Create visual representation
            var visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            visual.transform.SetParent(transform);
            visual.transform.localPosition = Vector3.zero;
            visual.GetComponent<Renderer>().material.color = Color.magenta;
            visual.name = $"Bot {id} Visual";
            
            ChooseNewTarget();
        }
        
        private void Update()
        {
            updateTimer += Time.deltaTime;
            
            if (updateTimer >= 1f / updateRate)
            {
                updateTimer = 0f;
                
                // Move towards target
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
                
                // Choose new target when reached
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    ChooseNewTarget();
                }
            }
        }
        
        private void ChooseNewTarget()
        {
            // Random position within movement radius
            Vector3 randomDirection = Random.insideUnitSphere * movementRadius;
            randomDirection.y = Mathf.Abs(randomDirection.y); // Keep above ground
            targetPosition = centerPosition + randomDirection;
        }
    }
}