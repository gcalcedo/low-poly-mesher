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

    public override PointSet Generate()
    {
        return PointSet.Build(
                new TemplateGroup(
                    new Pyramid(new Polygon(4, 4), 4),
                    new Pyramid(new Polygon(4, 3), 3.5f)
                        .Mod(new Translation(new Vector3(0, 2, 0))),
                    new Pyramid(new Polygon(4, 2), 3)
                        .Mod(new Translation(new Vector3(0, 4, 0)))
                ).Mod(new Translation(new Vector3(0, 2, 0)))
                //new PolyBox(new Polygon(7, 1), 2)
            );
    }
}
