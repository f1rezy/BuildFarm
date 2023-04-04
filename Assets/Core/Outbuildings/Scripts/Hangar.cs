using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar : MonoBehaviour, IStorageBuilding
{
    [SerializeField] private int _storageCapacity;

    private int _takenSpace;

    public void GetItems(int count, Action<int> callback = null)
    {
        if (!IsEnoughItemsToTake(count))
            count = _takenSpace;

        _takenSpace -= count;
        callback?.Invoke(count);
    }

    public void PutItems(int count, Action<int> callback = null)
    {
        if (!IsEnoughSpaceToPut(count))
            count = _storageCapacity - _takenSpace;

        _takenSpace += count;
        callback?.Invoke(count);
    }

    public bool IsEnoughItemsToTake(int count)
    {
        return _takenSpace >= count;
    }

    public bool IsEnoughSpaceToPut(int count)
    {
        return _storageCapacity - _takenSpace >= count;
    }
}
