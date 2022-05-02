using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Primitives", typeof(Point))]
public class Point : MeshTemplate
{
    public Vector3 point;

    /// <summary>
    /// A point in 3D space.
    /// </summary>
    /// <param name="point">Coordinates of the point.</param>
    public Point(Vector3 point)
    {
        this.point = point;
    }

    public override IEnumerable<VertexData> Generate()
    {
        return new List<VertexData> { new VertexData(new List<Vector3> { point }) };
    }
}
