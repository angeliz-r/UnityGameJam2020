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
    public int roundNum;

    [Header("Winner Panel")]
    public GameObject winnerPanel;
    public TextMeshProUGUI winnerName;
    public TextMeshProUGUI winnerScore;
    public GameObject manBG;
    public GameObject natureBG;

    private void Awake()
    {
        _manScore = GameObject.FindGameObjectWithTag("ManScore").GetComponent<GameScoring>();
        _natureScore = GameObject.FindGameObjectWithTag("NatureScore").GetComponent<GameScoring>();
    }
    private void Start()
    {
        RoundManager.current.runEndGameFunct += CompareScores;
    }
    public void CompareScores()
    {
        if (_manScore.ReturnTotalScore() > _natureScore.ReturnTotalScore())
        {
            _manScore.SaveCurrentScore();
            _manWins++;
            roundNum++;

        }
        else if (_manScore.ReturnTotalScore() == _natureScore.ReturnTotalScore())
        {
            //STALEMATE, do not add wins
        }
        else
        {
            _natureScore.SaveCurrentScore();
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
        natureBG.SetActive(true);
        manBG.SetActive(false);
        winnerName.text = "Man Wins!";
        winnerScore.text = "TOTAL SCORE: " + _manScore.AddTotalGameScore().ToString();
        winnerPanel.SetActive(true);
    }

    public void DisplayNatureWin()
    {
        natureBG.SetActive(false);
        manBG.SetActive(true);
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
        RoundManager.current.runEndGameFunct -= CompareScores;
    }
}

