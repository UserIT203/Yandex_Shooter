using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DeathUI : MonoBehaviour
{
    [Inject] private Player _player;
    [Inject] private WaveManager _waveManager;
    [Inject] private GameTimeManager _timeManager;

    [Header("Result Panel Links")]
    [SerializeField] private CanvasGroup _resultPanel;
    [SerializeField] private TMP_Text _enemiesCountText;   
    [SerializeField] private TMP_Text _bossCountText;   
    [SerializeField] private TMP_Text _totalTimeText;

    [Header("Resume Panel Links")]
    [SerializeField] private CanvasGroup _resumePanel;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Button _refuseButton;

    private float _totalTime;
    private bool _isPauseGame = false;

    private void OnEnable()
    {
        _refuseButton.onClick.AddListener(OpenResultAnimation);
        _applyButton.onClick.AddListener(OnRebornPlayer);
    }

    private void OnDisable()
    {
        _refuseButton.onClick.RemoveListener(OpenResultAnimation);
        _applyButton.onClick.RemoveListener(OnRebornPlayer);
    }

    private void Awake()
    {
        _timeManager.OnGamePaused += () => _isPauseGame = true;
        _timeManager.OnGameResumed += () => _isPauseGame = false;

        _resumePanel.Deactivate();
        _resultPanel.Deactivate();
    }

    private void Update()
    {
        if (_isPauseGame) return;

        _totalTime += Time.deltaTime;
    }

    public void OpenDeathPanel()
    {
        Debug.Log("Open Reslt Panel");
        OpenResumePanel();
    }

    private void OpenResumePanel()
    {
        _resumePanel.DOFade(1f, 1f)
           .SetEase(Ease.Linear, 1f)
           .SetUpdate(true);

        _resumePanel.Activate();

        PlayAnimationText(_description);
    }

    private void OpenResultAnimation()
    {
        Time.timeScale = 1f;

        _resultPanel.DOFade(1f, 1f)
            .SetEase(Ease.Linear, 1f)
            .OnComplete(FillInfoInResult);
    }

    private void FillInfoInResult()
    {
        _resultPanel.Activate();

        _bossCountText.text = _waveManager.BossDestroy.ToString();
        _enemiesCountText.text = _waveManager.EnemiesDestroy.ToString();
        _totalTimeText.text = string.Format("{0:f2}", _totalTime);

        PlayAnimationText(_bossCountText);
        PlayAnimationText(_totalTimeText);
        PlayAnimationText(_enemiesCountText);
    }

    private void PlayAnimationText(TMP_Text text)
    {
        text.maxVisibleCharacters = 0;

        DOTween.To(() => text.maxVisibleCharacters,
                   x => text.maxVisibleCharacters = x,
                   text.text.Length,
                   0.5f)
               .SetEase(Ease.Linear);
    }

    private void OnRebornPlayer()
    {
        _resultPanel.Deactivate();
        _resumePanel.Deactivate();

        _player.Reborn();
    }
}
