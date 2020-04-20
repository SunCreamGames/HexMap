using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMetrics
{
    public const float oRadius = 10f;
    public const float iRadius = oRadius * 0.866025404f;
    public static Vector3[] corners = {
        new Vector3(-0.5f*oRadius, 0f, iRadius),
        new Vector3(0.5f*oRadius, 0f, iRadius),
        new Vector3(oRadius, 0f,0f),
        new Vector3(0.5f*oRadius, 0f, -iRadius),
        new Vector3(-0.5f*oRadius, 0f, -iRadius),
        new Vector3(-oRadius, 0f,0f),

    };
}
