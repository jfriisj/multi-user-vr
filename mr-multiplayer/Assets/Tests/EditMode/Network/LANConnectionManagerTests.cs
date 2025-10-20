using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using XRMultiplayer;

namespace XRMultiplayer.Tests
{
    /// <summary>
    /// Comprehensive unit tests for LANConnectionManager.
    /// Tests host/client configuration, IP validation, error scenarios, and state management.
    /// </summary>
    public class LANConnectionManagerTests
    {
        private GameObject m_TestGameObject;
        private LANConnectionManager m_LANConnectionManager;
        private NetworkManager m_NetworkManager;
        private UnityTransport m_Transport;

        #region Setup and Teardown

        [SetUp]
        public void SetUp()
        {
            // Create test GameObject with required components
            m_TestGameObject = new GameObject("TestLANConnectionManager");
            
            // Add NetworkManager
            m_NetworkManager = m_TestGameObject.AddComponent<NetworkManager>();
            
            // Add UnityTransport
            m_Transport = m_TestGameObject.AddComponent<UnityTransport>();
            m_NetworkManager.NetworkConfig = new NetworkConfig();
            m_NetworkManager.NetworkConfig.NetworkTransport = m_Transport;
            
            // Add LANConnectionManager
            m_LANConnectionManager = m_TestGameObject.AddComponent<LANConnectionManager>();
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up NetworkManager if running
            if (m_NetworkManager != null && m_NetworkManager.IsListening)
            {
                m_NetworkManager.Shutdown();
            }

            // Destroy test GameObject
            if (m_TestGameObject != null)
            {
                Object.DestroyImmediate(m_TestGameObject);
            }
        }

        #endregion

        #region IP Address Validation Tests

        [Test]
        public void ValidateIPAddress_ValidIPv4_ReturnsTrue()
        {
            // Arrange
            string validIP = "192.168.1.100";

            // Act
            bool isValid = m_LANConnectionManager.ValidateIPAddress(validIP);

            // Assert
            Assert.IsTrue(isValid, "Valid IPv4 address should return true");
        }

        [Test]
        public void ValidateIPAddress_ValidIPv4_Localhost_ReturnsTrue()
        {
            // Arrange
            string localhostIP = "127.0.0.1";

            // Act
            bool isValid = m_LANConnectionManager.ValidateIPAddress(localhostIP);

            // Assert
            Assert.IsTrue(isValid, "Localhost IP should be valid");
        }

        [Test]
        public void ValidateIPAddress_InvalidFormat_ReturnsFalse()
        {
            // Arrange
            string[] invalidIPs = new string[]
            {
                "256.1.1.1",      // Out of range
                "192.168.1",      // Missing octet
                "192.168.1.1.1",  // Too many octets
                "abc.def.ghi.jkl", // Non-numeric
                "",               // Empty string
                null,             // Null
                "192.168.-1.1",   // Negative number
                "192.168.1.300"   // Out of range
            };

            // Act & Assert
            foreach (var invalidIP in invalidIPs)
            {
                bool isValid = m_LANConnectionManager.ValidateIPAddress(invalidIP);
                Assert.IsFalse(isValid, $"Invalid IP '{invalidIP}' should return false");
            }
        }

        [Test]
        public void GetLocalIPAddress_ReturnsValidIPOrNull()
        {
            // Act
            string localIP = m_LANConnectionManager.GetLocalIPAddress();

            // Assert
            if (localIP != null)
            {
                // If an IP is returned, it should be valid
                Assert.IsTrue(m_LANConnectionManager.ValidateIPAddress(localIP), 
                    "Returned local IP should be valid");
            }
            // If null, it's acceptable (no network interfaces available)
        }

        #endregion

        #region Initial State Tests

        [Test]
        public void InitialState_StatusIsDisconnected()
        {
            // Assert
            Assert.AreEqual(LANConnectionManager.LANConnectionStatus.Disconnected, 
                m_LANConnectionManager.Status, 
                "Initial status should be Disconnected");
        }

        [Test]
        public void InitialState_IsHostingIsFalse()
        {
            // Assert
            Assert.IsFalse(m_LANConnectionManager.IsHosting, 
                "IsHosting should be false initially");
        }

        [Test]
        public void InitialState_IsLANModeActiveIsFalse()
        {
            // Assert
            Assert.IsFalse(m_LANConnectionManager.IsLANModeActive, 
                "IsLANModeActive should be false initially");
        }

        [Test]
        public void InitialState_LocalIPAddressIsSet()
        {
            // Act - Component should set LocalIPAddress in Awake/Start
            string localIP = m_LANConnectionManager.LocalIPAddress;

            // Assert
            // LocalIPAddress can be null if no network interfaces are available
            if (localIP != null)
            {
                Assert.IsTrue(m_LANConnectionManager.ValidateIPAddress(localIP), 
                    "LocalIPAddress should be valid if set");
            }
        }

        #endregion

        #region Host Configuration Tests

        [Test]
        public void HostLAN_SetsStatusToConnecting()
        {
            // Arrange
            bool statusChangedToConnecting = false;
            m_LANConnectionManager.OnStatusChanged += (status) =>
            {
                if (status == LANConnectionManager.LANConnectionStatus.Connecting)
                {
                    statusChangedToConnecting = true;
                }
            };

            // Act
            m_LANConnectionManager.HostLAN();

            // Assert
            Assert.IsTrue(statusChangedToConnecting, 
                "Status should change to Connecting when hosting");
        }

        [Test]
        public void HostLAN_SetsIsHostingToTrue()
        {
            // Act
            m_LANConnectionManager.HostLAN();

            // Assert
            Assert.IsTrue(m_LANConnectionManager.IsHosting, 
                "IsHosting should be true after calling HostLAN");
        }

        [Test]
        public void HostLAN_RaisesStatusChangedEvent()
        {
            // Arrange
            bool eventRaised = false;
            m_LANConnectionManager.OnStatusChanged += (status) => { eventRaised = true; };

            // Act
            m_LANConnectionManager.HostLAN();

            // Assert
            Assert.IsTrue(eventRaised, "OnStatusChanged event should be raised");
        }

        [Test]
        public void HostLAN_WithCustomPort_ConfiguresTransport()
        {
            // Arrange
            ushort customPort = 8888;

            // Act
            m_LANConnectionManager.HostLAN(customPort);

            // Assert
            Assert.AreEqual(customPort, m_Transport.ConnectionData.Port, 
                "Transport should be configured with custom port");
        }

        #endregion

        #region Client Configuration Tests

        [Test]
        public void JoinLAN_WithValidIP_SetsStatusToConnecting()
        {
            // Arrange
            string validIP = "192.168.1.100";
            bool statusChangedToConnecting = false;
            m_LANConnectionManager.OnStatusChanged += (status) =>
            {
                if (status == LANConnectionManager.LANConnectionStatus.Connecting)
                {
                    statusChangedToConnecting = true;
                }
            };

            // Act
            m_LANConnectionManager.JoinLAN(validIP);

            // Assert
            Assert.IsTrue(statusChangedToConnecting, 
                "Status should change to Connecting when joining with valid IP");
        }

        [Test]
        public void JoinLAN_WithInvalidIP_DoesNotConnect()
        {
            // Arrange
            string invalidIP = "999.999.999.999";
            bool connectionFailed = false;
            m_LANConnectionManager.OnConnectionFailed += (reason) => { connectionFailed = true; };

            // Act
            m_LANConnectionManager.JoinLAN(invalidIP);

            // Assert
            Assert.IsTrue(connectionFailed, 
                "OnConnectionFailed should be raised for invalid IP");
            Assert.AreEqual(LANConnectionManager.LANConnectionStatus.Failed, 
                m_LANConnectionManager.Status, 
                "Status should be Failed for invalid IP");
        }

        [Test]
        public void JoinLAN_WithEmptyIP_RaisesConnectionFailed()
        {
            // Arrange
            bool connectionFailed = false;
            string failureReason = null;
            m_LANConnectionManager.OnConnectionFailed += (reason) => 
            { 
                connectionFailed = true;
                failureReason = reason;
            };

            // Act
            m_LANConnectionManager.JoinLAN("");

            // Assert
            Assert.IsTrue(connectionFailed, "OnConnectionFailed should be raised for empty IP");
            Assert.IsNotNull(failureReason, "Failure reason should be provided");
            StringAssert.Contains("invalid", failureReason.ToLower(), 
                "Failure reason should mention invalid IP");
        }

        [Test]
        public void JoinLAN_WithValidIPAndPort_ConfiguresTransport()
        {
            // Arrange
            string validIP = "192.168.1.100";
            ushort customPort = 8888;

            // Act
            m_LANConnectionManager.JoinLAN(validIP, customPort);

            // Assert
            Assert.AreEqual(validIP, m_Transport.ConnectionData.Address, 
                "Transport should be configured with provided IP");
            Assert.AreEqual(customPort, m_Transport.ConnectionData.Port, 
                "Transport should be configured with custom port");
        }

        [Test]
        public void JoinLAN_SetsIsHostingToFalse()
        {
            // Arrange
            string validIP = "192.168.1.100";

            // Act
            m_LANConnectionManager.JoinLAN(validIP);

            // Assert
            Assert.IsFalse(m_LANConnectionManager.IsHosting, 
                "IsHosting should be false when joining as client");
        }

        #endregion

        #region Disconnect Tests

        [Test]
        public void Disconnect_WhenConnected_SetsStatusToDisconnected()
        {
            // Arrange - Simulate connected state
            m_LANConnectionManager.HostLAN();
            bool statusChangedToDisconnected = false;
            m_LANConnectionManager.OnStatusChanged += (status) =>
            {
                if (status == LANConnectionManager.LANConnectionStatus.Disconnected)
                {
                    statusChangedToDisconnected = true;
                }
            };

            // Act
            m_LANConnectionManager.Disconnect();

            // Assert
            Assert.IsTrue(statusChangedToDisconnected, 
                "Status should change to Disconnected");
        }

        [Test]
        public void Disconnect_ResetsIsHosting()
        {
            // Arrange
            m_LANConnectionManager.HostLAN();

            // Act
            m_LANConnectionManager.Disconnect();

            // Assert
            Assert.IsFalse(m_LANConnectionManager.IsHosting, 
                "IsHosting should be false after disconnect");
        }

        [Test]
        public void Disconnect_WhenNotConnected_DoesNotThrow()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => m_LANConnectionManager.Disconnect(), 
                "Disconnect should not throw when not connected");
        }

        #endregion

        #region Event Tests

        [Test]
        public void OnStatusChanged_FiresWhenStatusChanges()
        {
            // Arrange
            int eventFireCount = 0;
            LANConnectionManager.LANConnectionStatus lastStatus = LANConnectionManager.LANConnectionStatus.Disconnected;
            
            m_LANConnectionManager.OnStatusChanged += (status) => 
            { 
                eventFireCount++;
                lastStatus = status;
            };

            // Act
            m_LANConnectionManager.HostLAN();

            // Assert
            Assert.GreaterOrEqual(eventFireCount, 1, 
                "OnStatusChanged should fire at least once");
            Assert.AreEqual(LANConnectionManager.LANConnectionStatus.Connecting, lastStatus, 
                "Last status should be Connecting");
        }

        [Test]
        public void OnConnectionFailed_FiresWithReason()
        {
            // Arrange
            bool eventFired = false;
            string receivedReason = null;
            m_LANConnectionManager.OnConnectionFailed += (reason) => 
            { 
                eventFired = true;
                receivedReason = reason;
            };

            // Act - Trigger failure with invalid IP
            m_LANConnectionManager.JoinLAN("invalid.ip");

            // Assert
            Assert.IsTrue(eventFired, "OnConnectionFailed should fire");
            Assert.IsNotNull(receivedReason, "Failure reason should be provided");
        }

        [Test]
        public void MultipleEventSubscribers_AllReceiveNotifications()
        {
            // Arrange
            int subscriber1Count = 0;
            int subscriber2Count = 0;
            int subscriber3Count = 0;

            m_LANConnectionManager.OnStatusChanged += (status) => { subscriber1Count++; };
            m_LANConnectionManager.OnStatusChanged += (status) => { subscriber2Count++; };
            m_LANConnectionManager.OnStatusChanged += (status) => { subscriber3Count++; };

            // Act
            m_LANConnectionManager.HostLAN();

            // Assert
            Assert.Greater(subscriber1Count, 0, "Subscriber 1 should receive notification");
            Assert.Greater(subscriber2Count, 0, "Subscriber 2 should receive notification");
            Assert.Greater(subscriber3Count, 0, "Subscriber 3 should receive notification");
        }

        #endregion

        #region Edge Cases

        [Test]
        public void HostLAN_CalledTwice_HandlesGracefully()
        {
            // Act
            m_LANConnectionManager.HostLAN();
            
            // Assert - Should not throw when called again
            Assert.DoesNotThrow(() => m_LANConnectionManager.HostLAN(), 
                "Calling HostLAN twice should not throw");
        }

        [Test]
        public void JoinLAN_CalledTwiceWithDifferentIPs_UpdatesIP()
        {
            // Arrange
            string firstIP = "192.168.1.100";
            string secondIP = "192.168.1.200";

            // Act
            m_LANConnectionManager.JoinLAN(firstIP);
            m_LANConnectionManager.JoinLAN(secondIP);

            // Assert
            Assert.AreEqual(secondIP, m_Transport.ConnectionData.Address, 
                "Transport should be updated with second IP");
        }

        [Test]
        public void Disconnect_ThenHost_WorksCorrectly()
        {
            // Arrange
            m_LANConnectionManager.HostLAN();
            m_LANConnectionManager.Disconnect();

            // Act
            m_LANConnectionManager.HostLAN();

            // Assert
            Assert.IsTrue(m_LANConnectionManager.IsHosting, 
                "Should be able to host after disconnect");
        }

        [Test]
        public void Disconnect_ThenJoin_WorksCorrectly()
        {
            // Arrange
            string validIP = "192.168.1.100";
            m_LANConnectionManager.JoinLAN(validIP);
            m_LANConnectionManager.Disconnect();

            // Act
            m_LANConnectionManager.JoinLAN(validIP);

            // Assert
            Assert.IsFalse(m_LANConnectionManager.IsHosting, 
                "Should be able to join after disconnect");
            Assert.AreEqual(LANConnectionManager.LANConnectionStatus.Connecting, 
                m_LANConnectionManager.Status, 
                "Status should be Connecting");
        }

        #endregion

        #region State Consistency Tests

        [Test]
        public void IsLANModeActive_TrueWhenHosting()
        {
            // Act
            m_LANConnectionManager.HostLAN();

            // Assert
            Assert.IsTrue(m_LANConnectionManager.IsLANModeActive, 
                "IsLANModeActive should be true when hosting");
        }

        [Test]
        public void IsLANModeActive_FalseWhenDisconnected()
        {
            // Arrange
            m_LANConnectionManager.HostLAN();
            m_LANConnectionManager.Disconnect();

            // Assert
            Assert.IsFalse(m_LANConnectionManager.IsLANModeActive, 
                "IsLANModeActive should be false when disconnected");
        }

        [Test]
        public void Status_TransitionsCorrectly_FromDisconnectedToConnecting()
        {
            // Arrange
            var statusHistory = new System.Collections.Generic.List<LANConnectionManager.LANConnectionStatus>();
            m_LANConnectionManager.OnStatusChanged += (status) => statusHistory.Add(status);

            // Act
            m_LANConnectionManager.HostLAN();

            // Assert
            Assert.Contains(LANConnectionManager.LANConnectionStatus.Connecting, statusHistory, 
                "Should transition to Connecting");
        }

        #endregion

        #region IP Validation Edge Cases

        [Test]
        public void ValidateIPAddress_BoundaryValues_HandledCorrectly()
        {
            // Arrange & Act & Assert
            Assert.IsTrue(m_LANConnectionManager.ValidateIPAddress("0.0.0.0"), 
                "0.0.0.0 should be valid");
            Assert.IsTrue(m_LANConnectionManager.ValidateIPAddress("255.255.255.255"), 
                "255.255.255.255 should be valid");
            Assert.IsFalse(m_LANConnectionManager.ValidateIPAddress("256.0.0.0"), 
                "256.0.0.0 should be invalid");
            Assert.IsFalse(m_LANConnectionManager.ValidateIPAddress("0.0.0.256"), 
                "0.0.0.256 should be invalid");
        }

        [Test]
        public void ValidateIPAddress_SpecialCharacters_ReturnsFalse()
        {
            // Arrange
            string[] invalidIPs = new string[]
            {
                "192.168.1.1/24",
                "192.168.1.1:7777",
                "192.168.1.1 ",
                " 192.168.1.1",
                "192.168.1.1\n",
                "192,168,1,1"
            };

            // Act & Assert
            foreach (var invalidIP in invalidIPs)
            {
                Assert.IsFalse(m_LANConnectionManager.ValidateIPAddress(invalidIP), 
                    $"IP with special characters '{invalidIP}' should be invalid");
            }
        }

        #endregion

        #region Component Dependencies Tests

        [Test]
        public void LANConnectionManager_RequiresNetworkManager()
        {
            // Arrange - Create new GameObject without NetworkManager
            var testObj = new GameObject("TestWithoutNetworkManager");
            var lanManager = testObj.AddComponent<LANConnectionManager>();

            // Assert
            // Component should handle missing NetworkManager gracefully
            Assert.DoesNotThrow(() => lanManager.HostLAN(), 
                "Should handle missing NetworkManager gracefully");

            // Cleanup
            Object.DestroyImmediate(testObj);
        }

        [Test]
        public void LANConnectionManager_RequiresUnityTransport()
        {
            // Arrange - Create new GameObject with NetworkManager but without UnityTransport
            var testObj = new GameObject("TestWithoutTransport");
            var netManager = testObj.AddComponent<NetworkManager>();
            netManager.NetworkConfig = new NetworkConfig();
            var lanManager = testObj.AddComponent<LANConnectionManager>();

            // Assert
            // Component should handle missing Transport gracefully
            Assert.DoesNotThrow(() => lanManager.HostLAN(), 
                "Should handle missing UnityTransport gracefully");

            // Cleanup
            Object.DestroyImmediate(testObj);
        }

        #endregion
    }
}
