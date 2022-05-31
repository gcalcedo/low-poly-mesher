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

    public override IEnumerable<MeshPackage> Generate()
    {
        return MeshPackage.Build(
            new Polygon(15, 10)
                .Mod(new NoisePosition(new Vector3(1, 0, 1), NoiseMode.DYNAMIC)),
            new Polygon(12, 10)
                .Mod(new NoisePosition(new Vector3(1, 2, 1), NoiseMode.DYNAMIC))
                .Mod(Translation.Y(5)),
            new Polygon(10, 10)
                .Mod(new NoisePosition(new Vector3(1, 2, 1), NoiseMode.DYNAMIC))
                .Mod(Translation.Y(10))
            );
    }
}
