using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public GridInfo gridInfo;

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
            //Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string jsonString = GridInfoSaver.SaveGridInfoToText(gridInfo);
        GridInfoData gridInfoData = new GridInfoData();
        gridInfoData.Grid = jsonString;
        string json = JsonUtility.ToJson(gridInfoData);
        SaveExtern(json);

        //PlayerPrefs.SetString("SaveFile", jsonString);
    }

    public void Load(string value)
    {
        //value = PlayerPrefs.GetString("SaveFile", string.Empty);
        var gridInfoData = JsonUtility.FromJson<GridInfoData>(value);
        gridInfo = GridInfoSaver.LoadGridInfoToText(gridInfoData.Grid);
    }
}
