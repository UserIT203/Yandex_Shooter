using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "XPItem", menuName = "Items/XP Item")]
public class XPItem: Item
{
    [SerializeField] private float _xpCount;

    private IXPItemHandler _handler;

    public void RegisterHandler(IXPItemHandler handler)
    {
        _handler = handler;
    }

    public override void Use()
    {
        base.Use();
        _handler?.HandleAction(_xpCount);
    }
}
