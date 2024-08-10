using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerController2D _playerController2D;
    private Transform _startPos;
    [SerializeField] private float speed;
    [SerializeField] private float friction;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float _groundCheckDistance;
    private Transform _groundCheck;
    [SerializeField]private LayerMask _layerMask;
    private float _horizontal;
    private bool _isRight;
    

    private void Awake()
    {
        _playerController2D = gameObject.AddComponent<PlayerController2D>();
        _rb = gameObject.AddComponent<Rigidbody2D>();
        _startPos = FindObjectOfType<startPos>().transform ;
    }

    private void Update()
    {
        if (_isRight && _horizontal > 0f)
        {
            Flip();
        }
        if (_isRight && _horizontal < 0f)
        {
            Flip();
        }
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
        _horizontal = context.ReadValue<Vector2>().x;
    }
    
    
}
