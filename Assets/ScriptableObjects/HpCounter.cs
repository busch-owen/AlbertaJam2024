using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HpCounter : MonoBehaviour
{
    [SerializeField] private Image tensDigit;

    [FormerlySerializedAs("HpSprites")] [SerializeField] private GameObject[] hpSprites;
    private int _health = 0;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Math.Min(value, 3);
            //Debug.Log($"Health set to {value}");
            RecalculateHealth(_health);
        }
        
    }

    public void RecalculateHealth(int energyToCheck)
    {
        
        for (int i = 0; i < _health; i++)
        {
            hpSprites[i].SetActive(true);
            //Debug.Log($"Hearts added at {i}");
        }

        for (int i = _health; i < hpSprites.Length; i++)
        {
            hpSprites[i].SetActive(false);
            //Debug.Log($"Hearts removed at {i}");
            //change hearts here
        }
    }


}
