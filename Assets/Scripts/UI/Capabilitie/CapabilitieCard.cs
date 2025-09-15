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

    private RectTransform _rectTransform;
    private Button _button;
    private CapabiliteUpgradeLevel _capability;
    private PlayerCapabilities _playerCapabilities;
    private ICapabilitie _currentCapabilite;

    public event Action onUpgrade;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnUpgrade);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnUpgrade);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(ICapabilitie capabilite)
    {
        OpenAnimation(capabilite);
    }

    private void OpenAnimation(ICapabilitie capabilite)
    {
        Vector3 rotation = new Vector3(0f, 360f, 0f);

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

        _icon.gameObject.SetActive(true);
        _icon.sprite = capabilite.GetCurrentUpgradeConfig().Icon;

        _descriptions.text = "Level: " + capabilite.Level;

        if (capabilite.IsMaxLevel())
        {
            _descriptions.text = "MAX";
            _button.interactable = false;
            return;
        }

        if (capabilite.IsUnlock == false)
        {
            _descriptions.text = "Unlock";
        }

        _button.interactable = true;
    }

    private void OnUpgrade()
    {
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
