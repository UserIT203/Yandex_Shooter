using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TutotialMenu : MenuBaseUI
{
    [Header("UI Links")]
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _enableTutorialPanel;

    private GameData _gameData;
    private Platform _platform;
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameData gameData, Platform platform, GameManager gameManager)
    {
        _gameData = gameData;
        _platform = platform;
        _gameManager = gameManager;

        _gameManager.onGameStart += OpenMenu;
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(CloseMenu);
        _enableTutorialPanel.onClick.AddListener(OnChangeStateTutoral);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(CloseMenu);
        _enableTutorialPanel.onClick.RemoveListener(OnChangeStateTutoral);
    }

    private void OnChangeStateTutoral()
    {
        _gameData.ChangeTutorialState();
        CloseMenu();
    }

    protected override void Initialized()
    {
        base.Initialized();

        _canvasGroup.Deactivate();
    }

    protected override void CloseMenuAnimation()
    {
        OnCloseMenu();
    }

    protected override void OpenMenuAnimation()
    {
        OnOpenMenu();
    }

    public override void OpenMenu()
    {
        if (_gameData.ShowTutorial == false || _platform != Platform.Desktop) return;

        Time.timeScale = 0f;
        base.OpenMenu();
    }

    public override void CloseMenu()
    {
        Time.timeScale = 1f;
        base.CloseMenu();
    }
}
