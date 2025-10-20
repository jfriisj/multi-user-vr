[← Documentation Home](../README.md) · [← Implementation](README.md)

# Guardian Integration (Meta Quest Boundary)

Use headset boundary geometry to prevent wall collisions and align shared spaces.

## Goals (Req 3.3)
- Query play area boundary and nearest distance to walls
- Visualize boundary for user awareness
- Align coordinate spaces across headsets using shared anchors

## Querying Boundary Geometry
Prefer Meta XR SDK boundary APIs when available; otherwise fallback to Unity XR boundary subsystem.

### Meta XR SDK (OVRBoundary)
```csharp
#if OVRPLUGIN_PRESENT
using UnityEngine;
using System.Collections.Generic;

public static class BoundaryUtil
{
    public static bool TryGetBoundary(out List<Vector3> points)
    {
        points = new List<Vector3>();
        var boundary = new OVRBoundary(OVRBoundary.BoundaryType.PlayArea);
        var geometry = boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);
        if (geometry == null || geometry.Length == 0) return false;
        foreach (var v in geometry) points.Add(v);
        return true;
    }
}
#endif
```

### Unity XR Subsystem Fallback
```csharp
using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public static class XRBoundaryFallback
{
    public static bool TryGetBoundary(out List<Vector3> points)
    {
        points = new List<Vector3>();
        var subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances(subsystems);
        foreach (var s in subsystems)
        {
            if (s.TryGetBoundaryPoints(points) && points.Count > 0)
                return true;
        }
        return false;
    }
}
```

## Nearest Distance to Boundary (2D)
```csharp
using UnityEngine;
using System.Collections.Generic;

public static class BoundaryMath
{
    public static float DistanceToBoundaryXZ(Vector3 p, List<Vector3> poly)
    {
        if (poly == null || poly.Count < 2) return float.PositiveInfinity;
        float min = float.PositiveInfinity;
        for (int i = 0; i < poly.Count; i++)
        {
            var a = poly[i]; var b = poly[(i+1)%poly.Count];
            min = Mathf.Min(min, DistPointToSegmentXZ(p, a, b));
        }
        return min;
    }

    static float DistPointToSegmentXZ(Vector3 p, Vector3 a, Vector3 b)
    {
        var ap = new Vector2(p.x - a.x, p.z - a.z);
        var ab = new Vector2(b.x - a.x, b.z - a.z);
        var t = Mathf.Clamp01(Vector2.Dot(ap, ab) / Mathf.Max(ab.sqrMagnitude, 1e-6f));
        var c = new Vector2(a.x, a.z) + t * ab;
        return Vector2.Distance(new Vector2(p.x, p.z), c);
    }
}
```

## Visualization & Protocol Tie-in
- Render boundary as a transparent line/mesh; switch to red when user within 0.5 m
- Feed min distance to SafetyCoordinator to escalate before wall contact

## Shared Space Alignment
- Use Spatial Anchors (Meta Shared Space) to align all headsets to common world origin
- On join: resolve shared anchor, compute transform to local space, and apply to XR Origin
- Validate by placing a calibration cube at known anchor and comparing positions across headsets

## Testing
- Walk around edges while monitoring computed distance; verify warning/critical thresholds
- Rotate room-setup orientation; verify boundary geometry transforms correctly
- Remove boundary permission/availability; ensure system fails safe (visualize default safe area, restrict movement near edges)
