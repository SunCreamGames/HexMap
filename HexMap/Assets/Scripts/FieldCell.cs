using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCell : HexCell
{
    Mesh mesh;
    [SerializeField]
    Material mat;
    [SerializeField]
    bool isHereAFarm = false, walkable = false;
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        mesh.name = "Hex";
        Vector3[] vertices = new Vector3[6];
        for (int i = 0; i < HexMetrics.corners.Length; i++)
        {
            vertices[i] = HexMetrics.corners[i];
        }

        mesh.vertices = vertices;
        mesh.triangles = new int[12] { 0, 1, 2, 2, 3, 4, 4, 5, 0, 0, 2, 4 };
        GetComponent<Renderer>().material = mat;
        mesh.RecalculateNormals();
    }
}
