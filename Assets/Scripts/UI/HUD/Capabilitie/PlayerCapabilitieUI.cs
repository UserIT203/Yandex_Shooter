using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCapabilitieUI : MonoBehaviour
{
    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _capabilitieIcon;
    [SerializeField] private TMP_Text _leveltText;
    [SerializeField] private Image _fillImage;

    private ICapabilitie _capabilitie;

    private void OnDisable()
    {
        _capabilitie.onUnlock -= Unlock;
        _capabilitie.onUpgrade -= Upgrage;
        _capabilitie.onCapabilitieTimer -= ChangeCapabilitieStatus;
    }

    public void Initialized(ICapabilitie capabilitie)
    {
        _capabilitie = capabilitie;
        _fillImage.fillAmount = 0f;
        _capabilitieIcon.enabled = false;

        _capabilitie.onUnlock += Unlock;
        _capabilitie.onUpgrade += Upgrage;
        _capabilitie.onCapabilitieTimer += ChangeCapabilitieStatus;

        if (_capabilitie.IsUnlock) Unlock();
    }

    private void Unlock()
    {
        _capabilitieIcon.enabled = true;
        _lockImage.gameObject.SetActive(false);
        UpdateInfo();
    }

    private void Upgrage()
    {
        UpdateInfo();
        Debug.Log(_capabilitie.GetType().Name + "Level " + _capabilitie.Level);
    }

    private void UpdateInfo()
    {
        _capabilitieIcon.sprite = _capabilitie.Upgrades[_capabilitie.Level - 1].Icon;
        _leveltText.text = (_capabilitie.Level).ToString();
    }

    private void ChangeCapabilitieStatus(float currentValue, float totalValue)
    {
        float value = Mathf.Clamp(currentValue / totalValue, 0, totalValue);
        _fillImage.DOFillAmount(1 - value, 0.2f).SetEase(Ease.OutBack);
    }
}
