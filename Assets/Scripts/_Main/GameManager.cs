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
    private float _currentTime;
    private GameData _gameData;
    private GameTimeManager _timeManager;
    private bool _isGamePaused;

    public float TotalTimeInGame => _currentTime;

    public event Action onGameStart;

    [Inject]
    public void Construct(GameData gameData, GameTimeManager timeManager)
    {
        _gameData = gameData;
        _timeManager = timeManager;

        _timeManager.OnGamePaused += () => _isGamePaused = true;
        _timeManager.OnGameResumed += () => _isGamePaused = false;
    }

    private void Update()
    {
        if (_isGamePaused == true) return;

        _currentTime += Time.deltaTime;
    }

    public void HandleActionWithValue(float value)
    {
        _currentXP += value;

        _playerXPUI.ChangeSliderValue(_currentXP, _maxXP);

        if (_currentXP >= _maxXP) 
        { 
            FillXp();
        }
    }

    public void HandleActionWithValue(int value)
    {
        _gameData.AddCoins(value);
    }

    private void FillXp()
    {
        YandexManager.Instance.ShowInterstitialAdv();
        _capabiliteManager.ShowUpgradeUI();
        _currentXP = _maxXP - _currentXP;
        _maxXP += _increaseMaxXPValue;

        _playerXPUI.ChangeSliderValue(_currentXP, _maxXP);
    }

    public void GameStart()
    {
        onGameStart?.Invoke();
    }
}
