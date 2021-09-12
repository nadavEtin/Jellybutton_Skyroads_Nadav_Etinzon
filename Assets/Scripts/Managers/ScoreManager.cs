using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : IStart
{
    #region Const Strings

    private const string SCORE_CONFIG_RESOURCE_NAME = "ScriptableObjects/ScoreConfig";
    private const string HIGH_SCORE_DATA_NAME = "HighScore";

    #endregion

    #region Private Variables

    private ScoreConfig _scoreConfig;
    private int _currentScore, _obstaclesPassed, _highScore;
    private bool _boostIsActive, _newHighScore;

    #endregion

    #region Methods

    //resets vars after player restarts the game
    private void ResetVariables()
    {
        _currentScore = 0;
        _obstaclesPassed = 0;
        _newHighScore = false;
        if (_highScore == 0)
            _highScore = PlayerPrefsService.Instance.LoadIntPrimitive(HIGH_SCORE_DATA_NAME);
        UIManager.Instance.SetHighScoreText(_highScore.ToString());
    }

    private void BoostClickedSM(BaseEventParams par)
    {
        var p = (BoostParams)par;
        _boostIsActive = p.BoostIsOn;
    }

    public void Start()
    {
        ResetVariables();
    }

    public ScoreManager()
    {
        _scoreConfig = Resources.Load<ScoreConfig>(SCORE_CONFIG_RESOURCE_NAME);
        EventBus.Instance.Subscribe(GameplayEventType.GameOver, GameOverSM);
        EventBus.Instance.Subscribe(GameplayEventType.BoostClicked, BoostClickedSM);
    }

    public void UpdateScore(int secondsPassed)
    {
        int scoreToAdd;
        if (_boostIsActive)
            scoreToAdd = secondsPassed * _scoreConfig.BoostedScorePerSecond;
        else
            scoreToAdd = secondsPassed * _scoreConfig.ScorePerSecond;

        _currentScore += scoreToAdd;
        UpdateScoreDisplay();

        //new high score achieved
        if (_currentScore > _highScore)
        {
            if (_newHighScore == false)
                _newHighScore = true;

            UpdateHighScore(_currentScore);
        }
    }

    //add score for passing an obstacle
    public void UpdateObstaclesPassed(int amntPassed)
    {
        _obstaclesPassed += amntPassed;
        _currentScore += amntPassed * _scoreConfig.ObstaclePassedScore;
        UIManager.Instance.SetObstacleText(_obstaclesPassed.ToString());
        UpdateScoreDisplay();
    }

    public void UpdateHighScore(int score)
    {
        _highScore = score;
        UIManager.Instance.SetHighScoreText(_highScore.ToString());
    }

    private void GameOverSM(BaseEventParams par)
    {
        if (_newHighScore)
            PlayerPrefsService.Instance.SavePrimitive(HIGH_SCORE_DATA_NAME, _highScore);
        UIManager.Instance.GameOver(_newHighScore);
    }

    private void UpdateScoreDisplay()
    {
        UIManager.Instance.SetScoreText(_currentScore.ToString());
    }

    #endregion
}