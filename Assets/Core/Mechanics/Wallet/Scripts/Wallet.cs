using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCounter;

    private int _count;

    public Action<int> OnCountChanged;

    public void Add(int count)
    {
        if (count < 0)
            throw new ArgumentException(nameof(count));
        _count += count;
        OnCountChanged?.Invoke(count);
    }

    public void Take(int count)
    {
        if (!IsEnoughToTake(count))
            throw new ArgumentException(nameof(count));
        _count -= count;
        OnCountChanged?.Invoke(count);
    }

    public bool IsEnoughToTake(int count) => count <= _count;

    private void OnEnable()
    {
        OnCountChanged += UpdateText;
    }

    private void OnDisable()
    {
        OnCountChanged -= UpdateText;
    }

    private void UpdateText(int count)
    {
        _textCounter.text = count.ToString();
    }
}
