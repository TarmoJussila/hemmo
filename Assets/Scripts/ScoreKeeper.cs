using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public delegate void ScoreEvent(int score);
    public static ScoreEvent OnScoreChanged;

    public int CurrentScore { get; private set; }

    public void AddScore()
    {
        CurrentScore++;
        OnScoreChanged?.Invoke(CurrentScore);
    }
}