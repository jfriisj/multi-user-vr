[← Documentation Home](../README.md) · [← Workflows](README.md)

# Deployment Guide (Android · Meta Quest 3)

Repeatable build, sign, and install process for Quest 3 (Req 5.3).

## Build Settings (Unity)
- Platform: Android; Architecture: ARM64; Scripting Backend: IL2CPP
- Minimum API Level: Android 10+
- Player: unique Application Identifier (e.g., `com.example.multiuservr`)
- Scenes In Build: include main scene(s)

## Build Script (example)
```csharp
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build.Reporting;

public static class BuildScript
{
    public static void BuildAndroid()
    {
        var scenes = System.Array.ConvertAll(EditorBuildSettings.scenes, s => s.path);
        var opts = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "Builds/Android/MultiUserVR.apk",
            target = BuildTarget.Android,
            options = BuildOptions.CompressWithLz4
        };
        var report = BuildPipeline.BuildPlayer(opts);
        if (report.summary.result != BuildResult.Succeeded)
            throw new System.Exception($"Build failed: {report.summary.result}");
    }
}
#endif
```

Run via CLI:
```powershell
"C:\\Program Files\\Unity\\Hub\\Editor\\2022.3.x\\Editor\\Unity.exe" `
  -batchmode -quit -projectPath .\vr `
  -executeMethod BuildScript.BuildAndroid `
  -logFile .\Logs\BuildAndroid.log
```

## Signing (Keystore)
- Create/upload a keystore; configure in Player Settings → Publishing Settings
- For AAB (store distribution), ensure proper signing keys

## Install to Device (ADB)
```powershell
adb devices
adb install -r .\vr\Builds\Android\MultiUserVR.apk
```

## Release Types
- Development: verbose logging, diagnostics UI enabled
- Research: metrics collection enabled, stable settings
- Demo: polished visuals, diagnostics hidden

## Post-Deploy Checklist
- Host/Client connect on target network; verify session stability
- Safety: warning/critical protocols trigger reliably
- Performance: 90 FPS with target scene; bandwidth within budget
- Crash logs: zero critical errors in logcat during 10-minute session
