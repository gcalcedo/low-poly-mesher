using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Base class for every mesh definition.
/// </summary>
public class MeshTemplate
{
    public bool Isolated { get; private set; }

    private class TemplateCopy : MeshTemplate
    {
        private readonly MeshTemplate template;

        public TemplateCopy(MeshTemplate template)
        {
            this.template = template;
            this.mods = new List<MeshModification>(template.Mods);
        }

        public override IEnumerable<VertexData> Generate()
        {
            return template.Generate();
        }
    }

    private ICollection<MeshModification> mods = new List<MeshModification>();

    public ICollection<MeshModification> Mods
    {
        get
        {
            if (mods is null) mods = new List<MeshModification>();
            return mods;
        }
    }

    /// <summary>
    /// Defines the <see cref="VertexData"/> of this <see cref="MeshTemplate"/>.
    /// </summary>
    public virtual IEnumerable<VertexData> Generate() { return new List<VertexData>(); }

    /// <summary>
    /// Adds a <see cref="MeshModification"/> to be applied to the generated <see cref="VertexData"/>.
    /// </summary>
    /// <param name="modification"></param>
    /// <returns>A self reference to this <see cref="MeshTemplate"/>.</returns>
    public MeshTemplate Mod(MeshModification modification)
    {
        mods.Add(modification);
        return this;
    }

    public MeshTemplate Copy()
    {
        return new TemplateCopy(this);
    }

    public MeshTemplate Isolate()
    {
        Isolated = true;
        return this;
    }
}
