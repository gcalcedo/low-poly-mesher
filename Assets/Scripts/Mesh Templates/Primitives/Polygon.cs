using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Primitives", typeof(Polygon))]
public class Polygon : MeshTemplate
{
    [SerializeField] private int order;
    [SerializeField] private float size;

    /// <summary>
    /// A n-vertex polygon on the XZ plane.
    /// </summary>
    /// <param name="order">Number of vertices of the polygon.</param>
    /// <param name="size">Distance from the center of the polygon to each vertex.</param>
    public Polygon(int order, float size)
    {
        this.order = order;
        this.size = size;
    }

    public override IEnumerable<MeshPackage> Generate()
    {
        ICollection<Vector3> vertices = new List<Vector3>();
        float angleStep = 360f / order;
        Vector3 vertex = new Vector3(size, 0, 0);

        for(int i = 0; i < order; i++)
        {
            vertices.Add(vertex);
            vertex = Quaternion.AngleAxis(angleStep, Vector3.up) * vertex;
        }

        return new List<MeshPackage> { new MeshPackage(vertices, new List<int>()) };
    }
}
