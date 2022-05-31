using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Nature", typeof(Grass))]
public class Grass : MeshTemplate
{
    public float height;

    public Grass(float height)
    {
        this.height = height;
    }

    public override IEnumerable<MeshPackage> Generate()
    {
        int leafCount = Random.Range(4, 8);
        MeshTemplate[] leafs = new MeshTemplate[leafCount];

        for (int i = 0; i < leafCount; i++)
        {
            leafs[i] = Leaf();
        }

        return MeshPackage.Build(leafs);
    }

    private MeshTemplate Leaf()
    {
        float sizeX = height / Random.Range(2f, 4f);
        float sizeZ = height / Random.Range(4f, 6f);
        Vector3 swayVector = Seed.XZ();

        return new Pyramid(new Polygon(4, sizeX / 2), height)
            .Mod(Scale.Z(sizeZ / sizeX))
            .Mod(Rotation.X(Seed.Range(20, 60)))
            .Mod(Translation.Z(height / 6f))
            .Mod(Rotation.Y(Seed.Range(0, 360)))
            .Isolate()
            .Anim(new GenericMod(p => p + swayVector * Easing.EaseInQuad(0, 1, p.y.Map(0, height, 0, 1))), Seed.Range(2f, 3f));
    }
}
