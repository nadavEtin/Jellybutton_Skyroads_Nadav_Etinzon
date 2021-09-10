using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : IStart
{
    #region Editor

    

    #endregion

    #region Variables



    #endregion

    #region Properties

    

    #endregion

    #region Methods

    public void Start()
    {
        GameplayElements.Instance.CreateLevelRoad();
        GameplayElements.Instance.CreateObstacles();
        EventBus.Instance.Publish(GameplayEventType.StartGame, BaseEventParams.Empty);
    }

    #endregion
}
