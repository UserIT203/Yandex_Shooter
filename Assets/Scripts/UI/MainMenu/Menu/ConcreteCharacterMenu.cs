using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ConcreteCharacterMenu : MenuBaseUI
{
    public const string LockCharacterText = "Equip";

    [Inject] private GeneralManager _generalManager;

    [Header("Links UI")]
    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private Image _characterView;
    [SerializeField] private Image _characterGameView;
    [SerializeField] private Image _ultimateIcon;
    [SerializeField] private TMP_Text _ultimateDescription;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _costText;

    private Character _characterInfo;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnClickBuyButton);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnClickBuyButton);
    }

    protected override void CloseMenuAnimation()
    {
        OnCloseMenu();
    }

    protected override void OpenMenuAnimation()
    {
        OnOpenMenu();
    }

    private void FillInfo()
    {
        _characterName.text = _characterInfo.Name;
        _characterGameView.sprite = _characterInfo.Config.GFX;
        _characterView.sprite = _characterInfo.CharacterView;
        _ultimateIcon.sprite = _characterInfo.Config.Ultimate.UltimateIcon;

        if( _characterInfo.IsBought == true)
            _costText.text = LockCharacterText;
        else
            _costText.text = _characterInfo.Cost.ToString();
    }

    private void OnClickBuyButton()
    {
        if (_characterInfo.IsBought == true)
        {
            _generalManager.EquipCharacter(_characterInfo);
        }
        else
        {
            _generalManager.BuyCharacter(_characterInfo);
            _costText.text = LockCharacterText;
        }   
    }

    public void OpenConcreteCharacter(int characterIndex)
    {   
        _characterInfo = _generalManager.Characters[characterIndex];
        FillInfo();
    }
}
