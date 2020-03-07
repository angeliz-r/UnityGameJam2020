using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class RoundTimer : MonoBehaviour
{
    public float timerTime;
    private TextMeshProUGUI _timerDisplay;
    private float _reducedTime;
    private Image _fillBar;
    private RoundScoring _roundScoring;
    private RoundTextDisplay _roundDisplay;
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
                StopCoroutine(StartTurnTimer());
                _roundDisplay.DisplayRoundNumber();
                _reducedTime = timerTime;
                TimerStart();
            }
            else if (_roundScoring.roundNum >= 3)
            {
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
