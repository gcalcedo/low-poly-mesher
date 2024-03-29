using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Primitives", typeof(Pyramid))]
public class Pyramid : MeshTemplate
{
    public Polygon basis;
    public float height;

    public Pyramid(Polygon basis, float height)
    {
        this.basis = basis;
        this.height = height;
    }

    public override IEnumerable<MeshPackage> Generate()
    {
        return MeshPackage.Build(
            basis.Copy(),
            new Point(new Vector3(0, height, 0))
            );
    }
}
