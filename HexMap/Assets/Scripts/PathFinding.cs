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
     List<HexCell> reachableCells;
     HexCell startCell, endCell;
     Material tempStart, tempEnd;
     

    private void Start()
    {
    
        cam = Camera.main;
        reachableCells = new List<HexCell>();
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray;
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
                        startCell  = endCell;
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
    public void AStar()
    {
        reachableCells.Add(startCell);
        while (!reachableCells.Contains(endCell))
        {
            Visit(reachableCells.Min(), endCell);
        }
    }
     private void Visit(HexCell cell, HexCell endCell)
    {
        foreach (HexCell c in cell.GetNeighbours())
        {
            if (c != null)
            {
                CountCellParam(cell, c, endCell);
                c.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/NeiColor");
            }
        }
    }
     private void CountCellParam(HexCell prevCell, HexCell cell, HexCell endCell)
    {
        try
        {
            cell.PathCost = prevCell.PathCost + cell.Cost;
            if (cell.DestinationDistance == 0)
                cell.DestinationDistance = GetDistance(cell, endCell);
            if (cell.PathCost == 0 || prevCell.PathCost + cell.Cost < cell.PathCost)
                cell.PathCost = prevCell.PathCost + cell.Cost;
            reachableCells.Add(cell);
        }
        catch
        {
            throw new ArgumentNullException();
        }
    }

     private float GetDistance(HexCell fromCell, HexCell toCell)
    {
        float distX = fromCell.transform.position.x - toCell.transform.position.x,
            distY = fromCell.transform.position.y - toCell.transform.position.y;
        return Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));
    }
}
