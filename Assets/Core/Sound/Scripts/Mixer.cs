using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Mixer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    private void OnEnable()
    {
        _musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _masterVolumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);
        _mixer.audioMixer.SetFloat("MusicVolume", volume);
    }

    public void ChangeVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        _mixer.audioMixer.SetFloat("MasterVolume", volume);
    }

    private void Start()
    {
        ChangeVolume(PlayerPrefs.GetFloat("volume", 0));
        ChangeMusicVolume(PlayerPrefs.GetFloat("musicVolume", 0));
    }
}
