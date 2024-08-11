using System;
using System.Collections;
using System.Collections.Generic;
using Microlight.MicroAudio;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] private AudioClip clicksound;

    private bool _rotatecat = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_rotatecat == true)
        {
            transform.Rotate(0,0,128);
        }
        
    }

    public void OnMouseDown()
    {
        _rotatecat = true;
        MicroAudio.PlayUISound(clicksound);
    }
}
