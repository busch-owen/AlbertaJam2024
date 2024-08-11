using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private string scene;
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
}
