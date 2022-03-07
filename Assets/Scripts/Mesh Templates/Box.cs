using UnityEngine;

[System.Serializable]
[TemplatePath("Primitives", typeof(Box))]
public class Box : MeshTemplate
{
    [SerializeField] private float sizeX;
    [SerializeField] private float sizeY;
    [SerializeField] private float sizeZ;

    /// <summary>
    /// A box in 3D space.
    /// </summary>
    /// <param name="sizeX">Size on the X axis (Width).</param>
    /// <param name="sizeY">Size on the Y axis (Height).</param>
    /// <param name="sizeZ">Size on the Z axis (Length).</param>
    public Box(float sizeX, float sizeY, float sizeZ)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.sizeZ = sizeZ;
    }

    public override PointSet Generate()
    {
        return PointSet.Build(
            new Plane(sizeX, sizeZ),
            new Plane(sizeX, sizeZ)
                .Mod(new Translation(new Vector3(0, sizeY, 0)))
            );
    }
}