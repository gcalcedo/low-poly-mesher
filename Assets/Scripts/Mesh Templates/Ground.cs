[System.Serializable]
[TemplatePath("Nature", typeof(Ground))]
public class Ground : MeshTemplate
{
    public float sizeX;
    public float sizeZ;
    public int resolution;

    public Ground(float sizeX, float sizeZ, int resolution=2)
    {
        this.sizeX = sizeX;
        this.sizeZ = sizeZ;
        this.resolution = resolution;
    }

    public override PointSet Generate()
    {
        return PointSet.Build(
                new Plane(sizeX, sizeZ, resolution)
                    .Mod(new NoisePosition(5, 1, 5))
            );
    }
}
