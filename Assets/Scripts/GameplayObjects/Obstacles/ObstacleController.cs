using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    #region Editor

    [Range(0, 100)]
    [SerializeField] private float _obstacleProbability = 15;

    #endregion

    #region Private Variables

    private List<BaseObstacle> _activeObstacleObj = new List<BaseObstacle>();

    #endregion

    #region Methods

    public void CreateLevelObstacles()
    {
        for (int i = 0; i < GameplayElements.Instance.RoadLength; i++)
        {
            var rnd = Random.Range(0, 100);
            if(rnd <= _obstacleProbability)
            {
                var obs = AddNewObstacle();
                GameplayElements.Instance.AddObstacleToRoad(obs, i);
            }
        }
    }

    private GameObject AddNewObstacle()
    {
        var newObs = GameplayElements.Instance.GetGameplayObject(PooledObjectType.Obstacle);
        newObs.SetActive(true);
        _activeObstacleObj.Add(newObs.GetComponent<BaseObstacle>());
        return newObs;
    }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        
    }

    #endregion

    #region

    #endregion
}
