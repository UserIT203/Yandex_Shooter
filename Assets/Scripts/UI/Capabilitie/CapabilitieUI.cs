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
        ChangeCardStatus(false);

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
        AudioManager.PlaySound("LevelUp");
        _canvasGroup.Activate();
        _timeManager.Pause();

        int randomValue = Random.Range(0, 2);
        int cardCount = randomValue == 1 ? capabilities.Length : capabilities.Length - 1;
        Debug.LogWarning("Random Value " + randomValue);

        for (int i = 0; i < cardCount; i++) 
        {
            _cards[i].Initialize(capabilities[i]);
        }
    }

    private void ClosePanel()
    {
        _canvasGroup.Deactivate();
        ChangeCardStatus(false);
        _timeManager.Resume();
    } 

    private void ChangeCardStatus(bool status)
    {
        foreach (CapabilitieCard card in _cards)
        {
            card.gameObject.SetActive(status);
        }
    }
}