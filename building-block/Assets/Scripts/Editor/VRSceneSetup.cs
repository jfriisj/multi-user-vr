using UnityEngine;
using UnityEditor;

namespace MultiUserVR.Setup
{
    /// <summary>
    /// Automated VR scene setup for Phase 1 MVP implementation
    /// Configures OVRCameraRig, OVRManager, and basic VR environment
    /// NOTE: This is an Editor-only script (uses UnityEditor namespace)
    /// </summary>
    public class VRSceneSetup
    {
        [MenuItem("Multi-User VR/Setup/Phase 1 - Configure VR Scene")]
        public static void SetupVRScene()
        {
            Debug.Log("[VR Setup] Starting Phase 1 scene configuration...");

            // Step 1: Remove default Main Camera (OVRCameraRig provides its own)
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (mainCamera != null && !mainCamera.GetComponent<OVRCameraRig>())
            {
                Debug.Log("[VR Setup] Removing default Main Camera");
                Object.DestroyImmediate(mainCamera);
            }

            // Step 2: Create or find OVRCameraRig
            GameObject cameraRig = GameObject.Find("OVRCameraRig");
            if (cameraRig == null)
            {
                cameraRig = new GameObject("OVRCameraRig");
                cameraRig.AddComponent<OVRCameraRig>();
                cameraRig.transform.position = Vector3.zero;
                Debug.Log("[VR Setup] Created OVRCameraRig");
            }
            else
            {
                Debug.Log("[VR Setup] OVRCameraRig already exists");
            }

            // Step 3: Configure OVRCameraRig component
            OVRCameraRig rigComponent = cameraRig.GetComponent<OVRCameraRig>();
            if (rigComponent != null)
            {
                // Note: In Meta XR SDK v78, tracking origin is set via OVRManager
                Debug.Log("[VR Setup] OVRCameraRig component found");
            }

            // Step 4: Create or find OVRManager
            GameObject managerObj = GameObject.Find("OVRManager");
            OVRManager manager = Object.FindFirstObjectByType<OVRManager>();
            
            if (manager == null)
            {
                if (managerObj == null)
                {
                    managerObj = new GameObject("OVRManager");
                }
                manager = managerObj.AddComponent<OVRManager>();
                Debug.Log("[VR Setup] Created OVRManager");
            }
            else
            {
                Debug.Log("[VR Setup] OVRManager already exists");
            }

            // Step 5: Configure OVRManager for Quest 3 and safety features
            ConfigureOVRManager(manager);

            // Step 6: Add input data sources for controllers/hands
            SetupInputSources(cameraRig);

            // Step 7: Mark scene as dirty so Unity saves changes
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene()
            );

            Debug.Log("[VR Setup] ✅ Phase 1 VR scene configuration complete!");
            Debug.Log("[VR Setup] Next steps: Test in Meta XR Simulator (Window > Meta XR > Test in Simulator)");
        }

        private static void ConfigureOVRManager(OVRManager manager)
        {
            // Quest 3 specific settings for Meta XR SDK v78
            manager.trackingOriginType = OVRManager.TrackingOrigin.FloorLevel;
            
            // Enable passthrough for safety features (Phase 3 - co-located VR)
            manager.isInsightPassthroughEnabled = true;
            
            Debug.Log("[VR Setup] OVRManager configured: FloorLevel tracking, Passthrough enabled");
            Debug.Log("[VR Setup] Note: Frame rate and hand tracking are auto-configured by Quest 3 runtime");
        }

        private static void SetupInputSources(GameObject cameraRig)
        {
            // Check if input sources already exist
            if (Object.FindFirstObjectByType<Oculus.Interaction.Input.FromOVRControllerDataSource>() != null)
            {
                Debug.Log("[VR Setup] Input data sources already configured");
                return;
            }

            // Create Controller Data Sources parent object
            GameObject inputSources = new GameObject("InputSources");
            inputSources.transform.SetParent(cameraRig.transform);
            inputSources.transform.localPosition = Vector3.zero;

            // Note: FromOVRControllerDataSource and FromOVRHandDataSource require
            // proper OVRCameraRig references which are set up by Meta XR Building Blocks
            // For MVP, we'll use Building Blocks to add these properly in Unity Editor

            Debug.Log("[VR Setup] InputSources parent created. Use Meta XR Building Blocks to add:");
            Debug.Log("  - Window > Meta XR > Tools > Building Blocks > Interaction SDK > Controller");
            Debug.Log("  - Window > Meta XR > Tools > Building Blocks > Interaction SDK > Hand Tracking");
        }

        [MenuItem("Multi-User VR/Setup/Phase 1 - Add Floor Plane")]
        public static void AddFloorPlane()
        {
            // Create a simple floor for testing
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floor.name = "Floor";
            floor.transform.position = Vector3.zero;
            floor.transform.localScale = new Vector3(5, 1, 5); // 50m x 50m floor
            
            // Add collider for potential physics testing
            if (!floor.GetComponent<Collider>())
            {
                floor.AddComponent<MeshCollider>();
            }

            Debug.Log("[VR Setup] Added floor plane for testing");
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene()
            );
        }

        [MenuItem("Multi-User VR/Setup/Validate Phase 1 Setup")]
        public static void ValidateSetup()
        {
            Debug.Log("=== Phase 1 Setup Validation ===");
            
            bool isValid = true;
            
            // Check OVRCameraRig
            OVRCameraRig cameraRig = Object.FindFirstObjectByType<OVRCameraRig>();
            if (cameraRig == null)
            {
                Debug.LogError("❌ OVRCameraRig not found!");
                isValid = false;
            }
            else
            {
                Debug.Log("✅ OVRCameraRig found");
            }

            // Check OVRManager
            OVRManager manager = Object.FindFirstObjectByType<OVRManager>();
            if (manager == null)
            {
                Debug.LogError("❌ OVRManager not found!");
                isValid = false;
            }
            else
            {
                Debug.Log("✅ OVRManager found");
                Debug.Log($"   - Tracking Origin: {manager.trackingOriginType}");
                Debug.Log($"   - Passthrough Enabled: {manager.isInsightPassthroughEnabled}");
                Debug.Log($"   - SDK Version: Meta XR v78 (hand tracking auto-configured by runtime)");
            }

            // Check for default Main Camera (should be removed)
            GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            if (mainCam != null && !mainCam.GetComponentInParent<OVRCameraRig>())
            {
                Debug.LogWarning("⚠️ Default Main Camera still present (should be removed)");
            }

            // Check Unity Netcode package
            #if UNITY_NETCODE_GAMEOBJECTS
            Debug.Log("✅ Unity Netcode for GameObjects package installed");
            #else
            Debug.LogWarning("⚠️ Unity Netcode for GameObjects not detected - package may still be importing");
            #endif

            if (isValid)
            {
                Debug.Log("=== ✅ Phase 1 Setup Valid ===");
                Debug.Log("Ready to test in Meta XR Simulator!");
            }
            else
            {
                Debug.LogError("=== ❌ Phase 1 Setup Incomplete ===");
                Debug.LogError("Run 'Multi-User VR > Setup > Phase 1 - Configure VR Scene' to fix");
            }
        }
    }
}
