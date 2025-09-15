using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CapabilitieUI : MonoBehaviour
{
    [Inject] private GameTimeManager _timeManager;

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

        _canvasGroup.Activate();
        _timeManager.Pause();

        for (int i = 0; i < capabilities.Length; i++) 
        {
            _cards[i].Initialize(capabilities[i]);
        }
    }

    private void ClosePanel()
    {
        _canvasGroup.Deactivate();
        _timeManager.Resume();
    } 
}
