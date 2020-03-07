using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class RoundTimer : MonoBehaviour
{
    public float timerTime = 60;
    private TextMeshProUGUI _timerDisplay;
    private float _reducedTime;
    private Image _fillBar;
    private RoundScoring _roundScoring;
    private RoundTextDisplay _roundDisplay;
    private GridController _grid;

    public event Action RoundStart = () => { };

    public bool doOnce;

    private void Awake()
    {
        _roundScoring = GameObject.FindGameObjectWithTag("roundScorer").GetComponent<RoundScoring>();
        _timerDisplay = this.transform.Find("TimerText").GetComponent<TextMeshProUGUI>();
        _fillBar = this.transform.Find("TimerFill").GetComponent<Image>();
        _roundDisplay = GameObject.FindGameObjectWithTag("RoundText").GetComponent<RoundTextDisplay>();
        _reducedTime = timerTime;
        _grid = FindObjectOfType<GridController>();
    }

    private void Start()
    {
        GameManager.current.runUpdate += TimerCheck;
        RoundManager.current.runStartGameFunct += TimerStart;
        TimerStart();
    }

    public void TimerStart()
    {
        if (_roundScoring.roundNum <= 2 && _roundScoring.roundNum != 0)
        {
            //show round number display
            _roundDisplay.DisplayRoundNumber();
        }
        if (_roundScoring.roundNum > 2)
        {
            _roundDisplay.DisplayRoundNumber();
            _grid.OnRoundChange();
        }
        StartCoroutine(StartTurnTimer());
    }
    public void TimerCheck()
    {
        if (_reducedTime <= 0)
        {
            _reducedTime = timerTime;
            _roundScoring.CompareScores();
            if (_roundScoring.roundNum < 3)
            {
                _roundDisplay.DisplayRoundEnd();
                StopCoroutine(StartTurnTimer());

                _grid.OnRoundChange();
                TimerStart();
            }
            else if (_roundScoring.roundNum == 2)
            {
                //compare scores w each other
                if (!doOnce)
                {
                    //count the amount of wins once rounds are over
                    _roundScoring.CountWins();
                    doOnce = true;
                }
                //stop timer completely
                StopCoroutine(StartTurnTimer());
            }
            else if (_roundScoring.roundNum > 2)
            {
                _roundDisplay.DisplayRoundEnd();
                if (!doOnce)
                {
                    //count the amount of wins once rounds are over
                    _roundScoring.CountWins();
                    doOnce = true;
                }
                StopCoroutine(StartTurnTimer());
            }
        }
    }
    public void TimeDisplay(int time)
    {
        TimeSpan cooldown = TimeSpan.FromSeconds(time);
        _timerDisplay.text = cooldown.ToString(@"mm\:ss");

        //with visual
        float fillAmount = _reducedTime / timerTime;
        _fillBar.fillAmount = fillAmount;
    }

    IEnumerator StartTurnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            TimeDisplay((int)_reducedTime);
            --_reducedTime;
        }
    }

    private void OnDestroy()
    {
        GameManager.current.runUpdate -= TimerCheck;
        RoundManager.current.runStartGameFunct -= TimerStart;
    }
}
