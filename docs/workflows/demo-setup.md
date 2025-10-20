[← Documentation Home](../README.md) · [← Workflows](README.md)

# Demo Setup (Reliable · Impressive)

End-to-end checklist and timeline to deliver stable, professional demonstrations (Req 5.4).

## Roles
- Demo Lead (speaks, drives narrative)
- Tech Operator (devices, network, builds)
- Safety Spotter (monitors room, triggers emergency procedures)

## Equipment Checklist
- 3× Meta Quest 3 headsets + straps + chargers/power banks
- Clean 5 GHz WiFi (dedicated AP if possible) + SSID/password ready
- Laptop with Unity (for backup Editor+Headset demo)
- Optional: HDMI display/capture to mirror a device view (for audience)
- Printed quick troubleshooting sheet

## Space Requirements
- Clear 3×3 m area; remove obstacles; mark floor reference points (tape)
- Adequate lighting (no strong reflections or darkness)
- Quiet environment (so voice cues are heard)

## Timeline (T‑60 to T‑0)
- T‑60: Arrive; power on AP; verify internet blocked for auto‑updates (optional)
- T‑45: Verify headsets battery ≥ 80%; enable Developer Mode; check storage
- T‑40: Room Setup on each headset; confirm Guardian boundaries
- T‑35: Align shared anchor (see Guardian Integration); verify avatar alignment
- T‑30: Build/Install latest demo build or verify installed version
- T‑20: Network smoke test (Host + 1 Client); verify spawn and pose sync
- T‑15: Full test with 3 devices; run through Scenarios 1–3 quickly
- T‑10: Reset positions; set Host device A at starting spot; quiet state
- T‑05: Brief presenters; confirm emergency procedures and fallback
- T‑00: Start Host (A) → Join (B, C); begin presentation

## Network Setup
- Use 5 GHz SSID with minimal interference; place AP elevated and nearby
- Avoid guest networks that block peer traffic; confirm port 7777 allowed
- Disable OS/Store auto‑updates during demo window

## App Prep
- On Device A: Start Host; confirm UI shows Host and 0/2 clients connected
- On Devices B/C: Join; confirm both appear; check head/hand motion smooth
- Open debug UI only if needed (keep presentation clean)

## Contingency Plans
- Fallback A (2‑user): Run Host + 1 Client if a device fails
- Fallback B (Editor+Headset): Use Single‑Headset Testing Guide Method 1
- Fallback C (Bots): Enable bot simulation to demonstrate interactions solo
- Quick Reset: Quit apps → relaunch → Start Host → Join; typical recovery < 60 s

## Safety Protocols (during demo)
- Safety Spotter monitors distances; trigger quick‑exit if needed
- Demonstrate Warning (yellow halo/haptic) without touching critical threshold first
- If Critical triggered, explain movement restriction as designed behavior

## Post‑Demo Wrap
- Export logs if requested (DataCollector folder)
- Recharge headsets; restore AP settings; note issues for follow‑up

See also: [Showcase Scenarios](../implementation/showcase-scenarios.md), [Guardian Integration](../implementation/guardian-integration.md), [Single‑Headset Testing Guide](../../Single-Headset-Testing-Guide.md).
