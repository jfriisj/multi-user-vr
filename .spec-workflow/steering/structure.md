# Project Structure

## Directory Organization

### Repository Structure
```
multi-user-vr/                  # Repository root
├── .git/                       # Git version control
├── .gitignore                  # Git ignore rules for Unity + documentation
├── .idea/                      # JetBrains Rider IDE configuration
├── .vscode/                    # Visual Studio Code configuration
│   └── extensions.json         # Recommended extensions for team
├── .spec-workflow/             # Specification workflow system
│   ├── specs/                  # Feature specifications
│   ├── steering/               # Project steering documents
│   └── templates/              # Document templates
├── docs/                       # Project documentation
│   └── README.md               # Documentation index
├── mr-multiplayer/             # Unity project from MR Multiplayer Tabletop template
│   ├── Assets/
│   ├── Packages/
│   ├── ProjectSettings/
│   └── UserSettings/
└── README.md                   # Main project documentation
```

### Unity Project Structure (mr-multiplayer/Assets/)
```
Assets/                         # Unity assets root
├── XRMP/                       # XR Multiplayer sample content
│   ├── BasicScene.unity        # Minimal multiplayer scene
│   └── Prefabs/
│       └── Managers/
│           └── Network Manager XR Multiplayer.prefab
├── MRTabletopAssets/           # Tabletop template assets (sandbox/slingshot/chess)
│   └── Games/
│       └── Chess/
│           └── Scenes/
│               └── SlicesChess.unity
├── Samples/                    # Imported package samples (XRI, XR Hands, etc.)
├── Scenes/                     # Additional scenes
├── Scripts/                    # C# source code (project-specific)
│   ├── Networking/             # NGO networking logic and sync
│   ├── VR/                     # Input/interaction utilities atop XRI
│   ├── Safety/                 # Proximity, boundaries, overrides
│   ├── GameLogic/              # Avatar mgmt, interactions
│   ├── Research/               # Data collection and metrics
│   └── Utils/                  # Shared utilities
├── Prefabs/                    # Project prefabs (players, UI, etc.)
└── Settings/                   # Input, XR, rendering
```

## Naming Conventions

### Unity Assets
- Scenes: PascalCase (e.g., `BasicScene`, `SlicesChess`)
- Prefabs: PascalCase with descriptive suffix (e.g., `XRI_Network_Player_Avatar`)
- Scripts: PascalCase following C# conventions

### C# Code
- Classes: PascalCase; Interfaces: IPascalCase
- Methods/Properties: PascalCase; private fields: camelCase
- Constants: UPPER_SNAKE_CASE

## Code Organization Principles
1. Safety‑first: safety layer can gate movement/interaction
2. Network‑aware: NGO ownership, authority, and sync patterns
3. Template‑aligned: extend template prefabs/patterns rather than duplicating
4. Unity best practices: component‑based, lifecycle‑aware

## Module Boundaries
- Networking: connection, spawn, sync
- VR Input: XR input/interactions
- Safety: detection, alerts, overrides
- Game Logic: experience rules and flows
- Research: metrics and export

## Testing & Builds
- Editor: XR Device Simulator; Multiplayer Play Mode or ParrelSync
- Devices: Quest 3 APK; Wi‑Fi LAN; optional UGS Relay/Lobby
- Tests: `mr-multiplayer/Assets/Tests/` (Edit/Play mode)
