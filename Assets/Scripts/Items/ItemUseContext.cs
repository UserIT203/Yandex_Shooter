using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemUseContext
{
    private IItemHandler _player;
    private IItemHandler _gameManager;

    public ItemUseContext(IItemHandler player, IItemHandler gameManager)
    {
        _player = player;
        _gameManager = gameManager;
    }

    public IItemHandler GetHandler(HandlerType type)
    {
        return type switch
        {
            HandlerType.Player => _player,
            HandlerType.GameManager => _gameManager,
            _ => null
        };
    }
}

public enum HandlerType
{
    Player,
    GameManager
}
