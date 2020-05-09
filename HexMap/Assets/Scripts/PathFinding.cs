using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO.Compression;
using System.Linq.Expressions;
using System;
using Microsoft.Win32.SafeHandles;

public class PathFinding : MonoBehaviour
{
    Camera cam;
     List<HexCell> reachableCells, wayCells, mainWay;
    [SerializeField]
     HexCell startCell, endCell;
     Material tempStart, tempEnd;
    RaycastHit hit;
    Ray ray;

    private void Start()
    {
        cam = Camera.main;
        reachableCells = new List<HexCell>();
        wayCells= new List<HexCell>();
        mainWay= new List<HexCell>();
    }
 
    public void AStar()
    {
        Debug.Log("Astar Started");

        reachableCells.Add(startCell);
        while (!reachableCells.Contains(endCell))
        {
            Debug.Log("Visit start");
            if (wayCells.Contains(reachableCells.Min()))
            {
                reachableCells.Remove(reachableCells.Min());
            }
            else
            {
                Visit(reachableCells.Min(), endCell);
            }
        }
        HexCell curCell = endCell.Parent;
        while (curCell != startCell)
        {
            if (curCell == null)
            {
                Debug.Log("AAAA");
            }
            curCell.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/FinalWayColor");
            curCell = curCell.Parent;
        }
    }
     private void Visit(HexCell cell, HexCell endCell)
    {
        foreach (HexCell c in cell.GetNeighbours())
        {
            Debug.Log("Nei foreach");

            if (c != null)
            {
                Debug.Log("Neil rePaint");          
                CountCellParam(cell, c, endCell);
                if (!wayCells.Contains(c))
                {
                    c.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/NeiColor");
                }
            }
        }
        wayCells.Add(cell);
        cell.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/WayColor");
    }
     private void CountCellParam(HexCell prevCell, HexCell cell, HexCell endCell)
    {
        try
        {
            cell.PathCost = prevCell.PathCost + cell.Cost;
            if (cell.DestinationDistance == 0)
            {
                cell.DestinationDistance = GetDistance(cell, endCell);
            }
            if ((cell.PathCost == 0 && cell!= startCell)|| prevCell.PathCost + cell.Cost < cell.PathCost)
            {
                cell.PathCost = prevCell.PathCost + cell.Cost;
                cell.Parent = prevCell;
            }
            reachableCells.Add(cell);
            reachableCells.Remove(prevCell);
        }
        catch
        {
            throw new ArgumentNullException();
        }
    }

     private int GetDistance(HexCell fromCell, HexCell toCell)
    {
        return 10*(Math.Abs(toCell.Y- fromCell.Y)+Math.Abs(toCell.X- fromCell.X)+Math.Abs(toCell.Z- fromCell.Z))/2;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<HexCell>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<HexCell>() == endCell)
                    {
                        if (startCell != null)
                        {
                            startCell.GetComponent<MeshRenderer>().material = tempStart;
                        }
                        startCell = endCell;
                        endCell = null;
                        tempStart = tempEnd;
                        tempEnd = null;
                    }
                    else
                    {
                        if (startCell != null)
                        {
                            startCell.GetComponent<MeshRenderer>().material = tempStart;
                        }
                        startCell = hit.collider.gameObject.GetComponent<HexCell>();
                        tempStart = startCell.GetComponent<MeshRenderer>().material;
                    }
                    startCell.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/StartColor");
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<HexCell>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<HexCell>() == startCell)
                    {
                        if (endCell != null)
                        {
                            endCell.GetComponent<MeshRenderer>().material = tempEnd;
                        }
                        endCell = startCell;
                        startCell = null;
                        tempEnd = tempStart;
                        tempStart = null;
                    }
                    else
                    {
                        if (endCell != null)
                        {
                            endCell.GetComponent<MeshRenderer>().material = tempEnd;
                        }
                        endCell = hit.collider.gameObject.GetComponent<HexCell>();
                        tempEnd = endCell.GetComponent<MeshRenderer>().material;
                    }
                    endCell.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/EndColor");
                }
            }
        }
    }
}
