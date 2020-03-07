﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundTextDisplay : MonoBehaviour
{
    [SerializeField]private GameObject _roundObj;
    [SerializeField]private TextMeshProUGUI _roundText;
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private TextMeshProUGUI _tutorialText;
    private RoundScoring _roundScoring;
    private void Awake()
    {
        _roundText = this.GetComponent<TextMeshProUGUI>();
        _roundScoring = GameObject.FindGameObjectWithTag("roundScorer").GetComponent<RoundScoring>();
    }

    private void Start()
    {
        RoundManager.current.runStartGameFunct += DisplayRoundNumber;
        DisplayRoundNumber();
    }

    public IEnumerator MoveRound()
    {
        LeanTween.moveX(_roundObj, 500, 0.5f).setEaseInOutCubic();
        yield return new WaitForSeconds(2f);
        LeanTween.moveX(_roundObj, 1500, 0.5f).setEaseInOutCubic();
        StopCoroutine(MoveRound());
    }
    public IEnumerator MoveTutorial()
    {
        LeanTween.moveX(_tutorial, 400, 0.5f).setEaseInOutCubic();
        yield return new WaitForSeconds(2f);
        LeanTween.moveX(_tutorial, 1500, 0.5f).setEaseInOutCubic();
        StopCoroutine(MoveTutorial());
    }

    public void DisplayRoundNumber()
    {
        StartCoroutine(MoveRound()); 
        _roundText.text = "ROUND " + (_roundScoring.roundNum + 1).ToString();
        if (_roundScoring.roundNum == 0)
        {
            StartCoroutine(MoveTutorial());
        }
        else
        {
            _tutorialText.text = "";
        }

    }

    public void DisplayRoundEnd()
    {
        StartCoroutine(MoveRound());
        _roundText.text = "TIME'S UP!";
    }
}
