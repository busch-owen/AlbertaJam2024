using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresHandler : MonoBehaviour
{
    private Camera _camera;
    
    private LayerMask _leftConnectorLayer, _rightConnectorLayer;

    private Vector2 _mousePosition;

    private bool _pullingWire;

    [SerializeField] private LineRenderer topWire, middleWire, bottomWire;

    private void Awake()
    {
        _camera = Camera.main;
        _leftConnectorLayer = LayerMask.GetMask("LeftConnector");
        _rightConnectorLayer = LayerMask.GetMask("RightConnector");
    }

    public void UpdateMousePos(Vector2 mousePos)
    {
        _mousePosition = mousePos;
        if (!_pullingWire) return;

        RaycastHit hit;
        if (Physics.Raycast(_camera.ScreenPointToRay(_mousePosition), out hit, Mathf.Infinity))
        {
            if (topWire.enabled)
            {
                topWire.SetPosition(1, hit.point);
            }
            else if (middleWire.enabled)
            {
                middleWire.SetPosition(1, hit.point);
            }
            else
            {
                bottomWire.SetPosition(1, hit.point);
            }
        }
        
    }
    
    public void CheckConnector()
    {
        RaycastHit hit;
        if (!Physics.Raycast(_camera.ScreenPointToRay(_mousePosition), out hit, Mathf.Infinity,_leftConnectorLayer)) return;
        _pullingWire = true;
        switch (hit.collider.tag)
        {
            case "Top":
            {
                Debug.Log("Hit Top Connector");
                topWire.SetPosition(0, hit.transform.position);
                topWire.SetPosition(1, hit.point);
                topWire.enabled = true;
                break;
            }
            case "Middle":
            {
                Debug.Log("Hit Middle Connector");
                middleWire.SetPosition(0, hit.transform.position);
                middleWire.SetPosition(1, hit.point);
                middleWire.enabled = true;
                break;
            }
            case "Bottom":
            {
                Debug.Log("Hit Bottom Connector");
                bottomWire.SetPosition(0, hit.transform.position);
                bottomWire.SetPosition(1, hit.point);
                bottomWire.enabled = true;
                break;
            }
        }
    }

    public void DropWire()
    {
        _pullingWire = false;
        DisableWires();
        //Also check if connector is correct
    }

    private void DisableWires()
    {
        topWire.enabled = false;
        middleWire.enabled = false;
        bottomWire.enabled = false;
    }
}
