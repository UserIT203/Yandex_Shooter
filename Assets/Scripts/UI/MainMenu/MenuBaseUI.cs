using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class MenuBaseUI : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;

    public event Action onOpenMenu;

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
    public virtual void OpenMenu()
    {
        OpenMenuAnimation();
        onOpenMenu?.Invoke();
    }
    public virtual void CloseMenu()
    {
        CloseMenuAnimation();
    }
}
