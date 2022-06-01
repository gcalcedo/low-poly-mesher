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

    public override IEnumerable<MeshPackage> Generate()
    {
        float size = Mathf.Max(sizeX, sizeZ);

        return MeshPackage.Build(
                new TemplateGroup(
                    new Plane(sizeX, sizeZ, resolution)
                        .Mod(new NoisePosition(new Vector3(size / 100f, size / 400f, size / 100f), NoiseMode.DYNAMIC)),
                    new Plane(sizeX, sizeZ, resolution)
                        .Mod(Translation.Y(-size / 10f))
                    )
                    .Color("#8DA47B", smoothness:0f)
            );
    }
}

[System.Serializable]
[TemplatePath("Nature", typeof(GroundDirt))]
public class GroundDirt : MeshTemplate
{
    public float sizeX;
    public float sizeZ;
    public int resolution;

    public GroundDirt(float sizeX, float sizeZ, int resolution = 2)
    {
        this.sizeX = sizeX;
        this.sizeZ = sizeZ;
        this.resolution = resolution;
    }

    public override IEnumerable<MeshPackage> Generate()
    {
        float size = Mathf.Max(sizeX, sizeZ);

        return MeshPackage.Build(
                new TemplateGroup(
                    new Plane(sizeX, sizeZ, resolution)
                        .Mod(new NoisePosition(new Vector3(size / 100f, size / 400f, size / 100f), NoiseMode.DYNAMIC)),
                    new Plane(sizeX, sizeZ, resolution)
                        .Mod(Translation.Y(-size / 10f))
                    )
                    .Color("#74654D", smoothness:0f)
            );
    }
}
