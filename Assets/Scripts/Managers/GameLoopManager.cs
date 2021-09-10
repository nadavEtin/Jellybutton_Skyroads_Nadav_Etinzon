using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    #region Editor

    

    #endregion

    #region Variables



    #endregion

    #region Properties

    

    #endregion

    #region Methods

    void Start()
    {
        GameplayElements.Instance.CreateLevelRoad();
        EventBus.Instance.Publish(GameplayEventType.StartGame, EventParams.Empty);
    }

    #endregion
}
