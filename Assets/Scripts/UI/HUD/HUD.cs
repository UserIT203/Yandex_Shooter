using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using DG.Tweening;

public class HUD : MonoBehaviour
{
    [Header("UI Links")]
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Slider _healthSlider;

    [Header("Animation Settings")]
    [SerializeField] private float _animationDuration;

    [Inject]
    public void Constract(Player player)
    {
        player.Stats.onChangeHealth += ChangeVisiblePlayerHealth;
        ChangeVisiblePlayerHealth(player.Stats.MaxHealth.GetValue(), player.Stats.MaxHealth.GetValue());
    }

    private void ChangeVisiblePlayerHealth(float currentHealth, float maxHealth)
    {
        string healthText = string.Format("{0} | {1}", currentHealth, maxHealth);
        _healthText.text = healthText;

        _healthSlider.DOKill();
        float currentValue = currentHealth / maxHealth;
        _healthSlider.DOValue(currentValue, _animationDuration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }
}
