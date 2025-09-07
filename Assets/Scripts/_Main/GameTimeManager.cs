using UnityEngine;
using System;

public class GameTimeManager : MonoBehaviour
{
    public event Action OnGamePaused;
    public event Action OnGameResumed;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        OnGamePaused?.Invoke();
    }

    public void Resume()
    {
        isPaused = false;
        OnGameResumed?.Invoke();
    }
}

