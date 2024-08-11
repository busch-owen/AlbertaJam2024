using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    [SerializeField] private GameObject _Level;
    private GameManager _gameManager;
    [SerializeField] private GameObject _enemy;

    public void TurnOn()
    {
        _Level.SetActive(true);
    }

    public void EnableEnemy()
    {
        if(_enemy)
            _enemy.SetActive(true);
    }

    public void Awake()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        _enemy.SetActive(false);
        _Level.SetActive(false);
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.gameStarted.AddListener(TurnOn);
    }
}
