using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventManager : MonoBehaviour
{

    public void RunPlayerDeath()
    {
        InvokePlayerDeath();
    }

    public event Player PlayerDeath;

    protected virtual void InvokePlayerDeath()
    {
        PlayerDeath?.Invoke();
    }

    public event Player PlayerHit;

    public void RunPlayerHit()
    {
        InvokePlayerHit();
    }

    protected virtual void InvokePlayerHit()
    {
        PlayerHit?.Invoke();
    }
}
