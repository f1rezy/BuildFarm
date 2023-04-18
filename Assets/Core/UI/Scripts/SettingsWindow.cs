 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private SmoothShowHide _smoothShowHide;

    public void Show()
    {
        _movement.enabled = false;
        _smoothShowHide.Show();
    }

    public void Hide()
    {
        _movement.enabled = true;
        _smoothShowHide.Hide();
    }
}
