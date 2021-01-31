using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private void OnEnable()
    {
        ScoreKeeper.OnScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        ScoreKeeper.OnScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int currentScore)
    {
        GetComponent<Text>().text = currentScore.ToString();
    }
}