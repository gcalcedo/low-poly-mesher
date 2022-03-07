using System;
using UnityEngine;

/// <summary>
/// A rotation around a defined axis in 3D space.
/// </summary>
public class Rotation : MeshModification
{
    private readonly float angle;
    private Vector3 axis;

    /// <summary>
    /// A rotation of <paramref name="angle"/> degrees around <paramref name="axis"/> in 3D space.
    /// </summary>
    /// <param name="angle">The amount of degrees of the rotation.</param>
    /// <param name="axis">The axis to rotate around.</param>
    public Rotation(float angle, Vector3 axis)
    {
        this.angle = angle;
        this.axis = axis;
    }

    public override Func<Vector3, Vector3> GetModAction()
    {
        return (p) => Quaternion.AngleAxis(angle, axis) * p;
    }
}
