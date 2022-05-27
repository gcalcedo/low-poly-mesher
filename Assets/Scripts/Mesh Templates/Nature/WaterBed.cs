using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Nature", typeof(WaterBed))]
public class WaterBed : MeshTemplate
{
    public float sizeX;
    public float sizeZ;
    public int resolution;

    public WaterBed(float sizeX, float sizeZ, int resolution)
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
                    .Mod(Translation.Y(-size / 10f)),
                new Plane(sizeX, sizeZ, resolution)
                    .Mod(new NoisePosition(new Vector3(size / 50f, 0, size / 50f), NoiseMode.DYNAMIC))
                    .Anim(Translation.Y(size / 100f))
            );
    }
}
