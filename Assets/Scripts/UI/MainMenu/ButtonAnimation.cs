using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Main Links")]
    [SerializeField] private Image _arrow;

    [Header("Open Animation Settings")]
    [SerializeField] private float _durationOpenAnimation = 1f;
    [SerializeField] private float _durationArrowBlink = 0.5f;

    [Header("Hinglighted Animation Settings")]
    [SerializeField] private float _durationHinglightedAnimation;

    [Header("Exit Animation Settings")]
    [SerializeField] private float _durationExitAnimation;

    [Header("Click Animation Settigs")]
    [SerializeField] private float _endScale = 0.8f;
    [SerializeField] private float _durationClickAnimation = 0.5f;

    private Image _image;
    private TMP_Text _text;

    private Sequence _hinglightedAnimationSequence;
    private Sequence _exitAnimationSequence;
    private Sequence _clickAnimationSequence;

    private MenuBaseUI _menu;

    private void OnDisable()
    {
        _hinglightedAnimationSequence.Kill();
        _exitAnimationSequence.Kill();
        _text.DOComplete();
    }

    private void Awake()
    {
        _text = transform.GetComponentInChildren<TMP_Text>();
        _image = GetComponent<Image>();

        _arrow?.gameObject.SetActive(false);

        if(transform.parent.TryGetComponent<MenuBaseUI>(out var menu))
        {
            _menu = menu;
            _menu.onOpenMenu += OpenSceneAnimation;
        }
        else
        {
            Debug.LogWarning("Menu не найден");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HinglightedAnimation();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ExitAnimation();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickAnimation();
    }

    public void OpenSceneAnimation()
    {
        _text.rectTransform.localScale = Vector3.one;
        

        _arrow?.DOFade(1f, _durationArrowBlink)
            .From(0f)
            .SetEase(Ease.InOutBack)
            .SetLoops(-1, LoopType.Yoyo);

        PlayAnimationText(_text);
    }

    private void HinglightedAnimation()
    {
        _hinglightedAnimationSequence?.Kill();
        
        _hinglightedAnimationSequence = DOTween.Sequence();

        _arrow?.gameObject.SetActive(true);

        _hinglightedAnimationSequence
            .Append(_image.DOFade(0.5f, _durationHinglightedAnimation)
                .SetEase(Ease.Linear));

        _hinglightedAnimationSequence.Play();
    }

    private void ExitAnimation()
    {
        _exitAnimationSequence?.Kill();

        _exitAnimationSequence = DOTween.Sequence();

        _arrow?.gameObject.SetActive(false);

        _exitAnimationSequence
            .Append(_image.DOFade(0f, _durationExitAnimation)
                .SetEase(Ease.Linear));
        
        _exitAnimationSequence.Play();
    }

    private void ClickAnimation()
    {
        _clickAnimationSequence?.Kill();

        _clickAnimationSequence = DOTween.Sequence();

        _clickAnimationSequence
            .Append(_text.rectTransform.DOScale(_endScale, _durationClickAnimation)
                .SetEase(Ease.InExpo))
            .OnComplete(() => _text.rectTransform.DOComplete());

        _clickAnimationSequence.Play();
    }

    private void PlayAnimationText(TMP_Text text)
    {
        text.maxVisibleCharacters = 0;

        DOTween.To(() => text.maxVisibleCharacters,
                   x => text.maxVisibleCharacters = x,
                   text.text.Length,
                   _durationOpenAnimation)
               .SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        _hinglightedAnimationSequence.Kill();
        _exitAnimationSequence.Kill();
        _arrow.DOKill();
    }
}
