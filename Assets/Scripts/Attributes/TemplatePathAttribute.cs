using System;

/// <summary>
/// Defines the in-editor path of this <see cref="MeshTemplate"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TemplatePathAttribute : Attribute
{
    /// <summary>
    /// In-editor path of this <see cref="MeshTemplate"/>.
    /// </summary>
    public string Path { get; private set; }

    public TemplatePathAttribute(string path, Type template)
    {
        Path = path + "/" + template.Name;
    }
}
