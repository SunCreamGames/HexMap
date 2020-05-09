using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO.Compression;
using System.Linq.Expressions;
using System;
using Microsoft.Win32.SafeHandles;
using UnityEditor.PackageManager;

public class PathFinding : MonoBehaviour
{
    bool canWork;
    Camera cam;
     List<HexCell> reachableCells, wayCells, mainWay;
    [SerializeField]
     HexCell startCell, endCell;
     Material tempStart, tempEnd;
    Timer stepTimer;
    RaycastHit hit;
    Ray ray;

    private void Start()
    {
        stepTimer = GetComponent<Timer>();
        stepTimer.Duration = 3f;
        canWork = true;
        cam = Camera.main;
        reachableCells = new List<HexCell>();
        wayCells= new List<HexCell>();
    }
 
    public void PathFind()
    {
        StartCoroutine(AStar());
    }
    IEnumerator AStar()
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
                yield return new WaitForSeconds(2f);
                Visit(reachableCells.Min(), endCell);
            }
        }
        HexCell curCell = endCell.Parent;
        while (curCell != startCell)
        {
            curCell.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/FinalWayColor");
            curCell = curCell.Parent;
        }
    }
    private void Visit(HexCell cell)
    {
        foreach (HexCell c in cell.GetNeighbours())
        {
            foreach (HexCell c in cell.GetNeighbours())
            {
                if (!wayCells.Contains(c))
                {
                    CountCellParam(cell, c);
                    c.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/NeiColor");
                }
            }
            reachableCells.Remove(cell);
            wayCells.Add(cell);
            cell.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/WayColor");
        }
    }
     private void CountCellParam(HexCell prevCell, HexCell cell)
    {
        if (cell.DestinationDistance == 0)
        {
            cell.DestinationDistance = GetDistance(cell, endCell);
        }
        if (cell != startCell)
        {
            if (cell.PathCost > prevCell.PathCost + cell.Cost || cell.PathCost == 0)
            {
                cell.PathCost = prevCell.PathCost + cell.Cost;
                cell.Parent = prevCell;
            }
            reachableCells.Add(cell);
        }
    }

     private int GetDistance(HexCell fromCell, HexCell toCell)
    {
        return (Math.Abs(toCell.Y- fromCell.Y)+Math.Abs(toCell.X- fromCell.X)+Math.Abs(toCell.Z- fromCell.Z))/2;
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
