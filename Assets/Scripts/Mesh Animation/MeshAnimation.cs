using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAnimation
{
    public MeshModification target;
    public float speed;

    public MeshAnimation(MeshModification target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }
}
