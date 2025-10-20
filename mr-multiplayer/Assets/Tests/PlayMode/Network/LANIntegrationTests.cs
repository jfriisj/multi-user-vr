using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using XRMultiplayer;

namespace XRMultiplayer.Tests.Integration
{
    /// <summary>
    /// Integration tests for complete LAN connection workflow.
    /// Tests end-to-end scenarios including UI interactions, network establishment,
    /// and component coordination across the system.
    /// </summary>
    public class LANIntegrationTests
    {
        private Scene m_TestScene;
        private GameObject m_NetworkManagerObject;
        private NetworkManager m_NetworkManager;
        private LANConnectionManager m_LANConnectionManager;
        private ConnectionModeManager m_ConnectionModeManager;
        private LANConnectionStatus m_LANConnectionStatus;
        private UnityTransport m_Transport;

        private const float k_NetworkTimeout = 10.0f;
        private const float k_ConnectionEstablishmentTime = 5.0f;
        private const ushort k_TestPort = 7777;

        #region Setup and Teardown

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            // Create a new test scene
            m_TestScene = SceneManager.CreateScene("LANIntegrationTestScene");
            SceneManager.SetActiveScene(m_TestScene);

            // Wait for scene to be fully loaded
            yield return null;

            // Create NetworkManager GameObject with all required components
            m_NetworkManagerObject = new GameObject("NetworkManager");
            SceneManager.MoveGameObjectToScene(m_NetworkManagerObject, m_TestScene);

            // Add NetworkManager
            m_NetworkManager = m_NetworkManagerObject.AddComponent<NetworkManager>();
            
            // Add and configure UnityTransport
            m_Transport = m_NetworkManagerObject.AddComponent<UnityTransport>();
            m_NetworkManager.NetworkConfig = new NetworkConfig();
            m_NetworkManager.NetworkConfig.NetworkTransport = m_Transport;

            // Add LAN components
            m_LANConnectionManager = m_NetworkManagerObject.AddComponent<LANConnectionManager>();
            m_ConnectionModeManager = m_NetworkManagerObject.AddComponent<ConnectionModeManager>();
            m_LANConnectionStatus = m_NetworkManagerObject.AddComponent<LANConnectionStatus>();

            // Wait for components to initialize
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            // Shutdown NetworkManager if running
            if (m_NetworkManager != null && m_NetworkManager.IsListening)
            {
                m_NetworkManager.Shutdown();
                yield return new WaitForSeconds(0.5f);
            }

            // Destroy NetworkManager GameObject
            if (m_NetworkManagerObject != null)
            {
                Object.DestroyImmediate(m_NetworkManagerObject);
            }

            // Unload test scene
            if (m_TestScene.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(m_TestScene);
            }

            // Wait for cleanup
            yield return null;
        }

        #endregion

        #region Host Workflow Integration Tests

        [UnityTest]
        public IEnumerator HostWorkflow_ComponentsInitialize_Successfully()
        {
            // Arrange
            bool hostingStarted = false;
            m_LANConnectionManager.OnStatusChanged += (status) =>
            {
                if (status == LANConnectionManager.LANConnectionStatus.Connecting)
                {
                    hostingStarted = true;
                }
            };

            // Act
            m_LANConnectionManager.HostLAN(k_TestPort);
            yield return new WaitForSeconds(0.5f);

            // Assert
            Assert.IsTrue(hostingStarted, "Hosting should start");
            Assert.IsTrue(m_LANConnectionManager.IsHosting, "LANConnectionManager should be in hosting mode");
        }

        [UnityTest]
        public IEnumerator HostWorkflow_NetworkManagerStarts_AsServer()
        {
            // Act
            m_LANConnectionManager.HostLAN(k_TestPort);
            yield return new WaitForSeconds(1.0f);

            // Assert
            Assert.IsTrue(m_NetworkManager.IsServer || m_NetworkManager.IsHost, 
                "NetworkManager should be running as server or host");
        }

        [UnityTest]
        public IEnumerator HostWorkflow_TransportConfiguration_IsCorrect()
        {
            // Act
            m_LANConnectionManager.HostLAN(k_TestPort);
            yield return null;

            // Assert
            Assert.AreEqual(k_TestPort, m_Transport.ConnectionData.Port, 
                "Transport port should match requested port");
            Assert.AreEqual("0.0.0.0", m_Transport.ConnectionData.Address, 
                "Host should listen on all interfaces (0.0.0.0)");
        }

        [UnityTest]
        public IEnumerator HostWorkflow_StatusProgression_FollowsExpectedSequence()
        {
            // Arrange
            var statusHistory = new System.Collections.Generic.List<LANConnectionManager.LANConnectionStatus>();
            m_LANConnectionManager.OnStatusChanged += (status) => statusHistory.Add(status);

            // Act
            m_LANConnectionManager.HostLAN(k_TestPort);
            yield return new WaitForSeconds(2.0f);

            // Assert
            Assert.Contains(LANConnectionManager.LANConnectionStatus.Connecting, statusHistory, 
                "Should transition through Connecting state");
            CollectionAssert.IsOrdered(statusHistory, 
                "Status should progress in logical order");
        }

        [UnityTest]
        public IEnumerator HostWorkflow_LocalIPAddress_IsDetected()
        {
            // Act
            m_LANConnectionManager.HostLAN(k_TestPort);
            yield return null;

            // Assert
            string localIP = m_LANConnectionManager.LocalIPAddress;
            Assert.IsNotNull(localIP, "Local IP address should be detected");
            if (localIP != null)
            {
                Assert.IsTrue(m_LANConnectionManager.ValidateIPAddress(localIP), 
                    "Detected local IP should be valid");
            }
        }

        #endregion

        #region Client Workflow Integration Tests

        [UnityTest]
        public IEnumerator ClientWorkflow_ValidIP_InitiatesConnection()
        {
            // Arrange
            string testIP = "192.168.1.100";
            bool connectionStarted = false;
            m_LANConnectionManager.OnStatusChanged += (status) =>
            {
                if (status == LANConnectionManager.LANConnectionStatus.Connecting)
                {
                    connectionStarted = true;
                }
            };

            // Act
            m_LANConnectionManager.JoinLAN(testIP, k_TestPort);
            yield return new WaitForSeconds(0.5f);

            // Assert
            Assert.IsTrue(connectionStarted, "Connection attempt should start");
            Assert.IsFalse(m_LANConnectionManager.IsHosting, "Should not be in hosting mode");
        }

        [UnityTest]
        public IEnumerator ClientWorkflow_InvalidIP_TriggersFailure()
        {
            // Arrange
            string invalidIP = "999.999.999.999";
            bool failureEventReceived = false;
            string failureReason = null;

            m_LANConnectionManager.OnConnectionFailed += (reason) =>
            {
                failureEventReceived = true;
                failureReason = reason;
            };

            // Act
            m_LANConnectionManager.JoinLAN(invalidIP, k_TestPort);
            yield return new WaitForSeconds(0.5f);

            // Assert
            Assert.IsTrue(failureEventReceived, "Failure event should be triggered");
            Assert.IsNotNull(failureReason, "Failure reason should be provided");
            StringAssert.Contains("invalid", failureReason.ToLower(), 
                "Failure message should mention invalid IP");
        }

        [UnityTest]
        public IEnumerator ClientWorkflow_TransportConfiguration_MatchesTargetIP()
        {
            // Arrange
            string testIP = "192.168.1.100";

            // Act
            m_LANConnectionManager.JoinLAN(testIP, k_TestPort);
            yield return null;

            // Assert
            Assert.AreEqual(testIP, m_Transport.ConnectionData.Address, 
                "Transport should be configured with target IP");
            Assert.AreEqual(k_TestPort, m_Transport.ConnectionData.Port, 
                "Transport should be configured with target port");
        }

        [UnityTest]
        public IEnumerator ClientWorkflow_Timeout_HandledCorrectly()
        {
            // Arrange
            string unreachableIP = "192.168.254.254"; // Typically unreachable
            bool timeoutDetected = false;
            
            m_LANConnectionManager.OnStatusChanged += (status) =>
            {
                if (status == LANConnectionManager.LANConnectionStatus.Timeout ||
                    status == LANConnectionManager.LANConnectionStatus.Failed)
                {
                    timeoutDetected = true;
                }
            };

            // Act
            m_LANConnectionManager.JoinLAN(unreachableIP, k_TestPort);
            
            // Wait for connection timeout
            float waitTime = 0;
            while (!timeoutDetected && waitTime < k_NetworkTimeout)
            {
                yield return new WaitForSeconds(0.5f);
                waitTime += 0.5f;
            }

            // Assert
            Assert.IsTrue(timeoutDetected, "Timeout should be detected within timeout period");
        }

        #endregion

        #region Connection Mode Integration Tests

        [UnityTest]
        public IEnumerator ConnectionMode_SwitchToLAN_ActivatesLANComponents()
        {
            // Arrange
            bool modeChanged = false;
            m_ConnectionModeManager.OnModeChanged += (mode) =>
            {
                if (mode == ConnectionModeManager.ConnectionMode.LANDirect)
                {
                    modeChanged = true;
                }
            };

            // Act
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.LANDirect);
            yield return null;

            // Assert
            Assert.IsTrue(modeChanged, "Mode change event should fire");
            Assert.AreEqual(ConnectionModeManager.ConnectionMode.LANDirect, m_ConnectionModeManager.CurrentMode, 
                "Current mode should be LANDirect");
        }

        [UnityTest]
        public IEnumerator ConnectionMode_SwitchToCloud_DeactivatesLANComponents()
        {
            // Arrange
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.LANDirect);
            yield return null;

            bool modeChanged = false;
            m_ConnectionModeManager.OnModeChanged += (mode) =>
            {
                if (mode == ConnectionModeManager.ConnectionMode.Cloud)
                {
                    modeChanged = true;
                }
            };

            // Act
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.Cloud);
            yield return null;

            // Assert
            Assert.IsTrue(modeChanged, "Mode change event should fire");
            Assert.AreEqual(ConnectionModeManager.ConnectionMode.Cloud, m_ConnectionModeManager.CurrentMode, 
                "Current mode should be Cloud");
        }

        [UnityTest]
        public IEnumerator ConnectionMode_LANHosting_PreventsModeSwitch()
        {
            // Arrange
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.LANDirect);
            m_LANConnectionManager.HostLAN(k_TestPort);
            yield return new WaitForSeconds(0.5f);

            bool switchFailed = false;
            m_ConnectionModeManager.OnModeSwitchFailed += (reason) => { switchFailed = true; };

            // Act
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.Cloud);
            yield return new WaitForSeconds(0.2f);

            // Assert
            Assert.IsTrue(switchFailed, "Should trigger failure when trying to switch while hosting");
            Assert.AreEqual(ConnectionModeManager.ConnectionMode.LANDirect, m_ConnectionModeManager.CurrentMode, 
                "Mode should remain LANDirect");
        }

        #endregion

        #region Error Handling Integration Tests

        [UnityTest]
        public IEnumerator ErrorHandling_ConnectionFailure_NotifiesStatus()
        {
            // Arrange
            string invalidIP = "invalid.ip.address";
            bool statusComponentNotified = false;
            
            // Subscribe to LANConnectionStatus error events
            if (m_LANConnectionStatus != null)
            {
                m_LANConnectionStatus.OnErrorCategoryChanged += (category) =>
                {
                    statusComponentNotified = true;
                };
            }

            // Act
            m_LANConnectionManager.JoinLAN(invalidIP, k_TestPort);
            yield return new WaitForSeconds(1.0f);

            // Assert
            Assert.IsTrue(statusComponentNotified, "LANConnectionStatus should be notified of errors");
        }

        [UnityTest]
        public IEnumerator ErrorHandling_MultipleFailures_MaintainsStability()
        {
            // Act - Trigger multiple failures
            m_LANConnectionManager.JoinLAN("invalid1", k_TestPort);
            yield return new WaitForSeconds(0.5f);

            m_LANConnectionManager.JoinLAN("invalid2", k_TestPort);
            yield return new WaitForSeconds(0.5f);

            m_LANConnectionManager.JoinLAN("999.999.999.999", k_TestPort);
            yield return new WaitForSeconds(0.5f);

            // Assert
            Assert.AreEqual(LANConnectionManager.LANConnectionStatus.Failed, 
                m_LANConnectionManager.Status, 
                "Status should reflect failure state");
            Assert.IsNotNull(m_LANConnectionManager, "Component should remain stable");
            Assert.IsNotNull(m_NetworkManager, "NetworkManager should remain stable");
        }

        [UnityTest]
        public IEnumerator ErrorHandling_DisconnectDuringConnection_CleansUpProperly()
        {
            // Arrange
            string testIP = "192.168.1.100";
            m_LANConnectionManager.JoinLAN(testIP, k_TestPort);
            yield return new WaitForSeconds(0.2f);

            // Act - Disconnect while connecting
            m_LANConnectionManager.Disconnect();
            yield return new WaitForSeconds(0.5f);

            // Assert
            Assert.AreEqual(LANConnectionManager.LANConnectionStatus.Disconnected, 
                m_LANConnectionManager.Status, 
                "Status should be Disconnected");
            Assert.IsFalse(m_NetworkManager.IsListening, 
                "NetworkManager should not be listening");
        }

        #endregion

        #region State Transition Integration Tests

        [UnityTest]
        public IEnumerator StateTransition_DisconnectedToHosting_Smooth()
        {
            // Arrange
            var statusHistory = new System.Collections.Generic.List<LANConnectionManager.LANConnectionStatus>();
            m_LANConnectionManager.OnStatusChanged += (status) => statusHistory.Add(status);

            // Act
            m_LANConnectionManager.HostLAN(k_TestPort);
            yield return new WaitForSeconds(2.0f);

            // Assert
            Assert.Greater(statusHistory.Count, 0, "At least one status change should occur");
            Assert.AreEqual(LANConnectionManager.LANConnectionStatus.Disconnected, 
                statusHistory[0], 
                "Should start from Disconnected state");
        }

        [UnityTest]
        public IEnumerator StateTransition_HostingToDisconnected_Smooth()
        {
            // Arrange
            m_LANConnectionManager.HostLAN(k_TestPort);
            yield return new WaitForSeconds(1.0f);

            var statusHistory = new System.Collections.Generic.List<LANConnectionManager.LANConnectionStatus>();
            m_LANConnectionManager.OnStatusChanged += (status) => statusHistory.Add(status);

            // Act
            m_LANConnectionManager.Disconnect();
            yield return new WaitForSeconds(0.5f);

            // Assert
            Assert.Contains(LANConnectionManager.LANConnectionStatus.Disconnected, statusHistory, 
                "Should transition to Disconnected");
            Assert.IsFalse(m_LANConnectionManager.IsHosting, "Should not be hosting");
        }

        [UnityTest]
        public IEnumerator StateTransition_DisconnectedToClient_Smooth()
        {
            // Arrange
            string testIP = "192.168.1.100";
            var statusHistory = new System.Collections.Generic.List<LANConnectionManager.LANConnectionStatus>();
            m_LANConnectionManager.OnStatusChanged += (status) => statusHistory.Add(status);

            // Act
            m_LANConnectionManager.JoinLAN(testIP, k_TestPort);
            yield return new WaitForSeconds(1.0f);

            // Assert
            Assert.Greater(statusHistory.Count, 0, "At least one status change should occur");
            Assert.Contains(LANConnectionManager.LANConnectionStatus.Connecting, statusHistory, 
                "Should transition through Connecting");
        }

        #endregion

        #region Component Coordination Tests

        [UnityTest]
        public IEnumerator ComponentCoordination_LANManagerAndStatus_Synchronized()
        {
            // Arrange
            bool managerStatusChanged = false;
            bool statusComponentNotified = false;

            m_LANConnectionManager.OnStatusChanged += (status) => managerStatusChanged = true;
            
            if (m_LANConnectionStatus != null)
            {
                m_LANConnectionStatus.OnErrorCategoryChanged += (category) => 
                    statusComponentNotified = true;
            }

            // Act
            m_LANConnectionManager.JoinLAN("invalid.ip", k_TestPort);
            yield return new WaitForSeconds(1.0f);

            // Assert
            Assert.IsTrue(managerStatusChanged, "LANConnectionManager should change status");
            Assert.IsTrue(statusComponentNotified, "LANConnectionStatus should be notified");
        }

        [UnityTest]
        public IEnumerator ComponentCoordination_ModeManagerAndLANManager_WorkTogether()
        {
            // Arrange
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.LANDirect);
            yield return null;

            // Act
            m_LANConnectionManager.HostLAN(k_TestPort);
            yield return new WaitForSeconds(0.5f);

            // Assert
            Assert.AreEqual(ConnectionModeManager.ConnectionMode.LANDirect, m_ConnectionModeManager.CurrentMode, 
                "Mode should be LANDirect");
            Assert.IsTrue(m_LANConnectionManager.IsHosting, "Should be hosting");
        }

        [UnityTest]
        public IEnumerator ComponentCoordination_AllComponents_InitializeCorrectly()
        {
            // Assert - All components should be initialized
            Assert.IsNotNull(m_LANConnectionManager, "LANConnectionManager should exist");
            Assert.IsNotNull(m_ConnectionModeManager, "ConnectionModeManager should exist");
            Assert.IsNotNull(m_LANConnectionStatus, "LANConnectionStatus should exist");
            Assert.IsNotNull(m_NetworkManager, "NetworkManager should exist");
            Assert.IsNotNull(m_Transport, "UnityTransport should exist");

            yield return null;

            // Assert - Components should have references to each other where needed
            Assert.IsNotNull(m_NetworkManager.NetworkConfig, "NetworkManager config should be set");
            Assert.IsNotNull(m_NetworkManager.NetworkConfig.NetworkTransport, 
                "NetworkManager should have transport reference");
        }

        #endregion

        #region Realistic User Journey Tests

        [UnityTest]
        public IEnumerator UserJourney_HostSession_CompleteWorkflow()
        {
            // This test simulates a complete user journey for hosting a LAN session
            
            // Step 1: User selects LAN mode
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.LANDirect);
            yield return null;
            Assert.AreEqual(ConnectionModeManager.ConnectionMode.LANDirect, m_ConnectionModeManager.CurrentMode);

            // Step 2: User clicks "Host" button
            string localIP = m_LANConnectionManager.GetLocalIPAddress();
            Assert.IsNotNull(localIP, "Should be able to get local IP for display");

            // Step 3: Hosting starts
            m_LANConnectionManager.HostLAN(k_TestPort);
            yield return new WaitForSeconds(1.0f);
            Assert.IsTrue(m_LANConnectionManager.IsHosting, "Should be hosting");

            // Step 4: Verify session is active
            yield return new WaitForSeconds(1.0f);
            Assert.IsTrue(m_NetworkManager.IsServer || m_NetworkManager.IsHost, 
                "NetworkManager should be running");

            // Step 5: User disconnects
            m_LANConnectionManager.Disconnect();
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(LANConnectionManager.LANConnectionStatus.Disconnected, 
                m_LANConnectionManager.Status);
        }

        [UnityTest]
        public IEnumerator UserJourney_JoinSession_CompleteWorkflow()
        {
            // This test simulates a complete user journey for joining a LAN session
            
            // Step 1: User selects LAN mode
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.LANDirect);
            yield return null;
            Assert.AreEqual(ConnectionModeManager.ConnectionMode.LANDirect, m_ConnectionModeManager.CurrentMode);

            // Step 2: User enters host IP address
            string hostIP = "192.168.1.100";
            bool isValidIP = m_LANConnectionManager.ValidateIPAddress(hostIP);
            Assert.IsTrue(isValidIP, "IP validation should work");

            // Step 3: User clicks "Join" button
            bool connectionFailed = false;
            m_LANConnectionManager.OnConnectionFailed += (reason) => connectionFailed = true;

            m_LANConnectionManager.JoinLAN(hostIP, k_TestPort);
            yield return new WaitForSeconds(1.0f);

            // Step 4: Verify connection attempt was made
            Assert.IsFalse(m_LANConnectionManager.IsHosting, "Should not be hosting as client");
            Assert.AreNotEqual(LANConnectionManager.LANConnectionStatus.Disconnected, 
                m_LANConnectionManager.Status, 
                "Status should have changed from Disconnected");

            // Step 5: User cancels/disconnects
            m_LANConnectionManager.Disconnect();
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(LANConnectionManager.LANConnectionStatus.Disconnected, 
                m_LANConnectionManager.Status);
        }

        [UnityTest]
        public IEnumerator UserJourney_SwitchModes_WhileDisconnected()
        {
            // This test simulates switching between Cloud and LAN modes
            
            // Step 1: Start in Cloud mode
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.Cloud);
            yield return null;
            Assert.AreEqual(ConnectionModeManager.ConnectionMode.Cloud, m_ConnectionModeManager.CurrentMode);

            // Step 2: Switch to LAN mode
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.LANDirect);
            yield return null;
            Assert.AreEqual(ConnectionModeManager.ConnectionMode.LANDirect, m_ConnectionModeManager.CurrentMode);

            // Step 3: Switch back to Cloud mode
            m_ConnectionModeManager.SetConnectionMode(ConnectionModeManager.ConnectionMode.Cloud);
            yield return null;
            Assert.AreEqual(ConnectionModeManager.ConnectionMode.Cloud, m_ConnectionModeManager.CurrentMode);

            // All transitions should complete without errors
            yield return null;
        }

        #endregion

        #region Network Performance Tests

        [UnityTest]
        public IEnumerator Performance_MultipleHostDisconnectCycles_MaintainsStability()
        {
            // Test system stability under repeated host/disconnect cycles
            for (int i = 0; i < 3; i++)
            {
                // Host
                m_LANConnectionManager.HostLAN(k_TestPort);
                yield return new WaitForSeconds(0.5f);
                Assert.IsTrue(m_LANConnectionManager.IsHosting, $"Cycle {i}: Should be hosting");

                // Disconnect
                m_LANConnectionManager.Disconnect();
                yield return new WaitForSeconds(0.5f);
                Assert.AreEqual(LANConnectionManager.LANConnectionStatus.Disconnected, 
                    m_LANConnectionManager.Status, 
                    $"Cycle {i}: Should be disconnected");
            }

            // System should remain stable
            Assert.IsNotNull(m_LANConnectionManager, "LANConnectionManager should still exist");
            Assert.IsNotNull(m_NetworkManager, "NetworkManager should still exist");
        }

        [UnityTest]
        public IEnumerator Performance_RapidConnectionAttempts_HandledGracefully()
        {
            // Test rapid successive connection attempts
            string[] testIPs = { "192.168.1.100", "192.168.1.101", "192.168.1.102" };

            foreach (var ip in testIPs)
            {
                m_LANConnectionManager.JoinLAN(ip, k_TestPort);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(1.0f);

            // System should handle rapid requests without crashing
            Assert.IsNotNull(m_LANConnectionManager, "Component should remain stable");
            Assert.AreNotEqual(LANConnectionManager.LANConnectionStatus.Disconnected, 
                m_LANConnectionManager.Status, 
                "Should be in some connection-related state");
        }

        #endregion
    }
}
