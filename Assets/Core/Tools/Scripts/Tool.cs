using System;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private float _minDistaneToMine;
    private float _miningSpeed = 0.001f;
    public IStorager _storager;
    private bool _isMining = false;

    public float MinDistanceToMine => _minDistaneToMine;
    
    public event Action<int> OnMined;
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
                
                _isMining = false;
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
