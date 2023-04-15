using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string MiningTrigger = "Mining";
    private const string IdleTrigger = "Idle";
    private const string RunningTrigger = "Running";
    private const string GatheringTrigger = "Gathering";

    public void SetIdle()
    {
        _animator.SetTrigger(IdleTrigger);
    }

    public void SetRunning()
    {
        _animator.SetTrigger(RunningTrigger);
    }

    public void SetMining()
    {
        _animator.SetTrigger(MiningTrigger);
    }
    
    public void SetGathering()
    {
        _animator.SetTrigger(GatheringTrigger);
    }
}
