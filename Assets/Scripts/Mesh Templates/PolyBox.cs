using UnityEngine;

[System.Serializable]
[TemplatePath("Primitives", typeof(PolyBox))]
public class PolyBox : MeshTemplate
{
    [SerializeField] private Polygon basis;
    [SerializeField] private float height;

    /// <summary>
    /// A n-vertex-base box on 3D space.
    /// </summary>
    /// <param name="basis">Base of the box.</param>
    /// <param name="height">Height of the box.</param>
    public PolyBox(Polygon basis, float height)
    {
        this.basis = basis;
        this.height = height;
    }

    /// <summary>
    /// A n-vertex-base box on 3D space.
    /// </summary>
    /// <param name="order">Number of vertices of the base polygon.</param>
    /// <param name="size">Distance from the center of the base polygon to each vertex.</param>
    /// <param name="height">Height of the box.</param>
    public PolyBox(int order, float size, float height)
    {
        basis = new Polygon(order, size);
        this.height = height;
    }

    public override PointSet Generate()
    {
        return PointSet.Build(
            basis.Copy(),
            basis.Copy()
                .Mod(new Translation(new Vector3(0, height, 0)))
            );
    }
}
