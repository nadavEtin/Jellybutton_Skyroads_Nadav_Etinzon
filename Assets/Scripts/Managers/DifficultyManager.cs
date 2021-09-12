using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager
{
    #region 

    #endregion

    #region Variables

    private int _currentDifficultyStage;

    #endregion

    #region Methods

    public DifficultyManager()
    {
        EventBus.Instance.Subscribe(GameplayEventType.IncreaseDifficulty, IncreaseDifficultyDM);
    }

    private void IncreaseDifficultyDM(BaseEventParams par)
    {
        _currentDifficultyStage++;
    }

    #endregion
}