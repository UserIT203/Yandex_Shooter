using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "Tank", menuName = "Player Ultimate/Tank")]
public class TankUltimate : UltimateBase
{
    [Header("Tank Animation Settings")]
    [SerializeField] private float _targetScaleValue;
    [SerializeField] private float _animationDuration;

    [Header("Tank Ultimate Settings")]
    [SerializeField] private LayerMask _enemiesLayerMask;
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;

    private PlayerVFX _playerGFX;

    public override void Initialized(Player player)
    {
        base.Initialized(player);
    }

    protected override void Execute()
    {
        base.Execute();
        _player.Stats.SetInvulnerableStatus(true);
        _playerGFX = _player.transform.GetChild(0).GetComponent<PlayerVFX>();

        Attack();
        PlayAnimation();
    }

    protected override void CleanUp()
    {
        base.CleanUp();
        _player.Stats.SetInvulnerableStatus(false);
    }

    private void PlayAnimation()
    {
        Vector3 originScale = _playerGFX.transform.localScale;

        Sequence scaleSequence = DOTween.Sequence();
        scaleSequence
            .Append(_playerGFX.transform.DOScale(_targetScaleValue, _animationDuration)
                .SetEase(Ease.OutBack))
            .AppendInterval(UltimateDuration)
            .Append(_playerGFX.transform.DOScale(originScale, _animationDuration)
                .SetEase(Ease.OutBack));
    }

    private void Attack()
    {
        _playerGFX.PlaySwordUltimateEffect();

        Collider[] colliders = Physics.OverlapSphere(_player.transform.position
            , _radius, _enemiesLayerMask);

        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent<EnemyUnit>(out var enemy))
                enemy.TakeDamage(_damage);                
        } 
    }
}
