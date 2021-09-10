using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/Obstacle Params", fileName = "Obstacle Params")]
public class ObstacleParams : ScriptableObject
{
    [SerializeField] private Object _asteroidPrefab;

    public Object AsteroidPrefab => _asteroidPrefab;
}
