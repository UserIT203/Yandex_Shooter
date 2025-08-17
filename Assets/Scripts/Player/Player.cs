using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IDamagable
{
    [Header("Player Settings")]
    [SerializeField] private LayerMask _picUpItemMask;

    private PlayerStats _characterStats;

    public PlayerStats Stats => _characterStats;

    [Inject]
    public void Construct(PlayerConfig playerConfig)
    {
        _characterStats = new PlayerStats(playerConfig);
        _characterStats.onDie += Die;
    }

    private void Update()
    {
        CollectPickUpItems();
    }

    private void Die()
    {
        Debug.Log("Player is Die");
    }

    private void CollectPickUpItems()
    {
        Collider[] itemsPickUp = Physics.OverlapSphere(transform.position,
            _characterStats.ItemPickUpRadius.GetValue(), _picUpItemMask);

        ItemPickUp pickUpItem;

        foreach (Collider collider in itemsPickUp) 
        { 
            if(collider.TryGetComponent<ItemPickUp>(out pickUpItem))
            {
                pickUpItem.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_characterStats == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 
            _characterStats.ItemPickUpRadius.GetValue());
    }

    public void TakeDamage(float damage)
    {
        _characterStats?.TakeDamage(damage);
        Debug.Log("Player Bullet Damage");
    }
}
