using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    public void StartGame()
    {
        Loader.Load(Loader.Scene.Main);
    }
}
