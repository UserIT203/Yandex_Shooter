using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [Header("Main Settigns")]
    [SerializeField] private Platform _platform;
    [Header("Mobile Settings")]
    [SerializeField] private LayerMask _mobileShootingLayerMask;
    [SerializeField] private CanvasGroup _mobileInput;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _reloadButton;
    [SerializeField] private Button _dashButton;
    [Header("Desktop Settings")]
    [SerializeField] private LayerMask _desktopShootingLayerMask;
    [Header("Player Links")]
    [SerializeField] private Player _player;
    [SerializeField] private PlayerConfig _playerConfig;

    public override void InstallBindings()
    {
        BindPlayerSettings();
        //GlobalBinding();
    }

    private void GlobalBinding()
    {
        Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle().NonLazy();
    }

    private void BindPlayerSettings()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig);
        Container.Bind<Player>().FromInstance(_player).AsSingle();

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
        _mobileInput.Deactivate();
        Container.Bind<IInput>().To<DesktopInput>().AsSingle();

        Container.Bind<LayerMask>().FromInstance(_desktopShootingLayerMask).AsSingle();
        Container.Bind<IShootingSystem>().To<PCShootingSystem>().AsSingle();
        Container.Bind<PlayerCombat>().FromInstance(_player.GetComponent<PlayerCombat>()).AsSingle();
    }

    private void BindMobileInput()
    {
        _mobileInput.Activate();

        Container.Bind<Joystick>().FromInstance(_joystick);

        Container.Bind<IInput>().To<MobileInput>().AsSingle()
            .WithArguments(_dashButton);

        Container.Bind<LayerMask>().FromInstance(_mobileShootingLayerMask).AsSingle();
        Container.Bind<PlayerCombat>().FromInstance(_player.GetComponent<PlayerCombat>()).AsSingle();

        Container.Bind<IShootingSystem>().To<MobileShootingSystem>().AsSingle()
            .WithArguments(_reloadButton);
    }
}

public enum Platform
{
    Desktop,
    Mobile
}
