using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainSceneIntaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GeneralManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MenuManager>().FromComponentInHierarchy().AsSingle();
    }
}
