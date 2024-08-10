using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    [SerializeField] private float swapTime;
    private Animator _anim;
    private WaitForSeconds _waitForSwapTime;
    private static readonly int SwapFuses = Animator.StringToHash("SwapFuses");
    private static readonly int GoodFuse = Animator.StringToHash("GoodFuse");

    private InputHandler _inputHandler;
    
    private GameManager _gameManager;

    public bool IsBroken { get; private set; }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _inputHandler = FindObjectOfType<InputHandler>();
        _gameManager = FindObjectOfType<GameManager>();
        _waitForSwapTime = new WaitForSeconds(swapTime);
        _anim.SetBool(GoodFuse, true);
    }

    public void BreakFuse()
    {
        IsBroken = true;
        _anim.SetBool(GoodFuse, false);
    }

    public void Swap()
    {
        if(IsBroken)
            StartCoroutine(SwapFuse());
    }

    private IEnumerator SwapFuse()
    {
        _anim.SetTrigger(SwapFuses);
        _anim.SetBool(GoodFuse, true);
        _inputHandler.DisableInput();
        yield return _waitForSwapTime;
        IsBroken = false;
        _gameManager.CheckFuseCompletion();
    }
}
