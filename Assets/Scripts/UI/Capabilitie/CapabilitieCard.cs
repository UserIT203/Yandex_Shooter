using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using DG.Tweening;

public class CapabilitieCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Links")]
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _descriptions;

    [Header("Animation Settigs")]
    [SerializeField] private float _scaleForEnter = 1.25f;
    [SerializeField] private float _durationOnEnter = 0.25f;
    [SerializeField] private float _durationOnOpen = 1f;

    [Header("Reward Settings")]
    [SerializeField] private bool _isReward = false;
    [SerializeField] private Image _rewardIcon;

    [Header("Translating Text Settings")]
    [SerializeField] private TranslatingText _levelText;
    [SerializeField] private TranslatingText _maxText;
    [SerializeField] private TranslatingText _unlockText;

    private RectTransform _rectTransform;
    private Button _button;
    private ICapabilitie _currentCapabilite;

    public event Action onUpgrade;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickAction);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickAction);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(ICapabilitie capabilite)
    {
        gameObject.SetActive(true);
        OpenAnimation(capabilite);
    }

    private void OpenAnimation(ICapabilitie capabilite)
    {
        Vector3 rotation = new Vector3(0f, 360f, 0f);
        _rectTransform.localScale = Vector3.one;

        _rewardIcon?.gameObject.SetActive(false);
        _icon.gameObject.SetActive(false);
        _descriptions.text = string.Empty;
        _button.interactable = false;

        _rectTransform
            .DORotate(rotation, _durationOnOpen, RotateMode.FastBeyond360)
            .OnComplete(() => FillInfo(capabilite));
    }

    private void FillInfo(ICapabilitie capabilite)
    {
        _currentCapabilite = capabilite;

        _rewardIcon?.gameObject.SetActive(true);
        _icon.gameObject.SetActive(true);
        _icon.sprite = capabilite.GetCurrentUpgradeConfig().Icon;

        _descriptions.text = string.Format(_levelText.Text, capabilite.Level + 1);

        if (capabilite.IsMaxLevel())
        {
            _descriptions.text = _maxText.Text;
            _button.interactable = false;
            return;
        }

        if (capabilite.IsUnlock == false)
        {
            _descriptions.text = _unlockText.Text;
        }

        _button.interactable = true;
    }

    private void OnClickAction()
    {
        if (_isReward)
        {
            Debug.Log("Play Reward");
            YandexManager.Instance.ShowRewardAdv("capabilitieReward", OnUpgrade);
            return;
        }

        OnUpgrade();
    }

    private void OnUpgrade()
    {
        AudioManager.PlaySound("SelectCapabilitie");

        if (_currentCapabilite.IsUnlock == false)
            _currentCapabilite.Unlock();
        else
            _currentCapabilite.TryUpgrade();

        onUpgrade?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.DOScale(_scaleForEnter, _durationOnEnter).SetEase(Ease.OutQuart);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.DOScale(1f, _durationOnEnter).SetEase(Ease.OutQuart);
    }
}
