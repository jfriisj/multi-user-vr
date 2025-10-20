[← Documentation Home](../README.md) · [← Workflows](README.md)

# Development Process (Spec-Driven · Repeatable · Quality-Gated)

End-to-end workflow integrating spec-workflow, GitHub Flow, Unity development, and quality gates (Req 5.1).

## Branching & Commit Policy (GitHub Flow)
- Branches: `feature/<area>/<desc>`, `bugfix/<area>/<desc>`, `docs/<desc>`
- Base: `main` is always releasable; rebase or merge via PR only
- Commits: Conventional Commits (e.g., `feat(network): add ownership server validation`)
- PR: 1+ reviewer; status checks must pass before merge

## Daily Loop
1) Select a task from `.spec-workflow/specs/implementation-documentation/tasks.md`
2) Edit `tasks.md`: change `[ ]` → `[-]` for the task
3) Create branch and implement
4) Run local quality gates (tests/build)
5) Update docs and diagrams as needed
6) Edit `tasks.md`: change `[-]` → `[x]` when done
7) Open PR; ensure all checks pass; request review

## Local Setup
- Unity LTS supported by the template with Android Build Support
- Clone repo and (when present) open Unity project at `mr-multiplayer/`
- Import required packages (see Implementation → VR Setup)

## Quality Gates (must pass before merge)
- Unit tests (EditMode) pass
- Integration tests (PlayMode) for core scenes pass
- Safety scenarios validated (no failing proximity/guardian tests)
- Build succeeds for Android (Quest 3)
- Diagrams render (Mermaid) and docs links resolve
- Bandwidth/latency budgets within targets (see Network Optimization)

## Suggested Automation (Unity CLI)
Use Unity command line to ensure repeatability (adjust editor path):

```powershell
# EditMode tests
"C:\\Program Files\\Unity\\Hub\\Editor\\<LTS_VERSION>\\Editor\\Unity.exe" `
  -batchmode -quit -projectPath .\\mr-multiplayer `
  -runTests -testPlatform EditMode `
  -logFile .\\Logs\\EditMode.log `
  -testResults .\\Logs\\EditMode.xml `
  -testResultsFormatter NUnit

# PlayMode tests
"C:\\Program Files\\Unity\\Hub\\Editor\\<LTS_VERSION>\\Editor\\Unity.exe" `
  -batchmode -quit -projectPath .\\mr-multiplayer `
  -runTests -testPlatform PlayMode `
  -logFile .\\Logs\\PlayMode.log `
  -testResults .\\Logs\\PlayMode.xml `
  -testResultsFormatter NUnit
```

## Code Review Checklist
- [ ] Tests updated/added (EditMode/PlayMode)
- [ ] Safety: no regressions, protocols enforced
- [ ] Networking: ownership/security validated on server
- [ ] Performance: 90 FPS target + <20 ms sync met
- [ ] Docs: updated guides, links valid
- [ ] Tasks: `tasks.md` status updated correctly

## Release Readiness
- Tag version (SemVer) and draft release notes
- Produce builds per Deployment Guide (Dev/Research/Demo)
- Attach APK/AAB artifacts and test matrix results
