using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class MenuBaseUI : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;

    public event Action onOpenMenu;
    public event Action onCloseMenu;

    private void Awake()
    {
        Initialized();
    }

    protected virtual void Initialized()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    protected abstract void OpenMenuAnimation();
    protected abstract void CloseMenuAnimation();

    protected void OnOpenMenu() => onOpenMenu?.Invoke();
    protected void OnCloseMenu() => onCloseMenu?.Invoke();

    public virtual void OpenMenu()
    {
        _canvasGroup.Activate();
        OpenMenuAnimation();
    }
    public virtual void CloseMenu()
    {
        _canvasGroup.Deactivate();
        CloseMenuAnimation();
    }
}
