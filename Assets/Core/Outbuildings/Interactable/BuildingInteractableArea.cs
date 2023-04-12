using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BuildingInteractableArea : MonoBehaviour
{
    public Action OnInteractionStarted;
    public Action OnInteractionFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterMovement character))
        {
            OnInteractionStarted?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterMovement character))
        {
            OnInteractionFinished?.Invoke();
        }
    }
}
