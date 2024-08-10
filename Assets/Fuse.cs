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

    public bool IsBroken { get; private set; }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _inputHandler = FindObjectOfType<InputHandler>();
        _waitForSwapTime = new WaitForSeconds(swapTime);
    }

    public void BreakFuse()
    {
        IsBroken = true;
    }

    public void Swap()
    {
        StartCoroutine(SwapFuse());
    }

    private IEnumerator SwapFuse()
    {
        _anim.SetTrigger(SwapFuses);
        _anim.SetBool(GoodFuse, true);
        _inputHandler.DisableInput();
        yield return _waitForSwapTime;
        IsBroken = false;
        _inputHandler.EnableInput();
    }
}
