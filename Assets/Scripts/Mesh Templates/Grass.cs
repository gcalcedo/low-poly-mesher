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

    public override PointSet Generate()
    {
        float sizeX = height / Random.Range(2f, 4f);
        float sizeZ = height / Random.Range(4f, 6f);

        PointSet ps = PointSet.Build(
                new Pyramid(new Polygon(4, sizeX / 2), height)
                    .Mod(new Scale(new Vector3(1, 1, sizeZ / sizeX)))
                    .Mod(new Rotation(Random.Range(20, 60), Vector3.left))
                    .Mod(new Translation(new Vector3(0, 0, -Random.Range(0, height / 6f))))
                    .Mod(new Rotation(Random.Range(0, 360), Vector3.up))
            );

        return ps;
    }
}
