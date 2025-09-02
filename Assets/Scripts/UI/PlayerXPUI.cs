using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PlayerXPUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _animationDuration = 0.2f;

    public void ChangeSliderValue(float value, float maxValue)
    {
        _slider.DOKill();
        float currentValue = value / maxValue;
        _slider.DOValue(currentValue, _animationDuration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }
}
