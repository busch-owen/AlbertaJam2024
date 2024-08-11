using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public bool LightsOn;
    private PlayerEventManager _eventManager;
    [SerializeField]private float FlickerSpeed;
    [SerializeField] private Light _light;
    [SerializeField] private Light ScreenLight;
    [SerializeField] private float OnIntensity, OffIntesity;
    [SerializeField] private int NumberOfFlickers;
    private Screen _screen;
    private Canvas _canvas;
    private HpCounter _hpCounter;
    
    

    private void Start()
    {
        _eventManager = FindObjectOfType<PlayerEventManager>();
        _screen = FindObjectOfType<Screen>();
        _canvas = FindObjectOfType<Canvas>();
        _eventManager.PlayerHit += LightFLicker;
        _eventManager.PlayerDeath += LightsOff;
    }

    private void Awake()
    {
        _hpCounter = FindObjectOfType<HpCounter>();

    }


    public void LightFLicker()
    {
        if (LightsOn)
        {
            StartCoroutine("FlickerLights");
        }
    }

    public void LightsOff()
    {
        if (_light != null)
        {
            _light.intensity = 0;
            Invoke("TurnOff",1.0f);
            Invoke("ScreenOff", 1.5f);
        }
    }

    public void ScreenOff()
    {
        if(ScreenLight)
            ScreenLight.intensity = 0.5f;
    }

    public void TurnOff()
    {
        _screen.gameObject.SetActive(false);
        _canvas.gameObject.SetActive(false);
        _energyCounter?.gameObject.SetActive(false);
    }

    IEnumerator FlickerLights()
    {
        LightsOn = false;
        for (var i = 0; i < NumberOfFlickers; i++)
        {
            _light.intensity = OnIntensity;
            yield return new WaitForSeconds(FlickerSpeed);
            _light.intensity = OffIntesity;
            yield return new WaitForSeconds(FlickerSpeed);
        }
        _light.intensity = OnIntensity;
        LightsOn = true;
    }
}
