using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    private SceneHandler _sceneHandler;
    [SerializeField] private string scene;
    [SerializeField] private GameObject _nextLevel;
    [SerializeField] private GameObject _currentLevel;

    private void Start()
    {
        _sceneHandler = FindObjectOfType<SceneHandler>();
        _sceneHandler.SceneChange += NextScene;
    }


    public void NextScene()
    {
        SceneManager.LoadScene(scene);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        _nextLevel.SetActive(true);
        _currentLevel.SetActive(false);
    }
    
    
}
