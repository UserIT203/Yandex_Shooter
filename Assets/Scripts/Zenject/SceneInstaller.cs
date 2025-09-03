using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [Header("Main Settigns")]
    [SerializeField] private Platform _platform;
    [Header("Mobile Settings")]
    [SerializeField] private LayerMask _mobileShootingLayerMask;
    [SerializeField] private WeaponHUD _mobileInput;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _reloadButton;
    [SerializeField] private Button _dashButton;
    [SerializeField] private Button _ultimateButton;
    [Header("Desktop Settings")]
    [SerializeField] private LayerMask _desktopShootingLayerMask;
    [SerializeField] private WeaponHUD _desktopWeaponHUD;
    [Header("Player Links")]
    [SerializeField] private Player _player;
    [SerializeField] private PlayerConfig _playerConfig;
    [Header("Class Handler")]
    [SerializeField] private GameManager _gameManager;

    public override void InstallBindings()
    {
        BindPlayerSettings();
        BindItemInstaller();
    }

    private void BindPlayerSettings()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig);
        Container.Bind<Player>().FromInstance(_player).AsSingle();
        Container.Bind<WaveManager>().FromComponentInHierarchy().AsSingle();

        switch (_platform)
        {
            case Platform.Desktop:
                BindDesktopInput();
                break;
            case Platform.Mobile:
                BindMobileInput();
                break;
        }
    }

    private void BindDesktopInput()
    {
        _mobileInput.gameObject.SetActive(false);
        _desktopWeaponHUD.gameObject.SetActive(true);

        Container.Bind<IInput>().To<DesktopInput>().AsSingle();

        Container.Bind<LayerMask>().FromInstance(_desktopShootingLayerMask).AsSingle();
        Container.Bind<IShootingSystem>().To<PCShootingSystem>().AsSingle();
        Container.Bind<PlayerCombat>().FromInstance(_player.GetComponent<PlayerCombat>()).AsSingle();
        Container.Bind<IWeaponHUD>().FromInstance(_desktopWeaponHUD).AsSingle();
    }

    private void BindMobileInput()
    {
        _mobileInput.gameObject.SetActive(true);
        _desktopWeaponHUD.gameObject.SetActive(false);

        Container.Bind<Joystick>().FromInstance(_joystick);

        Container.Bind<IInput>().To<MobileInput>().AsSingle()
            .WithArguments(_dashButton);

        Container.Bind<LayerMask>().FromInstance(_mobileShootingLayerMask).AsSingle();
        Container.Bind<PlayerCombat>().FromInstance(_player.GetComponent<PlayerCombat>()).AsSingle();

        Container.Bind<IShootingSystem>().To<MobileShootingSystem>().AsSingle()
            .WithArguments(_reloadButton, _ultimateButton);

        Container.Bind<IWeaponHUD>().FromInstance(_mobileInput).AsSingle();
    }

    private void BindItemInstaller()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ItemUseContext>().FromMethod(CreateItemUseContext).AsSingle();
    }

    private ItemUseContext CreateItemUseContext(InjectContext ctx)
    {
        var player = ctx.Container.Resolve<Player>();
        var gameManager = ctx.Container.Resolve<GameManager>();
        return new ItemUseContext(player, gameManager);
    }
}

public enum Platform
{
    Desktop,
    Mobile
}
