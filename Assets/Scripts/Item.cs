using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "SOs/Item", order = 1)]
public class ItemModel : ScriptableObject
{
    public event Action<int> AmountChangeEvent;
    private int _amount;
    [field:SerializeField] public ItemType Type { get; private set; }
    [field:SerializeField]
    [field:SerializeField]
    public int Amount
    {
        get => _amount;
        set
        {
            _amount = value;
            AmountChangeEvent?.Invoke(_amount);
        }
    }
}



