using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    private TextMeshProUGUI score;
    private int startingPoints = 0;
    private int currentPoints;

    void Start()
    {
        score = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        StartScore();
    }

    void StartScore()
    {
        score.text = startingPoints.ToString();
    }

    void AddScore(int points)
    {
        currentPoints += points;
        UpdateCurrentPoints();
    }

    void UpdateCurrentPoints()
    {
        score.text += currentPoints.ToString();
    }
}
