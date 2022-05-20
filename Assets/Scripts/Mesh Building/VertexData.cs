using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VertexData
{
    public MeshAnimation animation;

    /// <summary>
    /// Collection of vertices of this <see cref="VertexData"/>.
    /// </summary>
    public IList<Vector3> Set { get; private set; }

    /// <summary>
    /// Creates an empty <see cref="VertexData"/>.
    /// </summary>
    public VertexData()
    {
        Set = new List<Vector3>();
    }

    /// <summary>
    /// Creates a <see cref="VertexData"/> containing <paramref name="points"/>.
    /// </summary>
    /// <param name="points"></param>
    public VertexData(IEnumerable<Vector3> points)
    {
        Set = new List<Vector3>(points);
    }

    /// <summary>
    /// Creates a <see cref="VertexData"/> from any number of <see cref="MeshTemplate"/>.
    /// </summary>
    /// <param name="group">The collections of <see cref="MeshTemplate"/> to be used to create the <see cref="VertexData"/></param>
    public static IEnumerable<VertexData> Build(params MeshTemplate[] group)
    {
        List<VertexData> build = new List<VertexData> { new VertexData() };
        foreach (MeshTemplate ms in group)
        {
            IEnumerable<VertexData> vds = ms.Generate();
            for (int i = 0; i < vds.Count(); i++)
            {
                if (!(ms.Animation is null)) { vds.ElementAt(i).animation = ms.Animation; }

                foreach (MeshModification mod in ms.Mods)
                {
                    vds.ElementAt(i).Modify(mod);
                }

                if (i == 0 && !ms.Isolated)
                {
                    build[0].animation = vds.ElementAt(i).animation;
                    build[0].Append(vds.ElementAt(i));
                }
                else
                {
                    build.Add(vds.ElementAt(i));
                }
            }
        }

        return build;
    }

    /// <summary>
    /// Merges <paramref name="pointSet"/> into this <see cref="VertexData"/>.
    /// </summary>
    /// <param name="pointSet"></param>
    private void Append(VertexData pointSet)
    {
        foreach (Vector3 p in pointSet.Set)
        {
            Set.Add(p);
        }
    }

    /// <summary>
    /// Applies a <see cref="MeshModification"/> to every vertex in this <see cref="VertexData"/>.
    /// </summary>
    /// <param name="mod"></param>
    private void Modify(MeshModification mod)
    {
        for (int i = 0; i < Set.Count; i++)
        {
            Set[i] = mod.GetModAction().Invoke(Set[i]);
        }
    }
}
