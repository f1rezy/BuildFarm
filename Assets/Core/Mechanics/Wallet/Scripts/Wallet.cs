using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCounter;

    [SerializeField] private int _count = 150;

    public Action<int> OnCountChanged;

    private void Start()
    {
        _count = PlayerPrefs.GetInt("Wallet", 120);

        OnCountChanged?.Invoke(_count);
    }

    public void Add(int count)
    {
        if (count < 0)
            throw new ArgumentException(nameof(count));
        _count += count;
        OnCountChanged?.Invoke(_count);
    }

    public void Take(int count)
    {
        if (!IsEnoughToTake(count))
            throw new ArgumentException(nameof(count));
        _count -= count;
        Debug.Log(_count);
        OnCountChanged?.Invoke(_count);
    }

    public bool IsEnoughToTake(int count) => count <= _count;

    private void OnEnable()
    {
        OnCountChanged += Save;
        OnCountChanged += UpdateText;
    }

    private void OnDisable()
    {
        OnCountChanged -= Save;
        OnCountChanged -= UpdateText;
    }

    private void Save(int value)
    {
        PlayerPrefs.SetInt("Wallet", value);
    }

    private void UpdateText(int count)
    {
        _textCounter.text = count.ToString();
    }
}
