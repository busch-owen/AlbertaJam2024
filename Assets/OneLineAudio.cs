using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneLineAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySFX("sHooray");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
