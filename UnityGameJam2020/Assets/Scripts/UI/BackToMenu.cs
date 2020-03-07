using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackToMenu : MonoBehaviour
{
    private AudioController _audio;
    private void Awake()
    {
        _audio = GetComponent<AudioController>();
    }
    public void ToMainMenu()
    {
        _audio.PlaySoundEffect(SFXCollection.click);
        SceneManager.LoadScene(0);
    }

}
