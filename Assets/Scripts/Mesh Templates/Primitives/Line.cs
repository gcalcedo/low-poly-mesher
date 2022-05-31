using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Primitives", typeof(Line))]
public class Line : MeshTemplate
{
    public Vector3 from;
    public Vector3 to;
    public int order;
    public Vector3 distanceStep;

    /// <summary>
    /// A line in 3D space.
    /// </summary>
    /// <param name="from">First end of the line.</param>
    /// <param name="to">Second end of the line.</param>
    public Line(Vector3 from, Vector3 to, int order=2)
    {
        this.from = from;
        this.to = to;
        this.order = order;
        distanceStep = (to - from) / (order - 1);
    }

    public override IEnumerable<MeshPackage> Generate()
    {
        MeshTemplate[] points = new MeshTemplate[order];
        Vector3 point = from;
        for(int i = 0; i < order; i++)
        {
            points[i] = new Point(point);
            point += distanceStep;
        }
        return MeshPackage.Build(points);
    }
}
