using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Nature", typeof(Crystal))]
public class Crystal : MeshTemplate
{
    public float size;
    public float height;

    public Crystal(float size, float height)
    {
        this.size = size;
        this.height = height;
    }

    public override IEnumerable<MeshPackage> Generate()
    {
        return MeshPackage.Build(
                new Rock(size, height)
                    .Color("#E81F3A", metallic: 1f, smoothness: 0.8f, hexEmission: "#6F1F1F")
            );
    }
}
