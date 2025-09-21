using DG.Tweening;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    [Header("Open Animation Settings")]
    [SerializeField] private float _durationOpenAnimation = 1f;
    [SerializeField] private Vector3 _offsetPosition;

    private RectTransform _rectTransform;
    private Vector3 _startPositionY;

    private MenuBaseUI _menu;

    private void OnDisable()
    {
        _menu.onOpenMenu -= OpenAnimation;
        _menu.onCloseMenu -= CloseAnimation;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPositionY = _rectTransform.localPosition;

        if(transform.parent.TryGetComponent(out _menu))
        {
            _menu.onOpenMenu += OpenAnimation;
            _menu.onCloseMenu += CloseAnimation;
        }
    }

    private void OpenAnimation()
    {
        _rectTransform.DOLocalMove(_startPositionY, _durationOpenAnimation)
            .From(_startPositionY + _offsetPosition)
            .SetEase(Ease.Linear);
    }

    private void CloseAnimation()
    {
        Debug.Log("Close Menu");
    }
}
