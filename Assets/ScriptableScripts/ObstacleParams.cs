using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/ObstacleParams", fileName = "ObstacleParams")]
public class ObstacleParams : ScriptableObject
{
    [SerializeField] private Object _asteroidPrefab;

    public Object AsteroidPrefab => _asteroidPrefab;
}