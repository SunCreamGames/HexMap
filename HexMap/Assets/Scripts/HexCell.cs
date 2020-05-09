using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public abstract class HexCell : MonoBehaviour , IComparable
{
    [SerializeField]
    public int X, Y, Z, Cost, PathCost, DestinationDistance, abba;
    public HexCell N, NE, SE, S, SW, NW;
    public HexCell Parent { get; set; }
    public IEnumerable<HexCell> GetNeighbours()
    {
        if(N != null) 
            yield return N;
        if (NE != null)
            yield return NE;
        if (SE != null)
            yield return SE;
        if (S != null)
            yield return S;
        if (SW != null)
            yield return SW;
        if (NW != null)
            yield return NW;
    }
    public float GetCost
    {
        get
        {
            abba = PathCost + DestinationDistance;
            return abba;
        }
    }

    public int CompareTo(object obj)
    {
        HexCell cell = obj as HexCell;
        if (this.GetCost == cell.GetCost)
            return (int)(this.DestinationDistance - cell.DestinationDistance);
        else return (int)(this.GetCost - cell.GetCost);
    }
}

