using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _decription;
    [SerializeField] private Button _buyButton;

    public Item item;

    private void Start()
    {
        _title.text = item.Title;
        _decription.text = item.Description;
        _buyButton.onClick.AddListener(Buy);
    }

    private void Buy()
    {
        throw new NotImplementedException();
    }
}
