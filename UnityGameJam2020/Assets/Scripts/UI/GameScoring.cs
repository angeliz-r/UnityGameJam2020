﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameScoring : MonoBehaviour
{
    public float plantUnitScore;
    public TextMeshProUGUI scoreDisplay;
    private RoundScoring _roundScoring;

    private float _totalScore;
    private float _totalGameScore;

    private float _finalTotalScore;
    private int index = 0;
    void Awake()
    {
        _roundScoring = GameObject.FindGameObjectWithTag("roundScorer").GetComponent<RoundScoring>();
    }
    private void Start()
    {
        GameManager.current.runUpdate += UpdateScoreDisplay;
    }
    public void SaveCurrentScore()
    {       
        //save in an array to add them all together later
        index = _roundScoring.roundNum;
        _finalTotalScore += _totalScore;
    }
    public void UpdateScoreDisplay()
    {
        scoreDisplay.text = _totalScore.ToString();

    }
    public void AddLiveScore()
    {
        _totalScore = _totalScore + plantUnitScore;
    }

    public void DestroyLiveScore()
    {
        _totalScore = _totalScore - plantUnitScore;
    }

    public float AddTotalGameScore()
    {
        return _finalTotalScore;
    }

    public float ReturnTotalScore()
    {
        return _totalScore;
    }

    private void OnDestroy()
    {
        GameManager.current.runUpdate -= UpdateScoreDisplay;
    }
}
