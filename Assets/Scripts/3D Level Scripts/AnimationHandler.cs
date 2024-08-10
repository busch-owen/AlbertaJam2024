using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _cameraAnimator;
    private static readonly int PullToRight = Animator.StringToHash("PullToRight");
    private static readonly int PullToLeft = Animator.StringToHash("PullToLeft");
    private static readonly int ToCenter = Animator.StringToHash("ReturnToCenter");
    private static readonly int InGame = Animator.StringToHash("InGame");

    private void Awake()
    {
        _cameraAnimator = Camera.main?.GetComponent<Animator>();
    }

    public void MoveRight()
    {
        _cameraAnimator.SetTrigger(PullToRight);
    }

    public void MoveLeft()
    {
        _cameraAnimator.SetTrigger(PullToLeft);
    }

    public void ReturnToCenter()
    {
        _cameraAnimator.SetTrigger(ToCenter);
    }

    public void PullIntoCabinet()
    {
        _cameraAnimator.SetBool(InGame, true);
    }

    public void PullAwayFromCabinet()
    {
        _cameraAnimator.SetBool(InGame, false);
    }
}
