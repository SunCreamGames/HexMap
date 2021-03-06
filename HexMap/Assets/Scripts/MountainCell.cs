﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainCell : HexCell
{
    Mesh mesh;
    [SerializeField]
    Material mat;
    [SerializeField]
    Vector3[] vertices;
    bool walkable = false;
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        mesh.Clear();
        mesh.name = "Hex";
        vertices = new Vector3[HexMetrics.corners.Length + 1];
        for (int i = 0; i < HexMetrics.corners.Length; i++)
        {
            vertices[i] = HexMetrics.corners[i];
        }



        //for (int i = 7; i < 7+HexMetrics.corners.Length; i++)
        //{
        //    vertices[i+1] = HexMetrics.corners[i];
        //}


        mesh.vertices = vertices;
        Debug.Log("1");
        mesh.triangles = new int[18] { 0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6, 0, 6, 1 };
        GetComponent<Renderer>().material = mat;
        mesh.RecalculateNormals();
        Debug.Log("2");

        gameObject.AddComponent<MeshCollider>();

    }

}
