using System;
using Microlight.MicroAudio;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] public Sound[] musicSounds, sfxSounds;

    public void PlayMusic(string name)
    {
        Sound m = Array.Find(musicSounds, x => x.name == name);
        if (m == null)
        {
            Debug.Log("No Music! Check your filenames");
        }
        else
        {
            MicroAudio.StopMusic();
            MicroAudio.PlayOneTrack(m.clip);
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name); 
        Debug.Log($"Asked to play {name}");
        if (s == null)
        {
            Debug.Log(sfxSounds + name);
            Debug.Log("No Sound! Check your filenames");
        }
        else
        {
            MicroAudio.PlayEffectSound(s.clip);
        }
    }
}