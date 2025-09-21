using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private MenuBaseUI _menu;

    [Header("Animation Settings")]
    [SerializeField] private float _durationText = 0.5f;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        ActionClose();
        _menu.onOpenMenu += OpenAnimation;
        _menu.onCloseMenu += ActionClose;

        if (_menu != null) return;

        if (transform.parent.TryGetComponent<MenuBaseUI>(out var menu))
        {
            _menu = menu;
            _menu.onOpenMenu += OpenAnimation;
            _menu.onCloseMenu += ActionClose;
        }
        else
        {
            Debug.LogWarning("Menu не найден");
        }
    }

    private void OpenAnimation()
    {
        PlayAnimationText(_text);
    }

    private void PlayAnimationText(TMP_Text text)
    {
        text.maxVisibleCharacters = 0;

        DOTween.To(() => text.maxVisibleCharacters,
                   x => text.maxVisibleCharacters = x,
                   text.text.Length,
                   _durationText)
               .SetEase(Ease.Linear);
    }

    private void ActionClose()
    {
        _text.maxVisibleCharacters = 0;
    }
}
