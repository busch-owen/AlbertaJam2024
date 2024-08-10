using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    private Camera _camera;

    private Vector2 _mousePosition;

    private LayerMask _handleLayer;

    private PanelAnimationControler _animationControler;

    private void Awake()
    {
        _camera = Camera.main;
        _handleLayer = LayerMask.GetMask("Handle");
    }

    public void UpdateMousePosition(Vector2 mousePos)
    {
        _mousePosition = mousePos;
    }

    public void ShootRayAtDoors()
    {
        RaycastHit hit;
        if (!Physics.Raycast(_camera.ScreenPointToRay(_mousePosition), out hit, Mathf.Infinity,_handleLayer)) return;
        _animationControler = hit.transform.GetComponentInParent<PanelAnimationControler>();
        _animationControler.OpenDoor();
    }
}
