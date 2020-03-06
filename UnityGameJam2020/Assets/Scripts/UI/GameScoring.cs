using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameScoring : MonoBehaviour
{
    public float plantUnitScore = 10;
    public TextMeshProUGUI scoreDisplay;
    private float _totalScore;
    private float _totalGameScore;
    private float[] _totalScoreArray;
    public bool newRound;
    private int index = 0;
    void Awake()
    {
        if (newRound)
        {

            _totalScoreArray[index] = _totalScore;
            _totalScore = 0;
            index++;
        }
    }
    private void Start()
    {
        GameManager.current.runUpdate += UpdateScoreDisplay;
    }

    public void UpdateScoreDisplay()
    {
        scoreDisplay.text = _totalScore.ToString();
    }
    public void AddLiveScore()
    {
        _totalScore = _totalScore + plantUnitScore;
    }

    public void DestroyLiveScore()
    {
        _totalScore = _totalScore - plantUnitScore;
    }

    public float AddTotalGameScore()
    {
        for (int i = 0; i > index; i++)
        {
            _totalGameScore = _totalGameScore + _totalScoreArray[i];
        }
        return _totalGameScore;
    }

    public float ReturnTotalScore()
    {
        return _totalScore;
    }

    private void OnDestroy()
    {
        GameManager.current.runUpdate -= UpdateScoreDisplay;
    }
}
