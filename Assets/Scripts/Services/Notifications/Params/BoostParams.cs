using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostParams : BaseEventParams
{
    public bool BoostIsOn { get; private set; }

    public BoostParams(bool boostOn)
    {
        BoostIsOn = boostOn;
    }
}
