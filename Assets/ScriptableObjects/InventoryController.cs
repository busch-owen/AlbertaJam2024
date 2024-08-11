using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] public Inventory _inventory;
    [SerializeField] private InventoryView _view;

    void Start()
    {
        //_inventory.ItemCollectedEvent += ItemCollected;
    }

    public void ItemCollected(ItemType itemType)
    {
        _inventory.Items[itemType].Amount++;
        Debug.Log(_inventory.Items);
    }
    
}


