using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public enum ItemType{
    Points, Spinach, Fuse, Pliers,
}
namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "SOs/Inventory", order = 1)]
    public class Inventory: ScriptableObject
    {
        public Dictionary<ItemType, ItemModel> Items { get; private set; } = new Dictionary<ItemType, ItemModel>();
        public event Action<ItemType> ItemCollectedEvent;

        private void OnEnable()
        {
            
        }

        public void ItemCollected(ItemType type)
        {
            ItemCollectedEvent?.Invoke(type);
        }        
    }

    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventoryView _view;

        void Start()
        {
            _inventory.ItemCollectedEvent += ItemCollected;
        }

        private void ItemCollected(ItemType itemType)
        {
            _inventory.Items[itemType].Amount++;
        }
    }

    public class InventoryView: MonoBehaviour
    {
        
    }
}