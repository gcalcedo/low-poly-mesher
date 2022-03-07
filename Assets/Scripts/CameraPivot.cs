using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public float speed;
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, -360, 0), speed, RotateMode.FastBeyond360)
            .SetRelative()
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

}
