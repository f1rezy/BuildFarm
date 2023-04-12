using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopBuildingPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void UpdateText(string text)
    {
        _text.text = text;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
