using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBall : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Player _player;
    private IBulletObserver _observer;
    private float _freezeTime;

    private Vector3 _targetPosition;

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.Lerp(
                transform.position,
                _targetPosition,
                _speed * Time.deltaTime
            );

        if (Vector3.Distance(transform.position, _player.transform.position) < 0.3f)
            HitPlayer();
    }

    private void HitPlayer()
    {
        Debug.Log("Target Close");
        _player.GetComponent<PlayerMovement>().FreezeMoving(_freezeTime);
        _observer.HandleHit();
        Destroy(gameObject);
    }

    public void SetTraget(Player target, float freezeTime, IBulletObserver observer)
    {
        _player = target;
        _observer = observer;
        _freezeTime = freezeTime;
        _targetPosition = target.transform.position;
    }
}
