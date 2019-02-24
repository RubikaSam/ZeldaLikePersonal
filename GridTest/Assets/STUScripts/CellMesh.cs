using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CellMesh : MonoBehaviour
{

    Mesh cellMesh;
    List<Vector3> vertices;
    List<int> triangles;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = cellMesh = new Mesh();
        cellMesh.name = "Cell Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }

    public void Triangulate(SquareCellScript[] cells)
    {
        cellMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        for (int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }
        cellMesh.vertices = vertices.ToArray();
        cellMesh.triangles = triangles.ToArray();
        cellMesh.RecalculateNormals();
    }

    void Triangulate(SquareCellScript cell)
    {
        Vector3 center = cell.transform.localPosition;
        //for (int i = 0; i < 4; i++)
        {
            AddTriangle(
                center,
                center + SquareMetrics.corners[0],
                center + SquareMetrics.corners[1]
            );
        }
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }
}
