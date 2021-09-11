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
    //private int _currentDifficultyStage;

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
        EventBus.Instance.Unsubscribe(GameplayEventType.StartGame, GameStart);
        EventBus.Instance.Unsubscribe(GameplayEventType.GameOver, GameOver);
    }

    #endregion

    #region Methods

    private void SubscribeToEvents()
    {
        EventBus.Instance.Subscribe(GameplayEventType.StartGame, GameStart);
        EventBus.Instance.Subscribe(GameplayEventType.GameOver, GameOver);
    }

    private void GameStart(BaseEventParams par)
    {
        StartCoroutine(CountGameTime());
        DifficultyPeriodStart();
        GameTimerStart();
    }

    private void DifficultyPeriodStart()
    {
        InfrastructureServices.AwaitService.WaitFor(_difficultyIncreasePeriod).OnEnd(DifficultyPeriodEnd);
    }

    private void DifficultyPeriodEnd()
    {
        EventBus.Instance.Publish(GameplayEventType.IncreaseDifficulty, BaseEventParams.Empty);
        if (_gameActive)
            DifficultyPeriodStart();
    }

    private void GameTimerStart()
    {
        _gameActive = true;
        StartCoroutine(CountGameTime());
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

    private void GameOver(BaseEventParams par)
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