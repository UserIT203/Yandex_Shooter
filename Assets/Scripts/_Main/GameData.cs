using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    [field: SerializeField] public PlayerConfig Character { get; set; }
    [field: SerializeField] public int Coins { get; private set; }
    [field: SerializeField] public bool ShowTutorial { get; private set; }

    public event Action onAddCoins;

    public void InitializedData(PlayerConfig character, int coins, bool showTutorial)
    {
        this.Character = character;
        this.Coins = coins;
        this.ShowTutorial = showTutorial;
    }

    public void AddCoins(int count)
    {
        Coins += count;
        onAddCoins?.Invoke();
    }

    public bool TryRemoveCoins(int count)
    {
        if(count > Coins)
            return false;
        
        Coins -= count;
        return true;
    }

    public void ChangeTutorialState()
    {
        ShowTutorial = false;
    }
}
