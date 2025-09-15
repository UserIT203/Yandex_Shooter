using UnityEngine;
using System;

public class GameTimeManager : MonoBehaviour
{
    public event Action OnGamePaused;
    public event Action OnGameResumed;

    private int _pauseCount = 0;

    public void Pause()
    {
        _pauseCount++;

        if(_pauseCount == 1)
        {
            OnGamePaused?.Invoke();
        }
    }

    public void Resume()
    {
        _pauseCount--;

        if(_pauseCount <= 0)
        {
            OnGameResumed?.Invoke();
        }
    }
}

