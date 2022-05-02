using System;
using UnityEngine;

/// <summary>
/// Base class for every per-point <see cref="VertexData"/> modification.
/// </summary>
public abstract class MeshModification
{
    /// <summary>
    /// Defines the modification to be applied to each <see cref="Vector3"/> of a <see cref="VertexData"/>.
    /// </summary>
    public abstract Func<Vector3, Vector3> GetModAction();
}
