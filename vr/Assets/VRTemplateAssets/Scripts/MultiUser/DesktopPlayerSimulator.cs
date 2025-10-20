using UnityEngine;
using Unity.Netcode;

namespace Unity.VRTemplate.MultiUser
{
    /// <summary>
    /// Desktop player simulation for testing multi-user VR with keyboard/mouse
    /// Allows Unity Editor to act as a desktop client while VR headset is the host
    /// </summary>
    public class DesktopPlayerSimulator : MonoBehaviour
    {
        [Header("Desktop Controls")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float mouseSpeed = 2f;
        [SerializeField] private KeyCode grabKey = KeyCode.Space;
        [SerializeField] private KeyCode switchHandKey = KeyCode.Tab;
        
        [Header("Visual Feedback")]
        [SerializeField] private bool showDesktopPlayer = true;
        [SerializeField] private GameObject desktopPlayerPrefab;
        
        // Desktop player components
        private Camera desktopCamera;
        private Transform desktopHead;
        private Transform desktopLeftHand;
        private Transform desktopRightHand;
        private bool useLeftHand = true;
        
        // Input tracking
        private Vector2 mouseInput;
        private Vector3 currentPosition;
        
        private void Start()
        {
            // Enable desktop controls in editor or if XR is not active
            bool enableDesktop = Application.isEditor || !UnityEngine.XR.XRSettings.enabled || !UnityEngine.XR.XRSettings.isDeviceActive;
            
            if (enableDesktop)
            {
                SetupDesktopPlayer();
                
                // Unlock cursor for desktop controls
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
                Debug.Log("Desktop Player Simulator enabled - Click in Game view, then use WASD to move, mouse to look, Space to grab");
                Debug.Log("Press ESC to unlock mouse cursor");
            }
            else
            {
                enabled = false;
            }
        }
        
        private void SetupDesktopPlayer()
        {
            // Create desktop player visual
            if (desktopPlayerPrefab != null)
            {
                var desktopPlayer = Instantiate(desktopPlayerPrefab);
                desktopPlayer.name = "Desktop Player";
                
                // Find components
                desktopHead = desktopPlayer.transform.Find("Head");
                desktopLeftHand = desktopPlayer.transform.Find("LeftHand");
                desktopRightHand = desktopPlayer.transform.Find("RightHand");
            }
            else
            {
                CreateSimpleDesktopPlayer();
            }
            
            // Setup camera
            desktopCamera = Camera.main;
            if (desktopCamera == null)
            {
                desktopCamera = FindObjectOfType<Camera>();
            }
            
            // Initial position
            currentPosition = transform.position + Vector3.up * 1.7f; // Head height
        }
        
        private void CreateSimpleDesktopPlayer()
        {
            var playerObj = new GameObject("Desktop Player");
            
            // Create head
            desktopHead = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            desktopHead.name = "Head";
            desktopHead.localScale = Vector3.one * 0.2f;
            desktopHead.GetComponent<Renderer>().material.color = Color.cyan;
            desktopHead.SetParent(playerObj.transform);
            
            // Create hands
            desktopLeftHand = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            desktopLeftHand.name = "LeftHand";
            desktopLeftHand.localScale = Vector3.one * 0.1f;
            desktopLeftHand.GetComponent<Renderer>().material.color = Color.green;
            desktopLeftHand.SetParent(playerObj.transform);
            
            desktopRightHand = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            desktopRightHand.name = "RightHand";
            desktopRightHand.localScale = Vector3.one * 0.1f;
            desktopRightHand.GetComponent<Renderer>().material.color = Color.red;
            desktopRightHand.SetParent(playerObj.transform);
        }
        
        private void Update()
        {
            HandleCursorControl();
            HandleMovement();
            HandleMouseLook();
            HandleHandControl();
            HandleGrabbing();
            
            UpdateVisualComponents();
        }
        
        private void HandleCursorControl()
        {
            // Toggle cursor lock with right mouse click
            if (Input.GetMouseButtonDown(1))
            {
                if (Cursor.lockState == CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Debug.Log("Mouse locked - use mouse to look around");
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Debug.Log("Mouse unlocked - click UI elements");
                }
            }
            
            // ESC to unlock cursor
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        
        private void HandleMovement()
        {
            // WASD movement
            Vector3 moveInput = Vector3.zero;
            
            if (Input.GetKey(KeyCode.W)) moveInput += transform.forward;
            if (Input.GetKey(KeyCode.S)) moveInput -= transform.forward;
            if (Input.GetKey(KeyCode.A)) moveInput -= transform.right;
            if (Input.GetKey(KeyCode.D)) moveInput += transform.right;
            if (Input.GetKey(KeyCode.Q)) moveInput += Vector3.up;
            if (Input.GetKey(KeyCode.E)) moveInput -= Vector3.up;
            
            currentPosition += moveInput * moveSpeed * Time.deltaTime;
            transform.position = currentPosition;
        }
        
        private void HandleMouseLook()
        {
            // Only handle mouse look when cursor is locked
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                mouseInput.x += Input.GetAxis("Mouse X") * mouseSpeed;
                mouseInput.y -= Input.GetAxis("Mouse Y") * mouseSpeed;
                mouseInput.y = Mathf.Clamp(mouseInput.y, -90f, 90f);
                
                transform.rotation = Quaternion.Euler(mouseInput.y, mouseInput.x, 0f);
            }
        }
        
        private void HandleHandControl()
        {
            // Switch active hand
            if (Input.GetKeyDown(switchHandKey))
            {
                useLeftHand = !useLeftHand;
                Debug.Log($"Switched to {(useLeftHand ? "Left" : "Right")} hand");
            }
            
            // Position active hand in front of camera
            var activeHand = useLeftHand ? desktopLeftHand : desktopRightHand;
            if (activeHand != null)
            {
                Vector3 handOffset = useLeftHand ? Vector3.left * 0.3f : Vector3.right * 0.3f;
                activeHand.position = transform.position + transform.forward * 0.5f + handOffset;
                activeHand.rotation = transform.rotation;
            }
        }
        
        private void HandleGrabbing()
        {
            if (Input.GetKeyDown(grabKey))
            {
                // Simulate grab attempt
                var activeHand = useLeftHand ? desktopLeftHand : desktopRightHand;
                TryGrabNearestObject(activeHand.position);
            }
        }
        
        private void TryGrabNearestObject(Vector3 handPosition)
        {
            // Find nearest interactable within grab range
            var interactables = FindObjectsOfType<NetworkedInteractable>();
            NetworkedInteractable nearest = null;
            float nearestDistance = 1f; // Grab range
            
            foreach (var interactable in interactables)
            {
                float distance = Vector3.Distance(handPosition, interactable.transform.position);
                if (distance < nearestDistance)
                {
                    nearest = interactable;
                    nearestDistance = distance;
                }
            }
            
            if (nearest != null)
            {
                if (!nearest.IsGrabbed)
                {
                    nearest.OnGrabbed();
                    Debug.Log($"Desktop player grabbed: {nearest.name}");
                }
                else
                {
                    nearest.OnReleased();
                    Debug.Log($"Desktop player released: {nearest.name}");
                }
            }
        }
        
        private void UpdateVisualComponents()
        {
            if (desktopHead != null)
            {
                desktopHead.position = currentPosition;
                desktopHead.rotation = transform.rotation;
            }
            
            // Highlight active hand
            if (desktopLeftHand != null)
            {
                var leftRenderer = desktopLeftHand.GetComponent<Renderer>();
                leftRenderer.material.color = useLeftHand ? Color.yellow : Color.green;
            }
            
            if (desktopRightHand != null)
            {
                var rightRenderer = desktopRightHand.GetComponent<Renderer>();
                rightRenderer.material.color = !useLeftHand ? Color.yellow : Color.red;
            }
        }
        
        private void OnGUI()
        {
            if (!showDesktopPlayer) return;
            
            try
            {
                GUILayout.BeginArea(new Rect(Screen.width - 320, 10, 310, 200));
                GUILayout.Label("=== Desktop Player Controls ===");
                GUILayout.Label("WASD - Move");
                GUILayout.Label("QE - Up/Down");
                GUILayout.Label("Right Click - Lock/unlock mouse");
                GUILayout.Label("Mouse - Look around (when locked)");
                GUILayout.Label($"Tab - Switch hand ({(useLeftHand ? "Left" : "Right")})");
                GUILayout.Label("Space - Grab/Release");
                GUILayout.Label("ESC - Unlock mouse");
                GUILayout.Label("");
                
                string mouseStatus = Cursor.lockState == CursorLockMode.Locked ? "LOCKED (look mode)" : "FREE (UI mode)";
                GUILayout.Label($"Mouse Status: {mouseStatus}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"GUI Error in DesktopPlayerSimulator: {ex.Message}");
            }
            finally
            {
                GUILayout.EndArea();
            }
        }
    }
}