using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Nature", typeof(Plateau))]
public class Plateau : MeshTemplate
{
    public Plateau()
    {
    }

    public override IEnumerable<VertexData> Generate()
    {
        return VertexData.Build(
            new Polygon(15, 10)
                .Mod(new NoisePosition(1, 0, 1)),
            new Polygon(12, 10)
                .Mod(new NoisePosition(1, 2, 1))
                .Mod(new Translation(new Vector3(0, 5, 0))),
            new Polygon(10, 10)
                .Mod(new NoisePosition(1, 2, 1))
                .Mod(new Translation(new Vector3(0, 10, 0)))
            );
    }
}
