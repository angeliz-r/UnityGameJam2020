using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RoundManager : MonoBehaviour
{
    public static RoundManager current;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        StartGame();
    }

    public event Action runStartGameFunct;
    public void StartGame()
    {
        if (runStartGameFunct != null)
            runStartGameFunct();
    }

    public event Action runEndGameFunct;
    public void EndGame()
    {
        if (runEndGameFunct != null)
            runEndGameFunct();
    }
}
