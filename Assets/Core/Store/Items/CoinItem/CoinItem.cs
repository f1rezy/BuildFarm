using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : StoreItem
{
    [SerializeField] private Bag _bag;
    [SerializeField] private int _amountCoins = 30;
    protected override void Buy()
    {
        _wallet.Add(_amountCoins);
    }

    protected override void TryBuy()
    {
        if (_bag.CanTake(_cost))
        {
            _bag.Take(_cost);
            Buy();
        }
    }
}
