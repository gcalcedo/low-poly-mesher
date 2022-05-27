using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Nature", typeof(Rock))]
public class Rock : MeshTemplate
{
    public float size;
    public float height;

    private int magnitude;
    private int order;
    private int orderStep;
    private float sizeStep;
    private float heightStep;

    public Rock(float size, float height)
    {
        
        this.size = size;
        this.height = height;
    }

    public override IEnumerable<VertexData> Generate()
    {
        magnitude = height > size ? (int)(height / size) * 4 : (int)(size / height) * 2;
        order = 12;
        sizeStep = size / (magnitude * 2);
        orderStep = order / magnitude;
        heightStep = height / magnitude;

        return VertexData.Build(BuildRock());
    }

    private MeshTemplate[] BuildRock()
    {
        MeshTemplate[] rock = new MeshTemplate[magnitude];

        for(int i = 0; i < magnitude; i++)
        {
            rock[i] = RockPlane(i);
        }

        return rock;
    }

    private MeshTemplate RockPlane(int n)
    {
        bool isBase = n == 0;
        bool isTop = n == magnitude - 1;

        int minOrder = isTop ? 1 : order - (n + 1) * orderStep;
        int maxOrder = order - n * orderStep;

        float minSize = isTop ? size / 16 : size - (n + 1) * sizeStep;
        float maxSize = isTop ? size - (n + 1) * sizeStep : size - n * sizeStep;

        float minNoiseScale = sizeStep * 0.1f;
        float maxNoiseScale = sizeStep * 0.2f;

        float noisePos = isBase ? 0 : height * 0.02f;

        float minRot = 30f / magnitude;
        float maxRot = 50f / magnitude;

        float minHeight = heightStep * n;
        float maxHeight = isBase ? 0 : heightStep * (n + 1);

        return new TemplateGroup(
            new Polygon(
                Seed.Range(minOrder, maxOrder),
                Seed.Range(minSize, maxSize)
            )
                .Mod(PlaneNoise(Seed.Range(minNoiseScale, maxNoiseScale), noisePos))
                .Mod(PlaneRotation(n == 0 ? 0 : Seed.Range(minRot, maxRot)))
                .Mod(Translation.Y(Seed.Range(minHeight, maxHeight)))
            );
    }

    private MeshModification PlaneRotation(float angle)
    {
        return new ModGroup(
            Rotation.Y(Seed.Range(0, 360)),
            Rotation.Z(Seed.Range(-angle, angle)),
            Rotation.X(Seed.Range(-angle, angle)));
    }

    private MeshModification PlaneNoise(float scale, float position)
    {
        return new ModGroup(
            new NoiseScale(scale, NoiseMode.STATIC),
            new NoisePosition(position, NoiseMode.DYNAMIC)
            );
    }
}
