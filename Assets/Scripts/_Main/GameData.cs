using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    [field: SerializeField] public PlayerConfig Character { get; set; }
    [field: SerializeField] public int Coins { get; private set; }

    public void AddCoins(int count)
    {
        Coins += count;
    }

    public bool TryRemoveCoins(int count)
    {
        if(count > Coins)
            return false;
        
        Coins -= count;
        return true;
    }
}
