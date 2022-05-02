using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Nature", typeof(Tree))]
public class Tree : MeshTemplate
{
    public Tree()
    {

    }

    public override IEnumerable<VertexData> Generate()
    {
        return VertexData.Build(
                new TemplateGroup(
                    new Pyramid(
                        new Polygon(4, 2.5f)
                            .Mod(new NoisePosition(0.3f, 0.5f, 0.3f)) as Polygon
                        , 4)
                        .Mod(new Translation(new Vector3(0, 2, 0)))
                        .Isolate(),
                    new Pyramid(
                        new Polygon(4, 2f)
                            .Mod(new NoisePosition(0.2f, 0.4f, 0.2f)) as Polygon
                        , 3.5f)
                        .Mod(new Translation(new Vector3(0, 4, 0)))
                        .Isolate(),
                    new Pyramid(
                        new Polygon(4, 1.5f)
                            .Mod(new NoisePosition(0.1f, 0.3f, 0.1f)) as Polygon
                        , 2.5f)
                        .Mod(new Translation(new Vector3(0, 6, 0)))
                        .Isolate(),
                    new PolyBox(new Polygon(7, 0.5f), 2)
                        .Mod(new NoiseScale(0.5f, 0, 0.5f))
                )
                    .Mod(new Scale(new Vector3(2, 2, 2)))
                    .Mod(new NoiseScale(0.3f))
                    .Mod(new Rotation(Random.Range(0f, 360f), Vector3.up))
            );
    }
}