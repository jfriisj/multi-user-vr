[← Documentation Home](../README.md) · [← Implementation](README.md)

# Data Collection (Privacy-First · Non‑Intrusive)

Instrumentation architecture for collecting meaningful research data without interfering with VR functionality (Req 6.1).

## Principles
- Privacy by design: no PII, pseudonymous IDs, opt-in research mode
- Low overhead: buffered, asynchronous writes; adjustable sampling rates
- Safety aligned: capture safety-related events reliably
- Reproducible: schema versioning and consistent file formats (CSV/JSON)

## Data Model Overview
- Session: `session_id`, `device_hash`, `role` (Host/Client), `app_version`, `unity_version`
- Timestamps: Unix epoch milliseconds in UTC
- Streams (examples):
  - tracking: head/hand poses, velocities, tracking state
  - safety: proximity state, min distance, boundary distance, actions taken
  - network: rtt, bytes/s, packet loss estimate
  - performance: fps, frame time, cpu/gpu time
  - events: join/leave, grab/release, warnings, critical triggers

## Pseudonymization
- `session_id`: random GUID per run
- `device_hash`: SHA-256 of device identifier + salt (stored only locally)
- No names/emails; avoid raw IPs—store /24 masked if needed

## Reference Implementation
Create a lightweight, pluggable collector with a background writer.

```csharp
namespace MultiUserVR.Research
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using UnityEngine;

    [Serializable]
    public class ResearchSettings
    {
        public bool Enabled = false;         // Opt-in research mode
        public int SampleHz = 10;            // Default sampling rate
        public string SchemaVersion = "1.0";
        public bool WriteCsv = true;         // CSV streams + JSON summary
        public string Subdir = "Research";  // Under persistentDataPath
    }

    public static class Privacy
    {
        public static string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input ?? ""));
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }
    }

    public class BackgroundLogger : IDisposable
    {
        private readonly ConcurrentQueue<string> _queue = new();
        private readonly string _path;
        private volatile bool _running = true;
        private readonly Thread _thread;

        public BackgroundLogger(string filePath)
        {
            _path = filePath;
            Directory.CreateDirectory(Path.GetDirectoryName(_path));
            _thread = new Thread(Loop) { IsBackground = true };
            _thread.Start();
        }

        public void Enqueue(string line) => _queue.Enqueue(line);

        private void Loop()
        {
            using var sw = new StreamWriter(_path, append: true, Encoding.UTF8);
            while (_running)
            {
                if (_queue.TryDequeue(out var line)) sw.WriteLine(line);
                else Thread.Sleep(10);
            }
        }

        public void Dispose() { _running = false; _thread.Join(); }
    }

    public class DataCollector : MonoBehaviour
    {
        public ResearchSettings Settings = new();

        public Transform head, leftHand, rightHand;
        public string Role = "Client"; // or Host

        private string _sessionId;
        private string _deviceHash;
        private float _accum;
        private BackgroundLogger _trackCsv;
        private BackgroundLogger _safetyCsv;
        private string _root;

        void Start()
        {
            if (!Settings.Enabled) { enabled = false; return; }
            _sessionId = Guid.NewGuid().ToString("N");
            // Use hashed device id; if unavailable, hash system info
            _deviceHash = Privacy.Hash(SystemInfo.deviceUniqueIdentifier + "|" + SystemInfo.deviceModel);
            _root = Path.Combine(Application.persistentDataPath, Settings.Subdir, _sessionId);

            _trackCsv = new BackgroundLogger(Path.Combine(_root, "tracking.csv"));
            _safetyCsv = new BackgroundLogger(Path.Combine(_root, "safety.csv"));

            _trackCsv.Enqueue("ts_ms,session_id,device_hash,role,hx,hy,hz,hrx,hry,hrz,hrw,lx,ly,lz,rx,ry,rz");
            _safetyCsv.Enqueue("ts_ms,session_id,device_hash,role,state,minDist,boundaryDist");
        }

        void Update()
        {
            if (!Settings.Enabled) return;
            _accum += Time.deltaTime;
            var interval = 1f / Mathf.Max(1, Settings.SampleHz);
            if (_accum < interval) return;
            _accum = 0f;

            long ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var h = head; var l = leftHand; var r = rightHand;
            if (h && l && r)
            {
                _trackCsv.Enqueue(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "{0},{1},{2},{3},{4:F4},{5:F4},{6:F4},{7:F5},{8:F5},{9:F5},{10:F5},{11:F4},{12:F4},{13:F4},{14:F4},{15:F4},{16:F4}",
                    ts, _sessionId, _deviceHash, Role,
                    h.position.x, h.position.y, h.position.z,
                    h.rotation.x, h.rotation.y, h.rotation.z, h.rotation.w,
                    l.position.x, l.position.y, l.position.z,
                    r.position.x, r.position.y, r.position.z));
            }
        }

        public void OnSafetySample(string state, float minDist, float boundaryDist)
        {
            if (!Settings.Enabled) return;
            long ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            _safetyCsv.Enqueue(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "{0},{1},{2},{3},{4},{5:F3},{6:F3}", ts, _sessionId, _deviceHash, Role, state, minDist, boundaryDist));
        }

        void OnDestroy()
        {
            _trackCsv?.Dispose();
            _safetyCsv?.Dispose();
            // Optionally write a JSON summary here (see Metrics Tracking)
        }
    }
}
```

### Hooking Sources
- Tracking: assign XR Origin head/hands to `DataCollector`
- Safety: call `OnSafetySample(state, minDist, boundaryDist)` from SafetyCoordinator/ProximityMonitor
- Network: extend with RTT/bytes per second samples (see Synchronization/Optimization)

## Storage & Export
- Location: `Application.persistentDataPath/Research/<session_id>/`
- Files: `tracking.csv`, `safety.csv`, plus `summary.json` on session end
- Export: copy via USB or upload in Research builds (opt-in only)

## Privacy & Ethics
- Obtain informed consent; document study goals and data types
- Use pseudonymous IDs; never collect names, images, audio without explicit consent
- Data retention policy: define retention period and deletion process
- Access control: store locally; restrict upload endpoints; encrypt at rest if required

## Verification Checklist
- [ ] Research mode toggles on/off correctly; no impact on gameplay when off
- [ ] CSV headers present; timestamps monotonic; no missing columns
- [ ] Safety samples correlate with on-screen indicators
- [ ] Export path exists; files readable on desktop
- [ ] Session ID unique per run; device hash consistent across runs on same device
