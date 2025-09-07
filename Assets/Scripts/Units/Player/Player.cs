using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IDamagable, IItemHandler
{
    [Header("Player Settings")]
    [SerializeField] private LayerMask _picUpItemMask;

    private PlayerStats _characterStats;
    private GameTimeManager _timeManager;

    public bool IsFreeze { get; private set; }
    public PlayerStats Stats => _characterStats;

    public event Action<float> onTakeDamage;

    [Inject]
    public void Construct(PlayerConfig playerConfig, GameTimeManager timeManager)
    {
        _characterStats = new PlayerStats(playerConfig);
        _characterStats.onDie += Die;

        _timeManager = timeManager;

        playerConfig.Ultimate.Initialized(this);
    }


    private void OnEnable()
    {
        _timeManager.OnGamePaused += Freeze;
        _timeManager.OnGameResumed += Unfreeze;
    }

    private void OnDisable()
    {
        _timeManager.OnGamePaused -= Freeze;
        _timeManager.OnGameResumed -= Unfreeze;
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

    private void Freeze() => IsFreeze = true;

    private void Unfreeze() => IsFreeze = false;

    private void OnDrawGizmosSelected()
    {
        if (_characterStats == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 
            _characterStats.ItemPickUpRadius.GetValue());
    }

    public void TakeDamage(float damage)
    {
        onTakeDamage?.Invoke(damage);
        _characterStats?.TakeDamage(damage);
    }

    public void HandleActionWithValue(float value)
    {
        _characterStats.Heal(value);
    }
}
