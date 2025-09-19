using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MenuBaseUI
{
    protected override void CloseMenuAnimation()
    {
        _canvasGroup.DOFade(0f, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() => _canvasGroup.Deactivate());
    }

    protected override void OpenMenuAnimation()
    {
        _canvasGroup.DOFade(1f, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() => _canvasGroup.Activate());
    }
}
