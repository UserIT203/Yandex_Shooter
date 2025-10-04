using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
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

    public event Action onBuy;

    private void Awake()
    {
        if (_gameData.Character == null)
            _gameData.Character = _defaultPlayerConfig;

        DontDestroyOnLoad(this);
    }

    public bool TryBuyCharacter(Character character)
    {
        if(_gameData.TryRemoveCoins(character.Cost) == false)
        {
            _popUpMenu.OpenPanel("No money");
            return false;
        }

        Character boughtCharacter = _characters.Find(c => c.GetHashCode() == character.GetHashCode());
        boughtCharacter.IsBought = true;
        onBuy?.Invoke();

        return true;
    }

    public void EquipCharacter(Character character)
    {
        _gameData.Character = character.Config;
    }
}

[System.Serializable]
public class Character
{
    public PlayerConfig Config;
    public string Name;
    public Sprite CharacterView;
    public int Cost;
    public bool IsBought;
}