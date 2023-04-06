using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string IdleTrigger = "Idle";
    private const string RunningTrigger = "Running";

    public void SetIdle()
    {
        _animator.SetTrigger(IdleTrigger);
    }

    public void SetRunning()
    {
        _animator.SetTrigger(RunningTrigger);
    }
}
