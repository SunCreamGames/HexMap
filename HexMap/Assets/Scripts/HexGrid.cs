using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HexGrid : MonoBehaviour
{
    [SerializeField]
    int width = 10, height = 10;
    [SerializeField]
    FieldCell fCell;
    [SerializeField]
    MountainCell mCell;
    [SerializeField]
    SeaCell sCell;
    public static HexCell[] cells;
    public Text cellLabelPrefab;
    RaycastHit hit;
    Ray ray;
    Canvas gridCanvas;
    Camera cam;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&Input.GetAxis("Jump")>0)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/WayColor");
              }
        }
    }
    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        cells = new HexCell[width * height];
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }
    
    private void Start()
    {
        cam = Camera.main;
        cells[0].X = cells[0].Y = cells[0].Z = 0;
        for (int i = 0; i < cells.Length; i ++)
        {
            if (i >= width)
            {
                cells[i].S = cells[i - width];
            }  // South
            if (i < cells.Length - width)
            {
                cells[i].N = cells[i + width];
            }   // North


            if (i % width != width - 1 &&( i>=width||(i<width && i%2!=0)))
            {
                if ((i % width % 2) != 0)
                {
                    cells[i].SE = cells[i + 1];
                }
                else
                {
                    cells[i].SE = cells[i + 1 - width];
                }
            }   // South-East
            
            if (i % width != 0 && (i > width || (i < width && i % 2 != 0)))
            {
                if ((i % width % 2) != 0)
                {
                    cells[i].SW = cells[i - 1];
                }
                else
                {
                    cells[i].SW = cells[i - width-1];
                }
            }  // South-West

            if (i % width != width-1 && ((i < cells.Length-width) ||((i>=cells.Length-width)&& i %width% 2 == 0)))
            {
                if ((i % width % 2)== 0)
                {
                    cells[i].NE = cells[i + 1];
                }
                else
                {
                    cells[i].NE = cells[i+ 1 + width];
                }
            }     //North-East

            if (i % width !=0&& (i<width*(height-1)||(i > width * (height - 1) && i % width % 2 == 0)))
            {
                if ((i % width % 2) != 0)
                {
                    cells[i].NW = cells[i - 1 + width];
                }
                else
                {
                    cells[i].NW = cells[i - 1];
                }
            }   //North-West



                if (cells[i].N != null)
                {
                    cells[i].N.X = cells[i].X + 1;
                    cells[i].N.Y = cells[i].Y;
                    cells[i].N.Z = cells[i].Z - 1;
                }
                if (cells[i].S != null)
                {
                    cells[i].S.X = cells[i].X - 1;
                    cells[i].S.Y = cells[i].Y;
                    cells[i].S.Z = cells[i].Z + 1;
                }
                if (cells[i].NW != null)
                {
                    cells[i].NW.X = cells[i].X + 1;
                    cells[i].NW.Y = cells[i].Y - 1;
                    cells[i].NW.Z = cells[i].Z;
                }

                if (cells[i].SW != null)
                {
                    cells[i].SW.X = cells[i].X;
                    cells[i].SW.Y = cells[i].Y - 1;
                    cells[i].SW.Z = cells[i].Z + 1;
                }

                if (cells[i].NE != null)
                {
                    cells[i].NE.X = cells[i].X;
                    cells[i].NE.Y = cells[i].Y + 1;
                    cells[i].NE.Z = cells[i].Z - 1;
                }

                if (cells[i].SE != null)
                {
                    cells[i].SE.X = cells[i].X - 1;
                    cells[i].SE.Y = cells[i].Y + 1;
                    cells[i].SE.Z = cells[i].Z;
                }


            Text label = Instantiate(cellLabelPrefab, null);
            label.rectTransform.SetParent(gridCanvas.transform, false);
            label.rectTransform.anchoredPosition =
                new Vector2(cells[i].transform.position.x, cells[i].transform.position.z);
            label.text = cells[i].X.ToString() + "\n" + cells[i].Y.ToString() + "\n" + cells[i].Z.ToString();
        }
            
        
    }

    public HexCell[] ReturnCells()
    {
        return cells;
    }
        void CreateCell(int x, int z, int i)
        {
            Vector3 pos;
            if (x % 2 == 0)
            {
                pos = new Vector3(x * HexMetrics.oRadius * 1.5f, 0, z * HexMetrics.iRadius * 2);
            }
            else
            {
                pos = new Vector3(x * HexMetrics.oRadius * 1.5f, 0, z * HexMetrics.iRadius * 2 + HexMetrics.iRadius);
            }


            int sa = Random.Range(0, 10);
            if (sa < 6)
            {
                cells[i] = Instantiate(sCell, pos, Quaternion.identity, null);
            }
           //else if (sa <5 )
           //{
           //    cells[i] = Instantiate(mCell, pos, Quaternion.identity, null);
           //}
            else
            {
                cells[i] = Instantiate(fCell, pos, Quaternion.identity, null);
            }

       

    }



}

