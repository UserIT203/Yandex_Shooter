using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Zenject;
using UnityEngine.UI;


public class WaveUI : MonoBehaviour
{
    private const string WaveLabel = "WAVE {0}";

    [Header("Text Settigns")]
    [SerializeField] private TMP_Text _waveNumberText;
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private float _endScale;
    [SerializeField] private float _animationDuration;

    [Header("Win Panel Settings")]
    [SerializeField] private CanvasGroup _winPanelCanvas;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _resultButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private ResultPanelUI _resultPanelUI;

    [Header("Win Panel Animation Settings")]
    [SerializeField] private float _openAnimationDuration;
    [SerializeField] private List<FireworkUI> _firework;

    private Sequence _animationSequence;
    private WaveManager _waveManager;
    private GameTimeManager _timeManager;
    private Queue<FireworkUI> _queueFirework;

    [Inject]
    public void Construct(WaveManager waveManager, GameTimeManager timeManager)
    {
        _waveManager = waveManager;
        _timeManager = timeManager;
        _waveManager.onStartWave += StartNewWave;
        _waveManager.onWavesEnd += OpenWinPanel;

        _waveNumberText.text = string.Empty;
    }

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(OnClickHomeButton);
        _restartButton.onClick.AddListener(OnClickRestartButton);
        _resultButton.onClick.AddListener(OnClickResultPanel);

        _homeButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
        _restartButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
        _resultButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
    }

    private void OnDisable()
    {
        _waveManager.onStartWave -= StartNewWave;
        _waveManager.onWavesEnd -= OpenWinPanel;

        _homeButton.onClick.RemoveListener(OnClickHomeButton);
        _restartButton.onClick.RemoveListener(OnClickRestartButton);
        _resultButton.onClick.RemoveListener(OnClickResultPanel);

        _homeButton.onClick.RemoveListener(() => AudioManager.PlaySound("ButtonClick"));
        _restartButton.onClick.RemoveListener(() => AudioManager.PlaySound("ButtonClick"));
        _resultButton.onClick.RemoveListener(() => AudioManager.PlaySound("ButtonClick"));

        _animationSequence?.Kill();
    }

    private void Awake()
    {
        _winPanelCanvas.Deactivate();
        _queueFirework = new Queue<FireworkUI>(_firework);
    }

    private void StartNewWave(int currentWaveNumber)
    {
        _waveNumberText.rectTransform.localScale = _startScale;
        _waveNumberText.text = string.Format(WaveLabel, currentWaveNumber + 1);

        _animationSequence?.Kill();
        _animationSequence = DOTween.Sequence();

        _animationSequence
            .Append(_waveNumberText.rectTransform.DOScale(_endScale, _animationDuration)
                .SetEase(Ease.OutBack))
            .SetDelay(0.8f)
            .Append(_waveNumberText.rectTransform.DOScale(_startScale.x, _animationDuration)
                .SetEase(Ease.OutElastic))
            .OnComplete(() => _waveNumberText.text = string.Empty);

        _animationSequence.Play();
    }

    private void OpenWinPanel()
    {
        _timeManager.Pause();

        StartCoroutine(FireworkAnimationPlay());

        _winPanelCanvas.DOFade(1f, _animationDuration)
            .SetEase(Ease.InOutBack)
            .From(0f)
            .OnComplete(() => _winPanelCanvas.Activate());
    }

    private IEnumerator FireworkAnimationPlay()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(0.5f);

        do
        {
            FireworkUI firework = _queueFirework.Dequeue();
            firework.PlayAnimation();

            yield return waitingTime;
        }
        while (_queueFirework.Count > 0);
    }

    private void OnClickHomeButton() => SceneTransition.SwitchScene("MainMenu");
    private void OnClickRestartButton() => SceneTransition.RestartScene();

    private void OnClickResultPanel() => _resultPanelUI.OpenMenu();
}
