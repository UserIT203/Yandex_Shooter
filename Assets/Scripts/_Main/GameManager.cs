using System.Collections;
using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour, IItemHandler
{
    [SerializeField] private CapabilitiesManager _capabiliteManager;
    [Header("Main Settigns")]
    [SerializeField] private float _maxXP = 100;
    [SerializeField] private int _increaseMaxXPValue;
    [Header("UI Links")]
    [SerializeField] private PlayerXPUI _playerXPUI;

    private float _currentXP;

    public event Action onGameStart;

    public void HandleActionWithValue(float value)
    {
        _currentXP += value;

        _playerXPUI.ChangeSliderValue(_currentXP, _maxXP);

        if (_currentXP >= _maxXP) 
        { 
            FillXp();
        }
    }

    private void FillXp()
    {
        _capabiliteManager.ShowUpgradeUI();
        _currentXP = _maxXP - _currentXP;
        _maxXP += _increaseMaxXPValue;

        _playerXPUI.ChangeSliderValue(_currentXP, _maxXP);
    }

    public void GameStart()
    {
        onGameStart?.Invoke();
        Debug.Log("Game Start");
    }
}
