using System.Collections.Generic;
using UnityEngine;

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

    public override IEnumerable<VertexData> Generate()
    {
        float size = Mathf.Max(sizeX, sizeZ);

        return VertexData.Build(
                new Plane(sizeX, sizeZ, resolution)
                    .Mod(new NoisePosition(size / 100f, size / 400f, size / 100f)),
                new Plane(sizeX, sizeZ, resolution)
                    .Mod(Translation.Y(-size / 10f))
            );
    }
}
