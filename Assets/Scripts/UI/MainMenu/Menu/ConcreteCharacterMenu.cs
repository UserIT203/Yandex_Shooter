using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ConcreteCharacterMenu : MenuBaseUI
{
    [Header("Links UI")]
    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private Image _characterView;
    [SerializeField] private Image _characterGameView;
    [SerializeField] private Image _ultimateIcon;
    [SerializeField] private TMP_Text _ultimateDescription;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _coinsCountText;

    [Header("Button Settings")]
    [SerializeField] private Sprite _eqipCharacter;
    [SerializeField] private Sprite _uneqipCharacter;

    [Header("Translation Text Settings")]
    [SerializeField] private TranslatingText _equipText;

    private Character _characterInfo;

    private GeneralManager _generalManager;

    [Inject]
    public void Construct(GeneralManager generalManager)
    {
        _generalManager = generalManager;
        _generalManager.GameData.onAddCoins += SetCoinsText;
    }

    private void OnEnable()
    {
        _generalManager.onBuy += SetCoinsText;
        _buyButton.onClick.AddListener(OnClickBuyButton);
    }

    private void OnDisable()
    {
        _generalManager.onBuy -= SetCoinsText;
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
        _characterName.text = _characterInfo.Name.Text;
        _characterGameView.sprite = _characterInfo.Config.GFX;
        _characterView.sprite = _characterInfo.CharacterView;
        _ultimateIcon.sprite = _characterInfo.Config.Ultimate.UltimateIcon;
        _ultimateDescription.text = _characterInfo.Config.Ultimate.Description;

        SetCoinsText();

        if (_generalManager.CurrentCharacte.GetHashCode() == _characterInfo.Config.GetHashCode())
            _buyButton.GetComponent<Image>().sprite = _eqipCharacter;
        else
            _buyButton.GetComponent<Image>().sprite = _uneqipCharacter;

        if ( _characterInfo.IsBought == true)
            _costText.text = _equipText.Text;
        else
            _costText.text = _characterInfo.Cost.ToString();
    }

    private void SetCoinsText()
    {
        _coinsCountText.text = _generalManager.CoinsCount.ToString();
    }

    private void OnClickBuyButton()
    {
        if (_characterInfo.IsBought == true)
        {
            _generalManager.EquipCharacter(_characterInfo);
            _buyButton.GetComponent<Image>().sprite = _eqipCharacter;
        }
        else
        {
            BuyCharacter();
        }
    }

    private void BuyCharacter()
    {
        if(_generalManager.TryBuyCharacter(_characterInfo) == true)
            _costText.text = _equipText.Text;
    }


    public void OpenConcreteCharacter(int characterIndex)
    {   
        _characterInfo = _generalManager.Characters[characterIndex];
        FillInfo();
    }
}
