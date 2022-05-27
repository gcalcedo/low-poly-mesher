using System;

/// <summary>
/// Random generator for every <see cref="MeshTemplate"/> and <see cref="MeshModification"/>.
/// </summary>
public static class Seed
{
    private static Random generator = new Random(Guid.NewGuid().GetHashCode());

    /// <summary>
    /// Initializes the generator with the given <paramref name="seed"/>.
    /// </summary>
    /// <param name="seed">The seed to be used.</param>
    public static void Init(int seed)
    {
        generator = new Random(seed);
    }

    /// <summary>
    /// <see langword="Returns"/> a random <see langword="float"/> between 
    /// <b>0f</b> and <b>1f</b>.
    /// </summary>
    public static float Float() { return (float)generator.NextDouble(); }

    /// <summary>
    /// <see langword="Returns"/> a random <see langword="int"/> between 
    /// <b>min</b> and <b>max</b>. Max is not included.
    /// </summary>
    public static int Range(int min, int max) { return generator.Next(min, max); }

    /// <summary>
    /// <see langword="Returns"/> a random <see langword="float"/> between 
    /// <b>min</b> and <b>max</b>. Max is not included.
    /// </summary>
    public static float Range(float min, float max) { return (float) generator.NextDouble() * (max - min) + min; }
}
