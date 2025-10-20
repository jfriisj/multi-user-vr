[← Documentation Home](../README.md) · [← Workflows](README.md)

# Research Procedures (Ethics · Privacy · Rigor)

Standardized, ethical procedures for VR user studies (Req 6.3). These steps must not interfere with core VR functionality.

## Pre‑Session
1) Protocol Approval: IRB/ethics approval on file; risks and mitigations documented
2) Consent: provide study information; obtain written consent (and assent when applicable)
3) Privacy Prep: ensure research mode is opt-in; verify no cameras/audio recorded without consent
4) Room Setup: 3x3m space; remove obstacles; verify Guardian boundaries per device
5) Calibration: align shared anchor; validate positions across headsets (see Guardian Integration)

## Session
1) Assign Roles: 1 Host, 2 Clients; confirm network connectivity
2) Start Research Mode: enable `ResearchSettings.Enabled`
3) Baseline Metrics: record idle bandwidth/FPS for 60 s
4) Tasks: run defined scenarios (interaction, movement, safety edge cases)
5) Annotations: operator marks notable events (voice or UI markers) without recording PII
6) Safety Monitoring: ensure escalation ladder triggers correctly; be ready to execute emergency procedures
7) Breaks: enforce rest periods to prevent fatigue

## Post‑Session
1) Stop Research Mode; ensure files flushed and summary written
2) Data Integrity: verify files present; compute checksums; store in session folder
3) De‑identification: confirm only pseudonymous IDs exist; remove temporary mappings if any
4) Secure Storage: move to encrypted storage; restrict access to research team
5) Retention: follow retention policy (e.g., 12 months) then delete securely

## Folder Structure
```
<export_root>/
  study-<name>/
    YYYYMMDD-HHMM-session-<session_id>/
      tracking.csv
      safety.csv
      summary.json
      notes.txt (optional, no PII)
```

## Quality Gates (Research)
- Consent logged; protocol version recorded
- All required files present; no PII detected
- Metrics within performance budgets
- Safety incidents reviewed; follow-up actions recorded

## Risk Mitigation
- Immediate freeze & quick-exit controls available to operator
- Strict boundary monitoring; pre‑emptive warnings
- Battery and thermal monitoring to avoid device shutdown mid‑session

## References
- See Implementation → [Data Collection](../implementation/data-collection.md) and [Metrics Tracking](../implementation/metrics-tracking.md)
- See Safety → [Collision Detection](../implementation/collision-detection.md) and [Safety Protocols](../implementation/safety-protocols.md)
