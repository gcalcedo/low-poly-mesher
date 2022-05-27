using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Building", typeof(Tent))]
public class Tent : MeshTemplate
{
    public Tent(int height, int width, int length)
    {

    }

    public override IEnumerable<VertexData> Generate()
    {
        return VertexData.Build(
                new Pyramid(new Polygon(4, 2), 2)
                    .Mod(new Rotation(45, Vector3.up)),
                new Pyramid(new Polygon(4, 1.25f), 1.25f)
                    .Mod(new Rotation(45, Vector3.up))
                    .Mod(new Translation(new Vector3(0.6f, 0, 0)))
                    .Isolate(),
                new PolyBox(new Polygon(6, 0.03f), Mathf.Sqrt(13))
                    .Mod(new Rotation(60, Vector3.forward))
                    .Mod(new Rotation(45, Vector3.up))
                    //.Mod(Translation.Z(-3))
                    .Isolate()
            );
    }
}
