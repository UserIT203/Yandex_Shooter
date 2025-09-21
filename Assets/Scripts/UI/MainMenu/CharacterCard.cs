using System.Collections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CharacterCard : MonoBehaviour
{
    [Inject] private GeneralManager _generalManager;

    [Header("Links")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _view;
    [SerializeField] private Button _buyButton;

    private int _characterIndex;
    private Character _characterInfo;
    private Button _button;
    private TMP_Text _buyButtonText;

    public event Action<int> onClickCard;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenConcreteMenu);
        _buyButton.onClick.AddListener(BuyCharacter);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenConcreteMenu);
        _buyButton.onClick.RemoveListener(BuyCharacter);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buyButtonText = _buyButton.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void Initialized(Character characterInfo, int characterIndex)
    {
        _characterIndex = characterIndex;
        _characterInfo = characterInfo;
        _nameText.text = _characterInfo.Name;
        _view.sprite = _characterInfo.CharacterView;

        if (_characterInfo.IsBought == true)
            _buyButtonText.text = "Equip";
        else
            _buyButtonText.text = _characterInfo.Cost.ToString();
    }

    private void BuyCharacter()
    {
        Debug.Log("Buy " + _characterInfo.Name);
    }

    private void OpenConcreteMenu()
    {
        onClickCard?.Invoke(_characterIndex);
    }
}
