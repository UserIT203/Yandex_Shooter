using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LootBag : MonoBehaviour
{
    [SerializeField] private List<ItemInLootBag> _items;
    [SerializeField] private int _maxCreateItem;
    
    [Header("Spawner Settings")]
    [SerializeField] private float _createItemRadius;
    [SerializeField] private float _minDistanceBetweenObjects;

    private int _currentItemsCount = 0;
    private ItemInLootBag[] _droppedItems;
    private List<Vector3> _spawnedPositions = new List<Vector3>();


    private void OnValidate()
    {
        if (_items == null || _items.Count == 0)
        {
            _maxCreateItem = 0;
            return;
        }

        _maxCreateItem = Mathf.Clamp(_maxCreateItem, 0, _items.Count);
    }

    private void Start()
    {
        InitializeItem();
    }

    private void InitializeItem()
    {
        _droppedItems = new ItemInLootBag[_maxCreateItem];
        float randomValue = Random.Range(0, 100f);

        _items.OrderBy(item => item.Probability);

        foreach (ItemInLootBag item in _items) 
        { 
            if(item.Probability >= randomValue)
            {
                if (_currentItemsCount >= _maxCreateItem) break;

                _droppedItems[_currentItemsCount] = item;
                _currentItemsCount++;
            }
        }
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach(Vector3 objectPosition in _spawnedPositions)
        {
            if (Vector3.Distance(position, objectPosition) < _minDistanceBetweenObjects)
                return false;
        }

        return true;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;

        do
        {
            Vector2 randomCircle = Random.insideUnitCircle * _createItemRadius;
            randomPosition = transform.position + new Vector3(randomCircle.x, 0f,
                randomCircle.y);

        } while (IsPositionValid(randomPosition) == false);
    
        return randomPosition;
    }

    public void CreateItems(Player player, ItemUseContext context)
    {
        _spawnedPositions.Clear();

        for (int i = 0; i < _droppedItems.Length; i++)
        {
            Vector3 randomPosition = GetRandomPosition();

            if (_droppedItems[i] == null) continue;

            GameObject obj = Instantiate(_droppedItems[i].Item.ItemPrefab, randomPosition, Quaternion.identity);
            obj.AddComponent<ItemPickUp>().Create(player, _droppedItems[i].Item, context);
            _spawnedPositions.Add(randomPosition);
        }
    }
}

[System.Serializable]
public class ItemInLootBag
{
    public Item Item;
    [Range(0, 100)] public float Probability;
}
