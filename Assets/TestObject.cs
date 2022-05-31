using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Test", typeof(TestObject))]
public class TestObject : MeshTemplate
{
    public override IEnumerable<MeshPackage> Generate()
    {
        Pyramid pyramid = new Pyramid(new Polygon(4, 3), 2);

        return MeshPackage.Build(
                new TemplateGroup(
                    pyramid.Copy()
                        .Mod(Translation.Y(2)),
                    pyramid.Copy()
                        .Mod(Rotation.Z(180))
                )
            );
    }
}
