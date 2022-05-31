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

        public override IEnumerable<MeshPackage> Generate()
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
    /// Defines the <see cref="MeshPackage"/> of this <see cref="MeshTemplate"/>.
    /// </summary>
    public virtual IEnumerable<MeshPackage> Generate() { return new List<MeshPackage>(); }

    /// <summary>
    /// Adds a <see cref="MeshModification"/> to be applied to the generated set of <see cref="MeshPackage"/>.
    /// </summary>
    /// <param name="modification"></param>
    /// <returns>A self reference to this <see cref="MeshTemplate"/>.</returns>
    public MeshTemplate Mod(MeshModification modification)
    {
        mods.Add(modification);
        return this;
    }

    /// <summary>
    /// Adds a <see cref="MeshAnimation"/> to be applied to the generated set of <see cref="MeshPackage"/>.
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    public MeshTemplate Anim(MeshModification animation, float speed=-1)
    {
        if (Animation is null) Animation = new MeshAnimation(new ModGroup(), speed);
        Animation = new MeshAnimation(new ModGroup(Animation.target, animation), speed);
        return this;
    }

    /// <summary>
    /// <see langword="Returns"/> a copy of the current state of this <see cref="MeshTemplate"/>.
    /// </summary>
    public MeshTemplate Copy()
    {
        return new TemplateCopy(this);
    }

    /// <summary>
    /// Isolates the <b>vertices</b> of this <see cref="MeshTemplate"/>. 
    /// This will result in an independent mesh reconstruction of only these <b>vertices</b>.
    /// </summary>
    /// <returns></returns>
    public MeshTemplate Isolate()
    {
        Isolated = true;
        return this;
    }
}
