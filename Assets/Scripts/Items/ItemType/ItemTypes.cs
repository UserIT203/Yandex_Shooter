using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "XPItem", menuName = "Items/XP Item")]
public class XPItem: Item
{
    [SerializeField] private float _xpCount;

    [Inject]
    public void Construct(IItemHandler handler)
    {
        Debug.Log("Handler name " + handler.GetType().Name);
        _handler = handler;
    }

    public override void Use()
    {
        base.Use();
        _handler?.HandleAction(_xpCount);
    }
}

[CreateAssetMenu(fileName = "Heatlh Item", menuName = "Items/Health Item")]
public class HealthItem : Item
{

}
