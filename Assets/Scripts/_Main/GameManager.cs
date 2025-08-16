using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IXPItemHandler
{
    [SerializeField] private CapabilitiesManager _capabiliteManager;
    [Header("Main Settigns")]
    [SerializeField] private float _maxXP = 100;
    [SerializeField] private int _increaseMaxXPValue;
    [Header("Player XP Options")]
    [SerializeField] private XPItem _xpItem;
    [Header("UI Links")]
    [SerializeField] private PlayerXPUI _playerXPUI;

    private float _currentXP;

    private void OnEnable()
    {
        _xpItem.RegisterHandler(this);
    }

    private void OnDisable()
    {
        _xpItem.RegisterHandler(null);
    }

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
