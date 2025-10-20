using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;

namespace XRMultiplayer
{
    /// <summary>
    /// Static utility service for network interface detection and IP address discovery.
    /// Optimized for Meta Quest 3 platform with Android-specific networking considerations.
    /// </summary>
    /// <remarks>
    /// This service provides reliable WiFi interface detection with platform-specific optimizations.
    /// It prioritizes WiFi interfaces over mobile data for VR headset use cases and handles
    /// network permission exceptions gracefully across different platforms.
    /// </remarks>
    public static class IPDiscoveryService
    {
        /// <summary>
        /// Result of an IP discovery operation with detailed status information.
        /// </summary>
        public struct IPDiscoveryResult
        {
            /// <summary>
            /// The discovered IP address, or null if discovery failed.
            /// </summary>
            public string ipAddress;

            /// <summary>
            /// Whether the discovery was successful.
            /// </summary>
            public bool success;

            /// <summary>
            /// The network interface that was used, or null if not found.
            /// </summary>
            public string interfaceName;

            /// <summary>
            /// The type of network interface (WiFi, Ethernet, etc.).
            /// </summary>
            public NetworkInterfaceType interfaceType;

            /// <summary>
            /// Error message if discovery failed.
            /// </summary>
            public string errorMessage;

            /// <summary>
            /// Whether the discovered interface is a WiFi interface.
            /// </summary>
            public bool isWiFi => interfaceType == NetworkInterfaceType.Wireless80211;
        }

        const string k_DebugPrepend = "<color=#20B2AA>[IP Discovery Service]</color> ";
        const bool k_EnableDebugLogging = true;

        /// <summary>
        /// Gets the local IP address with detailed discovery information.
        /// Prioritizes WiFi interfaces for Quest 3 compatibility.
        /// </summary>
        /// <returns>IPDiscoveryResult with discovery details</returns>
        public static IPDiscoveryResult GetLocalIPAddressDetailed()
        {
            var result = new IPDiscoveryResult
            {
                success = false,
                ipAddress = null,
                interfaceName = null,
                errorMessage = null
            };

            try
            {
                // Strategy 1: Try to get WiFi interface first (priority for Quest 3)
                result = TryGetWiFiInterface();
                if (result.success)
                {
                    Log($"WiFi IP found: {result.ipAddress} on {result.interfaceName}");
                    return result;
                }

                // Strategy 2: Try Ethernet interface (fallback for desktop development)
                result = TryGetEthernetInterface();
                if (result.success)
                {
                    Log($"Ethernet IP found: {result.ipAddress} on {result.interfaceName}");
                    return result;
                }

                // Strategy 3: Try any active interface with valid IPv4 address
                result = TryGetAnyActiveInterface();
                if (result.success)
                {
                    Log($"Active interface IP found: {result.ipAddress} on {result.interfaceName}");
                    return result;
                }

                // Strategy 4: Platform-specific fallback
#if UNITY_ANDROID && !UNITY_EDITOR
                result = TryAndroidFallback();
                if (result.success)
                {
                    Log($"Android fallback IP found: {result.ipAddress}");
                    return result;
                }
#endif

                // All strategies failed
                result.errorMessage = "No valid network interface found. Please check WiFi settings.";
                LogWarning(result.errorMessage);
                return result;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = $"IP Discovery exception: {ex.Message}";
                LogError(result.errorMessage);
                return result;
            }
        }

        /// <summary>
        /// Gets the local IP address as a simple string.
        /// Returns "IP not found" if discovery fails.
        /// </summary>
        /// <returns>IP address string or error message</returns>
        public static string GetLocalIPAddress()
        {
            var result = GetLocalIPAddressDetailed();
            return result.success ? result.ipAddress : "IP not found";
        }

        /// <summary>
        /// Validates an IP address string format.
        /// Ensures it's a valid IPv4 address.
        /// </summary>
        /// <param name="ipString">IP address string to validate</param>
        /// <returns>True if valid IPv4 address format</returns>
        public static bool ValidateIPAddress(string ipString)
        {
            if (string.IsNullOrWhiteSpace(ipString))
            {
                LogWarning("IP address string is null or empty");
                return false;
            }

            // Try to parse as IP address
            if (!IPAddress.TryParse(ipString, out IPAddress address))
            {
                LogWarning($"Failed to parse IP address: {ipString}");
                return false;
            }

            // Ensure it's IPv4
            if (address.AddressFamily != AddressFamily.InterNetwork)
            {
                LogWarning($"IP address is not IPv4: {ipString}");
                return false;
            }

            // Additional validation: not loopback, not zero
            if (IPAddress.IsLoopback(address))
            {
                LogWarning($"IP address is loopback: {ipString}");
                return false;
            }

            if (address.Equals(IPAddress.Any) || address.Equals(IPAddress.None))
            {
                LogWarning($"IP address is invalid (0.0.0.0 or 255.255.255.255): {ipString}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the WiFi network interface if available.
        /// Prioritized for Meta Quest 3 platform.
        /// </summary>
        /// <returns>IPDiscoveryResult with WiFi interface information</returns>
        public static IPDiscoveryResult GetWiFiInterface()
        {
            return TryGetWiFiInterface();
        }

        /// <summary>
        /// Checks if the device has an active WiFi connection.
        /// </summary>
        /// <returns>True if WiFi is connected and operational</returns>
        public static bool IsWiFiConnected()
        {
            var result = TryGetWiFiInterface();
            return result.success;
        }

        /// <summary>
        /// Gets all available network interfaces with their IP addresses.
        /// Useful for debugging network configuration.
        /// </summary>
        /// <returns>Array of IPDiscoveryResult for all interfaces</returns>
        public static IPDiscoveryResult[] GetAllNetworkInterfaces()
        {
            var results = new System.Collections.Generic.List<IPDiscoveryResult>();

            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus != OperationalStatus.Up)
                        continue;

                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork &&
                            !IPAddress.IsLoopback(ip.Address))
                        {
                            results.Add(new IPDiscoveryResult
                            {
                                success = true,
                                ipAddress = ip.Address.ToString(),
                                interfaceName = ni.Name,
                                interfaceType = ni.NetworkInterfaceType,
                                errorMessage = null
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Failed to enumerate network interfaces: {ex.Message}");
            }

            return results.ToArray();
        }

        #region Private Helper Methods

        /// <summary>
        /// Attempts to find a WiFi interface with valid IP address.
        /// Primary strategy for Quest 3 devices.
        /// </summary>
        private static IPDiscoveryResult TryGetWiFiInterface()
        {
            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Look for active WiFi interfaces
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                        ni.OperationalStatus == OperationalStatus.Up)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork &&
                                !IPAddress.IsLoopback(ip.Address))
                            {
                                return new IPDiscoveryResult
                                {
                                    success = true,
                                    ipAddress = ip.Address.ToString(),
                                    interfaceName = ni.Name,
                                    interfaceType = ni.NetworkInterfaceType,
                                    errorMessage = null
                                };
                            }
                        }
                    }
                }
            }
            catch (NetworkInformationException ex)
            {
                LogError($"Network information exception while searching for WiFi: {ex.Message}");
            }
            catch (Exception ex)
            {
                LogError($"Exception while searching for WiFi interface: {ex.Message}");
            }

            return new IPDiscoveryResult
            {
                success = false,
                errorMessage = "No active WiFi interface found"
            };
        }

        /// <summary>
        /// Attempts to find an Ethernet interface with valid IP address.
        /// Fallback strategy for desktop development.
        /// </summary>
        private static IPDiscoveryResult TryGetEthernetInterface()
        {
            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Look for active Ethernet interfaces
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                        ni.OperationalStatus == OperationalStatus.Up)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork &&
                                !IPAddress.IsLoopback(ip.Address))
                            {
                                return new IPDiscoveryResult
                                {
                                    success = true,
                                    ipAddress = ip.Address.ToString(),
                                    interfaceName = ni.Name,
                                    interfaceType = ni.NetworkInterfaceType,
                                    errorMessage = null
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Exception while searching for Ethernet interface: {ex.Message}");
            }

            return new IPDiscoveryResult
            {
                success = false,
                errorMessage = "No active Ethernet interface found"
            };
        }

        /// <summary>
        /// Attempts to find any active network interface with valid IPv4 address.
        /// Last resort strategy before platform-specific fallbacks.
        /// </summary>
        private static IPDiscoveryResult TryGetAnyActiveInterface()
        {
            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus == OperationalStatus.Up)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork &&
                                !IPAddress.IsLoopback(ip.Address))
                            {
                                return new IPDiscoveryResult
                                {
                                    success = true,
                                    ipAddress = ip.Address.ToString(),
                                    interfaceName = ni.Name,
                                    interfaceType = ni.NetworkInterfaceType,
                                    errorMessage = null
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Exception while searching for any active interface: {ex.Message}");
            }

            return new IPDiscoveryResult
            {
                success = false,
                errorMessage = "No active network interface with IPv4 address found"
            };
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        /// <summary>
        /// Android-specific fallback for IP address discovery.
        /// Uses platform-specific APIs when standard methods fail.
        /// </summary>
        private static IPDiscoveryResult TryAndroidFallback()
        {
            try
            {
                // On Android, try to use Java connectivity APIs through Unity's AndroidJavaObject
                using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
                    .GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    using (AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext"))
                    {
                        using (AndroidJavaObject connectivityManager = context.Call<AndroidJavaObject>("getSystemService", "connectivity"))
                        {
                            if (connectivityManager != null)
                            {
                                // Get active network info
                                using (AndroidJavaObject activeNetwork = connectivityManager.Call<AndroidJavaObject>("getActiveNetworkInfo"))
                                {
                                    if (activeNetwork != null && activeNetwork.Call<bool>("isConnected"))
                                    {
                                        // Try to get WiFi manager for WiFi-specific info
                                        using (AndroidJavaObject wifiManager = context.Call<AndroidJavaObject>("getSystemService", "wifi"))
                                        {
                                            if (wifiManager != null)
                                            {
                                                using (AndroidJavaObject wifiInfo = wifiManager.Call<AndroidJavaObject>("getConnectionInfo"))
                                                {
                                                    if (wifiInfo != null)
                                                    {
                                                        int ipInt = wifiInfo.Call<int>("getIpAddress");
                                                        
                                                        // Convert int to IP address string
                                                        string ipAddress = string.Format("{0}.{1}.{2}.{3}",
                                                            ipInt & 0xff,
                                                            (ipInt >> 8) & 0xff,
                                                            (ipInt >> 16) & 0xff,
                                                            (ipInt >> 24) & 0xff);

                                                        if (ValidateIPAddress(ipAddress))
                                                        {
                                                            Log($"Android WiFi API returned IP: {ipAddress}");
                                                            return new IPDiscoveryResult
                                                            {
                                                                success = true,
                                                                ipAddress = ipAddress,
                                                                interfaceName = "Android WiFi",
                                                                interfaceType = NetworkInterfaceType.Wireless80211,
                                                                errorMessage = null
                                                            };
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Android fallback failed: {ex.Message}");
            }

            return new IPDiscoveryResult
            {
                success = false,
                errorMessage = "Android fallback IP discovery failed"
            };
        }
#endif

        #endregion

        #region Logging Helpers

        /// <summary>
        /// Logs a message with IP Discovery Service prefix.
        /// </summary>
        private static void Log(string message)
        {
            if (k_EnableDebugLogging)
            {
                Utils.Log($"{k_DebugPrepend}{message}");
            }
        }

        /// <summary>
        /// Logs a warning message with IP Discovery Service prefix.
        /// </summary>
        private static void LogWarning(string message)
        {
            Utils.LogWarning($"{k_DebugPrepend}{message}");
        }

        /// <summary>
        /// Logs an error message with IP Discovery Service prefix.
        /// </summary>
        private static void LogError(string message)
        {
            Utils.Log($"{k_DebugPrepend}{message}", 2);
        }

        #endregion

        #region Editor Helpers

#if UNITY_EDITOR
        /// <summary>
        /// Tests IP discovery and prints results to console.
        /// Available in Unity Editor through menu: Tools/Network/Test IP Discovery
        /// </summary>
        [UnityEditor.MenuItem("Tools/Network/Test IP Discovery")]
        public static void TestIPDiscovery()
        {
            Debug.Log("=== IP Discovery Service Test ===");

            var result = GetLocalIPAddressDetailed();
            if (result.success)
            {
                Debug.Log($"✓ Discovery successful!");
                Debug.Log($"  IP Address: {result.ipAddress}");
                Debug.Log($"  Interface: {result.interfaceName}");
                Debug.Log($"  Type: {result.interfaceType}");
                Debug.Log($"  WiFi: {result.isWiFi}");
            }
            else
            {
                Debug.LogWarning($"✗ Discovery failed: {result.errorMessage}");
            }

            Debug.Log("\n=== All Network Interfaces ===");
            var allInterfaces = GetAllNetworkInterfaces();
            foreach (var iface in allInterfaces)
            {
                Debug.Log($"  [{iface.interfaceType}] {iface.interfaceName}: {iface.ipAddress}");
            }

            Debug.Log($"\n=== WiFi Status ===");
            Debug.Log($"  WiFi Connected: {IsWiFiConnected()}");

            Debug.Log($"\n=== Simple IP Lookup ===");
            Debug.Log($"  GetLocalIPAddress(): {GetLocalIPAddress()}");
        }

        /// <summary>
        /// Tests IP address validation.
        /// Available in Unity Editor through menu: Tools/Network/Test IP Validation
        /// </summary>
        [UnityEditor.MenuItem("Tools/Network/Test IP Validation")]
        public static void TestIPValidation()
        {
            Debug.Log("=== IP Validation Test ===");

            string[] testIPs = new string[]
            {
                "192.168.1.1",      // Valid
                "10.0.0.1",         // Valid
                "172.16.0.1",       // Valid
                "127.0.0.1",        // Loopback (invalid for LAN)
                "0.0.0.0",          // Any (invalid)
                "256.1.1.1",        // Out of range
                "192.168.1",        // Incomplete
                "abc.def.ghi.jkl",  // Non-numeric
                "",                 // Empty
                null                // Null
            };

            foreach (var ip in testIPs)
            {
                bool valid = ValidateIPAddress(ip);
                string status = valid ? "✓ VALID" : "✗ INVALID";
                Debug.Log($"  {status}: \"{ip}\"");
            }
        }
#endif

        #endregion
    }
}
