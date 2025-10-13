using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCard : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _view;
    [SerializeField] private Button _buyButton;

    [Header("Card Settings")]
    [SerializeField] private Sprite _equipSprite;
    [SerializeField] private Sprite _defaultSprite;

    [Header("Button Setting")]
    [SerializeField] private Color _buyColor;
    [SerializeField] private Color _equipColor;

    private int _characterIndex;
    private Image _cardImage;
    private Character _characterInfo;
    private Button _button;
    private TMP_Text _buyButtonText;
    private GeneralManager _generalManager;

    public event Action<int> onClickCard;
    public event Action onEquipCharacter;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenConcreteMenu);
        _buyButton.onClick.AddListener(OnClickBuyButton);

        _button.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
        _buyButton.onClick.AddListener(() => AudioManager.PlaySound("ButtonClick"));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenConcreteMenu);
        _buyButton.onClick.RemoveListener(OnClickBuyButton);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _cardImage = GetComponent<Image>();
        _buyButtonText = _buyButton.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void Initialized(GeneralManager generalManager, int characterIndex)
    {
        _generalManager = generalManager;
        _characterIndex = characterIndex;
        _characterInfo = generalManager.Characters[characterIndex];
        _nameText.text = _characterInfo.Name;
        _view.sprite = _characterInfo.CharacterView;

        if (_characterInfo.Config.GetHashCode() == generalManager.CurrentCharacte.GetHashCode())
            _cardImage.sprite = _equipSprite;
        else
            _cardImage.sprite = _defaultSprite;

        if (_characterInfo.IsBought == true)
        {
            _buyButtonText.text = "Equip";
            _buyButton.image.color = _equipColor;
        }
        else
        {
            _buyButton.image.color = _buyColor;
            _buyButtonText.text = _characterInfo.Cost.ToString();
        }
    }

    private void OnClickBuyButton()
    {
        if (_characterInfo.IsBought == true)
            EquipCharacter();
        else
            BuyCharacter();
    }

    private void BuyCharacter()
    {
        if (_generalManager.TryBuyCharacter(_characterInfo) == true)
        {
            _buyButtonText.text = "Equip";
            _buyButton.image.color = _equipColor;
        }
    }

    private void EquipCharacter()
    {
        _generalManager.EquipCharacter(_characterInfo);
        onEquipCharacter?.Invoke();
    }

    private void OpenConcreteMenu()
    {
        onClickCard?.Invoke(_characterIndex);
    }
}
