using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldInteractableArea : BuildingInteractableArea
{
    [SerializeField] private TopBuildingPopUp _popUp;

    private IPopUpSupporter _supporter;

    private void Start()
    {
        _supporter = GetComponent<IPopUpSupporter>();

        var field = GetComponent<Field>();
        field.OnGrowthProgressChanged += SetPopupText;
    }

    private void SetPopupText()
    {
        _popUp.UpdateText(_supporter.GetPopUpText());
    }
}
