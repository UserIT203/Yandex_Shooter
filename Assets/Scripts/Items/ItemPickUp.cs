using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemPickUp : MonoBehaviour
{
    private readonly float _speed = 5;
    private readonly float _stoppingDistance = 0.5f;

    private Transform _target;
    private Item _item;
    private ItemUseContext _context;
    private FloatingText _floatingText;

    private bool _hasInteract = false;

    private void Update()
    {
        if(_hasInteract == true)
            AttractToPlayer();
    }

    public void Interact() => _hasInteract = true;

    public void Create(Player player, Item item, ItemUseContext context, FloatingText floatingText)
    {
        _target = player.transform;
        _item = item;
        _context = context;
        _floatingText = floatingText;
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

    private void CreateFloatingText()
    {
        FloatingText floatingText = Instantiate(_floatingText, transform.position, Quaternion.identity);
        floatingText.SetSettings(_item.Value, _item.FloatingTextColor);
    }

    private void UseItem()
    {
        _item.Use(_context);
        CreateFloatingText();
        Destroy(gameObject);
    }
}
