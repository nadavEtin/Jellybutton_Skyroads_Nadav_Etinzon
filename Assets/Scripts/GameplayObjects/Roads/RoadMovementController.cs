using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovementController : MonoBehaviour
{
    #region Editor

    [SerializeField] private float _moveSpeed = 4;
    [SerializeField] private float _speedUpOnDiffIncrease = 1;
    [SerializeField] private float _boostSpeedMultiplier = 2;
    [SerializeField] private RoadManager _roadManager;

    #endregion

    #region Private Variables

    private List<BaseRoad> _activeRoadObjects;
    private Vector3 _moveDir = Vector3.back;
    private bool _shouldMove;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        EventBus.Instance.Subscribe(GameplayEventType.BoostClicked, BoostClickedRMC);
        EventBus.Instance.Subscribe(GameplayEventType.StartGame, StartGameRMC);
        EventBus.Instance.Subscribe(GameplayEventType.IncreaseDifficulty, DifficultyIncreaseRMC);
        EventBus.Instance.Subscribe(GameplayEventType.GameOver, GameOverRMC);
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(GameplayEventType.BoostClicked, BoostClickedRMC);
        EventBus.Instance.Unsubscribe(GameplayEventType.StartGame, StartGameRMC);
        EventBus.Instance.Unsubscribe(GameplayEventType.IncreaseDifficulty, DifficultyIncreaseRMC);
        EventBus.Instance.Unsubscribe(GameplayEventType.GameOver, GameOverRMC);
    }

    private void FixedUpdate()
    {
        if (_shouldMove)
            MoveRoad();
    }

    #endregion

    #region Methods

    public void InitVariables(ref List<BaseRoad> activeRoads)
    {
        _activeRoadObjects = activeRoads;
    }

    private void MoveRoad()
    {
        //move all active road pieces
        for (int i = 0; i < _activeRoadObjects.Count; i++)
        {
            _activeRoadObjects[i].transform.localPosition += _moveDir * _moveSpeed * Time.fixedDeltaTime;
        }
    }

    private void BoostClickedRMC(BaseEventParams par)
    {
        var boostParam = (BoostParams)par;
        if (boostParam.BoostIsOn)
        {
            //increase speed while boost is on
            _moveSpeed *= _boostSpeedMultiplier;
            _speedUpOnDiffIncrease *= _boostSpeedMultiplier;
        }
        else
        {
            //go back to normal speed
            _moveSpeed /= _boostSpeedMultiplier;
            _speedUpOnDiffIncrease /= _boostSpeedMultiplier;
        }
    }

    private void GameOverRMC(BaseEventParams par)
    {
        //stop road movement when game is over
        _shouldMove = false;
    }

    private void StartGameRMC(BaseEventParams par)
    {
        _shouldMove = true;
    }

    private void DifficultyIncreaseRMC(BaseEventParams par)
    {
        //increase movement speed every time difficulty ticks up
        _moveSpeed += _speedUpOnDiffIncrease;
    }

    #endregion
}