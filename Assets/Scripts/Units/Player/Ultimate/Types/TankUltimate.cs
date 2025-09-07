using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "Tank", menuName = "Player Ultimate/Tank")]
public class TankUltimate : UltimateBase
{
    [Header("Tank Settings")]
    [SerializeField] private float _targetScaleValue;
    [SerializeField] private float _animationDuration;

    private Transform _playerGFX;

    public override void Initialized(Player player)
    {
        base.Initialized(player);
    }

    protected override void Execute()
    {
        base.Execute();
        _player.Stats.SetInvulnerableStatus(true);
        _playerGFX = _player.transform.GetChild(0);

        Vector3 originScale = _playerGFX.localScale;

        Sequence scaleSequence = DOTween.Sequence();
        scaleSequence
            .Append(_playerGFX.DOScale(_targetScaleValue, _animationDuration)
                .SetEase(Ease.OutBack))
            .AppendInterval(UltimateDuration)
            .Append(_playerGFX.DOScale(originScale, _animationDuration)
                .SetEase(Ease.OutBack));
    }

    protected override void CleanUp()
    {
        base.CleanUp();
        _player.Stats.SetInvulnerableStatus(false);
    }
}
