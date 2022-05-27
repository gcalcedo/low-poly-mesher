using System;
using UnityEngine;

/// <summary>
/// Noise for scale in 3D space.
/// </summary>
public class NoiseScale : MeshModification
{
    private readonly NoiseMode mode;
    private Vector3 noise;

    /// <summary>
    /// Noise for scale in 3D space.
    /// </summary>
    /// <param name="noise">The noise for the scale on each axis (X, Y, Z).</param>
    public NoiseScale(Vector3 noise, NoiseMode mode)
    {
        this.mode = mode;
        if (mode == NoiseMode.STATIC)
        {
            this.noise = new Vector3(
                Seed.Range(1 - noise.x, 1 + noise.x),
                Seed.Range(1 - noise.y, 1 + noise.y),
                Seed.Range(1 - noise.z, 1 + noise.z));
        }
        else
        {
            this.noise = noise;
        }
    }

    /// <summary>
    /// Noise for scale in 3D space.
    /// </summary>
    /// <param name="noise">The noise for the scale on every axis.</param>
    public NoiseScale(float noise, NoiseMode mode) : this(new Vector3(noise, noise, noise), mode) { }

    /// <summary>
    /// Noise for scale in the <b>X</b> axis.
    /// </summary>
    /// <param name="noise">The magnitude of the noise for the scale.</param>
    public static NoiseScale X(float noise, NoiseMode mode) { return new NoiseScale(new Vector3(noise, 0, 0), mode); }

    /// <summary>
    /// Noise for scale in the <b>Y</b> axis.
    /// </summary>
    /// <param name="noise">The magnitude of the noise for the scale.</param>
    public static NoiseScale Y(float noise, NoiseMode mode) { return new NoiseScale(new Vector3(0, noise, 0), mode); }

    /// <summary>
    /// Noise for scale in the <b>Z</b> axis.
    /// </summary>
    /// <param name="noise">The magnitude of the noise for the scale.</param>
    public static NoiseScale Z(float noise, NoiseMode mode) { return new NoiseScale(new Vector3(0, 0, noise), mode); }

    public override Func<Vector3, Vector3> GetModAction()
    {
        if (mode == NoiseMode.STATIC)
        {
            return new Scale(new Vector3(noise.x, noise.y, noise.z)).GetModAction();
        }
        else
        {
            return new Scale(new Vector3(
                Seed.Range(1 - noise.x, 1 + noise.x),
                Seed.Range(1 - noise.y, 1 + noise.y),
                Seed.Range(1 - noise.z, 1 + noise.z))).GetModAction();
        }
    }
}
