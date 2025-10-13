using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Item : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string SoundName { get; private set; }
    [field: SerializeField] public GameObject ItemPrefab { get; private set; }
    [field: SerializeField] public HandlerType HandlerType { get; private set; }
    [field: SerializeField] public Color FloatingTextColor { get; private set; }
    [field: SerializeField] public int Value { get; private set; }

    public virtual void Use(ItemUseContext context)
    {
        Debug.Log("Handler " + context.GetHandler(HandlerType).GetType().Name);
        Debug.Log("Использован предмет " + Name);
    } 
}
