using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScreen : MonoBehaviour
{
    public GameObject[] Screens;
    public void ShowKeyboardControls()
    {
        Screens[1].SetActive(true);
        Screens[0].SetActive(false);
    }

    public void ShowControllerControls()
    {
        Screens[1].SetActive(false);
        Screens[0].SetActive(true);
    }
}
