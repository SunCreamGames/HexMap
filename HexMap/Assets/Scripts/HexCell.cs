using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public abstract class HexCell : MonoBehaviour
{

    public int X, Y, Z, Cost;
    public HexCell N, NE, SE, S, SW, NW;
   
}

