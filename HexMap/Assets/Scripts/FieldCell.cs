using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCell : HexCell
{
    Mesh mesh;


    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        mesh.name = "Hex";
        Vector3[] vertices = new Vector3[7];
        vertices[0] = transform.position;
        for (int i = 0; i < HexMetrics.corners.Length; i++)
        {
            vertices[i] = HexMetrics.corners[i];
        }

        mesh.vertices = vertices;
        mesh.triangles = new int[18] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6, 0, 6, 1 };

    }
}
