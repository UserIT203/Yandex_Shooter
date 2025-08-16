using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerXPUI : MonoBehaviour
{
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
