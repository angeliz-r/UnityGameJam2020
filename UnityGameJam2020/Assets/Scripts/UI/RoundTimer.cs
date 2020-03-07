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

    public event Action RoundStart = () => { };

    private void Awake()
    {
        _roundScoring = GameObject.FindGameObjectWithTag("roundScorer").GetComponent<RoundScoring>();
        _timerDisplay = this.transform.Find("TimerText").GetComponent<TextMeshProUGUI>();
        _fillBar = this.transform.Find("TimerFill").GetComponent<Image>();
        _roundDisplay = GameObject.FindGameObjectWithTag("RoundText").GetComponent<RoundTextDisplay>();
        _reducedTime = timerTime;
    }

    private void Start()
    {
        GameManager.current.runUpdate += TimerCheck;
        RoundManager.current.runStartGameFunct += TimerStart;
    }

    public void TimerStart()
    {
        StartCoroutine(StartTurnTimer());
    }
    public void TimerCheck()
    {
        if (_reducedTime <= 0)
        {
            
            _roundDisplay.DisplayRoundEnd();
            if (_roundScoring.roundNum < 3)
            {
                //compare scores & stop timer
                _roundScoring.CompareScores();
                //stop
                StopCoroutine(StartTurnTimer());
                //show round number display
                _roundDisplay.DisplayRoundNumber();

                //reset number & restart timer
                _reducedTime = timerTime;
                TimerStart();
                RoundStart();
            }
            else if (_roundScoring.roundNum >= 3)
            {
                //compare scores w each other
                _roundScoring.CompareScores();
                //count the amount of wins once rounds are over
                _roundScoring.CountWins();
                //stop timer completely
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
