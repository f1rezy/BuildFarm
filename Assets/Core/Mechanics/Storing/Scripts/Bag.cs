using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour, IStorager
{
    [SerializeField] private int _capacity = 20;

    [SerializeField] private int _taken = 0;

    public int Add(int count)
    {
        if (!CanStorage(count))
            count = _capacity - _taken;
        
        _taken += count;
        return count;
    }

    public bool CanStorage(int count)
    {
        return count <= _capacity - _taken;
    }
}
