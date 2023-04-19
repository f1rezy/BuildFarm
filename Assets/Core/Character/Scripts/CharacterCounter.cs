using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCounter : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void OnEnable()
    {
        _bag.OnChanged += SetText;
    }

    private void OnDisable()
    {
        _bag.OnChanged = SetText;
    }

    public void SetText(int count)
    {
        _textMeshPro.text = $"{count}/{_bag.Capacity}";
    }
}
