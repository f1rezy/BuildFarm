using UnityEngine;
using UnityEngine.UI;

public abstract class StoreItem : MonoBehaviour
{
    [SerializeField] protected int _cost;
    [SerializeField] protected Button _buyButton;

    [SerializeField] protected Wallet _wallet;
    
    protected void Start()
    {
        _buyButton.onClick.AddListener(TryBuy);
    }

    protected virtual void Buy()
    {
        
    }

    protected virtual void TryBuy()
    {
        if (_wallet.IsEnoughToTake(_cost))
        {
            _wallet.Take(_cost);
            Buy();
        }
    }
}
