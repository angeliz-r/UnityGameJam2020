using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundScoring : MonoBehaviour
{
    public GameScoring manScore;
    public GameScoring natureScore;

    [SerializeField]private int _manWins;
    [SerializeField]private int _natureWins;
    public int roundNum;

    [Header("Winner Panel")]
    public GameObject winnerPanel;
    public TextMeshProUGUI winnerName;
    public TextMeshProUGUI winnerScore;
    public GameObject manBG;
    public GameObject natureBG;
    public RoundTimer roundTimer;
    private void Awake()
    {
        manScore = GameObject.FindGameObjectWithTag("ManScore").GetComponent<GameScoring>();
        natureScore = GameObject.FindGameObjectWithTag("NatureScore").GetComponent<GameScoring>();
    }
    private void Start()
    {
       // RoundManager.current.runEndGameFunct += CompareScores;
    }
    public void CompareScores()
    {
        if (manScore.ReturnTotalScore() > natureScore.ReturnTotalScore())
        {
            _manWins++;
            roundNum++;

        }
        else if (manScore.ReturnTotalScore() == natureScore.ReturnTotalScore())
        {
            roundNum++;
        }
        else
        {
            _natureWins++;
            roundNum++;
        }
        manScore.SaveCurrentScore();
        natureScore.SaveCurrentScore();
    }

    public void CountWins()
    {
        CompareScores();
        int computeManWinsDiff = _manWins - _natureWins;
        int computeNatureWinsDiff = _natureWins - _manWins;
        if (computeManWinsDiff > computeNatureWinsDiff)
        {
            DisplayManWin();
        }
        else if (computeNatureWinsDiff > computeManWinsDiff)
        {
            DisplayNatureWin();
        }
        else
        {
            //if no one has >2 wins, restart
            roundNum++;
            roundTimer.TimerStart();
            roundTimer.doOnce = false;
        }
    }

    public void DisplayManWin()
    {
        natureBG.SetActive(true);
        manBG.SetActive(false);
        winnerName.text = "Man Wins!";
        winnerScore.text = "TOTAL SCORE: " + manScore.AddTotalGameScore().ToString();
        winnerPanel.SetActive(true);
        StopAllCoroutines();
    }

    public void DisplayNatureWin()
    {
        natureBG.SetActive(false);
        manBG.SetActive(true);
        winnerName.text = "Nature Wins!";
        winnerScore.text = "TOTAL SCORE: " + natureScore.AddTotalGameScore().ToString();
        winnerPanel.SetActive(true);
        StopAllCoroutines();
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
       // RoundManager.current.runEndGameFunct -= CompareScores;
    }
}

