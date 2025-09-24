using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    [SerializeField] private PlayerConfig _defaultPlayerConfig;
    [SerializeField] private List<Character> _characters;

    public PlayerConfig CurrentPlayerConfig { get; private set; }

    public List<Character> Characters => _characters;

    private void Awake()
    {
        if (CurrentPlayerConfig == null)
            CurrentPlayerConfig = _defaultPlayerConfig;

        DontDestroyOnLoad(this);
    }

    public void BuyCharacter(Character character)
    {
        Character boughtCharacter = _characters.Find(c => c.GetHashCode() == character.GetHashCode());
        boughtCharacter.IsBought = true;

        Debug.Log("Buy character " + boughtCharacter.Name);
    }

    public void EquipCharacter(Character character)
    {
        CurrentPlayerConfig = character.Config;
        Debug.Log("Equip " + character.Name);
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