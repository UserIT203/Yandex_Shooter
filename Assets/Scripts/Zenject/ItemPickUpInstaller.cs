using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "ItemInstaller", menuName = "Installers/ItemInstaller")]
public class ItemPickUpInstaller : ScriptableObjectInstaller<ItemPickUpInstaller>
{
    [Header("Items")]
    [SerializeField] private XPItem _xpItem;

    public override void InstallBindings()
    { 
        Container.BindInstance(_xpItem).AsSingle();
    }
}
