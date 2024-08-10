using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimationControler : MonoBehaviour
{
    private Animator _panelAnimator;
    private static readonly int DoorOpen = Animator.StringToHash("DoorOpen");

    private void OnEnable()
    {
        _panelAnimator = GetComponentInChildren<Animator>();
    }

    public void OpenDoor()
    {
        _panelAnimator.SetBool(DoorOpen, true);
    }

    public void CloseDoor()
    {
        _panelAnimator.SetBool(DoorOpen, false);
    }
}
