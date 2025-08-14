using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapabilitieUI : MonoBehaviour
{
    [SerializeField] private List<CapabilitieCard> _cards;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.Deactivate();

        foreach (CapabilitieCard card in _cards)
        {
            card.onUpgrade += ClosePanel;
        }
    }

    private void OnDisable()
    {
        foreach (CapabilitieCard card in _cards)
        {
            card.onUpgrade -= ClosePanel;
        }
    }

    public void Show(ICapabilitie[] capabilities)
    {
        Debug.Log(capabilities.Length);

        for (int i = 0; i < capabilities.Length; i++) 
        {
            _cards[i].Initialize(capabilities[i]);
        }

        _canvasGroup.Activate();
        Time.timeScale = 0f;
    }

    private void ClosePanel()
    {
        _canvasGroup.Deactivate();
        Time.timeScale = 1f;
    } 
}
