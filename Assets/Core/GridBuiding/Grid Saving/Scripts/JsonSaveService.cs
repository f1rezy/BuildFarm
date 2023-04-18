using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSaveService : MonoBehaviour
{
    public string Save(GridInfo data)
    {
        return JsonUtility.ToJson(data);
    }

    public GridInfo Get(string json)
    {
        return JsonUtility.FromJson<GridInfo>(json);
    }
}
