using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Represents relevant data for any 3D mesh.
/// </summary>
public class MeshData
{
    /// <summary>
    /// The vertices of the mesh.
    /// </summary>
    public List<Vector3> Vertices { get; private set; }

    /// <summary>
    /// The indices of the vertices that make up the triangles of the mesh.
    /// </summary>
    public List<int> Triangles { get; private set; }

    public bool IsAnimated { get { return !(Animation is null); } }
    public MeshAnimation Animation { get; set; }

    public MeshData()
    {
        Vertices = new List<Vector3>();
        Triangles = new List<int>();
        Animation = null;
    }

    public MeshData(IEnumerable<Vector3> vertices, IEnumerable<int> triangles, MeshAnimation animation = null)
    {
        Vertices = new List<Vector3>(vertices);
        Triangles = new List<int>(triangles);
        Animation = animation;
    }

    /// <summary>
    /// <see langword="Returns true"/> if the triangles of the mesh are facing inwards, <see langword="false"/> otherwise.
    /// This assumes a consistent face orientation.
    /// </summary>
    public bool HasInwardFaces()
    {
        if (Vertices.Count < 3) return false;

        Vector3 centerPoint = Vector3.zero;
        foreach (Vector3 v in Vertices)
        {
            centerPoint += v;
        }
        centerPoint /= Vertices.Count;

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
        Triangles.Reverse();
    }

    public void Merge(MeshData md)
    {
        Vertices.AddRange(md.Vertices);

        IEnumerable<int> triangles = Enumerable.Range(Triangles.Count, md.Triangles.Count);
        if (md.HasInwardFaces()) {
            triangles = triangles.Reverse();
        }
        Triangles.AddRange(triangles);
    }

    public void LaunchAnimation()
    {
        for(int i = 0; i < Vertices.Count; i++)
        {
            int vertexID = i;
            DOTween.To(
               () => Vertices[vertexID],
               (v) => Vertices[vertexID] = v,
               Animation.target.GetModAction().Invoke(Vertices[vertexID]),
               Animation.speed
               )
               .SetRelative()
               .SetEase(Ease.InOutQuad)
               .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
