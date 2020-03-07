using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager current;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        StartEvent();
    }

    private void Update()
    {
        UpdateEvent();
    }

    public event Action runUpdate;
    public void UpdateEvent()
    {
        if (runUpdate != null)
            runUpdate();
    }

    public event Action runStart;
    public void StartEvent()
    {
        if (runStart != null)
            runStart();
    }
}
