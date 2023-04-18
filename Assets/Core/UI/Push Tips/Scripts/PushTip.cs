using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTip : MonoBehaviour
{
    [SerializeField] private BuildingInteractableArea _interactable;
    private SmoothShowHide _smoothShowHide;

    private void Awake()
    {
        _smoothShowHide = GetComponent<SmoothShowHide>();
    }

    private void OnEnable()
    {
        _interactable.OnInteractionStarted += _smoothShowHide.Show;
        _interactable.OnInteractionFinished += _smoothShowHide.Hide;
    }

    private void OnDisable()
    {
        _interactable.OnInteractionStarted -= _smoothShowHide.Show;
        _interactable.OnInteractionFinished -= _smoothShowHide.Hide;
    }
}
