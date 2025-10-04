using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : UnitVFX
{
    [Header("Effect Particle")]
    [SerializeField] private ParticleSystem _shieldParticle;
    [SerializeField] private ParticleSystem _dualWeaponParticle;
    [SerializeField] private ParticleSystem _frozeenParticle;
    [SerializeField] private ParticleSystem _swordUltimate;

    private Coroutine _dualWeaponCoroutine = null;

    private void OnDisable()
    {
        transform.root.GetComponent<Player>().onReborn -= PlayShieldAnimation;
        transform.root.GetComponent<Player>().onUseDualWeaponUltimate -= StartDualWeaponEffect;
        transform.root.GetComponent<PlayerMovement>().onFreeze -= PlayFrozeenParticle;
    }

    private void Awake()
    {
        transform.root.GetComponent<Player>().onReborn += PlayShieldAnimation;
        transform.root.GetComponent<Player>().onUseDualWeaponUltimate += StartDualWeaponEffect;
        transform.root.GetComponent<PlayerMovement>().onFreeze += PlayFrozeenParticle;
    }

    private void PlayShieldAnimation(float time)
    {
        SetValueInEffect(time);
        _shieldParticle.Play();
    }

    private void SetValueInEffect(float time)
    {
        ParticleSystem particle = _shieldParticle.transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = particle.main;
        main.startLifetime = time;
    }

    private IEnumerator PlayDualWeaponEffect(float time)
    {
        Debug.Log("Stat Dual Effect");
        _dualWeaponParticle.Play();

        yield return new WaitForSeconds(time);

        _dualWeaponParticle.Stop();
        _dualWeaponCoroutine = null;
    }

    private void PlayFrozeenParticle(bool state)
    {
        if (state == true)
            _frozeenParticle.Play();
        else
            _frozeenParticle.Stop();
    }

    public void StartDualWeaponEffect(float time)
    {
        Debug.Log("Start Weapon Effect");

        if(_dualWeaponCoroutine == null )
            _dualWeaponCoroutine = StartCoroutine(PlayDualWeaponEffect(time));
    }

    public void PlaySwordUltimateEffect() => _swordUltimate.Play();
}
