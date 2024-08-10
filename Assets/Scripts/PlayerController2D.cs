using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    [SerializeField] private float friction;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Transform _groundCheck;
    [SerializeField]private LayerMask _layerMask;
    private float _horizontal;
    private float _vertical;
    private bool _isRight;

    private void Update()
    {

        _rb.velocity = new Vector2(_horizontal * speed, _rb.velocity.y);
        if (_isRight && _horizontal > 0f)
        {
            Flip();
        }
        if (_isRight && _horizontal < 0f)
        {
            Flip();
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckDistance, _layerMask);
    }

    private void Flip()
    {
        _isRight = !_isRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
        }

        if (context.canceled && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("move");
        _horizontal = context.ReadValue<Vector2>().x;
    }
    
    
}
