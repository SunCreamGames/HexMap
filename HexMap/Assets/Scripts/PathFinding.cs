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
    private void Start()
    {
        cam = Camera.main;
        reachableCells = new List<HexCell>();
        AStar();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<HexCell>() != null)
                {
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
    }
    private void AStar()
    {
        HexGrid mainGrid = gameObject.GetComponent<HexGrid>();
        HexCell[] cells = mainGrid.ReturnCells();
        HexCell startCell = cells[0];
        HexCell endCell = cells[6];
        reachableCells.Add(startCell);
        while (!reachableCells.Contains(endCell))
        {
            Visit(reachableCells.Min(), endCell);
        }
    }
    private void Visit(HexCell cell, HexCell endCell)
    {
        foreach(HexCell c in cell.GetNeighbours())
            CountCellParam(cell, c, endCell);
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
