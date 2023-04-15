using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISound : MonoBehaviour
{
    private const int _UILayerIndex = 5;
    private AudioSource _audioSource;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                _audioSource.Play();
            }
        }
    }
}
