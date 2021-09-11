using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/ScoreConfig", fileName = "ScoreConfig")]
public class ScoreConfig : ScriptableObject
{
    [SerializeField] private int _scorePerSecond = 1, _boostedScorePerSec = 2, _obstaclePassedScore = 5;

    public int ScorePerSecond => _scorePerSecond;
    public int BoostedScorePerSecond => _boostedScorePerSec;
    public int ObstaclePassedScore => _obstaclePassedScore;
}
