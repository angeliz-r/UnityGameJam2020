using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoundTextDisplay : MonoBehaviour
{
    [SerializeField]private GameObject _roundObj;
    [SerializeField]private TextMeshProUGUI _roundText;
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private TextMeshProUGUI _tutorialText;

    private AudioController _audio;
    private RoundScoring _roundScoring;
    private void Awake()
    {
        _roundText = this.GetComponent<TextMeshProUGUI>();
        _roundScoring = GameObject.FindGameObjectWithTag("roundScorer").GetComponent<RoundScoring>();
        _audio = GetComponent<AudioController>();
    }

    private void Start()
    {
        RoundManager.current.runStartGameFunct += DisplayRoundNumber;
    }

    public IEnumerator MoveRound()
    {
        LeanTween.moveX(_roundObj, Screen.width / 2, 0.5f).setEaseInOutCubic();
        yield return new WaitForSeconds(2f);
        LeanTween.moveX(_roundObj, Screen.width * 2, 0.5f).setEaseInOutCubic();
        StopCoroutine(MoveRound());
    }
    public IEnumerator MoveTutorial()
    {
        LeanTween.moveX(_tutorial, Screen.width/2, 0.5f).setEaseInOutCubic();
        yield return new WaitForSeconds(2f);
        LeanTween.moveX(_tutorial, Screen.width*2, 0.5f).setEaseInOutCubic();
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
        _tutorialText.text = "";
        _audio.PlaySoundEffect(SFXCollection.time_up_sfx);
    }
}
