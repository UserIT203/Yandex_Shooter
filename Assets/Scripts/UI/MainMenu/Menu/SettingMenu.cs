using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MenuBaseUI
{
    [Header("Open Animation Settings")]
    [SerializeField] private float _duration = 0.5f;

    protected override void CloseMenuAnimation()
    {
        _canvasGroup.DOFade(0f, _duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => _canvasGroup.Deactivate());
    }

    protected override void OpenMenuAnimation()
    {
        _canvasGroup.DOFade(1f, _duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => _canvasGroup.Activate());
    }
}
