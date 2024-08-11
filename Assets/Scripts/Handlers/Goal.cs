using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    private SceneHandler _sceneHandler;
    [SerializeField] private string scene;
    [SerializeField] private GameObject _nextLevel;
    [SerializeField] private GameObject _currentLevel;
    private PlayerController2D _playerController2D;
    [SerializeField] private InventoryController _inventoryController;

    private void Start()
    {
        _sceneHandler = FindObjectOfType<SceneHandler>();
        _sceneHandler.SceneChange += NextScene;
        _playerController2D = FindObjectOfType<PlayerController2D>();
    }


    public void NextScene()
    {
        SceneManager.LoadScene(scene);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(_inventoryController._inventory.Items[ItemType.Fuse].Amount);
            if (_inventoryController._inventory.Items[ItemType.Fuse].Amount > 0)
            {
                _playerController2D.RecalculateHealth();
                NextLevel();
            }
        }
    }

    private void NextLevel()
    {
        _nextLevel.SetActive(true);
        _currentLevel.SetActive(false);
    }
    
    
}
