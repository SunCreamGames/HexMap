using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HexGrid : MonoBehaviour
{

    [SerializeField]
    int width = 10, height = 10;
    public FieldCell cellPrefab;
    FieldCell[] cells;
    public Text cellLabelPrefab;

    Canvas gridCanvas;

    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        cells = new FieldCell[width * height];
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
        foreach (HexCell cell in cells)
        {
            if()
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

            FieldCell cell = cells[i] = Instantiate(cellPrefab,pos, Quaternion.identity, null);
            Text label = Instantiate(cellLabelPrefab, null);
            label.rectTransform.SetParent(gridCanvas.transform, false);
            label.rectTransform.anchoredPosition =
                new Vector2(cell.transform.position.x, cell.transform.position.z);
            label.text = x.ToString() + "\n" + z.ToString();

    }
        
    
}

