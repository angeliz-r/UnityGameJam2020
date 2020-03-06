using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayRoundWins : MonoBehaviour
{
    public bool isMan;
    private TextMeshProUGUI _roundWinText;
    private RoundScoring roundScoring;
    private void Awake()
    {
        roundScoring = GameObject.FindGameObjectWithTag("roundScorer").GetComponent<RoundScoring>();
        _roundWinText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        GameManager.current.runUpdate += UpdateRoundWinText;
    }

    public void UpdateRoundWinText()
    {
        if (isMan)
            _roundWinText.text = roundScoring.ReturnManWins().ToString();
        else
            _roundWinText.text = roundScoring.ReturnNatureWins().ToString();
    }

    private void OnDestroy()
    {
        GameManager.current.runUpdate -= UpdateRoundWinText;
    }
}
