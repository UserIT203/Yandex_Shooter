using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Default Item")]
public class Item : ScriptableObject    
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public GameObject ItemPrefab { get; private set; }

    public virtual void Use()
    {
        Debug.Log("Использован предмет " + Name);
    } 
}
