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

    public override IEnumerable<VertexData> Generate()
    {
        int leafCount = Random.Range(4, 8);
        //leafCount = 4;
        MeshTemplate[] leafs = new MeshTemplate[leafCount];

        for (int i = 0; i < leafCount; i++)
        {
            leafs[i] = Leaf();
        }

        return VertexData.Build(leafs);
    }

    private MeshTemplate Leaf()
    {
        float sizeX = height / Random.Range(2f, 4f);
        float sizeZ = height / Random.Range(4f, 6f);

        return new Pyramid(new Polygon(4, sizeX / 2), height)
            .Mod(new Scale(new Vector3(1, 1, sizeZ / sizeX)))
            .Mod(new Rotation(Random.Range(20, 60), Vector3.left))
            .Mod(new Translation(new Vector3(0, 0, -Random.Range(0, height / 6f))))
            .Mod(new Rotation(Random.Range(0, 360), Vector3.up))
            .Isolate();
    }
}
