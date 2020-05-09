using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]

public class OutLine : MonoBehaviour
{
    Mesh mesh;
    Material mat1;
    Vector3[] vertices;
    void Start()
    {
        mat1 = Resources.Load<Material>("Materials/WayColor");
        mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        mesh.Clear();
        mesh.name = "Outline";
        vertices = new Vector3[HexMetrics.corners.Length + 1];
        for (int i = 0; i < HexMetrics.corners.Length; i++)
        {
            vertices[i] = HexMetrics.corners[i];
        }
        mesh.vertices = vertices;
        mesh.triangles = new int[]
        { 7,8,1,
        1,8,2,
        2,8,9,
        2,9,3,
        3,9,10,
        3,10,4,
        4,10,11,
        11,5,4,
        5,11,12,
        12,6,5,
        6,12,7,
        7,1,6
        
        };
        mesh.RecalculateNormals();
        gameObject.AddComponent<MeshCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
