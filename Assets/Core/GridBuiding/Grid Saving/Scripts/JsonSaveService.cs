using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSaveService : MonoBehaviour
{
    public string Save(GridInfoBuilder data)
    {
        return JsonUtility.ToJson(data);
    }

    public GridInfoBuilder Get(string json)
    {
        return JsonUtility.FromJson<GridInfoBuilder>(json);
    }
}
