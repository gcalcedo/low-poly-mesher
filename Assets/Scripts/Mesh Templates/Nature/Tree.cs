using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Nature", typeof(Tree))]
public class Tree : MeshTemplate
{
    public override IEnumerable<MeshPackage> Generate()
    {
        Vector3 swayVector = Seed.XZ();

        return MeshPackage.Build(
                new TemplateGroup(
                    new TemplateGroup(
                        new Pyramid(
                            new Polygon(4, 2.5f)
                                .Mod(new NoisePosition(new Vector3(0.3f, 0.5f, 0.3f), NoiseMode.DYNAMIC)) as Polygon
                            , 4)
                            .Mod(Translation.Y(2))
                            .Isolate(),
                        new Pyramid(
                            new Polygon(4, 2f)
                                .Mod(new NoisePosition(new Vector3(0.2f, 0.4f, 0.2f), NoiseMode.DYNAMIC)) as Polygon
                            , 3.5f)
                            .Mod(Translation.Y(4))
                            .Isolate(),
                        new Pyramid(
                            new Polygon(4, 1.5f)
                                .Mod(new NoisePosition(new Vector3(0.1f, 0.3f, 0.1f), NoiseMode.DYNAMIC)) as Polygon
                            , 2.5f)
                            .Mod(Translation.Y(6))
                            .Isolate()
                        )
                        .Color("#78877A"),
                    new PolyBox(new Polygon(7, 0.5f), 4)
                        .Mod(new NoiseScale(new Vector3(0.5f, 0, 0.5f), NoiseMode.DYNAMIC))
                        .Color("#9A6C45")
                )
                    .Mod(new Scale(2))
                    .Mod(new NoiseScale(0.3f, NoiseMode.STATIC))
                    .Mod(Rotation.Y(Seed.Range(0, 360)))
                    .Anim(new GenericMod(p => p + swayVector * Easing.EaseInQuad(0, 1, p.y.Map(0, 10f, 0, 1))), Seed.Range(2f, 3f))
            );
    }
}