using System.Collections.Generic;

public class TemplateGroup : MeshTemplate
{
    private readonly MeshTemplate[] group;

    /// <summary>
    /// Groups any number of <see cref="MeshTemplate"/> into a single one.
    /// </summary>
    /// <param name="group">The collection of <see cref="MeshTemplate"/> to be grouped.</param>
    public TemplateGroup(params MeshTemplate[] group)
    {
        this.group = group;
    }

    public override IEnumerable<MeshPackage> Generate()
    {
        return MeshPackage.Build(group);
    }
}
