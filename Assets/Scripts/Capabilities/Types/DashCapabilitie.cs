using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DashCapabilitie : CapabilitieBase
{
    private float _delay;
    private float _dashForce;
    private bool _canDash = true;
    private CharacterController _characterController;

    public DashCapabilitie(List<CapabilitieConfig> upgrades, CapabilitieConfig defaultConfig, Player player) : base(upgrades, defaultConfig, player)
    {
        _characterController = player.GetComponent<CharacterController>();
    }

    public override void Unlock()
    {
        base.Unlock();
        SetUpgrade(DefaultConfig);
    }

    public override void Activate(Vector3 direction)
    {
        if (_canDash == false || IsUnlock == false)
            return;

        _canDash = false;

        Player.StartCoroutine(StartDash(direction));
    }

    protected override void SetUpgrade(CapabilitieConfig config)
    {
        _dashForce = config.Speed;
        _delay = config.Delay;
    }

    private IEnumerator StartDash(Vector3 direction)
    {
        _characterController.Move(direction * _dashForce);

        float timer = 0f;

        OnCapabilitieTimer(timer, _delay);

        while (timer < _delay)
        {
            timer += Time.deltaTime;
            OnCapabilitieTimer(timer, _delay);
            yield return null;
        }
        
        _canDash = true;
    }
}
