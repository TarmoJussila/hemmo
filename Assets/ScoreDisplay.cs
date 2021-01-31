﻿using UnityEngine;
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

    private void Start()
    {
        GetComponent<Text>().text = "?";
    }

    private void OnScoreChanged(int currentScore, int maxScore)
    {
        GetComponent<Text>().text = currentScore + "/" + maxScore;
    }
}