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
            //LoadExtern();
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string jsonString = GridInfoSaver.SaveGridInfoToText(gridInfo);
        //SaveExtern($"{{{jsonString}}}");

        PlayerPrefs.SetString("SaveFile", jsonString);
    }

    public void Load()
    {
        string value;

        value = PlayerPrefs.GetString("SaveFile", string.Empty);
        gridInfo = GridInfoSaver.LoadGridInfoToText(value);
    }
}
