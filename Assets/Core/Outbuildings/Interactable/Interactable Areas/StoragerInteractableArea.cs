using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoragerInteractableArea : BuildingInteractableArea
{
    [SerializeField] private TopBuildingPopUp _popUp;
    [SerializeField] private TopBuildingPopUp _popUpButton;

    private IPopUpSupporter _supporter;

    private void OnEnable()
    {
        OnInteractionStarted += _popUpButton.Show;
        OnInteractionFinished += _popUpButton.Hide;
    }

    private void OnDisable()
    {
        OnInteractionStarted -= _popUpButton.Show;
        OnInteractionFinished -= _popUpButton.Hide;
    }

    private void Start()
    {
        _supporter = GetComponent<IPopUpSupporter>();

        var hangar = GetComponent<Hangar>();
        hangar.OnCountChanged += SetPopupText;
    }

    private void SetPopupText()
    {
        _popUp.UpdateText(_supporter.GetPopUpText());
    }
}
