using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CorrespondingColor
{
    Red, Blue, Yellow
}

public class WireConnector : MonoBehaviour
{
    [field: SerializeField] public CorrespondingColor WireColor { get; private set; }
    
    public bool ConnectedCorrectly { get; private set; }

    public void ConnectWire()
    {
        ConnectedCorrectly = true;
    }

    public void ResetConnections()
    {
        ConnectedCorrectly = false;
    }

    public void ChangeColor(CorrespondingColor color)
    {
        WireColor = color;
    }
}
