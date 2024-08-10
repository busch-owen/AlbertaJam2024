using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresHandler : MonoBehaviour
{
    private Camera _camera;
    
    private LayerMask _leftConnectorLayer, _rightConnectorLayer;

    private void Awake()
    {
        _camera = Camera.main;
        _leftConnectorLayer = LayerMask.GetMask("LeftConnector");
        _rightConnectorLayer = LayerMask.GetMask("RightConnector");
    }

    public void CheckConnector()
    {
        //if(Physics.Raycast())
    }
}
