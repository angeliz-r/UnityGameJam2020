using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingProgressBar : MonoBehaviour
{
    private Image loadImg;
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
        StopCoroutine(LoadBar());
    }

}
