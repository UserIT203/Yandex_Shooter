using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float _durationText = 0.5f;

    private TMP_Text _text;
    private MenuBaseUI _menu;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        if (transform.parent.TryGetComponent<MenuBaseUI>(out var menu))
        {
            _menu = menu;
            _menu.onOpenMenu += OpenAnimation;
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
}
