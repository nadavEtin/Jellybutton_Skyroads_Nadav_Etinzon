using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    #region Const Strings

    private const string GAME_START_MESSAGE = "Press any key to start playing";
    private const string NEW_HIGH_SCORE_MESSAGE = "\nCongratulations, you broke your high score!";

    #endregion

    #region Editor

    [SerializeField] private TextMeshProUGUI _titleTxt, _timeTxt, _obstacleTxt, _scoreTxt, _highScoreTxt;
    [SerializeField] private Button _restartBtn, _quitBtn;

    #endregion

    protected override UIManager ProvideInstance()
    {
        return this;
    }

    #region Unity Methods

    private void Start()
    {
        _restartBtn.gameObject.SetActive(false);
        _quitBtn.gameObject.SetActive(false);
        _titleTxt.gameObject.SetActive(true);
        EventBus.Instance.Subscribe(GameplayEventType.StartGame, StartGameUIM);
        SetTitleText(GAME_START_MESSAGE);
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(GameplayEventType.StartGame, StartGameUIM);
    }

    #endregion

    #region Methods

    public void SetTitleText(string text)
    {
        _titleTxt.text = text;
    }

    public void SetTimeText(string text)
    {
        _timeTxt.text = string.Format("Time: {0}", text);
    }

    public void SetObstacleText(string text)
    {
        _obstacleTxt.text = string.Format("Asteroids passed: {0}", text);
    }

    public void SetScoreText(string text)
    {
        _scoreTxt.text = string.Format("Score: {0}", text);
    }

    public void SetHighScoreText(string text)
    {
        _highScoreTxt.text = string.Format("High score: {0}", text);
    }

    public void RestartButtonClick()
    {
        GameLoopManager.Instance.RestartGame();
    }

    public void QuitBtnClick()
    {
        GameLoopManager.Instance.Quit();
    }

    public void GameOver(bool newHighScore)
    {
        _restartBtn.gameObject.SetActive(true);
        _quitBtn.gameObject.SetActive(true);
        _titleTxt.gameObject.SetActive(true);
        _titleTxt.text = "Game Over";
        if (newHighScore)
            _titleTxt.text += NEW_HIGH_SCORE_MESSAGE;
    }

    private void StartGameUIM(BaseEventParams par)
    {
        _titleTxt.gameObject.SetActive(false);
    }

    #endregion
}