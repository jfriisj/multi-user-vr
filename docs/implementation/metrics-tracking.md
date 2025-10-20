[← Documentation Home](../README.md) · [← Implementation](README.md)

# Metrics Tracking (Meaningful · Actionable)

Define and compute research metrics for analysis while preserving privacy (Req 6.2).

## Metric Categories
- Proximity/Safety: min interpersonal distance (MID), time-in-warning, time-in-critical, incident count, response latency
- Networking: RTT mean/median/p95, upstream/downstream bytes/s, packet loss estimate
- Performance: FPS mean/p95, CPU/GPU frame time, dropped frames
- Tracking: loss rate, average duration of tracking loss, positional jitter (stddev)

## Online Aggregation (rolling)
Use exponential moving averages and periodic snapshots to minimize overhead.

```csharp
namespace MultiUserVR.Research
{
    using System;
    using System.Globalization;
    using UnityEngine;

    public class RollingMetrics : MonoBehaviour
    {
        [Range(0f,1f)] public float alpha = 0.1f; // EMA factor
        public float FpsEma { get; private set; }
        public float RttEma { get; private set; }
        public float BytesPerSecEma { get; private set; }

        private float _frames;
        private float _time;
        private long _bytesWindow;
        private float _bytesTime;

        void Update()
        {
            _frames += 1f; _time += Time.unscaledDeltaTime;
            if (_time >= 1f)
            {
                var fps = _frames / _time;
                FpsEma = Mathf.Lerp(FpsEma, fps, alpha);
                _frames = 0f; _time = 0f;
            }
        }

        public void OnRttSample(float rttMs)
        {
            RttEma = Mathf.Lerp(RttEma, rttMs, alpha);
        }

        public void OnBytesSample(long bytes, float dt)
        {
            _bytesWindow += bytes; _bytesTime += dt;
            if (_bytesTime >= 1f)
            {
                var bps = _bytesWindow / _bytesTime; _bytesWindow = 0; _bytesTime = 0f;
                BytesPerSecEma = Mathf.Lerp(BytesPerSecEma, bps, alpha);
            }
        }
    }
}
```

## Session Summary (JSON)
At end of session, write a summary derived from CSV streams.

```csharp
// Example JSON summary structure
public class SessionSummary
{
    public string schema = "1.0";
    public string session_id;
    public float fps_mean, fps_p95;
    public float rtt_mean, rtt_p95;
    public float mid_min;              // minimum interpersonal distance
    public float warn_time_sec;
    public float crit_time_sec;
    public int safety_incidents;
}
```

## Computation Guidelines
- Percentiles: compute p50/p95 using one-pass reservoir or offline Python/R
- Jitter: stddev of positional deltas over 1 s windows
- Response latency: time from Warning to user deceleration or Critical trigger
- Normalize timestamps to UTC; align across devices using host time if needed

## Naming & Schema
- CSV headers use lowercase snake_case
- JSON fields use lowerCamelCase or snake_case consistently per schema version
- Include `schema` and `generated_at` fields in summaries

## Validation Checklist
- [ ] Summary values match recomputation from raw CSV within tolerance
- [ ] No PII present; IDs are pseudonymous
- [ ] Metrics sampling has negligible impact on FPS
- [ ] Percentiles computed over sufficient samples (N ≥ 300 for stable p95)
