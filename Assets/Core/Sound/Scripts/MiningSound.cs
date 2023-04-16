using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _miningSounds;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void MiningSoundPlay()
    {
        _audioSource.PlayOneShot(_miningSounds[Random.Range(0, _miningSounds.Length)]);
    }
}
