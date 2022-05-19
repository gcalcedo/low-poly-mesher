using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[TemplatePath("Primitives", typeof(Plane))]
public class Plane : MeshTemplate
{
    public float sizeX;
    public float sizeZ;
    public int resolution;

    /// <summary>
    /// A plane on the XZ axis. Orthogonal to the Y axis.
    /// </summary>
    /// <param name="sizeX">Size on the X axis (Width).</param>
    /// <param name="sizeZ">Size on the Z axis (Length).</param>
    /// <param name="resolution">Number of vertices in each dimension.</param>
    public Plane(float sizeX, float sizeZ, int resolution = 2)
    {
        this.sizeX = sizeX;
        this.sizeZ = sizeZ;
        this.resolution = resolution;
    }

    override public IEnumerable<VertexData> Generate()
    {
        Vector3 startCorner = new Vector3(-sizeX / 2, 0, -sizeZ / 2);
        float stepZ = sizeZ / (resolution - 1);

        MeshTemplate[] plane = new MeshTemplate[resolution];
        for (int i = 0; i < resolution; i++)
        {
            plane[i] = new Line(startCorner, new Vector3(startCorner.x + sizeX, 0, startCorner.z), resolution);
            startCorner += new Vector3(0, 0, stepZ);
        }

        return VertexData.Build(plane);
    }
}