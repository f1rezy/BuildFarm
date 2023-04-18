using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerInfo
{
    // Пример дата класса
}

public class Progress : MonoBehaviour
{
    public PlayerInfo PlayerInfo;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    public static Progress Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            LoadExtern();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonString);
    }

    public void Load(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
    }
}
