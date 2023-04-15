using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketInteractableArea : BuildingInteractableArea
{
    [SerializeField] private TopBuildingPopUp _button;

    private void OnEnable()
    {
        OnInteractionStarted += _button.Show;
        OnInteractionFinished += _button.Hide;
    }

    private void OnDisable()
    {
        OnInteractionStarted -= _button.Show;
        OnInteractionFinished -= _button.Hide;
    }
}
