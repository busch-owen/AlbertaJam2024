using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private SceneHandler _sceneHandler;
    [SerializeField] private string scene;
    [SerializeField] private GameObject _nextLevel;
    [SerializeField] private GameObject _currentLevel;
    private PlayerController2D _playerController2D;
    [SerializeField] private InventoryController _inventoryController;
    [SerializeField] private bool isEnd;
    [SerializeField] private string EndScene;
    private Collectable[] _collectables;
    private Sprite _sprite;
    [SerializeField]private Sprite LightSprite;

    private void Start()
    {
        _sprite = GetComponent<Sprite>();
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
        if (other.GetComponent<PlayerController2D>())
        {
            try
            {
                if (isEnd)
                {
                    SceneManager.LoadScene(EndScene);
                }
                if (_inventoryController._inventory.Items[ItemType.Fuse].Amount > 0 && _inventoryController._inventory.Items[ItemType.Pliers].Amount > 0)
                {
                    _playerController2D.RecalculateHealth();
                    NextLevel();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log("you don't have all of the items you need");
            }
            
        }
    }

    private void NextLevel()
    {
        _currentLevel.SetActive(false);
        _nextLevel.SetActive(true);
        _collectables = FindObjectsByType<Collectable>(sortMode: FindObjectsSortMode.None);
        _inventoryController._inventory.Items.Clear();
    }

    private void FixedUpdate()
    {
        if (_inventoryController._inventory.GetAmount(ItemType.Fuse) > 0 &&
            _inventoryController._inventory.GetAmount(ItemType.Pliers) > 0)
        {
            GetComponent<SpriteRenderer>().sprite = LightSprite;

            //_sprite = LightSprite;
        }
            

    }
}
