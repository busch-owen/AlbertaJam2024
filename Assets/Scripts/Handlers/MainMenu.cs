using System;
using System.Collections;
using System.Collections.Generic;
using Microlight.MicroAudio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private string scene;
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }
   

    public void OnEnable()
    {
        MicroAudio.StopMusic();
    }
}
