using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffIncreaseEventParams : BaseEventParams
{
    private int _currentDifficultyStage;

    public int CurrentDifficultyStage => _currentDifficultyStage;

    public DiffIncreaseEventParams(int curStage)
    {
        _currentDifficultyStage = curStage;
    }
}
