using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void _Random();

public class Canvas : MonoBehaviour
{
    [SerializeField] private GameObject _Level;
    private GameManager _gameManager;
    [SerializeField] private GameObject _enemy;
    private PlayerEventManager _eventManager;
    private Screen _screen;

    public void TurnOn()
    {
        _Level.SetActive(true);
        _screen.enabled = true;
    }

    public void EnableEnemy()
    {
        if(_enemy)
            _enemy.SetActive(true);
    }

    public void TurnOff()
    {
        _screen.enabled = false;
        _Level.SetActive(false);
        if(_enemy)
            _enemy.SetActive(false);
    }

    public void Awake()
    {
        _screen = FindObjectOfType<Screen>();
        _eventManager = FindObjectOfType<PlayerEventManager>();
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        _enemy.SetActive(false);
        _Level.SetActive(false);
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.gameStarted.AddListener(TurnOn);
        _eventManager.Random += TurnOff;
    }
}
