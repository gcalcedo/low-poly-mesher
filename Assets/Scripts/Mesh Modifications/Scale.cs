using System;
using UnityEngine;

public class Scale : MeshModification
{
    private Vector3 scale;

    /// <summary>
    /// A scale in 3D space.
    /// </summary>
    /// <param name="scale">The amount to scale on each axis (X, Y, Z).</param>
    public Scale(Vector3 scale)
    {
        this.scale = scale;
    }

    public override Func<Vector3, Vector3> GetModAction()
    {
        return v => new Vector3(v.x * scale.x, v.y * scale.y, v.z * scale.z);
    }
}
