using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Scene();
public class SceneHandler : MonoBehaviour
{

    public event Scene SceneChange ;
    
    public void RunSceneChange()
    {
        InvokeSceneChange();
    }

    protected virtual void InvokeSceneChange()
    {
        SceneChange?.Invoke();
    }
}
