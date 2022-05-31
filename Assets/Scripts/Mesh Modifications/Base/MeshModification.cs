using System;
using UnityEngine;

/// <summary>
/// Base class for every per-vertex <see cref="MeshPackage"/> modification.
/// </summary>
public abstract class MeshModification
{
    /// <summary>
    /// Defines the modification to be applied to each <b>Vertex</b> (<see cref="Vector3"/>) of a <see cref="MeshPackage"/>.
    /// </summary>
    public abstract Func<Vector3, Vector3> GetModAction();
}

/// <summary>
/// Type of noise for any random <see cref="MeshModification"/>.
/// </summary>
public enum NoiseMode
{
    /// <summary>
    /// Applies the <b>SAME</b> noise to every <b>vertex</b> of the <see cref="MeshTemplate"/>.
    /// </summary>
    STATIC,
    /// <summary>
    /// Applies a <b>DIFFERENT</b> noise to every <b>vertex</b> of the <see cref="MeshTemplate"/>.
    /// </summary>
    DYNAMIC
}
