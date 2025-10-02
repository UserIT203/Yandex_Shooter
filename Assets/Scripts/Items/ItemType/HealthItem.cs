using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heatlh Item", menuName = "Items/Health Item")]
public class HealthItem : Item
{
    public override void Use(ItemUseContext context)
    {
        context.GetHandler(HandlerType)?.HandleActionWithValue((float)Value);
        base.Use(context);
    }
}
