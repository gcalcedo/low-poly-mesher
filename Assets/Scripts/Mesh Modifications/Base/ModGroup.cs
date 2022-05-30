using System;
using UnityEngine;

/// <summary>
/// Groups any number of <see cref="MeshModification"/> into a single one.
/// </summary>
public class ModGroup : MeshModification
{
    private readonly MeshModification[] group;

    public ModGroup(params MeshModification[] group)
    {
        this.group = group;
    }

    public override Func<Vector3, Vector3> GetModAction()
    {
        return v =>
        {
            foreach (MeshModification mod in group)
            {
                v = mod.GetModAction().Invoke(v);
            }

            return v;
        };
    }
}
