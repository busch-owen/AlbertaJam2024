using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoorespondingColor
{
    Red, Blue, Yellow
}

public class WireConnector : MonoBehaviour
{
    [field: SerializeField] public CoorespondingColor wireColor { get; private set; }
    
    public bool ConnectedCorrectly { get; private set; }

    public void ConnectWire()
    {
        ConnectedCorrectly = true;
    }

    public void ResetConnections()
    {
        ConnectedCorrectly = false;
    }
}
