# Spec Features (MVP, Template‑Aligned)

This file enumerates the minimal feature specs required to deliver the MVP using Unity’s MR Multiplayer Tabletop template (`mr-multiplayer/`) and our guide in `MultiUser-VR-MVP-Guide.md`.

Scope: focus on co‑located local multiplayer (same room), player visualization, and basic NGO/XRI integration. Post‑MVP items are excluded here.

Read first: MultiUser-VR-MVP-Guide.md, docs/README.md, .spec-workflow/steering/product.md, tech.md, structure.md

## Core MVP feature specs

1) session-management-ui
- Host/Join/Leave actions (Editor + device)
- Player list with display names and role (Host/Client)
- Minimal in‑scene UI; works with XRMP Network Manager prefab

2) avatar-synchronization
- Owner‑write head/hands, remote avatar visualization
- Update rates: ~20 Hz poses, interpolation/smoothing
- Spawn via NGO PlayerPrefab; OpenXR + Meta plugin

3) networked-interactables
- XRI interactables with NGO ownership transfer
- Object pose sync while grabbed (~30 Hz); idle optimization

4) lan-join-and-manual-ip
- Unity Transport over LAN by default
- Manual IP entry fallback; optional UGS Lobby/Relay later

5) local-co-located-visualization
- In‑room visualization of players (name tags, simple color coding)
- Optional mini “tabletop map” overlay anchored to play area
- Basic distance indicators for awareness (non‑blocking)

6) appearance-customization
- Per‑player color/material + display name selection
- Synced to all clients; persists for session

## Non‑goals (out of MVP scope)
- Full safety/guardian systems, research instrumentation, advanced analytics

## Notes
- Use template assets/scenes where possible (XRMP/BasicScene, MRTabletopAssets/Chess)
- Favor ParrelSync or Multiplayer Play Mode for Editor testing

## Spec IDs to create under .spec-workflow/specs
- session-management-ui
- avatar-synchronization
- networked-interactables
- lan-join-and-manual-ip
- local-co-located-visualization
- appearance-customization
