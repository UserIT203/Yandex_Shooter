using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class CharacterMenu : MenuBaseUI
{
    private const string ConcreteMenuName = "ConcreteCharacterMenu";

    [SerializeField] private ConcreteCharacterMenu _concreteCharacterMenu;
    [SerializeField] private RectTransform _upLabelRectTransform;
    [SerializeField] private Transform _content;
    [SerializeField] private TMP_Text _coinsCountText;

    [Header("Characters")]
    [SerializeField] private CharacterCard _characterCardTemplate;

    [Header("Animation Settings")]
    [SerializeField] private float _duration;
    [SerializeField] private float _offsetPositionY;

    private List<CharacterCard> _cards = new List<CharacterCard>();
    private float _startPositionY;
    private GeneralManager _generalManager;
    private MenuManager _menuManager;

    [Inject]
    public void Constuct(GeneralManager generalManager, MenuManager menuManager)
    {
        _generalManager = generalManager;
        _menuManager = menuManager;

        _generalManager.GameData.onAddCoins += SetCoinsCountText;
    }

    protected override void Initialized()
    {
        base.Initialized();

        _generalManager.onBuy += SetCoinsCountText;
        SetCoinsCountText();
        CreateCharacterCard();
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

        _startPositionY = _upLabelRectTransform.localPosition.y;

        _upLabelRectTransform.DOLocalMoveY(_startPositionY, _duration)
            .From(_startPositionY + _offsetPositionY)
            .SetEase(Ease.Linear)
            .OnComplete(OnOpenMenu);
    }

    private void SetCoinsCountText()
    {
        _coinsCountText.text = _generalManager.CoinsCount.ToString();    
    }

    private void CreateCharacterCard()
    {
        for (int i = 0; i < _generalManager.Characters.Count; i++)
        {
            CharacterCard card = Instantiate(_characterCardTemplate);
            card.transform.SetParent(_content);

            card.onClickCard += OpenConcreteCharacterMenu;
            card.onEquipCharacter += UpdateInfoInCard;

            _cards.Add(card);
        }
    }

    private void UpdateInfoInCard()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].Initialized(_generalManager, i);
        }
    }

    private void OpenConcreteCharacterMenu(int characterIndex)
    {
        _concreteCharacterMenu.OpenConcreteCharacter(characterIndex);
        _menuManager.OpenMenu(ConcreteMenuName);
    }
}

