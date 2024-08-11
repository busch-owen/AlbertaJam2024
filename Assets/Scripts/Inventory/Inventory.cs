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
    public class Inventory : ScriptableObject
    {
        public Dictionary<ItemType, ItemModel> Items { get; private set; } = new Dictionary<ItemType, ItemModel>();
        public event Action<ItemType> ItemCollectedEvent;

        public InventoryController InventoryController;

        private void OnEnable()
        {
            
        }

        public void ItemCollected(ItemType type, ItemModel model)
        {
            Items.TryAdd(type,model);
            Items[type].Amount++;
            //InventoryController.ItemCollected(type);
            ItemCollectedEvent?.Invoke(type);
            
            Debug.Log(type);
        }

        public int GetAmount(ItemType type)
        {
           
            if  (Items.ContainsKey(type))
                return Items[type].Amount;
            return 0;
        }

        public void AddItemAmount()
        {
            
        }
    }
}