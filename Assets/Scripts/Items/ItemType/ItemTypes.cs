using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "XPItem", menuName = "Items/XP Item")]
public class XPItem: Item
{
    [SerializeField] private float _xpCount;

    public override void Use(ItemUseContext context)
    {
        context.GetHandler(HandlerType)?.HandleActionWithValue(_xpCount);
        base.Use(context);
    }
}

[CreateAssetMenu(fileName = "Heatlh Item", menuName = "Items/Health Item")]
public class HealthItem : Item
{
    [SerializeField] private float _healthValue;

    public override void Use(ItemUseContext context)
    {
        context.GetHandler(HandlerType)?.HandleActionWithValue(_healthValue);
        base.Use(context);
    }
}
