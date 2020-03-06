﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundScoring : MonoBehaviour
{
    private GameScoring _manScore;
    private GameScoring _natureScore;

    private int _manWins;
    private int _natureWins;
    public int roundNum;

    [Header("Winner Panel")]
    public GameObject winnerPanel;
    public TextMeshProUGUI winnerName;
    public TextMeshProUGUI winnerScore;

    private void Awake()
    {
        _manScore = GameObject.FindGameObjectWithTag("ManScore").GetComponent<GameScoring>();
        _natureScore = GameObject.FindGameObjectWithTag("NatureScore").GetComponent<GameScoring>();
    }
    private void Start()
    {
        GameManager.current.runEndGameFunct += CompareScores;
    }
    public void CompareScores()
    {
        if (_manScore.ReturnTotalScore() > _natureScore.ReturnTotalScore())
        {
            _manWins++;
            roundNum++;

        }
        else if (_manScore.ReturnTotalScore() == _natureScore.ReturnTotalScore())
        {
            //STALEMATE, do not add wins
        }
        else
        {
            _natureWins++;
            roundNum++;
        }
        CountWins();
    }

    public void CountWins()
    {
        if (_manWins >= 2)
        {
            DisplayManWin();
        }
        else if (_natureWins >= 2)
        {
            DisplayNatureWin();
        }
    }

    public void DisplayManWin()
    {
        winnerName.text = "Man Wins!";
        winnerScore.text = "TOTAL SCORE: " + _manScore.AddTotalGameScore().ToString();
        winnerPanel.SetActive(true);
    }

    public void DisplayNatureWin()
    {
        winnerName.text = "Nature Wins!";
        winnerScore.text = "TOTAL SCORE: " + _natureScore.AddTotalGameScore().ToString();
        winnerPanel.SetActive(true);
    }

    public int ReturnManWins()
    {
        return _manWins;
    }

    public int ReturnNatureWins()
    {
        return _natureWins;
    }

    private void OnDestroy()
    {
        GameManager.current.runEndGameFunct -= CompareScores;
    }
}

