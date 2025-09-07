using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(fileName = "Rocket", menuName = "Player Ultimate/Rockets")]
public class RocketUltimate : UltimateBase
{
    private const float DelayToCreateRocket = 0.2f;
    private const float UpperPlayerY = 0.3f;

    [Header("Rocket Settings")]
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;
    [SerializeField] private int _maxTarget;
    [SerializeField] private Rocket rocketPrefab;
    [SerializeField] private LayerMask _enemyLayerMask;

    protected override void Execute()
    {
        base.Execute();
        Debug.Log("Targets in radius " + GetTargets().Length);
        _player.StartCoroutine(CreateRocket(GetTargets()));
    }

    private Collider[] GetTargets()
    {
        Collider[] targets = Physics.OverlapSphere(_player.transform.position, _radius,
            _enemyLayerMask);

        return targets;
    }

    private IEnumerator CreateRocket(Collider[] targets)
    {
        int targetCount = Mathf.Min(targets.Length, _maxTarget);
        WaitForSeconds waitingTime = new WaitForSeconds(DelayToCreateRocket);

        while (targetCount > 0)
        { 
            yield return waitingTime;

            //Создание ракеты

            Vector3 position = new Vector3
                (
                    _player.transform.position.x,
                    _player.transform.position.y + UpperPlayerY,
                    _player.transform.position.z
                );

            Debug.Log(position);
            
            if(targets[targetCount - 1] != null)
            {
                Rocket rocket = Instantiate(rocketPrefab, position, Quaternion.identity);
                rocket.SetTarget(targets[targetCount - 1].transform, _damage);
            }
            
            targetCount--;
        }
    }
}
