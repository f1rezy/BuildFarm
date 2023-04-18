using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void AddCoinsExtern(int value);

    public void ShowAdvButton()
    {
        AddCoinsExtern(100);
    }

    public void AddCoins(int value)
    {
        // Тута начисляем монеты
    }
}
