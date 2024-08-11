using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "SOs/Item", order = 1)]
public class ItemModel : ScriptableObject
{
    public event Action<int> AmountChangeEvent;
    private int _amount;
    [SerializeField]private SceneHandler _sceneHandler;
    [field:SerializeField] public ItemType Type { get; private set; }
    public int Amount
    {
        get => _amount;
        set
        {
            _amount = value;
            AmountChangeEvent?.Invoke(_amount);
        }
    }
    

    private void OnEnable()
    {
        Amount = 0;
        Debug.Log(Amount);
    }

    public void ResetCount()
    {
        Amount = 0;
    }
}



