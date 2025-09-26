using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstlaller : MonoInstaller
{
    [SerializeField] private GameData _gameData;

    public override void InstallBindings()
    {
        Container.BindInstance(_gameData).AsSingle();
    }
}