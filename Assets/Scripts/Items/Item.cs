using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Item : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public GameObject ItemPrefab { get; private set; }

    protected IItemHandler _handler;

    public virtual void Use()
    {
        Debug.Log("Использован предмет " + Name);
    } 
}
