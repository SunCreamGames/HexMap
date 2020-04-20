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
    HexCell[] cells;
    public Text cellLabelPrefab;

    Canvas gridCanvas;

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
            if (i % width != width - 1 && i<width && i%2!=0)
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
            
            if (i % width != width - 1 && i < width*(height-1))
            {
                if ((i % width % 2) != 0)
                {
                    cells[i].NE = cells[i + 1 + width];
                }
                else
                {
                    cells[i].NE = cells[i + 1];
                }
            }  // South-West

            if (i % width != 0 && i < width && i % 2 != 0)
            {
                if ((i % width % 2) != 0)
                {
                    cells[i].SW = cells[i - 1];
                }
                else
                {
                    cells[i].SW = cells[i - 1 - width];
                }
            }     // North-East
            
            if (i % width != 0 && i > width*(height-1) && i%width%2==0)
            {
                Debug.Log(i.ToString());
                if ((i % width % 2) != 0)
                {
                    cells[i].NW = cells[i - 1 + width];
                }
                else
                {
                    cells[i].NW = cells[i - 1];
                }
            }   //North-West

        }

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


            int sa = Random.Range(0, 3);
            if (sa == 0)
            {
                cells[i] = Instantiate(sCell, pos, Quaternion.identity, null);
            }
            else if (sa == 1)
            {
                cells[i] = Instantiate(fCell, pos, Quaternion.identity, null);
            }
            else
            {
                cells[i] = Instantiate(mCell, pos, Quaternion.identity, null);
            }
            Text label = Instantiate(cellLabelPrefab, null);
            label.rectTransform.SetParent(gridCanvas.transform, false);
            label.rectTransform.anchoredPosition =
                new Vector2(pos.x, pos.z);
            label.text = x.ToString() + "\n" + z.ToString();

        }


    
}

