using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] _tracks;
    [SerializeField] private AnimationCurve _smoothingCurve;
    
    private int _currentTrack = 0;
    private AudioSource _audioSource;
    private float _timeTrack = 0;
    private float _targetVolume;
    private bool _recharged;

    private void NextTrack()
    {
        _audioSource.clip = _tracks[_currentTrack];
        _timeTrack = _tracks[_currentTrack].length;
        _recharged = true;
        _audioSource.volume = 0f;
        _audioSource.time = 0f;
        _audioSource.Play();
        StartCoroutine(SmoothPacingVolume(_targetVolume));
    }

    private IEnumerator SmoothPacingVolume(float to, float timeScale = 1f, bool isUp = true)
    {
        if(isUp)
        {
            for (float i = 0f; i < 1f; i += Time.deltaTime / timeScale)
            {
                _audioSource.volume = to * _smoothingCurve.Evaluate(i);
                yield return null;
            }
        }
        else
        {
            for (float i = 1f; i >= 0f; i -= Time.deltaTime / timeScale)
            {
                _audioSource.volume = to * _smoothingCurve.Evaluate(i);
                yield return null;
            }
        }
    }
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _targetVolume = _audioSource.volume;
        NextTrack();
    }

    private void Update()
    {
        if (_timeTrack - _audioSource.time <= 1f && _recharged)
        {
            _recharged = false;
            StartCoroutine(SmoothPacingVolume(_targetVolume, 1f, false));
        }
        
        if (_audioSource.time >= _timeTrack)
        {
            _audioSource.Pause();
            _currentTrack += 1;
            if (_currentTrack >= _tracks.Length) _currentTrack = 0;
            NextTrack();
        }
    }
}
