using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public delegate void Player();

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
    private Projectile _projectile;
    private HpCounter _hpCounter;
    [SerializeField] private CharacterStatsSO characterStats;
    [SerializeField]protected string currentScene;
    private PlayerEventManager _eventManager;
    private LightFlicker _lightFlicker;
    private bool _isDead;
    private bool _sfxHack = false;
    private SceneHandler _sceneHandler;

    private void Update()
    {

        _rb.velocity = new Vector2(_horizontal * speed, _rb.velocity.y);
        if (_isRight && _horizontal > 0f)
        {
            //Flip();
        }
        if (_isRight && _horizontal < 0f)
        {
            //Flip();
        }

        if (_hpCounter.Health <= 0)
        {
            _isDead = true;
            _eventManager.RunPlayerDeath();
            AudioManager.Instance.PlaySFX("sMeow");
            Invoke("SceneChange", 2.0f);
        }
    }

    public void SceneChange()
    {
        _sceneHandler.RunSceneChange();
        _hpCounter.Health = (int)characterStats.Health;
        _hpCounter = FindObjectOfType<HpCounter>();
    }
    
    public void RecalculateHealth()
    {
        _hpCounter.Health = (int)characterStats.Health;
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _eventManager = FindObjectOfType<PlayerEventManager>();
        _lightFlicker = FindObjectOfType<LightFlicker>();
        _sceneHandler = FindObjectOfType<SceneHandler>();
    }

    private void Awake()
    {
        _hpCounter = FindObjectOfType<HpCounter>();
        _hpCounter.Health = (int)characterStats.Health;
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            if (!_lightFlicker.LightsOn)
            {
                _lightFlicker.enabled = true;
            }
            _projectile = other.GetComponent<Projectile>();
            _hpCounter.Health -= (int)_projectile._damage;
           // _hpCounter.RecalculateHealth((int)currentHealth);
            if (!_isDead)
            {
                _eventManager.RunPlayerHit();
            }
        }
    }
    
    
}
