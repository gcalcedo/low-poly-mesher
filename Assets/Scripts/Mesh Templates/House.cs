using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Building", typeof(House))]
public class House : MeshTemplate
{
    public int height;
    public int width;
    public int length;

    public House(int height, int width, int length)
    {
        this.height = height;
        this.width = width;
        this.length = length;
    }

    public override IEnumerable<VertexData> Generate()
    {
        return VertexData.Build(
                new Box(width, 2, 4).Isolate(),
                new Plane(width * 1.2f, length * 1.2f)
                    .Mod(new Translation(new Vector3(0, 2, 0))),
                new Line(
                    new Vector3(-width / 2, 2 + height, 0),
                    new Vector3(width / 2, 2 + height, 0))
            );
    }
}
