﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMetrics
{
    public const float oRadius = 10f;
    public const float iRadius = oRadius * 0.866025404f;
    //0.1 iRadius or 0.2 iRadius   ==== delta y   
    //Mathf.Sin(60*Mathf.Deg2Rad)*0.2f*iRadius =============== delta x
    public static Vector3[] corners = {
        // CENTER
        new Vector3(0,0,0),
// =============== IN CORNERS ===============================

         new Vector3(0.5f*oRadius*0.8f, 0f, iRadius*0.8f),
        new Vector3(oRadius*0.8f, 0f,0f),
        new Vector3(0.5f*oRadius*0.8f, 0f, -iRadius*0.8f),
        new Vector3(-0.5f*oRadius*0.8f, 0f, -iRadius*0.8f),
        new Vector3(-oRadius*0.8f, 0f,0f),
        new Vector3(-0.5f*oRadius*0.8f, 0f, iRadius*0.8f),
// =============== OUT CORNERS + NEAR THEM CORNERS ===============================

        new Vector3(0.4f*oRadius,0,iRadius),
        new Vector3(0.5f*oRadius, 0f, iRadius),
        new Vector3(0.5f*oRadius*0.8f +Mathf.Sin(60*Mathf.Deg2Rad)*0.2f*iRadius , 0f, 0.9f*iRadius),

        new Vector3(0.8f*oRadius+Mathf.Sin(60*Mathf.Deg2Rad)*0.2f*iRadius, 0f,0.1f*iRadius),
        new Vector3(oRadius, 0f,0f),
        new Vector3(0.8f*oRadius+Mathf.Sin(60*Mathf.Deg2Rad)*0.2f*iRadius, 0f,-0.1f*iRadius),

        new Vector3(0.5f*oRadius*0.8f +Mathf.Sin(60*Mathf.Deg2Rad)*0.2f*iRadius , 0f, -0.9f*iRadius),
        new Vector3(0.5f*oRadius, 0f, -iRadius),
        new Vector3(0.4f*oRadius,0,-iRadius),

        new Vector3(-0.4f*oRadius,0,-iRadius),
        new Vector3(-0.5f*oRadius, 0f, -iRadius),
        new Vector3(-0.5f*oRadius*0.8f-Mathf.Sin(60*Mathf.Deg2Rad)*0.2f*iRadius , 0f, -0.9f*iRadius),

        new Vector3(-0.8f*oRadius-Mathf.Sin(60*Mathf.Deg2Rad)*0.2f*iRadius, 0f,0.1f*iRadius),
        new Vector3(-oRadius, 0f,0f),
        new Vector3(-0.8f*oRadius-Mathf.Sin(60*Mathf.Deg2Rad)*0.2f*iRadius, 0f,-0.1f*iRadius),

        new Vector3(-0.4f*oRadius,0,iRadius),
        new Vector3(-0.5f*oRadius, 0f, iRadius),
        new Vector3(-0.5f*oRadius*0.8f -Mathf.Sin(60*Mathf.Deg2Rad)*0.2f*iRadius , 0f, 0.9f*iRadius),

    };
}
