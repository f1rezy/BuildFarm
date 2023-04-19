using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [DllImport("__Internal")]
    private static extern void AddCoinsExtern(int value);

    public void ShowAdvButton(int value)
    {
        AddCoinsExtern(value);
    }

    public void AddCoins(int value)
    {
        _wallet.Add(value);
    }
}
