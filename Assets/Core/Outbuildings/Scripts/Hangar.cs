using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar : MonoBehaviour, IStorageBuilding, IPopUpSupporter
{
    [SerializeField] private int _storageCapacity;

    private int _takenSpace;

    public int Count => _takenSpace;
    public event Action OnCountChanged;

    private void Start()
    {
        OnCountChanged?.Invoke();
    }

    public int GetItems(int count, Action<int> callback = null)
    {
        if (!IsEnoughItemsToTake(count))
            count = _takenSpace;

        _takenSpace -= count;
        callback?.Invoke(count);
        OnCountChanged?.Invoke();

        return count;
    }

    public int PutItems(int count, Action<int> callback = null)
    {
        if (!IsEnoughSpaceToPut(count))
            count = _storageCapacity - _takenSpace;

        _takenSpace += count;
        callback?.Invoke(count);
        OnCountChanged?.Invoke();

        return count;
    }

    public bool IsEnoughItemsToTake(int count)
    {
        return _takenSpace >= count;
    }

    public bool IsEnoughSpaceToPut(int count)
    {
        return _storageCapacity - _takenSpace >= count;
    }

    public string GetPopUpText()
    {
        return $"{_takenSpace}/{_storageCapacity}";
    }
}
