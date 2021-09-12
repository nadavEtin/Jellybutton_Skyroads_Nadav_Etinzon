using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    #region Editor

    [SerializeField] private int _difficultyIncreasePeriod = 15;

    #endregion Private Variables

    private int _gameTimeCount;
    private bool _gameActive;

    #region Public Properties

    public int GameTime => _gameTimeCount;

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(GameplayEventType.StartGame, StartGameTM);
        EventBus.Instance.Unsubscribe(GameplayEventType.GameOver, GameOverTM);
    }

    #endregion

    #region Methods

    private void SubscribeToEvents()
    {
        EventBus.Instance.Subscribe(GameplayEventType.StartGame, StartGameTM);
        EventBus.Instance.Subscribe(GameplayEventType.GameOver, GameOverTM);
    }

    private void StartGameTM(BaseEventParams par)
    {
        InfrastructureServices.CoroutineService.RunCoroutine(CountGameTime());
        DifficultyPeriodStart();
        GameTimerStart();
    }

    //creates awaiter for new difficulty period
    private void DifficultyPeriodStart()
    {
        InfrastructureServices.AwaitService.WaitFor(_difficultyIncreasePeriod).OnEnd(DifficultyPeriodEnd);
    }

    //called when current difficulty period ends
    private void DifficultyPeriodEnd()
    {
        EventBus.Instance.Publish(GameplayEventType.IncreaseDifficulty, BaseEventParams.Empty);

        //if the game is still active, start next difficulty period
        if (_gameActive)
            DifficultyPeriodStart();
    }

    private void GameTimerStart()
    {
        _gameActive = true;
        InfrastructureServices.CoroutineService.RunCoroutine(CountGameTime());
    }

    private IEnumerator CountGameTime()
    {
        while (_gameActive)
        {
            yield return new WaitForSeconds(1);
            _gameTimeCount++;
            GameLoopManager.Instance.ScoreManager.UpdateScore(1);
            UIManager.Instance.SetTimeText(_gameTimeCount.ToString());
        }
    }

    private void GameOverTM(BaseEventParams par)
    {
        _gameActive = false;
        StopAllCoroutines();
    }

    protected override TimeManager ProvideInstance()
    {
        return this;
    }

    #endregion
}