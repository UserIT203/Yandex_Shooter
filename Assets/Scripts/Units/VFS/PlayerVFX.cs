using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : UnitVFX
{
    [Header("Shield Effect")]
    [SerializeField] private ParticleSystem _shieldParticle;

    [Header("Ultimate Dual Weapon Effect")]
    [SerializeField] private ParticleSystem _dualWeaponParticle;

    [Header("Frozeen Effect")]
    [SerializeField] private ParticleSystem _frozeenParticle;

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

    public void StartDualWeaponEffect(float time)
    {
        Debug.Log("Start Weapon Effect");

        if(_dualWeaponCoroutine == null )
            _dualWeaponCoroutine = StartCoroutine(PlayDualWeaponEffect(time));
    }

    private void PlayFrozeenParticle(bool state)
    {
        if(state == true)
            _frozeenParticle.Play();
        else
            _frozeenParticle.Stop();
    }
}
