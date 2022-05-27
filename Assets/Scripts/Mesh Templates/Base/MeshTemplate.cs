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
    private class TemplateCopy : MeshTemplate
    {
        private readonly MeshTemplate template;

        public TemplateCopy(MeshTemplate template)
        {
            Isolated = template.Isolated;
            this.template = template;
            mods = new List<MeshModification>(template.Mods);
        }

        public override IEnumerable<VertexData> Generate()
        {
            return template. Generate();
        }
    }

    public bool Isolated { get; private set; }

    private ICollection<MeshModification> mods = new List<MeshModification>();
    public ICollection<MeshModification> Mods
    {
        get
        {
            if (mods is null) mods = new List<MeshModification>();
            return mods;
        }
    }

    public MeshAnimation Animation;


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

    public MeshTemplate Anim(MeshModification animation, float speed=-1)
    {
        if (Animation is null) Animation = new MeshAnimation(new ModGroup(), speed);
        Animation = new MeshAnimation(new ModGroup(Animation.target, animation), speed);
        //Animation = new MeshAnimation(animation, speed);
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
