using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coin_Item", menuName = "Items/Coin")]
public class CoinItem : Item
{
    public override void Use(ItemUseContext context)
    {
        context.GetHandler(HandlerType)?.HandleActionWithValue(Value);
        base.Use(context);
    }
}
