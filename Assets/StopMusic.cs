using System.Collections;
using System.Collections.Generic;
using Microlight.MicroAudio;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MicroAudio.StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
