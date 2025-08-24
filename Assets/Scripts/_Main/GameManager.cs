using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IItemHandler
{
    [SerializeField] private CapabilitiesManager _capabiliteManager;
    [Header("Main Settigns")]
    [SerializeField] private float _maxXP = 100;
    [SerializeField] private int _increaseMaxXPValue;
    [Header("UI Links")]
    [SerializeField] private PlayerXPUI _playerXPUI;

    private float _currentXP;

    public void HandleAction(float value)
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
}
