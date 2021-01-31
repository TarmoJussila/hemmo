﻿using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public delegate void ScoreEvent(int currentScore, int maxScore);
    public static ScoreEvent OnScoreChanged;

    public int CurrentScore { get; private set; }
    public int MaxScore { get; private set; }

    private void Start()
    {
        MaxScore = FindObjectsOfType<Collectable>().Length;
    }

    public void AddScore()
    {
        CurrentScore++;
        OnScoreChanged?.Invoke(CurrentScore, MaxScore);
    }
}