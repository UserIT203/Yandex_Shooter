using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RedExplosion", menuName = "Boss Ultimate/Red Explosion")]
public class RedExplosionUltimate : UltimateBase
{
    [Header("Red Explosion Settings")]
    [SerializeField] private RedExplosion _redExplosionTemplate;
    [SerializeField] private float _createDelay;
    [Header("Bullet Settigs")]
    [SerializeField] private float _radiusExplosion;
    [SerializeField] private float _damage;

    private Coroutine _coroutine;

    protected override void Execute()
    {
        base.Execute();
        _coroutine = _boss.StartCoroutine(CreateRedExplosion());
    }

    protected override void CleanUp()
    {
        base.CleanUp();
        _boss.StopCoroutine(_coroutine);
    }

    private IEnumerator CreateRedExplosion()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_createDelay);

        while (true) 
        {
            RedExplosion redExplosion = Instantiate(
                _redExplosionTemplate, _player.transform.position, Quaternion.identity);

            redExplosion.Initialized(_radiusExplosion, _damage, _player);
            redExplosion.transform.position = new Vector3(
                redExplosion.transform.position.x,
                0f,
                redExplosion.transform.position.z);

            yield return waitingTime;
        }
    }
}
