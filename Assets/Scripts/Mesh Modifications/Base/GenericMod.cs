using System;
using UnityEngine;

public class GenericMod : MeshModification
{
    private readonly Func<Vector3, Vector3> action;

    /// <summary>
    /// A generic vertex modification defined through <paramref name="action"/>.
    /// </summary>
    /// <param name="action">The action to be performed on each vertex.</param>
    public GenericMod(Func<Vector3, Vector3> action)
    {
        this.action = action;
    }

    public override Func<Vector3, Vector3> GetModAction()
    {
        return action;
    }
}