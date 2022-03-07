using System.Collections.Generic;
using UnityEngine;

public class PointSet
{
    /// <summary>
    /// Collection of vertices of this <see cref="PointSet"/>.
    /// </summary>
    public IList<Vector3> Set { get; private set; }

    /// <summary>
    /// Creates an empty <see cref="PointSet"/>.
    /// </summary>
    public PointSet()
    {
        Set = new List<Vector3>();
    }

    /// <summary>
    /// Creates a <see cref="PointSet"/> containing <paramref name="points"/>.
    /// </summary>
    /// <param name="points"></param>
    public PointSet(IEnumerable<Vector3> points)
    {
        Set = new List<Vector3>(points);
    }

    /// <summary>
    /// Creates a <see cref="PointSet"/> from any number of <see cref="MeshTemplate"/>.
    /// </summary>
    /// <param name="group">The collections of <see cref="MeshTemplate"/> to be used to create the <see cref="PointSet"/></param>
    public static PointSet Build(params MeshTemplate[] group)
    {
        PointSet pointSet = new PointSet();
        foreach(MeshTemplate ms in group)
        {
            PointSet ps = ms.Generate();
            foreach(MeshModification mod in ms.Mods)
            {
                ps.Modify(mod);
            }

            pointSet.Append(ps);
        }
        return pointSet;
    }

    /// <summary>
    /// Merges <paramref name="pointSet"/> into this <see cref="PointSet"/>.
    /// </summary>
    /// <param name="pointSet"></param>
    private void Append(PointSet pointSet)
    {
        foreach(Vector3 p in pointSet.Set)
        {
            Set.Add(p);
        }
    }

    /// <summary>
    /// Applies a <see cref="MeshModification"/> to every vertex in this <see cref="PointSet"/>.
    /// </summary>
    /// <param name="mod"></param>
    private void Modify(MeshModification mod)
    {
        for(int i = 0; i < Set.Count; i++)
        {
            Set[i] = mod.GetModAction().Invoke(Set[i]);
        }
    }
}
