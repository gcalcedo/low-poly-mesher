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

        FixInsideOut();
    }

    /// <summary>
    /// Inverts the orientation of every triangle of the mesh.
    /// </summary>
    public void FlipFaces()
    {
        Triangles.Reverse();
    }

    private List<Tween> tween = new List<Tween>();
    private Dictionary<Vector3, Vector3> transformations = new Dictionary<Vector3, Vector3>();
    private Dictionary<Vector3, float> durations = new Dictionary<Vector3, float>();

    public void ClearAnimations()
    {
        tween.ForEach(t => {
            t.Kill();
        });
        tween.Clear();
    }

    public void LaunchAnimation()
    {
        if (!IsAnimated) return;

        for (int i = 0; i < Vertices.Count; i++)
        {
            int vertexID = i;

            Vector3 transform = Animation.target.GetModAction().Invoke(Vertices[vertexID]);
            float duration = Animation.speed == -1 ? UnityEngine.Random.Range(1f, 3f) : Animation.speed;
            if (transformations.ContainsKey(Vertices[vertexID]))    
            {
                transform = transformations[Vertices[vertexID]];
                duration = durations[Vertices[vertexID]];
            }
            else
            {
                transformations.Add(Vertices[vertexID], transform);
                durations.Add(Vertices[vertexID], duration);
            }

            tween.Add(DOTween.To(
               () => Vertices[vertexID],
               (v) => Vertices[vertexID] = v,
               transform,
               duration
               )
               .SetEase(Ease.InOutQuad)
               .SetLoops(-1, LoopType.Yoyo));
        }
    }

    private struct Triangle
    {
        public Vector3 A, B, C;
        public Vector3 center;
        public Vector3 normal;

        public Triangle(Vector3 A, Vector3 B, Vector3 C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            center = (A + B + C) / 3;
            normal = Vector3.Cross(B - A, C - A).normalized;
        }
    }
    public void FixInsideOut()
    {
        List<Triangle> tris = new List<Triangle>();
        for(int i = 0; i < Triangles.Count; i += 3)
        {
            Vector3 p0 = Vertices[Triangles[i]];
            Vector3 p1 = Vertices[Triangles[i + 1]];
            Vector3 p2 = Vertices[Triangles[i + 2]];
            tris.Add(new Triangle(p0, p1, p2));
        }

        int inwardCount = 0;
        int outWardCount = 0;

        foreach(Triangle t1 in tris)
        {
            int numberOfIntersections = 0;

            foreach(Triangle t2 in tris)
            {
                if (t1.A == t2.A && t1.B == t2.B && t1.C == t2.C) continue;

                Vector3 E1 = t2.B - t2.A;
                Vector3 E2 = t2.C - t2.A;
                Vector3 N = Vector3.Cross(E1, E2);
                float det = -Vector3.Dot(t1.normal, N);
                float invdet = 1.0f / det;
                Vector3 AO = t1.center - t2.A;
                Vector3 DAO = Vector3.Cross(AO, t1.normal);
                float u = Vector3.Dot(E2, DAO) * invdet;
                float v = -Vector3.Dot(E1, DAO) * invdet;
                float t = Vector3.Dot(AO, N) * invdet;

                if(det >= 1e-6f && t >= 0.0f && u >= 0.0f && v >= 0.0f && (u + v) <= 1.0f)
                {
                    numberOfIntersections++;
                }
            }

            if (numberOfIntersections % 2 == 0) outWardCount++; else inwardCount++;

            if(inwardCount > 100 || outWardCount > 100) break;
        }

        if(inwardCount > outWardCount)
        {
            FlipFaces();
        }
    }
}
