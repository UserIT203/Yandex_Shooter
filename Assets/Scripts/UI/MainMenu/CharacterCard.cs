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

    private int _characterIndex;
    private Character _characterInfo;
    private Button _button;
    private TMP_Text _buyButtonText;
    private GeneralManager _generalManager;

    public event Action<int> onClickCard;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenConcreteMenu);
        _buyButton.onClick.AddListener(OnClickBuyButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenConcreteMenu);
        _buyButton.onClick.RemoveListener(OnClickBuyButton);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buyButtonText = _buyButton.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void Initialized(GeneralManager generalManager, int characterIndex)
    {
        _generalManager = generalManager;
        _characterIndex = characterIndex;
        _characterInfo = generalManager.Characters[characterIndex];
        _nameText.text = _characterInfo.Name;
        _view.sprite = _characterInfo.CharacterView;

        if (_characterInfo.IsBought == true)
            _buyButtonText.text = "Equip";
        else
            _buyButtonText.text = _characterInfo.Cost.ToString();
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
            _buyButtonText.text = "Equip";
        }
    }

    private void OpenConcreteMenu()
    {
        onClickCard?.Invoke(_characterIndex);
    }
}
