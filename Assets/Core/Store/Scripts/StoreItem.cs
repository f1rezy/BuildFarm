using UnityEngine;
using UnityEngine.UI;

public abstract class StoreItem : MonoBehaviour
{
    [SerializeField] protected int _cost;
    [SerializeField] protected Button _buyButton;
    
    protected void Start()
    {
        _buyButton.onClick.AddListener(Buy);
    }

    protected virtual void Buy()
    {
        Debug.Log($"I bought for: {_cost.ToString()}");
    }
}
