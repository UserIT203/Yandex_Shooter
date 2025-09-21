using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    [SerializeField] private List<Character> _characters;

    public List<Character> Characters => _characters;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}

[System.Serializable]
public struct Character
{
    public PlayerConfig Config;
    public string Name;
    public Sprite CharacterView;
    public int Cost;
    public bool IsBought;
}