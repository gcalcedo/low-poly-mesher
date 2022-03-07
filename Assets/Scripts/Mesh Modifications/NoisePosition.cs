using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisePosition : MeshModification
{
    private readonly float strengthX;
    private readonly float strengthY;
    private readonly float strengthZ;

    /// <summary>
    /// Applies <paramref name="strength"/> amount of noise to the X, Y and Z position.
    /// </summary>
    /// <param name="strength">Strength of noise on every axis.</param>
    public NoisePosition(float strength)
    {
        strengthX = strength;
        strengthY = strength;
        strengthZ = strength;
    }

    /// <summary>
    /// Applies <paramref name="strengthX"/>, <paramref name="strengthY"/> and <paramref name="strengthZ"/> 
    /// amount of noise to the X, Y and Z position respectively.<br></br>
    /// </summary>
    /// <param name="strengthX">Strength of noise on the X axis.</param>
    /// <param name="strengthY">Strength of noise on the Y axis.</param>
    /// <param name="strengthZ">Strength of noise on the Z axis.</param>
    public NoisePosition(float strengthX, float strengthY, float strengthZ)
    {
        this.strengthX = strengthX;
        this.strengthY = strengthY;
        this.strengthZ = strengthZ;
    }

    public override Func<Vector3, Vector3> GetModAction()
    {
        return p => new Vector3(
            p.x + UnityEngine.Random.Range(-strengthX, strengthX),
            p.y + UnityEngine.Random.Range(-strengthY, strengthY),
            p.z + UnityEngine.Random.Range(-strengthZ, strengthZ)
            );
    }
}
