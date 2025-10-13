using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Moving Settings")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;

    private Player _player;
    private CharacterController _characterController;
    private IInput _input;
    private Vector3 _currentVelocity;
    private bool _canMove = true;

    public event Action<bool> onFreeze;

    [Inject]
    public void Construct(IInput input, Player player)
    {
        _input = input;
        _input.onMove += Moving;
        _input.onDash += OnDash;

        _player = player;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_player?.IsFreeze == true) return;

        _input.UpdateInput();

        if (_canMove)
        {
            _characterController.Move(_currentVelocity * Time.deltaTime);
            PlayFootstepSound();
        }
    }

    private void Moving(Vector3 direction)
    {
        direction *= _player.Stats.Speed.GetValue();

        if (direction.magnitude > 0.1f)
        {
            _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, direction.x, 
                _acceleration * Time.deltaTime);
            _currentVelocity.z = Mathf.Lerp(_currentVelocity.z, direction.z, 
                _acceleration * Time.deltaTime);
        }
        else
        {
            _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, 0, 
                _deceleration * Time.deltaTime);
            _currentVelocity.z = Mathf.Lerp(_currentVelocity.z, 0, 
                _deceleration * Time.deltaTime);
        }
    }

    private void OnDash(Vector3 direction)
    {
        Debug.Log("Player Dash");

        if(_canMove == false) return;

        _player.GetComponent<PlayerCapabilities>()
            .GetCapabilite(CapabilitiesType.Dash).Activate(direction);
    }

    private IEnumerator Frezee(float time)
    {
        AudioManager.PlaySound("PlayerFreez");

        _canMove = false;
        onFreeze?.Invoke(true);

        yield return new WaitForSeconds(time);

        _canMove = true;
        onFreeze?.Invoke(false);
    }

    private void PlayFootstepSound()
    {
        if (_characterController.velocity.magnitude > 0.01f)
            AudioManager.PlaySound("Footstep");
        else
            AudioManager.StopSound("Footstep");
    }

    public void FreezeMoving(float frezeeTime)
    {
        StartCoroutine(Frezee(frezeeTime));
    }
}
