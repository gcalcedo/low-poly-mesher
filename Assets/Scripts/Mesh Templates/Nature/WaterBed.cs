using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Nature", typeof(WaterBed))]
public class WaterBed : MeshTemplate
{
    public float size;
    public int resolution;

    public WaterBed(float size, int resolution)
    {
        this.size = size;
        this.resolution = resolution;
    }

    public override IEnumerable<VertexData> Generate()
    {
        return VertexData.Build(
                new Plane(size, size, resolution)
                    .Mod(new Translation(new Vector3(0, 2, 0)))
                    .Anim(new NoisePosition(0, 2, 0), 2)
            );
    }
}
