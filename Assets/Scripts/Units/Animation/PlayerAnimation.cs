using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimation : UnitAnimation
{
    private CharacterController _controller;
    private SpriteRenderer _spriteRenderer;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        gameManager.onOpenScene += OpenSceneAnimation;
    }

    public override void DieAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialized()
    {
        base.Initialized();
        _controller = transform.root.GetComponent<CharacterController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void SetUnitSpeed()
    {
        MoveAnimation(_controller.velocity.magnitude);
    }

    private void OpenSceneAnimation()
    {
        _spriteRenderer.DOFade(1f, 2f);
    }
}
