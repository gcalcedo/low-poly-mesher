using System;
using UnityEngine;

/// <summary>
/// Noise for position in 3D space.
/// </summary>
public class NoisePosition : MeshModification
{
    private readonly NoiseMode mode;
    private Vector3 noise;

    /// <summary>
    /// Noise for position in 3D space.
    /// </summary>
    /// <param name="noise">The noise for the position on each axis (X, Y, Z).</param>
    public NoisePosition(Vector3 noise, NoiseMode mode)
    {
        this.mode = mode;
        if (mode == NoiseMode.STATIC)
        {
            this.noise = new Vector3(
                Seed.Range(-noise.x, noise.x),
                Seed.Range(-noise.y, noise.y),
                Seed.Range(-noise.z, noise.z));
        }
        else
        {
            this.noise = noise;
        }
    }

    /// <summary>
    /// Noise for position in 3D space.
    /// </summary>
    /// <param name="noise">The noise for the position on every axis.</param>
    public NoisePosition(float noise, NoiseMode mode) : this(new Vector3(noise, noise, noise), mode) { }

    /// <summary>
    /// Noise for position in the <b>X</b> axis.
    /// </summary>
    /// <param name="noise">The magnitude of the noise for the position.</param>
    public static NoisePosition X(float noise, NoiseMode mode) { return new NoisePosition(new Vector3(noise, 0, 0), mode); }

    /// <summary>
    /// Noise for position in the <b>Y</b> axis.
    /// </summary>
    /// <param name="noise">The magnitude of the noise for the position.</param>
    public static NoisePosition Y(float noise, NoiseMode mode) { return new NoisePosition(new Vector3(0, noise, 0), mode); }

    /// <summary>
    /// Noise for position in the <b>Z</b> axis.
    /// </summary>
    /// <param name="noise">The magnitude of the noise for the position.</param>
    public static NoisePosition Z(float noise, NoiseMode mode) { return new NoisePosition(new Vector3(0, 0, noise), mode); }

    public override Func<Vector3, Vector3> GetModAction()
    {
        if (mode == NoiseMode.STATIC)
        {
            return new Translation(new Vector3(noise.x, noise.y, noise.z)).GetModAction();
        }
        else
        {
            return new Translation(new Vector3(
                Seed.Range(-noise.x, noise.x),
                Seed.Range(-noise.y, noise.y),
                Seed.Range(-noise.z, noise.z))).GetModAction();
        }
    }
}
