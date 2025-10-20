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

    [Header("Sound Button Settings")]
    [SerializeField] private Button _soundButton;
    [SerializeField] private Sprite _enableSoundSprite;
    [SerializeField] private Sprite _disableSoundSprite;

    private RectTransform _rectTransform;
    private Image _soundButtonImage;

    private void OnEnable()
    {
        _soundButton.onClick.AddListener(ChangeSoundStatus);
    }

    private void OnDisable()
    {
        _soundButton.onClick.RemoveListener(ChangeSoundStatus);
    }

    private void ChangeSoundStatus()
    {
        if(AudioManager.Instance.IsPlaySound == true)
        {
            _soundButtonImage.sprite = _disableSoundSprite;
            AudioManager.StopAllSounds();
        }
        else
        {
            _soundButtonImage.sprite = _enableSoundSprite;
            AudioManager.PlayAllSounds();
        }
    }

    protected override void Initialized()
    {
        base.Initialized();
        _rectTransform = GetComponent<RectTransform>();
        _soundButtonImage = _soundButton.transform.GetChild(0).GetComponent<Image>();
    }

    public override void OpenMenu()
    {
        base.OpenMenu();
        _soundButtonImage.sprite =
            AudioManager.Instance.IsPlaySound == true ? _enableSoundSprite : _disableSoundSprite;
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
