using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorager
{
    public bool CanStorage(int count);

    public int Add(int count);
    public int Take(int count);

    public int GetCount();
}
