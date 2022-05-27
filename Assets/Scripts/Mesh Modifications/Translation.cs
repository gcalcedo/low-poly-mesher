using System;
using UnityEngine;

/// <summary>
/// A translation in 3D space.
/// </summary>
public class Translation : MeshModification
{
    private Vector3 translation;

    /// <summary>
    /// A translation in 3D space.
    /// </summary>
    /// <param name="translation">The translation to be performed.</param>
    public Translation(Vector3 translation)
    {
        this.translation = translation;
    }

    /// <summary>
    /// A translation in the <b>X</b> axis.
    /// </summary>
    /// <param name="x">Magnitude of the translation.</param>
    public static Translation X(float x) { return new Translation(new Vector3(x, 0, 0)); }

    /// <summary>
    /// A translation in the <b>Y</b> axis.
    /// </summary>
    /// <param name="y">Magnitude of the translation.</param>
    public static Translation Y(float y) { return new Translation(new Vector3(0, y, 0)); }

    /// <summary>
    /// A translation in the <b>Z</b> axis.
    /// </summary>
    /// <param name="z">Magnitude of the translation.</param>
    public static Translation Z(float z) { return new Translation(new Vector3(0, 0, z)); }

    public override Func<Vector3, Vector3> GetModAction()
    {
        return (p) => p + translation;
    }
}
