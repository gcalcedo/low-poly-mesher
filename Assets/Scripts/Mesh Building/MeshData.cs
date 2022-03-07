using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents relevant data for any 3D mesh.
/// </summary>
public class MeshData
{
    /// <summary>
    /// The vertices of the mesh.
    /// </summary>
    public Vector3[] Vertices { get; private set; }

    /// <summary>
    /// The indices of the vertices that make up the triangles of the mesh.
    /// </summary>
    public int[] Triangles { get; private set; }

    public MeshData(Vector3[] vertices, int[] triangles)
    {
        Vertices = vertices;
        Triangles = triangles;
    }

    /// <summary>
    /// <see langword="Returns true"/> if the triangles of the mesh are facing inwards, <see langword="false"/> otherwise.
    /// This assumes a consistent face orientation.
    /// </summary>
    public bool HasInwardFaces()
    {
        Vector3 centerPoint = Vector3.zero;
        foreach(Vector3 v in Vertices)
        {
            centerPoint += v;
        }
        centerPoint /= Vertices.Length;

        Vector3 p0 = Vertices[Triangles[0]];
        Vector3 p1 = Vertices[Triangles[1]];
        Vector3 p2 = Vertices[Triangles[2]];
        Vector3 normal = Vector3.Cross(p1 - p0, p2 - p0).normalized;

        normal *= Vector3.Magnitude(p0 - centerPoint) / 4;

        float d0 = Vector3.Magnitude(normal - centerPoint + p0);
        float d1 = Vector3.Magnitude(normal + (normal / 2) - centerPoint + p0);

        return d0 > d1;
    }

    /// <summary>
    /// Inverts the orientation of every triangle of the mesh.
    /// </summary>
    public void FlipFaces()
    {
        Triangles = Triangles.Reverse().ToArray();
    }
}
