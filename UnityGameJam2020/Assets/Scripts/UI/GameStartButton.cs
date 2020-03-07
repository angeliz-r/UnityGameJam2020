using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    private AudioController _audio;
    private void Awake()
    {
        _audio = GetComponent<AudioController>();
    }
    public void StartGame()
    {
        _audio.PlaySoundEffect(SFXCollection.click);
        Loader.Load(Loader.Scene.Main);
    }
}
