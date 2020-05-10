using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO.Compression;
using System.Linq.Expressions;
using System;
using Microsoft.Win32.SafeHandles;
using UnityEditor.PackageManager;

public class PathFinding : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    float delay;
    Camera cam;
     List<HexCell> reachableCells, wayCells, mainWay, part1,part2;
    [SerializeField]
     HexCell startCell, endCell;
     Material tempStart, tempEnd;
    RaycastHit hit;
    Ray ray;
    enum materials
    {
        Start = 1,Final,Nei,Way,FinalWay
    }
 
    private void Start()
    {
        cam = Camera.main;
        reachableCells = new List<HexCell>();
        wayCells= new List<HexCell>();
        mainWay= new List<HexCell>();
        part1= new List<HexCell>();
        part2= new List<HexCell>();
    }
 
    public void PathFind1()
    {
        StartCoroutine(AStar());
    }
    public void PathFind2()
    {
        reachableCells.Add(startCell);
        reachableCells.Add(endCell);
        part1.Add(startCell);
        part2.Add(endCell);
        StartCoroutine(Dejkstra());
    }
    IEnumerator Dejkstra()
    {
        while (!reachableCells.Contains(endCell))
        {
            HexCell tempCell = GetMinForDejkstra();
            if (wayCells.Contains(tempCell))
            {
                reachableCells.Remove(tempCell);
            }
            else
            {
                yield return new WaitForSeconds(delay);
                VisitDejkstra(tempCell);
            }
        }
    }
    IEnumerator AStar()
    {
        reachableCells.Add(startCell);
        while (!reachableCells.Contains(endCell))
        {
            HexCell tempCell = reachableCells.Min();
            if (wayCells.Contains(tempCell))
            {
                reachableCells.Remove(tempCell);
            }
            else
            {
                yield return new WaitForSeconds(delay);
                Visit(tempCell);
            }
        }
        HexCell curCell = endCell.Parent;
        mainWay.Add(endCell);
        while (curCell != startCell)
        {
            mainWay.Add(curCell);
            Paint(curCell,(int)materials.FinalWay);
            curCell = curCell.Parent;
        }
        mainWay.Add(startCell);
        mainWay.Reverse();
    }
    private void VisitDejkstra(HexCell cell)
    {
        foreach (HexCell c in cell.GetNeighbours())
        {
            if (!wayCells.Contains(c))
            {
                CountCellParam(cell, c);
                if (c != startCell && c != endCell)
                {
                    Paint(c, (int)materials.Nei);
                }
            }
         
            reachableCells.Remove(cell);
            wayCells.Add(cell);
            if (cell != startCell && c != endCell)
            {
                Paint(cell, (int)materials.Way);
            }
        }
    }
    private void Visit(HexCell cell)
    {
        foreach (HexCell c in cell.GetNeighbours())
        {
            if (!wayCells.Contains(c))
            {
                CountCellParam(cell, c);
                if (c != startCell && c != endCell)
                {
                    Paint(c, (int)materials.Nei);
                }
            }
         
            reachableCells.Remove(cell);
            wayCells.Add(cell);
            if (cell != startCell && c != endCell)
            {
                Paint(cell, (int)materials.Way);
            }
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
    private void CountCellParamDejkstra(HexCell prevCell, HexCell cell)
    {
       if (cell != startCell && cell!=endCell)
        {
            if (cell.PathCost > prevCell.PathCost + cell.Cost || cell.PathCost == 0)
            {
                cell.PathCost = prevCell.PathCost + cell.Cost;
                cell.Parent = prevCell;
            }
            reachableCells.Add(cell);
        }
    }
    private float GetDistance(HexCell fromCell, HexCell toCell)
    {
        return 15*(Math.Abs(toCell.Y- fromCell.Y)+Math.Abs(toCell.X- fromCell.X)+Math.Abs(toCell.Z- fromCell.Z))/2;
    }
    void Update()
    {
        delay = slider.value;
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

    HexCell GetMinForDejkstra()
    {
        HexCell cell = reachableCells[0];
        foreach (HexCell cell1 in reachableCells)
        {
            if (cell1.PathCost <= cell.PathCost)
            {
                cell = cell1;
            }
        }
        return cell;
    }
    void Paint(HexCell cell, int mat)
    {
        switch (mat)
        {
            case 1:
                cell.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/StartColor");
                break;
            case 2:
                cell.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/EndColor");
                break;
            case 3:
                cell.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/NeiColor");
                break;
            case 4:
                cell.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/WayColor");
                break;
            default:
                cell.transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/FinalWayColor");
                break;
        }
    }
}
