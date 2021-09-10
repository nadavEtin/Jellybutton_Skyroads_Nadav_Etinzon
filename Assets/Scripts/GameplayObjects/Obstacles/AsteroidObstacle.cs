using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidObstacle : BaseObstacle
{
    private void Awake()
    {
        _type = PooledObjectType.Obstacle;
    }
}
