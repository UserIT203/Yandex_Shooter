using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
