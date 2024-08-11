using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusesHandler : MonoBehaviour
{
    private Camera _camera;
    private LayerMask _fusesLayer;
    private Vector2 _mousePosition;
    

    private void Awake()
    {
        _camera = Camera.main;
        
        _fusesLayer = LayerMask.GetMask("Fuse");
    }
    
    public void UpdateMousePosition(Vector2 mousePos)
    {
        _mousePosition = mousePos;
    }

    public void ClickFuse()
    {
        RaycastHit hit;
        if (!Physics.Raycast(_camera.ScreenPointToRay(_mousePosition), out hit, Mathf.Infinity, _fusesLayer)) return;

        var currentFuse = hit.transform.GetComponent<Fuse>();
        currentFuse.Swap();
    }
}
