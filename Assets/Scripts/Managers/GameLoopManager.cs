using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : NativeSingleton<GameLoopManager>, IStart
{
    #region Variables

    private DifficultyManager _difficultyMan = new DifficultyManager();
    private ScoreManager _scoreMan = new ScoreManager();
    private bool _boostIsOn;

    #endregion

    #region Public Properties

    public DifficultyManager DifficultyManager => _difficultyMan;
    public ScoreManager ScoreManager => _scoreMan;

    #endregion

    #region Methods

    public void BoostClicked()
    {
        //launch boost clicked event
        _boostIsOn = !_boostIsOn;
        EventBus.Instance.Publish(GameplayEventType.BoostClicked, new BoostParams(_boostIsOn));
    }

    public void Initialize()
    {
        InfrastructureServices.UnityCoreService.RegisterToStart(Instance);
        InfrastructureServices.UnityCoreService.RegisterToStart(_scoreMan);
    }

    public void Start()
    {
        GameplayElements.Instance.CreateLevelRoad();
        GameplayElements.Instance.CreateObstacles();
        _boostIsOn = false;
    }

    public void StartGame()
    {
        EventBus.Instance.Publish(GameplayEventType.StartGame, BaseEventParams.Empty);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion
}