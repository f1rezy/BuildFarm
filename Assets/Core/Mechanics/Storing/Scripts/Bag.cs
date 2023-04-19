using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour, IStorager
{
    [SerializeField] private int _capacity = 20;

    [SerializeField] private int _taken = 0;

    public Action<int> OnChanged;
    public int Capacity => _capacity;

    public int Add(int count)
    {
        if (!CanStorage(count))
            count = _capacity - _taken;
        
        _taken += count;
        OnChanged?.Invoke(_taken);
        return count;
    }

    public int Take(int count)
    {
        if (!CanTake(count))
            count = _taken;

        _taken -= count;
        OnChanged?.Invoke(_taken);
        return count;
    }

    public bool CanStorage(int count)
    {
        return count <= _capacity - _taken;
    }

    public bool CanTake(int count)
    {
        return count <= _taken;
    }

    public int GetCount()
    {
        return _taken;
    }
}
