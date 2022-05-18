using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeshAnimation
{
    public abstract Func<Vector3, Vector3> Animate(float speed);
}
