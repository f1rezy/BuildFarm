using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMineableItem : MonoBehaviour
{
    [SerializeField] private float _miningTime = 1f;
    [SerializeField] private float _growthTime = 60f;

    [SerializeField] private FieldItemAnimator _animator;

    private float _growthProgress = 0f;
    private int _plantsCount = 3;

    public int PlantsPerMine => _plantsCount;
    public Action<float> OnGrowthProgressChanged;

    public void Mine(Action<int> onMined = null, float miningSpeed = 1f)
    {
        if (CanMine())
        {
            StartCoroutine(StartMining(miningSpeed, onMined));
        }
    }

    public void StopMining()
    {
        StopAllCoroutines();
    }

    public bool CanMine()
    {
        return _growthProgress >= 1;
    }

    private void OnEnable()
    {
        OnGrowthProgressChanged += _animator.Animate;
    }

    private void OnDisable()
    {
        OnGrowthProgressChanged -= _animator.Animate;
    }

    private void FixedUpdate()
    {
        if (_growthProgress < 1)
            _growthProgress += (Time.deltaTime / _growthTime);
        else
            _growthProgress = 1f;

        OnGrowthProgressChanged?.Invoke(_growthProgress);
    }

    private IEnumerator StartMining(float miningSpeed, Action<int> onMined)
    {
        StartCoroutine(_animator.AnimateMining(_miningTime * miningSpeed));
        yield return new WaitForSeconds(_miningTime * miningSpeed);

        onMined?.Invoke(_plantsCount);
        _growthProgress = 0f;
        OnGrowthProgressChanged?.Invoke(_growthProgress);
    }
}
