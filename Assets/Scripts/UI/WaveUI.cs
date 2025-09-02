using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Zenject;

public class WaveUI : MonoBehaviour
{
    private const string WaveLabel = "WAVE {0}";

    [SerializeField] private TMP_Text _waveNumberText;
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private float _endScale;
    [SerializeField] private float _animationDuration;

    private Sequence _animationSequence;
    private WaveManager _waveManager;

    [Inject]
    public void Construct(WaveManager waveManager)
    {
        _waveManager = waveManager;
        _waveManager.onStartWave += StartNewWave;
        
        _waveNumberText.text = string.Empty;
    }

    private void OnDisable()
    {
        _waveManager.onStartWave -= StartNewWave;
        _animationSequence?.Kill();
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
}
