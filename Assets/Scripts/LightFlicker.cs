using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public bool LightsOn;
    private PlayerEventManager _eventManager;
    [SerializeField]private GameObject _gameObjects;
    [SerializeField]private float FlickerSpeed;
    [SerializeField] private Light _light;
    [SerializeField] private Light ScreenLight;
    [SerializeField] private float OnIntensity, OffIntesity;
    [SerializeField] private int NumberOfFlickers;
    
    

    private void Start()
    {
        _eventManager = FindObjectOfType<PlayerEventManager>();
        _eventManager.PlayerHit += LightFLicker;
        _eventManager.PlayerDeath += LightsOff;
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
        _light.intensity = 0;
        Invoke("ScreenOff", 1.0f);
    }

    public void ScreenOff()
    {
        if(ScreenLight)
            ScreenLight.intensity = 0.5f;
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
