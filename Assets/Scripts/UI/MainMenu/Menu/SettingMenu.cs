using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MenuBaseUI
{
    [Header("Open Animation Settings")]
    [SerializeField] private float _duration = 0.5f;

    private RectTransform _rectTransform;

    protected override void Initialized()
    {
        base.Initialized();
        _rectTransform = GetComponent<RectTransform>();
    }

    protected override void CloseMenuAnimation()
    {
        _rectTransform.DOScale(0f, _duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => _canvasGroup.Deactivate());

        OnCloseMenu();
    }

    protected override void OpenMenuAnimation()
    {
        _canvasGroup.Activate();

        _rectTransform.localScale = Vector3.zero;
        _rectTransform.DOScale(1f, _duration)
            .SetEase(Ease.Linear)
            .OnComplete(OnOpenMenu);
    }
}
