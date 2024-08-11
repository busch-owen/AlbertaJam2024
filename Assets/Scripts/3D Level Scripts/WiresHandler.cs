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
    
    private WireConnector _startConnector;
    private WireConnector _endConnector;

    private GameManager _gameManager;
    private InputHandler _inputHandler;

    [field: SerializeField] public MeshRenderer[] LightRenderers { get; private set; }
    [field: SerializeField] public Material[] LightMaterials { get; private set; }

    [SerializeField] private LineRenderer topWire, middleWire, bottomWire;
    private Vector3 _topWireStartPos, _middleWireStartPos, _bottomWireStartPos;

    private LineRenderer _selectedWire;
    
    [field: SerializeField] public ParticleSystem Sparks { get; private set; }

    private void Awake()
    {
        _camera = Camera.main;
        _gameManager = FindObjectOfType<GameManager>();
        _inputHandler = FindObjectOfType<InputHandler>();
        _leftConnectorLayer = LayerMask.GetMask("LeftConnector");
        _rightConnectorLayer = LayerMask.GetMask("RightConnector");
        _topWireStartPos = topWire.GetPosition(0);
        _middleWireStartPos = middleWire.GetPosition(0);
        _bottomWireStartPos = bottomWire.GetPosition(0);
    }

    public void UpdateMousePos(Vector2 mousePos)
    {
        _mousePosition = mousePos;
        if (!_pullingWire) return;

        RaycastHit hit;
        if (Physics.Raycast(_camera.ScreenPointToRay(_mousePosition), out hit, Mathf.Infinity))
        {
            if (_startConnector.ConnectedCorrectly) return;
            _selectedWire.SetPosition(1, hit.point);
        }
    }
    
    public void CheckConnector()
    {
        RaycastHit hit;
        if (!Physics.Raycast(_camera.ScreenPointToRay(_mousePosition), out hit, Mathf.Infinity,_leftConnectorLayer)) return;
        _pullingWire = true;
        
        _startConnector = hit.collider.GetComponent<WireConnector>();
        
        if(!_startConnector) return;
        
        if (_startConnector.ConnectedCorrectly) return;
        
        switch (hit.collider.tag)
        {
            case "Top":
            {
                _selectedWire = topWire;
                topWire.SetPosition(0, hit.transform.position);
                topWire.SetPosition(1, hit.point);
                topWire.enabled = true;
                break;
            }
            case "Middle":
            {
                _selectedWire = middleWire;
                middleWire.SetPosition(0, hit.transform.position);
                middleWire.SetPosition(1, hit.point);
                middleWire.enabled = true;
                break;
            }
            case "Bottom":
            {
                _selectedWire = bottomWire;
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
        RaycastHit hit;
        if(!_startConnector) return; 
        if (Physics.Raycast(_camera.ScreenPointToRay(_mousePosition), out hit, Mathf.Infinity, _rightConnectorLayer))
        {
            _endConnector = hit.collider.GetComponent<WireConnector>();
            if (_startConnector.WireColor == _endConnector.WireColor)
            {
                //Do something to tell the game that this wire is connected
                _startConnector.ConnectWire();
                _endConnector.ConnectWire();
                _gameManager.CheckWireCompletion();
                _selectedWire = null;
            }
            else
            {
                _gameManager.CheckWireCompletion();
                if (!_selectedWire) return;
                _selectedWire.enabled = false;
                _selectedWire = null;
            }
        }
        else
        {
            _gameManager.CheckWireCompletion();
            if (!_selectedWire) return;
            _selectedWire.enabled = false;
            _selectedWire = null;
        }
    }

    private void DisableAllWires()
    {
        topWire.enabled = false;
        middleWire.enabled = false;
        bottomWire.enabled = false;
    }

    public void ResetWirePositions()
    {
        topWire.SetPosition(0, _topWireStartPos);
        topWire.SetPosition(1, _topWireStartPos);
        middleWire.SetPosition(0, _middleWireStartPos);
        middleWire.SetPosition(1, _middleWireStartPos);
        bottomWire.SetPosition(0, _bottomWireStartPos);
        bottomWire.SetPosition(1, _bottomWireStartPos);
    }
}
