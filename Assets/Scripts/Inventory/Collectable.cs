using System;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] public ItemModel item;
    [SerializeField] public Inventory inventory;
    private InventoryController _inventoryController;
    public event Action<ItemType> collectedEvent;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //collectedEvent?.Invoke(item.Type);
            inventory.ItemCollected(item.Type, item);
            Debug.Log(item.Type);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _inventoryController = FindObjectOfType<InventoryController>();
    }
}