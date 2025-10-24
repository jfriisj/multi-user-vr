# Unity Discover - Complete Setup Guide

This guide provides step-by-step instructions to set up the Unity Discover Mixed Reality project on your development machine. Follow these steps carefully to avoid common setup issues.

## Prerequisites

- **Windows 10/11** (required for Meta Quest development)
- **Meta Quest 2/3/Pro** headset
- **USB-C cable** or **Quest Link wireless**
- **Git** with **Git LFS** installed
- **Android SDK** (installed via Unity Hub)

## 🔧 Initial Environment Setup

### 1. Install Unity Environment
- [ ] **Download Unity Hub** from [unity.com](https://unity.com/download)
- [ ] **Install Unity 6000.0.50f1 or newer** from Unity Hub
- [ ] **Install Git LFS**: Open terminal and run:
  ```bash
  git lfs install
  ```

### 2. Clone Project Repository
- [ ] **Clone the repository**:
  ```bash
  git clone https://github.com/oculus-samples/Unity-Discover.git
  ```
- [ ] **Open project in Unity Hub** and let Unity import all packages (this may take 10-15 minutes)

## 🛠️ Critical Build Fixes

### 3. Install Android SDK Requirements
**⚠️ CRITICAL**: Unity builds will fail without CMake 3.22.1

- [ ] **Install CMake 3.22.1** via Android SDK Manager:
  - Open **Command Prompt as Administrator**
  - Navigate to: `C:\Users\[USERNAME]\Android\Sdk\cmdline-tools\6.0\bin`
  - Execute: `sdkmanager.bat --install "cmake;3.22.1"`
  - Wait for installation to complete

### 4. Fix Unity Input System Settings
**⚠️ CRITICAL**: Android builds fail with "Both" input handling

- [ ] **Open Unity Project Settings** (Edit > Project Settings)
- [ ] **Navigate to** XR Plug-in Management > Input System Package
- [ ] **Change "Active Input Handling"** from "Both" to **"Input System Package (New)"**
- [ ] **Save project** (Ctrl+S)

## 🎮 Meta Quest Configuration

### 5. Setup Meta Quest Developer Account
- [ ] **Create Meta Developer Account** at [developers.meta.com](https://developers.meta.com/horizon/)
- [ ] **Create TWO applications**:
  - **Quest Application** (for device builds)
  - **PC VR Application** (for Unity editor testing)

### 6. Configure Data Use Checkup
- [ ] **Navigate to** your app in Meta Developer Center
- [ ] **Go to** "Data Use Checkup" section
- [ ] **Enable these data types**:
  - ✅ User ID (for Avatars, Oculus Username)
  - ✅ User Profile (for Avatars, Oculus Username)
  - ✅ Avatars (for avatar system)
- [ ] **Submit the configuration**

### 7. Enable Cloud Storage
- [ ] **Go to** Development > Cloud Storage in Meta Developer Center
- [ ] **Enable** "Automatic Cloud Backup"
- [ ] **Submit** the changes

### 8. Set Meta App ID in Unity
- [ ] **Copy App ID** from Meta Developer Center (API section)
- [ ] **In Unity**: Go to Meta > Platform > Edit Settings
- [ ] **Paste App ID** in OculusPlatformSettings.asset
- [ ] **Save settings**

## 🌐 Photon Networking Configuration

### 9. Setup Photon Account
- [ ] **Create free account** at [photonengine.com](https://www.photonengine.com)
- [ ] **Create NEW APP #1**:
  - Type: **"Fusion"**
  - SDK Version: **"Fusion 1"**
- [ ] **Create NEW APP #2**:
  - Type: **"Voice"**

### 10. Configure Photon in Unity
- [ ] **Copy both App IDs** from Photon dashboard
- [ ] **In Unity**: Go to Fusion > RealtimeSettings
- [ ] **Paste App IDs** in PhotonAppSettings.asset:
  - Fusion App ID → App Id Fusion field
  - Voice App ID → App Id Voice field
- [ ] **Save settings**

## 🥽 Headset Setup & Testing

### 11. Setup Quest Link for Testing
- [ ] **Enable Developer Mode** on Quest headset
- [ ] **Enable Quest Link**:
  - Put on headset
  - Quick Settings > Quest Link (or Air Link)
  - Select desktop and Launch
- [ ] **Connect headset** via USB-C or wireless

### 12. Configure Headset Permissions
- [ ] **Enable Point Cloud Sharing**:
  - Settings > Privacy > Device Permissions > Share Point Cloud Data ✅
- [ ] **Enable Spatial Data**:
  - Settings > Apps > Permissions > Spatial Data ✅

### 13. Upload Initial Build for Colocation
- [ ] **Build and upload** first APK to Meta release channel
- [ ] **Add test users** to release channel if testing multiplayer
- [ ] **Note**: Only first build needs upload, development builds work after this

### 14. Test Project Setup
- [ ] **Load scene**: Assets/Discover/Scenes/Discover.unity
- [ ] **Test in editor** with Quest Link:
  - Put on headset with Quest Link active
  - Press Play in Unity
  - Verify app launches in VR
- [ ] **Test device build** (optional):
  - Build and Run to device
  - Verify all features work

## 📦 Package Dependencies

This project includes these key packages (automatically imported):
- Meta Avatars SDK
- Meta XR Utilities & Interaction SDK
- Photon Fusion & Voice
- UniTask, ParrelSync, NaughtyAttributes

## 🚨 Common Issues & Solutions

### Build Fails: "Missing CMake 3.22.1"
**Solution**: Follow step 3 exactly - install CMake via Android SDK Manager

### Build Fails: "Active Input Handling set to Both"
**Solution**: Follow step 4 - change to "Input System Package (New)"

### Can't connect to Photon
**Solution**: Verify Photon App IDs are correctly pasted in step 10

### Spatial Anchors not working
**Solution**: Ensure step 13 (initial upload) is completed and headset permissions (step 12) are enabled

### Quest Link not working
**Solution**: Enable Developer Mode on headset and ensure USB connection or wireless is stable

## ✅ Setup Complete!

If all steps are completed successfully:
- ✅ Unity project builds without errors
- ✅ Editor testing works with Quest Link
- ✅ Device builds work on Quest headset
- ✅ Multiplayer networking functional
- ✅ Spatial anchors and colocation enabled

## 📋 Quick Checklist Summary

Essential steps that MUST be completed:
- [x] Unity 6000.0.50f1+ installed
- [x] CMake 3.22.1 installed via Android SDK
- [x] Input handling set to "Input System Package"
- [x] Meta App ID configured
- [x] Photon App IDs configured
- [x] Headset permissions enabled
- [x] Initial build uploaded to Meta release channel

## 📞 Getting Help

If you encounter issues:
1. Check this guide's "Common Issues" section
2. Review [Unity Discover Documentation](./Documentation/)
3. Check [Meta Quest Developer Documentation](https://developers.meta.com/horizon/documentation/)
4. Verify all prerequisites and steps are completed

---

**Created**: Based on Unity Discover v1.0 setup requirements and common build issues encountered during development.