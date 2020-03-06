using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundScoring : MonoBehaviour
{
    private GameScoring _manScore;
    private GameScoring _natureScore;

    private int _manWins;
    private int _natureWins;

    [Header("Winner Panel")]
    public GameObject winnerPanel;
    public TextMeshProUGUI winnerName;
    public TextMeshProUGUI winnerScore;

    private void Awake()
    {
        _manScore = GameObject.FindGameObjectWithTag("ManScore").GetComponent<GameScoring>();
        _natureScore = GameObject.FindGameObjectWithTag("NatureScore").GetComponent<GameScoring>();
    }

    public void CompareScores()
    {
        if (_manScore.ReturnTotalScore() > _natureScore.ReturnTotalScore())
        {
            _manWins++;
        }
        else if (_manScore.ReturnTotalScore() == _natureScore.ReturnTotalScore())
        {
            //STALEMATE, do not add wins
        }
        else
        {
            _natureWins++;
        }
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
        winnerScore.text = "Total score: " + _manScore.AddTotalGameScore().ToString();
        winnerPanel.SetActive(true);
    }

    public void DisplayNatureWin()
    {
        winnerName.text = "Nature Wins!";
        winnerScore.text = "Total score: " + _natureScore.AddTotalGameScore().ToString();
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
}

