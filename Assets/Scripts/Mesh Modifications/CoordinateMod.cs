using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateMod : MeshModification
{
    public Vector3 action;
    public float limit;

    public CoordinateMod(float limit)
    {
        this.action = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
        this.limit = limit;
    }

    public override Func<Vector3, Vector3> GetModAction()
    {     
        return p => p + EasingFunction.EaseInQuad(0, 1, p.y.Remap(0, limit, 0, 1)) * action;
    }
}

public static class FloatExtension
{
    public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}
