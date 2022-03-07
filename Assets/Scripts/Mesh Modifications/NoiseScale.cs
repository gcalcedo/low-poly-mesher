using System;
using UnityEngine;

public class NoiseScale : MeshModification
{
    private readonly float strengthX;
    private readonly float strengthY;
    private readonly float strengthZ;

    /// <summary>
    /// Applies <paramref name="strength"/> amount of noise to the X, Y and Z scale.
    /// </summary>
    /// <param name="strength">Strength of noise on every axis.</param>
    public NoiseScale(float strength)
    {
        strengthX = strength;
        strengthY = strength;
        strengthZ = strength;
    }

    /// <summary>
    /// Applies <paramref name="strengthX"/>, <paramref name="strengthY"/> and <paramref name="strengthZ"/> 
    /// amount of noise to the X, Y and Z scale respectively.<br></br>
    /// </summary>
    /// <param name="strengthX">Strength of noise on the X axis.</param>
    /// <param name="strengthY">Strength of noise on the Y axis.</param>
    /// <param name="strengthZ">Strength of noise on the Z axis.</param>
    public NoiseScale(float strengthX, float strengthY, float strengthZ)
    {
        this.strengthX = strengthX;
        this.strengthY = strengthY;
        this.strengthZ = strengthZ;
    }

    public override Func<Vector3, Vector3> GetModAction()
    {
        return p => new Vector3(
            p.x * UnityEngine.Random.Range(1 - strengthX, 1 + strengthX),
            p.y * UnityEngine.Random.Range(1 - strengthY, 1 + strengthY),
            p.z * UnityEngine.Random.Range(1 - strengthZ, 1 + strengthZ)
            );
    }
}
