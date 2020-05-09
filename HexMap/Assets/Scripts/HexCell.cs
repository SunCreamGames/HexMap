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
    public IEnumerable<HexCell> GetNeighbours()
    {
        yield return N;
        yield return NE;
        yield return SE;
        yield return S;
        yield return SW;
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

