[← Documentation Home](../README.md) · [← Implementation](README.md)

# VR Setup Guide (Unity 2022.3 LTS · Meta Quest 3)

This guide configures Unity for Meta Quest 3 using XR Interaction Toolkit (XRI), OpenXR, and Meta XR SDK.

## Prerequisites (Unity 2022.3+)
- Unity Hub with Unity 2022.3 LTS (Android Build Support installed)
- Meta Quest Developer account and Quest 3 in Developer Mode

## 1) Install Required Packages (Package Manager)
- XR Plugin Management (com.unity.xr.management)
- OpenXR Plugin (com.unity.xr.openxr)
- XR Interaction Toolkit 2.5+ (com.unity.xr.interaction.toolkit)
- XR Hands 1.4+ (com.unity.xr.hands)
- Meta XR SDK (per README: install via Package Manager/integration instructions)

## 2) Enable OpenXR for Android
- Edit → Project Settings → XR Plugin Management:
  - Android tab: Enable OpenXR
- Project Settings → OpenXR (Android):
  - Add/enable required features: Oculus/Meta Touch Controller Profile, Eye Gaze (optional), Hand Tracking (if used)

## 3) Input System
- Edit → Project Settings → Player → Other Settings → Active Input Handling: Both (recommended)
- Window → Package Manager → XR Interaction Toolkit → Import Input Samples (Starter Assets)

## 4) Create XR Rig
- GameObject → XR → XR Origin (Action-based)
- Optional: Add CharacterController to XR Origin (height ≈ 1.7, center (0,0.9,0), radius 0.2)
- Verify Left/Right controllers exist (Action-based controller prefabs) and Actions are assigned

## 5) Scene & App Settings
- File → Build Settings:
  - Switch Platform to Android
  - Add current scene to Scenes In Build
- Player Settings (Android):
  - Scripting Backend: IL2CPP
  - Target Architectures: ARM64
  - Minimum API Level: Android 10 (API 29)+
  - Uncheck Multithreaded Rendering only if debugging issues
- Quality: Ensure Mobile/VR-appropriate settings

## 6) Quest 3 Device Setup
- Enable Developer Mode (Meta Quest app)
- Enable USB Debugging on the headset
- In Unity: Build & Run to deploy

## 7) Optional Features
- Hand tracking: Project Settings → OpenXR (Android) → Enable Hand Tracking + import XR Hands sample rigs
- Passthrough/MR: Enable via Meta XR SDK features as needed

## Verification Checklist
- In Editor (with Device Simulator or mock HMD), XR Origin exists and controllers respond
- On device, app launches into VR, tracking is stable, controllers/hands work
- No OpenXR validation errors in Console

## Next Steps
- Proceed to [Avatar Synchronization](avatar-synchronization.md) and [Tracking Systems](tracking-systems.md)
