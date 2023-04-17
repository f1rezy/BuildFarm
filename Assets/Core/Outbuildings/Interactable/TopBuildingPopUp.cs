using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopBuildingPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SmoothShowHide _smoothShowHide;

    public void UpdateText(string text)
    {
        _text.text = text;
    }

    public void Show()
    {
        _smoothShowHide.Show();
    }

    public void Hide()
    {
        _smoothShowHide.Hide();
    }
}
