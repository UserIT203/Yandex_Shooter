using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager Instance;

    [Header("Translating Text Settings")]
    [SerializeField] private TranslatingText _errorText;

    [SerializeField] private PopUpMenu _popUpMenu;
    [SerializeField] private PlayerConfig _defaultPlayerConfig;
    [SerializeField] private List<Character> _characters;
    [SerializeField] private GameData _gameData;

    public PlayerConfig CurrentCharacte { get { return _gameData.Character; } }
    public int CoinsCount
    {
        get { return _gameData.Coins; }
    }
    public List<Character> Characters => _characters;
    public GameData GameData => _gameData;

    public event Action onBuy;

    private void Awake()
    {
        InitializedData();

        if (_gameData.Character == null)
            _gameData.Character = _defaultPlayerConfig;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
            
        DontDestroyOnLoad(this);
    }

    private void InitializedData()
    {
        if(YG2.saves.Data != null)
        {
            _gameData.InitializedData(_characters[YG2.saves.Data.CharacterID].Config,
                YG2.saves.Data.Coins,
                YG2.saves.Data.ShowTutorial);
        }
    }

    public int GetCharacterID(PlayerConfig playerConfig)
    {
        return _characters.FindIndex(p => p.Config.GetHashCode() == playerConfig.GetHashCode());
    }

    public bool TryBuyCharacter(Character character)
    {
        if(_gameData.TryRemoveCoins(character.Cost) == false)
        {
            _popUpMenu.OpenPanel(_errorText.Text);
            return false;
        }

        Character boughtCharacter = _characters.Find(c => c.GetHashCode() == character.GetHashCode());
        boughtCharacter.IsBought = true;
        onBuy?.Invoke();

        return true;
    }

    public void EquipCharacter(Character character)
    {
        AudioManager.PlaySound("EquipCharacter");
        _gameData.Character = character.Config;
    }
}

[System.Serializable]
public class Character
{
    public TranslatingText Name;

    public PlayerConfig Config;

    public Sprite CharacterView;
    public int Cost;
    public bool IsBought;
}