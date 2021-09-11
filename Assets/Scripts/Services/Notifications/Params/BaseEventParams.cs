using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEventParams
{
    private static EmptyParams _empty = new EmptyParams();

    public static EmptyParams Empty => _empty;
}