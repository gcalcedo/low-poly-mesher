using System;

/// <summary>
/// Defines the path of this <see cref="MeshTemplate"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TemplatePathAttribute : Attribute
{
    public string Path { get; private set; }

    public TemplatePathAttribute(string path, Type template)
    {
        Path = path + "/" + template.Name;
    }
}
