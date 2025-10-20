# Spec Feature List

This list defines specs to create for the project. Run spec-workflow-guide first, then scaffold each spec as shown.

## Prerequisites
- Load workflow guide (required before spec work)
```powershell
# In Warp, call the MCP tool: spec-workflow-guide
```

## Agent Prompt (Spec Workflow MCP)
IMPORTANT: Agents MUST use the Spec Workflow MCP tools to create specs. Do not create files directly or use the helper function below. Read steering and docs first.

### multiuservrsetup-auto-config

```
Implement the task for spec multiuservrsetup-auto-config, first run spec-workflow-guide to get the workflow guide then implement the task:
Role: Spec Author | Task: Create spec for 'multiuservrsetup-auto-config' using the Spec Workflow MCP tools ONLY
Must read before writing: 
- .spec-workflow/steering/product.md, tech.md, structure.md
- docs/README.md and docs/architecture/*, docs/implementation/*, docs/workflows/*
Feature (kebab-case):
- multiuservrsetup-auto-config — One‑click setup GameObject wiring, auto‑adding components
Restrictions: Use MCP tools (spec-workflow-guide, approvals, spec-status). Provide approvals with filePath ONLY. Poll status for approval, then delete approval before proceeding to next phase. Do not write files directly.
Success: Create requirements.md → approved, design.md → approved, tasks.md → approved under .spec-workflow/specs/multiuservrsetup-auto-config/ following templates.
```

### session-management-ui

```
Implement the task for spec session-management-ui, first run spec-workflow-guide to get the workflow guide then implement the task:
Role: Spec Author | Task: Create spec for 'session-management-ui' using the Spec Workflow MCP tools ONLY
Must read before writing: 
- .spec-workflow/steering/product.md, tech.md, structure.md
- docs/README.md and docs/architecture/*, docs/implementation/*, docs/workflows/*
Feature (kebab-case):
- session-management-ui — Host/Client controls + debug UI
Restrictions: Use MCP tools (spec-workflow-guide, approvals, spec-status). Provide approvals with filePath ONLY. Poll status for approval, then delete approval before proceeding to next phase. Do not write files directly.
Success: Create requirements.md → approved, design.md → approved, tasks.md → approved under .spec-workflow/specs/session-management-ui/ following templates.
```

### avatar-synchronization

```
Implement the task for spec avatar-synchronization, first run spec-workflow-guide to get the workflow guide then implement the task:
Role: Spec Author | Task: Create spec for 'avatar-synchronization' using the Spec Workflow MCP tools ONLY
Must read before writing: 
- .spec-workflow/steering/product.md, tech.md, structure.md
- docs/README.md and docs/architecture/*, docs/implementation/*, docs/workflows/*
Feature (kebab-case):
- avatar-synchronization — Owner‑write head/hands @20 Hz with interpolation
Restrictions: Use MCP tools (spec-workflow-guide, approvals, spec-status). Provide approvals with filePath ONLY. Poll status for approval, then delete approval before proceeding to next phase. Do not write files directly.
Success: Create requirements.md → approved, design.md → approved, tasks.md → approved under .spec-workflow/specs/avatar-synchronization/ following templates.
```

### networked-interactables

```
Implement the task for spec networked-interactables, first run spec-workflow-guide to get the workflow guide then implement the task:
Role: Spec Author | Task: Create spec for 'networked-interactables' using the Spec Workflow MCP tools ONLY
Must read before writing: 
- .spec-workflow/steering/product.md, tech.md, structure.md
- docs/README.md and docs/architecture/*, docs/implementation/*, docs/workflows/*
Feature (kebab-case):
- networked-interactables — Ownership transfer; object sync @30 Hz while grabbed
Restrictions: Use MCP tools (spec-workflow-guide, approvals, spec-status). Provide approvals with filePath ONLY. Poll status for approval, then delete approval before proceeding to next phase. Do not write files directly.
Success: Create requirements.md → approved, design.md → approved, tasks.md → approved under .spec-workflow/specs/networked-interactables/ following templates.
```

### networking-discovery-and-join

```
Implement the task for spec networking-discovery-and-join, first run spec-workflow-guide to get the workflow guide then implement the task:
Role: Spec Author | Task: Create spec for 'networking-discovery-and-join' using the Spec Workflow MCP tools ONLY
Must read before writing: 
- .spec-workflow/steering/product.md, tech.md, structure.md
- docs/README.md and docs/architecture/*, docs/implementation/*, docs/workflows/*
Feature (kebab-case):
- networking-discovery-and-join — LAN auto‑discovery + manual IP fallback
Restrictions: Use MCP tools (spec-workflow-guide, approvals, spec-status). Provide approvals with filePath ONLY. Poll status for approval, then delete approval before proceeding to next phase. Do not write files directly.
Success: Create requirements.md → approved, design.md → approved, tasks.md → approved under .spec-workflow/specs/networking-discovery-and-join/ following templates.
```

### single-headset-testing-and-bots

```
Implement the task for spec single-headset-testing-and-bots, first run spec-workflow-guide to get the workflow guide then implement the task:
Role: Spec Author | Task: Create spec for 'single-headset-testing-and-bots' using the Spec Workflow MCP tools ONLY
Must read before writing: 
- .spec-workflow/steering/product.md, tech.md, structure.md
- docs/README.md and docs/architecture/*, docs/implementation/*, docs/workflows/*
Feature (kebab-case):
- single-headset-testing-and-bots — Editor+Headset flow and bot simulation
Restrictions: Use MCP tools (spec-workflow-guide, approvals, spec-status). Provide approvals with filePath ONLY. Poll status for approval, then delete approval before proceeding to next phase. Do not write files directly.
Success: Create requirements.md → approved, design.md → approved, tasks.md → approved under .spec-workflow/specs/single-headset-testing-and-bots/ following templates.
```

## Helper: create a spec from templates
```powershell
function New-Spec { param([string]$n)
  $dir = ".spec-workflow\specs\$n"
  New-Item -ItemType Directory $dir -Force | Out-Null
  Copy-Item ".spec-workflow\templates\requirements-template.md" "$dir\requirements.md"
  Copy-Item ".spec-workflow\templates\design-template.md"       "$dir\design.md"
  Copy-Item ".spec-workflow\templates\tasks-template.md"        "$dir\tasks.md"
}
```

## MVP specs
- multiuservrsetup-auto-config — One‑click setup GameObject wiring, auto‑adding components
```powershell
New-Spec multiuservrsetup-auto-config
```
- session-management-ui — Host/Client controls + debug UI
```powershell
New-Spec session-management-ui
```
- avatar-synchronization — Owner‑write head/hands @20 Hz with interpolation
```powershell
New-Spec avatar-synchronization
```
- networked-interactables — Ownership transfer; object sync @30 Hz while grabbed
```powershell
New-Spec networked-interactables
```
- networking-discovery-and-join — LAN auto‑discovery + manual IP fallback
```powershell
New-Spec networking-discovery-and-join
```
- single-headset-testing-and-bots — Editor+Headset flow and bot simulation
```powershell
New-Spec single-headset-testing-and-bots
```

## Post‑MVP (optional) specs
- safety-system — Proximity warnings → movement restriction; guardian tie‑in
```powershell
New-Spec safety-system
```
- guardian-integration — Boundary distance, visuals, pre‑emptive warnings
```powershell
New-Spec guardian-integration
```
- research-instrumentation — Opt‑in data collection, metrics, procedures
```powershell
New-Spec research-instrumentation
```
- demo-and-presentation — Setup, scenarios, run‑of‑show
```powershell
New-Spec demo-and-presentation
```
- performance-monitoring-hud — In‑app EMA HUD for FPS/RTT/bytes thresholds
```powershell
New-Spec performance-monitoring-hud
```

## References
- docs/README.md → architecture, implementation, workflows
- .spec-workflow/steering/product.md, tech.md, structure.md
