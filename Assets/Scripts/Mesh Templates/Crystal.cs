using UnityEngine;

[System.Serializable]
[TemplatePath("Nature", typeof(Crystal))]
public class Crystal : MeshTemplate
{
    public Polygon basis;
    public float height;

    public Crystal(Polygon basis, float height)
    {
        this.basis = basis;
        this.height = height;
    }

    public override PointSet Generate()
    {
        return PointSet.Build(
            basis.Copy()
                .Mod(new Scale(new Vector3(0.7f, 0, 0.7f))),
            basis.Copy()
                .Mod(new Scale(new Vector3(0.8f, 0, 0.8f)))
                .Mod(new NoisePosition(height * 0.05f))
                .Mod(new Translation(new Vector3(0, height * 0.3f, 0))),
            basis.Copy()
                .Mod(new Scale(new Vector3(0.9f, 0, 0.9f)))
                .Mod(new NoisePosition(height * 0.05f))
                .Mod(new Translation(new Vector3(0, height * 0.6f, 0))),
            new Pyramid(basis, height * 0.2f)
                .Mod(new NoisePosition(height * 0.05f))
                .Mod(new Translation(new Vector3(0, height * 0.8f, 0)))
            );
    }
}
