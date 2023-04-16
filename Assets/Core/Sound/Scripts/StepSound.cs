using UnityEngine;

public class StepSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _stepSounds;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void StepSoundPlay()
    {
        _audioSource.PlayOneShot(_stepSounds[Random.Range(0, _stepSounds.Length)]);
    }
}
