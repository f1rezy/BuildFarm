using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string MiningBool = "MiningBool";
    private const string IdleTrigger = "Idle";
    private const string RunningTrigger = "Running";
    private const string GatheringBool = "GatheringBool";

    public void SetIdle()
    {
        _animator.SetBool(MiningBool, false);
        _animator.SetBool(GatheringBool, false);
        _animator.SetTrigger(IdleTrigger);
    }

    public void SetRunning()
    {
        _animator.SetBool(MiningBool, false);
        _animator.SetBool(GatheringBool, false);
        _animator.SetTrigger(RunningTrigger);
    }

    public void SetMining()
    {
        _animator.SetBool(MiningBool, true);
    }
    
    public void SetGathering()
    {
        _animator.SetBool(GatheringBool, true);
    }
}
