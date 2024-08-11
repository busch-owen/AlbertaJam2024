using System;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] public ItemModel item;
    [SerializeField] public Inventory inventory;
    public event Action<ItemType> collectedEvent;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //collectedEvent?.Invoke(item.Type);
            inventory.ItemCollected(item.Type);
            Destroy(this.gameObject);
        }
    }
}