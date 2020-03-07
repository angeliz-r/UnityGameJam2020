using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private AudioController _audio;
    private void Awake()
    {
        _audio = GetComponent<AudioController>();
    }
    public void Quit()
    {
        _audio.PlaySoundEffect(SFXCollection.click);
        Application.Quit();
    }
}
