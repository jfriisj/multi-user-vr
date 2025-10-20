[← Documentation Home](../README.md) · [← Implementation](README.md)

# Showcase Scenarios (Scripted · Impactful)

Curated sequences that highlight multi‑user co‑location, interaction, and safety.

## Scenario 1: Meet & Align (2–3 minutes)
Purpose: Prove co‑located alignment and presence.
- Steps:
  1) Host (A) starts session; Clients (B,C) join
  2) Each user waves hands; confirm others see accurate hands/head
  3) Walk to floor markers; verify virtual avatars align with physical spots
- What to say: "Notice how our avatars align with our real positions—this is the shared space."
- Success cues: Smooth head/hand motion; minimal offset; all users visible
- Recovery: If offsets visible, re‑align anchor; reseat guardian; rejoin session

## Scenario 2: Shared Object Interaction (3–4 minutes)
Purpose: Demonstrate synchronized grabbing and ownership.
- Steps:
  1) Introduce a networked grabbable (cube)
  2) User B grabs cube; move/rotate; release; User C grabs next
  3) Show that only one can hold at a time; others see motion in real time
- What to say: "Ownership transfers ensure one source of truth and smooth sync."
- Success cues: No duplicate owners; responsive motion; low latency
- Recovery: If jitter, reduce send rate to 20 Hz and retry; check NetworkedInteractable wired

## Scenario 3: Safety Awareness & Prevention (3–4 minutes)
Purpose: Showcase proactive safety system without collisions.
- Steps:
  1) Users B and C slowly approach to within ~1.2 m (warning radius)
  2) Show yellow halo and gentle haptics; stop before critical
  3) Explain movement restriction when crossing critical threshold
- What to say: "Safety escalates from visual to haptic to controlled movement."
- Success cues: Clear warnings; movement restriction only at critical
- Recovery: Increase radii temporarily; verify ProximityMonitor hookups

## Scenario 4: Boundary Awareness (2 minutes)
Purpose: Demonstrate guardian integration near walls.
- Steps:
  1) Approach room boundary slowly
  2) Show boundary visualization; explain pre‑emptive warnings
  3) Step back; system returns to safe state
- Success cues: Early warning before wall; no boundary crossing
- Recovery: Re‑run guardian setup; use XR boundary fallback

## Presenter Tips
- Keep narration concise; let interactions speak
- Maintain slow, deliberate motions to aid comfort and clarity
- Pause after each success cue for audience observation

## Metrics Capture (optional)
- Enable research mode to log tracking/safety metrics during demos
- Use RollingMetrics HUD to show FPS/RTT briefly (then hide for polish)
