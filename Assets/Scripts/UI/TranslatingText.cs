using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TranslatingText
{
    [SerializeField] private List<string> _texts;
    
    public string Text
    {
        get
        {
            return _texts[YandexManager.Instance.GetLanguageIndex()];
        }
    }
}
