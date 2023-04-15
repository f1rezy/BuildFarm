using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;

    public void Show()
    {
        _movement.enabled = false;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _movement.enabled = true;
        gameObject.SetActive(false);
    }
}
