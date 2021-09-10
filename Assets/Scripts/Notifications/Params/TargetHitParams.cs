using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitParams : EventParams
{
    public Vector3 HitPosition { get; private set; }

    public TargetHitParams(Vector3 hitPosition)
    {
        HitPosition = hitPosition;
    }
}
