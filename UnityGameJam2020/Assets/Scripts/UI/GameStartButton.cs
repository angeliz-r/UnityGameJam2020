using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        SceneManager.LoadScene(2); //to loading screen
    }

    public void StartGameFromLoadScreen()
    {
        _audio.PlaySoundEffect(SFXCollection.click);
        SceneManager.LoadScene(1); //to main game
    }
}
