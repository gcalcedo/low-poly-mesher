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
            if (!(template.Animation is null))
                Animation = new MeshAnimation(template.Animation.target, template.Animation.speed);
        }

        public override IEnumerable<MeshPackage> Generate()
        {
            return template.Generate();
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

    public Material Material;


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
    /// <returns>A self reference to this <see cref="MeshTemplate"/>.</returns>
    public MeshTemplate Anim(MeshModification animation, float speed = -1)
    {
        if (Animation is null) Animation = new MeshAnimation(new ModGroup(), speed);
        Animation = new MeshAnimation(new ModGroup(Animation.target, animation), speed);
        return this;
    }

    /// <summary>
    /// Sets the color properties for this <see cref="MeshTemplate"/>.
    /// </summary>
    /// <param name="hexColor">Diffuse component.</param>
    /// <param name="metallic">Metallic component.</param>
    /// <param name="smoothness">Smoothness component.</param>
    /// <returns>A self reference to this <see cref="MeshTemplate"/>.</returns>
    public MeshTemplate Color(string hexColor, float metallic = 0f, float smoothness = 0f, string hexEmission = "")
    {
        Material = new Material(Shader.Find("Standard"));

        Material.color = Colorizer.FromHex(hexColor);
        Material.SetFloat("_Metallic", metallic);
        Material.SetFloat("_Glossiness", smoothness);
        if (hexEmission != "")
        {
            Material.EnableKeyword("_EMISSION");
            Material.SetColor("_EmissionColor", Colorizer.FromHex(hexEmission));
        }

        if (Material.color.a < 1)
        {
            StandardShaderUtils.ChangeRenderMode(Material, StandardShaderUtils.BlendMode.Transparent);
        }
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
    /// This will result in an independent mesh reconstruction of only this <see cref="MeshTemplate"/>.
    /// </summary>
    /// <returns>A self reference to this <see cref="MeshTemplate"/>.</returns>
    public MeshTemplate Isolate()
    {
        Isolated = true;
        return this;
    }
}
