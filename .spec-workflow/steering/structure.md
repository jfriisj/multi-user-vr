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
│   │   └── diagram-generation/ # Diagram generation spec
│   ├── steering/               # Project steering documents
│   │   ├── product.md         # Product vision and objectives
│   │   ├── tech.md            # Technology stack decisions
│   │   └── structure.md       # This document
│   └── templates/             # Document templates
├── docs/                      # Project documentation
│   ├── diagrams/              # Mermaid diagrams for architecture
│   │   ├── architecture/      # System architecture diagrams
│   │   ├── workflows/         # Process and workflow diagrams
│   │   └── use-cases/         # Business use case diagrams
│   └── README.md              # Documentation index
├── vr/                        # Unity VR project directory
│   ├── Assets/                # Unity assets (see Unity Structure below)
│   ├── Library/               # Unity cache (ignored by Git)
│   ├── Logs/                  # Unity logs (ignored by Git)
│   ├── Packages/              # Unity Package Manager dependencies
│   ├── ProjectSettings/       # Unity project configuration
│   ├── UserSettings/          # User-specific Unity settings (ignored)
│   ├── *.csproj              # Auto-generated C# project files
│   └── vr.sln                # Visual Studio solution file
└── README.md                  # Main project documentation
```

### Unity Project Structure (vr/Assets/)
```
Assets/                         # Unity assets root
├── Samples/                    # Unity package samples (XR, Interaction Toolkit)
├── Scenes/                     # Unity scene files
│   ├── MainScene.unity        # Primary multi-user VR scene
│   └── TestScenes/            # Development and testing scenes
├── Scripts/                   # C# source code (planned structure)
│   ├── Networking/            # Netcode for GameObjects networking logic
│   │   ├── NetworkManagers/   # Network management and connection handling
│   │   ├── Synchronization/   # Avatar and object synchronization
│   │   └── RPC/              # Remote procedure calls
│   ├── VR/                    # VR-specific functionality
│   │   ├── Input/            # XR input handling and controllers
│   │   ├── Tracking/         # Inside-out tracking and interference management
│   │   ├── Interaction/      # XR Interaction Toolkit extensions
│   │   └── Guardian/         # Guardian system integration
│   ├── Safety/               # Collision prevention and safety systems
│   │   ├── CollisionDetection/ # Proximity monitoring
│   │   ├── WarningSystem/    # Visual and haptic alerts
│   │   └── MovementRestriction/ # Safety overrides
│   ├── GameLogic/            # Core game and interaction logic
│   │   ├── AvatarManagement/ # Avatar synchronization and representation
│   │   ├── StateManagement/ # Game state and session management
│   │   └── EventSystem/     # User action and event handling
│   ├── Research/             # Research instrumentation and data collection
│   │   ├── DataLogging/      # Performance and interference logging
│   │   ├── Metrics/          # Performance measurement tools
│   │   └── Export/           # Research data export utilities
│   └── Utils/                # Shared utilities and helper functions
│       ├── Extensions/       # C# extension methods
│       ├── Serialization/    # Data serialization helpers
│       └── Debug/            # Development and debugging tools
├── Prefabs/                  # Unity prefab assets
│   ├── Player/               # User avatar and controller prefabs
│   │   ├── AvatarPrefab      # Synchronized user avatar
│   │   └── LocalPlayer       # Local user representation
│   ├── Environment/          # Scene and environmental objects
│   │   ├── SharedObjects/    # Objects that can be interacted with by all users
│   │   └── SafetyBoundaries/ # Visual boundaries and collision objects
│   ├── UI/                   # User interface prefabs
│   │   ├── VRCanvas/         # VR-space UI elements
│   │   ├── SafetyAlerts/     # Warning and alert UI
│   │   └── DebugUI/          # Development and research UI
│   └── Networking/           # Network-related prefabs
│       ├── NetworkManager    # Main networking management prefab
│       └── SynchronizedObjects/ # Objects with network synchronization
├── Materials/                # Unity materials and shaders
│   ├── Avatar/               # User avatar materials
│   ├── Environment/          # Scene materials
│   └── Safety/               # Safety system visual materials (warnings, boundaries)
├── Audio/                    # Audio assets and sound effects
│   ├── Safety/               # Collision warnings and alert sounds
│   ├── Environment/          # Ambient and environmental audio
│   └── UI/                   # User interface sound effects
├── Settings/                 # Unity settings and configuration
│   ├── Input/                # XR input action maps
│   ├── Rendering/            # VR rendering pipeline settings
│   └── XR/                   # XR provider and SDK settings
├── TextMesh Pro/             # TextMesh Pro font and text assets
├── VRTemplateAssets/         # Unity VR template resources
├── XR/                       # XR-specific assets and configurations
└── XRI/                     # XR Interaction Toolkit samples and extensions
```

## Naming Conventions

### Unity Assets
- **Scenes**: `PascalCase` (e.g., `MainMultiUserScene`, `TestTrackingScene`)
- **Prefabs**: `PascalCase` with descriptive suffix (e.g., `UserAvatarPrefab`, `SafetyBoundaryObject`)
- **Materials**: `PascalCase_MaterialType` (e.g., `Avatar_Diffuse`, `Safety_Warning`)
- **Scripts**: `PascalCase` following C# conventions

### C# Code
- **Classes**: `PascalCase` (e.g., `NetworkManager`, `CollisionDetector`, `AvatarSynchronizer`)
- **Interfaces**: `IPascalCase` (e.g., `INetworkSynchronizable`, `ISafetySystem`)
- **Methods**: `PascalCase` (e.g., `DetectCollision()`, `SynchronizePosition()`)
- **Properties**: `PascalCase` (e.g., `IsCollisionDetected`, `NetworkLatency`)
- **Fields**: `camelCase` private, `PascalCase` public (e.g., `isConnected`, `MaxUsers`)
- **Constants**: `UPPER_SNAKE_CASE` (e.g., `MAX_USERS`, `COLLISION_WARNING_DISTANCE`)

### Documentation
- **Markdown Files**: `kebab-case` (e.g., `system-architecture.md`, `collision-prevention-workflow.md`)
- **Directory Names**: `kebab-case` for docs, `PascalCase` for Unity (e.g., `architecture/`, `Scripts/`)

## Code Structure Patterns

### Unity MonoBehaviour Organization
```csharp
public class ExampleVRComponent : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private float detectionRadius = 2.0f;
    [SerializeField] private LayerMask userLayer;
    #endregion

    #region Public Properties
    public bool IsActive { get; private set; }
    public event Action<Collision> OnCollisionDetected;
    #endregion

    #region Private Fields
    private NetworkVariable<Vector3> networkPosition;
    private List<GameObject> detectedUsers;
    #endregion

    #region Unity Lifecycle
    private void Awake() { /* Initialize components */ }
    private void Start() { /* Setup networking and references */ }
    private void Update() { /* Frame-based logic */ }
    private void OnDestroy() { /* Cleanup */ }
    #endregion

    #region Public Methods
    public void StartDetection() { /* Public API */ }
    public void StopDetection() { /* Public API */ }
    #endregion

    #region Network Callbacks (if NetworkBehaviour)
    public override void OnNetworkSpawn() { /* Network initialization */ }
    public override void OnNetworkDespawn() { /* Network cleanup */ }
    #endregion

    #region Private Methods
    private void DetectNearbyUsers() { /* Implementation details */ }
    private void TriggerSafetyWarning() { /* Internal logic */ }
    #endregion
}
```

### Namespace Organization
```csharp
namespace MultiUserVR.Networking { /* Networking components */ }
namespace MultiUserVR.VR.Input     { /* VR input handling */ }
namespace MultiUserVR.VR.Tracking  { /* Tracking systems */ }
namespace MultiUserVR.Safety       { /* Safety systems */ }
namespace MultiUserVR.GameLogic    { /* Core game logic */ }
namespace MultiUserVR.Research     { /* Research instrumentation */ }
namespace MultiUserVR.Utils        { /* Shared utilities */ }
```

## Code Organization Principles

1. **Safety-First Architecture**: Safety systems are isolated and can override all other functionality
2. **Network-Aware Design**: All shared objects implement network synchronization interfaces
3. **Research Instrumentation**: Data collection and logging are built into core systems, not added as afterthoughts
4. **Modular VR Components**: VR-specific functionality is separated from general game logic
5. **Unity Best Practices**: Follow Unity's component-based architecture and lifecycle patterns
6. **Educational Clarity**: Code is structured to be readable and understandable for learning objectives

## Module Boundaries

### Clear Separation of Concerns
- **Networking Layer**: Handles only network communication and synchronization
- **VR Input Layer**: Manages XR input without business logic
- **Safety System**: Operates independently and can override other systems
- **Game Logic**: Handles user interactions and experience flow
- **Research Layer**: Collects data without interfering with core functionality

### Dependency Direction
```
Research Layer    → All other layers (observes, doesn't control)
Game Logic        → VR Input, Networking (uses services)
Safety System     → VR Input, Networking (monitors and overrides)
VR Input Layer    → Unity XR (hardware abstraction)
Networking Layer  → Unity Netcode (framework abstraction)
```

### API Boundaries
- **Public APIs**: Exposed through interfaces for testing and modularity
- **Internal Implementation**: Hidden behind abstractions
- **Unity Integration**: Minimal Unity dependencies in business logic
- **Research Data**: Read-only access to operational data

## Code Size Guidelines

### File Organization
- **Maximum file size**: 500 lines per C# script (excluding generated code)
- **Single responsibility**: One primary class/component per file
- **Related functionality**: Helper classes can be in same file if <100 lines total

### Method Complexity
- **Maximum method size**: 50 lines per method
- **Cyclomatic complexity**: Maximum 10 decision points per method
- **Nesting depth**: Maximum 4 levels of nesting
- **Parameter count**: Maximum 5 parameters per method (use objects for complex data)

### Unity-Specific Guidelines
- **MonoBehaviour size**: Maximum 300 lines per MonoBehaviour script
- **Network synchronization**: Keep NetworkBehaviour implementations focused
- **Inspector fields**: Group related fields with `[Header]` attributes
- **Serialization**: Use `[SerializeField]` for private fields exposed to Inspector

## Documentation Standards

### Code Documentation
- **XML Documentation**: All public classes, methods, and properties must have XML documentation
- **Inline Comments**: Complex algorithms and Unity-specific code should include explanatory comments
- **Research Annotations**: Data collection points should be documented for academic review
- **Safety Critical Code**: All safety system code requires detailed documentation

### Unity-Specific Documentation
- **Component Usage**: MonoBehaviour components should document their purpose and dependencies
- **Prefab Documentation**: Complex prefabs should include setup instructions
- **Scene Documentation**: Each scene should have a text file explaining its purpose and setup
- **Inspector Tooltips**: Use `[Tooltip]` attributes for all SerializeField properties

### Research Documentation
- **Data Collection Points**: Document what data is collected and why
- **Performance Metrics**: Explain measurement methodology and expected ranges
- **Safety Validation**: Document all safety system test procedures
- **Experimental Setup**: Include room setup requirements and calibration procedures

## Development Workflow Structure

### Git Workflow
- **Branch Naming**: `feature/safety-system`, `research/tracking-interference`, `bugfix/collision-detection`
- **Commit Messages**: Follow conventional commits format with scope (e.g., `feat(safety): add proximity detection`)
- **Unity Collaboration**: Use Unity Smart Merge for scene and prefab conflicts

### Testing Structure
```
vr/Assets/Tests/               # Unity Test Framework tests
├── EditMode/                  # Edit mode tests (no Play mode required)
│   ├── Networking/            # Network logic unit tests
│   ├── Safety/                # Safety system unit tests
│   └── Utils/                 # Utility function tests
├── PlayMode/                  # Play mode tests (require Unity runtime)
│   ├── VRIntegration/         # VR system integration tests
│   ├── MultiUser/             # Multi-user scenario tests
│   └── Performance/           # Performance benchmark tests
└── Research/                  # Research-specific test scenarios
    ├── TrackingInterference/  # Tracking interference test cases
    └── SafetyValidation/      # Safety system validation tests
```

### Build and Deployment Structure
- **Development Builds**: Automatic builds for testing with extensive logging
- **Research Builds**: Optimized builds with data collection enabled
- **Demo Builds**: Polished builds for presentations and demonstrations
- **Platform Targets**: Android (Meta Quest 3) with fallback PC builds for development