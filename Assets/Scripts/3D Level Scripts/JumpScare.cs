using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScare : MonoBehaviour
{
    [SerializeField]private GameObject _light;
    [SerializeField] private float timeToScare;
    [SerializeField] private Animator _animator;
    private LightFlicker _flicker;
    [SerializeField] private float DisapearTime;
    [SerializeField]private GameObject _catMan;
    [SerializeField] private string endScene;

    private void LightsOut()
    {
        _light.GetComponent<Light>().intensity = 0f;
    }

    private void Start()
    {
        _flicker = FindObjectOfType<LightFlicker>();
        _flicker.LightFLicker();
        //_animator.enabled = false;
        Invoke("Inital",0.2f);
        Invoke("FlickerAgain", 1.5f);
        Invoke("LightsOut", timeToScare);
        Invoke("Disapear", DisapearTime);
        Invoke("StartScare", 2.5f);
        Invoke("DisableCat", 4.5f);
        Invoke("Restart", 5.5f);
        _animator.speed = 0.2f;

    }

    private void DisableCat()
    {
        _catMan.SetActive(false);
    }

    private void Restart()
    {
        SceneManager.LoadScene(endScene);
    }

    private void Inital()
    {
        _catMan.SetActive(false);
    }

    private void Disapear()
    {
        _flicker.LightFLicker();
        _catMan.SetActive(true);
        _animator.speed = 0.5f;
        
    }

    private void FlickerAgain()
    {
        Invoke("Disapear", DisapearTime);
        _flicker.LightFLicker();
        _catMan.SetActive(false);
        _animator.speed = 1.0f;
    }

    private void StartScare()
    {
        _catMan.SetActive(true);
        _animator.enabled = true;
        _animator.speed = 2.0f;
    }
}
