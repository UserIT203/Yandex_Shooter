using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _backgroundFillImage;
    [SerializeField] private Image _flashImage;

    [Header("Animation Settings")]
    [SerializeField] private float _fillDuration = 0.4f;
    [SerializeField] private float _delayDuration = 0.6f;
    [SerializeField] private float _flashDuration = 0.2f;

    [Header("Color Settings")]
    [SerializeField] private Gradient _healthGradient;

    private Tweener _fillTween, _delayTween, _flashTween;

    private void OnEnable()
    {
        transform.root.GetComponent<EnemyUnit>().Stats.onChangeHealth += UpdateHealth;
    }

    private void OnDisable()
    {
        transform.root.GetComponent<EnemyUnit>().Stats.onChangeHealth -= UpdateHealth;

        if (_fillTween != null) _fillTween.Pause();
        if (_delayTween != null) _delayTween.Pause();
        if (_flashTween != null) _flashTween.Pause();
    }

    // Основной метод для обновления здоровья извне
    public void UpdateHealth(float current, float max)
    {
        float targetFill = max > 0 ? current / max : 0;

        AnimateFill(_fillImage, targetFill, _fillDuration);
        AnimateDelayFill(targetFill, _delayDuration);
        AnimateFlash();

        // Обновление цвета
        UpdateHealthColor(targetFill);
    }

    private void AnimateFill(Image image, float targetFill, float duration)
    {
        if (image == null) return;

        _fillTween?.Kill();
        _fillTween = image.DOFillAmount(targetFill, duration)
            .SetEase(Ease.OutCubic);
    }

    private void AnimateDelayFill(float targetFill, float duration)
    {
        if (_backgroundFillImage == null) return;

        _delayTween?.Kill();
        _delayTween = _backgroundFillImage.DOFillAmount(targetFill, duration)
            .SetEase(Ease.OutQuart);
    }

    private void AnimateFlash()
    {
        if (_flashImage == null) return;

        _flashImage.color = new Color(1, 1, 1, 0.8f);
        _flashTween = _flashImage.DOFade(0f, _flashDuration)
            .SetEase(Ease.OutQuad);
    }

    private void UpdateHealthColor(float healthPercent)
    {
        if (_fillImage != null && _healthGradient != null)
        {
            _fillImage.color = _healthGradient.Evaluate(healthPercent);
        }
    }

    private void KillAllTweens()
    {
        if (_fillTween != null)
        {
            _fillTween.Kill();
            _fillTween = null;
        }

        if (_delayTween != null)
        {
            _delayTween.Kill();
            _delayTween = null;
        }

        if (_flashTween != null)
        {
            _flashTween.Kill();
            _flashTween = null;
        }
    }

    private void OnDestroy()
    {
        KillAllTweens();

        if (DOTween.instance != null)
        {
            DOTween.Kill(this);
        }
    }
}
