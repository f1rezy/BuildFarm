using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BuildingInteractableArea : MonoBehaviour
{
    public Action OnInteractStarted;
    public Action OnInteractFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterMovement character))
        {
            OnInteractStarted?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterMovement character))
        {
            OnInteractFinished?.Invoke();
        }
    }
}
