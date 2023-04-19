using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinForAdItem : StoreItem
{
    [SerializeField] private Yandex _yandexService;
    [SerializeField] private int _amountCoins = 50;
    protected override void Buy()
    {
    }

    protected override void TryBuy()
    {
        _yandexService.ShowAdvButton(_amountCoins);
        Buy();
    }
}
