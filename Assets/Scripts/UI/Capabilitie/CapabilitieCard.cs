using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CapabilitieCard : MonoBehaviour
{
    [Header("UI Links")]
    [SerializeField] private Button _actionButton;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _descriptions;

    private CapabiliteUpgradeLevel _capability;
    private PlayerCapabilities _playerCapabilities;
    private ICapabilitie _currentCapabilite;

    public event Action onUpgrade;

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnUpgrade);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnUpgrade);
    }

    public void Initialize(ICapabilitie capabilite)
    {
        _currentCapabilite = capabilite;

        _icon.sprite = capabilite.GetCurrentUpgradeConfig().Icon;

        _descriptions.text = "Level: " + capabilite.Level;

        if (capabilite.IsMaxLevel())
        {
            _descriptions.text = "MAX";
            _actionButton.interactable = false;
            return;
        }

        if (capabilite.IsUnlock == false)
        {
            _descriptions.text = "Unlock";
        }

        _actionButton.interactable = true;
    }

    private void OnUpgrade()
    {
        if(_currentCapabilite.IsUnlock == false) 
            _currentCapabilite.Unlock();
        else
            _currentCapabilite.TryUpgrade();

        onUpgrade?.Invoke();
    }
}
