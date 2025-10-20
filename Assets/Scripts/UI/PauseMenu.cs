using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Zenject;

public class PauseMenu : MenuBaseUI
{
    [Inject] private GameTimeManager _timeManager;

    [Header("UI Links")]
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;

    [Header("Setting Button")]
    [SerializeField] private SettingMenu _settingMenu;

    [Header("Settings")]
    [SerializeField] private KeyCode _pauseMenuKey;

    private bool _isOpen = false;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(CloseMenu);
        _homeButton.onClick.AddListener(OnHomeButtonClick);
        _settingsButton.onClick.AddListener(OnSettingButtonClick);

        _playButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
        _homeButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
        _settingsButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(CloseMenu);
        _homeButton.onClick.RemoveListener(OnHomeButtonClick);
        _settingsButton.onClick.RemoveListener(OnSettingButtonClick);

        _playButton.onClick.RemoveListener(() => AudioManager.PlaySound("ButtonClick"));
        _homeButton.onClick.RemoveListener(() => AudioManager.PlaySound("ButtonClick"));
        _settingsButton.onClick.RemoveListener(() => AudioManager.PlaySound("ButtonClick"));
    }

    private void Update()
    {
        if(Input.GetKeyDown(_pauseMenuKey))
            ChangeState();
    }

    private void ChangeState()
    {
        _isOpen = !_isOpen;

        if (_isOpen == true)
            OpenMenu();
        else
            CloseMenu();
    }

    private void OnHomeButtonClick()
    {
        SceneTransition.SwitchScene("MainMenu");
    }

    private void OnSettingButtonClick()
    {
        _settingMenu.OpenMenu();
    }

    protected override void CloseMenuAnimation()
    {
        _timeManager.Resume();
        OnCloseMenu();
    }

    protected override void OpenMenuAnimation()
    {
        _timeManager.Pause();
        OnOpenMenu();
    }
}
