using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResultPanelUI : MenuBaseUI
{
    [Inject] private readonly WaveManager _waveManager;
    [Inject] private readonly GameManager _gameManager;

    [Header("Text Links")]
    [SerializeField] private TMP_Text _totalDestroyEnemyText;
    [SerializeField] private TMP_Text _totalDestroyBossText;
    [SerializeField] private TMP_Text _totalTimeText;
    [SerializeField] private TMP_Text _totalWaveText;

    [Header("Button Links")]
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(OnClickHomeButton);
        _restartButton.onClick.AddListener(OnClickRestartButton);

        _homeButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
        _restartButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(OnClickHomeButton);
        _restartButton.onClick.RemoveListener(OnClickRestartButton);

        _homeButton.onClick.RemoveListener(() => AudioManager.PlaySound("ButtonClick"));
        _restartButton.onClick.RemoveListener(() => AudioManager.PlaySound("ButtonClick"));
    }

    private void FillInfo()
    {
        _totalDestroyEnemyText.text = _waveManager.EnemiesDestroy.ToString();
        _totalDestroyBossText.text = _waveManager.BossDestroy.ToString();
        _totalTimeText.text = string.Format("{0:f}", _gameManager.TotalTimeInGame);
        _totalWaveText.text = (_waveManager.CurrentWave + 1f).ToString();
    }

    private void OnClickHomeButton() => SceneTransition.SwitchScene("MainMenu");
    private void OnClickRestartButton() => SceneTransition.RestartScene();

    protected override void Initialized()
    {
        base.Initialized();
        _canvasGroup.Deactivate();
    }

    protected override void CloseMenuAnimation()
    {
        throw new System.NotImplementedException();
    }

    protected override void OpenMenuAnimation()
    {
        FillInfo();

        _canvasGroup.DOFade(1f, 1f)
            .From(0f)
            .SetEase(Ease.Linear)
            .OnComplete(OnOpenMenu);
    }

    public override void OpenMenu()
    {
        base.OpenMenu();
    }
}