using System;
using UnityEngine;

/// <summary>
/// A scale in 3D space.
/// </summary>
public class Scale : MeshModification
{
    private Vector3 scale;

    /// <summary>
    /// A scale in 3D space.
    /// </summary>
    /// <param name="scale">The magnitude of the scale on each axis (X, Y, Z).</param>
    public Scale(Vector3 scale)
    {
        this.scale = scale;
    }

    /// <summary>
    /// A scale in 3D space.
    /// </summary>
    /// <param name="scale">The magnitude of the scale on every axis.</param>
    public Scale(float scale) : this(new Vector3(scale, scale, scale)) { }

    /// <summary>
    /// A scale in the <b>X</b> axis.
    /// </summary>
    /// <param name="x">Magnitude of the scale.</param>
    public static Scale X(float x) { return new Scale(new Vector3(x, 1, 1)); }

    /// <summary>
    /// A scale in the <b>Y</b> axis.
    /// </summary>
    /// <param name="y">Magnitude of the scale.</param>
    public static Scale Y(float y) { return new Scale(new Vector3(1, y, 1)); }

    /// <summary>
    /// A scale in the <b>Z</b> axis.
    /// </summary>
    /// <param name="z">Magnitude of the scale.</param>
    public static Scale Z(float z) { return new Scale(new Vector3(1, 1, z)); }

    public override Func<Vector3, Vector3> GetModAction()
    {
        return v => new Vector3(v.x * scale.x, v.y * scale.y, v.z * scale.z);
    }
}
