using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "XPItem", menuName = "Items/XP Item")]
public class XPItem: Item
{
    public override void Use(ItemUseContext context)
    {
        context.GetHandler(HandlerType)?.HandleActionWithValue((float)Value);
        base.Use(context);
    }
}


