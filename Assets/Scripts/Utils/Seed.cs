using System;
using UnityEngine;

/// <summary>
/// Random generator for every <see cref="MeshTemplate"/> and <see cref="MeshModification"/>.
/// </summary>
public static class Seed
{
    private static System.Random generator = new System.Random(Guid.NewGuid().GetHashCode());

    /// <summary>
    /// Initializes the generator with the given <paramref name="seed"/>.
    /// </summary>
    /// <param name="seed">The seed to be used.</param>
    public static void Init(int seed)
    {
        generator = new System.Random(seed);
    }

    /// <summary>
    /// <see langword="Returns"/> a random <see langword="float"/> between 
    /// <b>0f</b> and <b>1f</b>.
    /// </summary>
    public static float Float() { return (float)generator.NextDouble(); }

    /// <summary>
    /// <see langword="Returns"/> a random <see langword="float"/> between 
    /// <b>0f</b> and <paramref name="max"/>.
    /// </summary>
    public static float Float(float max) { return Range(0f, max); }

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

    /// <summary>
    /// <see langword="Returns"/> a random normalized <see cref="Vector3"/> on the <b>XY</b> plane.
    /// </summary>
    /// <param name="max">Max value for the X and Y coordinate.</param>
    public static Vector3 XY() { return new Vector3(Range(-1f, 1f), Range(-1f, 1f), 0).normalized; }

    /// <summary>
    /// <see langword="Returns"/> a random normalized <see cref="Vector3"/> on the <b>XZ</b> plane.
    /// </summary>
    /// <param name="max">Max value for the X and Z coordinate.</param>
    public static Vector3 XZ() { return new Vector3(Range(-1f, 1f), 0, Range(-1f, 1f)).normalized; }

    /// <summary>
    /// <see langword="Returns"/> a random normalized <see cref="Vector3"/> on the <b>YZ</b> plane.
    /// </summary>
    /// <param name="max">Max value for the Y and Z coordinate.</param>
    public static Vector3 YZ() { return new Vector3(0, Range(-1f, 1f), Range(-1f, 1f)).normalized; }
}
