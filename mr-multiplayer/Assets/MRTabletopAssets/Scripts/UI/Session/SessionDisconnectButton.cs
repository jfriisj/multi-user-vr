using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XRMultiplayer;

namespace UnityEngine.XR.Templates.MRTTabletopAssets
{
    /// <summary>
    /// Adds a prominent Disconnect control usable in VR and Editor.
    /// Wires to ConnectionModeManager.Disconnect and shows non-blocking progress with 5s timeout.
    /// </summary>
    public class SessionDisconnectButton : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] Button m_Button;
        [SerializeField] TMP_Text m_Label;
        [SerializeField] GameObject m_ProgressIndicator;
        [SerializeField] float m_TimeoutSeconds = 5f;

        ConnectionModeManager m_ConnectionModeManager;
        XRINetworkGameManager m_GameManager;
        Coroutine m_DisconnectRoutine;
        bool m_Subscribed;

        void Awake()
        {
            m_ConnectionModeManager = FindFirstObjectByType<ConnectionModeManager>();
            // Use Singleton if available; otherwise find object
            m_GameManager = XRINetworkGameManager.Instance ?? FindFirstObjectByType<XRINetworkGameManager>();

            if (m_Button == null) m_Button = GetComponent<Button>();
            if (m_Label == null) m_Label = GetComponentInChildren<TMP_Text>(true);

            if (m_Label != null) m_Label.text = "Disconnect";
            if (m_Button != null)
            {
                m_Button.onClick.RemoveAllListeners();
                m_Button.onClick.AddListener(OnClick);
            }

            if (m_ProgressIndicator != null)
                m_ProgressIndicator.SetActive(false);
        }

        void OnEnable()
        {
            XRINetworkGameManager.Connected.Subscribe(OnConnectedChanged);
            m_Subscribed = true;
            OnConnectedChanged(XRINetworkGameManager.Connected.Value);
        }

        void OnDisable()
        {
            if (m_Subscribed)
            {
                XRINetworkGameManager.Connected.Unsubscribe(OnConnectedChanged);
                m_Subscribed = false;
            }
        }

        void OnConnectedChanged(bool connected)
        {
            // Show when connected, hide otherwise
            if (m_Button != null)
            {
                var go = m_Button.gameObject;
                if (go.activeSelf != connected) go.SetActive(connected);
                m_Button.interactable = connected;
            }
        }

        void OnClick()
        {
            if (m_DisconnectRoutine != null) return;
            m_DisconnectRoutine = StartCoroutine(DisconnectFlow());
        }

        IEnumerator DisconnectFlow()
        {
            if (m_Button != null) m_Button.interactable = false;
            if (m_Label != null) m_Label.text = "Disconnecting...";
            PlayerHudNotification.Instance?.ShowText("<b>Status:</b> Disconnecting...");
            if (m_ProgressIndicator != null) m_ProgressIndicator.SetActive(true);

            // Initiate disconnect via ConnectionModeManager (preferred entry point)
            if (m_ConnectionModeManager != null)
                m_ConnectionModeManager.Disconnect();
            else if (m_GameManager != null)
                _ = m_GameManager.DisconnectAsync();

            float start = Time.time;
            // Wait until disconnected or timeout
            while (XRINetworkGameManager.Connected.Value && Time.time - start < m_TimeoutSeconds)
                yield return null;

            if (XRINetworkGameManager.Connected.Value)
            {
                // Timeout
                PlayerHudNotification.Instance?.ShowText("Disconnect timeout", 2.0f);
            }
            else
            {
                PlayerHudNotification.Instance?.ShowText("<b>Status:</b> Disconnected");
                // Return to lobby panels based on current mode
                var lobby = FindFirstObjectByType<LobbyUI>();
                if (lobby != null) lobby.CheckInternetAsync();
            }

            if (m_Label != null) m_Label.text = "Disconnect";
            if (m_ProgressIndicator != null) m_ProgressIndicator.SetActive(false);
            if (m_Button != null) m_Button.interactable = !XRINetworkGameManager.Connected.Value;
            m_DisconnectRoutine = null;
        }
    }
}
