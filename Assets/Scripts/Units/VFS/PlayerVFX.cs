using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : UnitVFX
{
    [Header("Shield Effect")]
    [SerializeField] private ParticleSystem _shieldParticle;

    private Coroutine _shieldCoroutine;

    private void Awake()
    {
        transform.root.GetComponent<Player>().onReborn += PlayShieldAnimation;
    }

    private void PlayShieldAnimation(float time)
    {
        Debug.Log("Start Shield VFX");
        SetValueInEffect(time);

        _shieldParticle.Play();
    }

    private void SetValueInEffect(float time)
    {
        ParticleSystem particle = _shieldParticle.transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = particle.main;
        main.startLifetime = time;
    }
}
