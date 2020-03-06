using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public static GameManager instance { get { return current; } }

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        StartEvent();
    }

    private void Update()
    {
        UpdateEvent();
    }
    void CreateSingleton()
    {
        if (current != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            current = this;
            DontDestroyOnLoad(this.gameObject);
        }
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
