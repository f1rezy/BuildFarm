using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorGarage : MonoBehaviour, IGarageBuilding
{
    [SerializeField] private TractorSeeder _tractorPrefab;
    [SerializeField] private int _garageCapacity;

    private PoolMono<TractorSeeder> _pool;

    private void Start()
    {
        _pool = new PoolMono<TractorSeeder>(_tractorPrefab, gameObject.transform, _garageCapacity, false, false);
    }

    public bool ContatainsFreeVenicle()
    {
        return _pool.HasFreeElement(out var tractor);
    }

    public IVenicle SetFreeVenicleTarget(IProductiveBuilding target)
    {
        if (_pool.HasFreeElement(out var tractor))
        {
            tractor.SetTarget(target);
            return tractor;
        }
        throw new Exception("no free venicles");
    }
}
