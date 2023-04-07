using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorageBuilding
{
    public bool IsEnoughSpaceToPut(int count);
    public bool IsEnoughItemsToTake(int count);

    public void PutItems(int count, Action<int> callback = null);
    public void GetItems(int count, Action<int> callback = null);
}
