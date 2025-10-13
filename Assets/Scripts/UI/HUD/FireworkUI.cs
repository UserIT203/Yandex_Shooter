using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FireworkUI : MonoBehaviour
{
    [SerializeField] private float _startScale;
    [SerializeField] private float _endScale;
    [SerializeField] private float _duration;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void PlayAnimation()
    {
        gameObject.SetActive(true);

        _image.rectTransform.DOScale(_endScale, _duration)
            .From(_startScale)
            .SetEase(Ease.Linear);
    }
}
