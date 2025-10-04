using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _exitButton;

    [Header("Animation Settings")]
    [SerializeField] private float _startScale;
    [SerializeField] private float _duration;

    private Vector3 _targetScale;
    private RectTransform _rectTransform;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(ClosePanel);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(ClosePanel);
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();   
        _targetScale = _rectTransform.localScale;
        ClosePanel();
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void OpenAnimation()
    {
        gameObject.SetActive(true);

        _rectTransform.DOScale(_targetScale, _duration)
            .SetEase(Ease.OutQuad)
            .From(_startScale);
    }

    public void OpenPanel(string errorText)
    {
        _description.text = errorText;
        OpenAnimation();
    }
}
