using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    private float _miningSpeed = 1f;
    private IStorager _storager;
    private bool _isMining = false;

    public Action<int> OnMined;

    private void Start()
    {
        _storager = GetComponent<IStorager>();
    }

    private void OnEnable()
    {
        OnMined += Storage;
    }

    private void OnDisable()
    {
        OnMined -= Storage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FieldMineableItem mineable) &&
            _storager.CanStorage(mineable.PlantsPerMine) &&
            !_isMining)
        {
            if (mineable.CanMine())
            {
                OnMined += EnableMining;
                
                _isMining = true;
                mineable.Mine(OnMined, _miningSpeed);

                OnMined -= EnableMining;
            }
        }
    }

    private void EnableMining(int value) => _isMining = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FieldMineableItem mineable) &&
            _storager.CanStorage(mineable.PlantsPerMine))
        {
            mineable.StopMining();
            _isMining = false;
        }
    }

    private void Storage(int count ) => _storager.Add(count);
}
