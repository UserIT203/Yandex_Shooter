using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _description;

    [Header("Animation Settings")]
    [SerializeField] private float _endScale;
    [SerializeField] private float _duration;

    private Vector3 _startScale;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();   
        _startScale = _rectTransform.localScale;
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void OpenAnimation()
    {
        gameObject.SetActive(true);

        _rectTransform.DOScale(_startScale, _duration)
            .SetEase(Ease.OutQuad)
            .From(_endScale);
    }

    public void OpenPanel(string errorText)
    {
        _description.text = errorText;
        OpenAnimation();
    }
}
