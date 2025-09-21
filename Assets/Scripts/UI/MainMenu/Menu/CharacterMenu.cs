using DG.Tweening;
using ModestTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CharacterMenu : MenuBaseUI
{
    private const string ConcreteMenuName = "ConcreteCharacterMenu";

    [Inject] private GeneralManager _generalManager;
    [Inject] private MenuManager _menuManager;

    [SerializeField] private ConcreteCharacterMenu _concreteCharacterMenu;
    [SerializeField] private RectTransform _upLabelRectTransform;
    [SerializeField] private Transform _content;

    [Header("Characters")]
    [SerializeField] private CharacterCard _characterCardTemplate;

    [Header("Animation Settings")]
    [SerializeField] private float _duration;
    [SerializeField] private float _offsetPositionY;

    private List<CharacterCard> _cards = new List<CharacterCard>();
    private float _startPositionY;

    protected override void Initialized()
    {
        base.Initialized();
        
        CreateCharacterCard();

        Debug.Log(_generalManager);
        _startPositionY = _upLabelRectTransform.localPosition.y;
    }

    protected override void CloseMenuAnimation()
    {
        _canvasGroup.Deactivate();
        OnCloseMenu();
    }

    protected override void OpenMenuAnimation()
    {
        _canvasGroup.Activate();
        UpdateInfoInCard();

        _upLabelRectTransform.DOLocalMoveY(_startPositionY, _duration)
            .From(_startPositionY + _offsetPositionY)
            .SetEase(Ease.Linear)
            .OnComplete(OnOpenMenu);
    }

    private void CreateCharacterCard()
    {
        for (int i = 0; i < _generalManager.Characters.Count; i++)
        {
            CharacterCard card = Instantiate(_characterCardTemplate);
            card.onClickCard += OpenConcreteCharacterMenu;
            card.transform.SetParent(_content);

            _cards.Add(card);
        }
    }

    private void UpdateInfoInCard()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].Initialized(_generalManager.Characters[i], i);
        }
    }

    private void OpenConcreteCharacterMenu(int characterIndex)
    {
        _concreteCharacterMenu.OpenConcreteCharacter(characterIndex);
        _menuManager.OpenMenu(ConcreteMenuName);
    }
}

