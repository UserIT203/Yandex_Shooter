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

    [Inject]
    public void Construct(IInput input, Player player)
    {
        _input = input;
        _input.onMove += Moving;

        _player = player;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _input.UpdateInput();
        _characterController.Move(_currentVelocity * Time.deltaTime);
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
}
