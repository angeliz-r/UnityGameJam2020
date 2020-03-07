using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameScoring : MonoBehaviour
{
    public float plantUnitScore;
    public TextMeshProUGUI scoreDisplay;
    private float _totalScore;
    private bool newRound;
    void Awake()
    {
        if (newRound)
        {
            _totalScore = 0;
        }
    }

    public void AddLiveScore()
    {
        _totalScore = _totalScore + plantUnitScore;
    }

    public float ReturnTotalScore()
    {
        return _totalScore;
    }
}
