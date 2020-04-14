using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingProgressBar : MonoBehaviour
{
    public GameObject startButton;
    private Image loadImg;
    [SerializeField]private TextMeshProUGUI loadtext;
    private void Awake()
    {
        loadImg = GetComponent<Image>();
    }

    private void Start()
    {
       LoadingProgress();
    }

    void LoadingProgress()
    {
        StartCoroutine(LoadBar());
    }

    IEnumerator LoadBar()
    {
        // loadImg.fillAmount = Loader.GetLoadingProgress();
        loadImg.fillAmount = loadImg.fillAmount + 0.03f;
        yield return new WaitForSeconds(1f);
        loadImg.fillAmount = loadImg.fillAmount + 0.25f;
        yield return new WaitForSeconds(1f);
        loadImg.fillAmount = loadImg.fillAmount + 0.39f;
        yield return new WaitForSeconds(1f);
        loadImg.fillAmount = 1f;
        loadtext.text = "READY TO PLAY!";
        startButton.SetActive(true);
        StopCoroutine(LoadBar());
    }

}
