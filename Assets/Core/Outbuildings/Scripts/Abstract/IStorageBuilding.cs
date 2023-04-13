using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorageBuilding
{
    public bool IsEnoughSpaceToPut(int count);
    public bool IsEnoughItemsToTake(int count);

    public int PutItems(int count, Action<int> callback = null);
    public int GetItems(int count, Action<int> callback = null);
}
