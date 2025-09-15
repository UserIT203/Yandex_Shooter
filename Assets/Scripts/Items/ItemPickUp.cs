using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemPickUp : MonoBehaviour
{
    [Header("Settigs")]
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _stoppingDistance = 0.5f;

    private Transform _target;
    private Item _item;
    private ItemUseContext _context;

    private bool _hasInteract = false;

    private void Update()
    {
        if(_hasInteract == true)
            AttractToPlayer();
    }

    public void Interact() => _hasInteract = true;

    public void Create(Player player, Item item, ItemUseContext context)
    {
        _target = player.transform;
        _item = item;
        _context = context;
    }

    private void AttractToPlayer()
    {
        if (_target == null) return;

        if (Vector3.Distance(transform.position, _target.position) > _stoppingDistance)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                _target.position,
                _speed * Time.deltaTime
            );
        }
        else
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        _item.Use(_context);
        Destroy(gameObject);
    }
}
