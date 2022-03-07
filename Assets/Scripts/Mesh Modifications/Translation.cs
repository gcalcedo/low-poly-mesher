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

    public override Func<Vector3, Vector3> GetModAction()
    {
        return (p) => p + translation;
    }
}
