using UnityEngine;
using UnityEngine.Audio;

public class Button : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;

    private const string ButtonsVolume = nameof(ButtonsVolume);
    private const string MasterVolume = nameof(MasterVolume);
    private const string MusicVolume = nameof(MusicVolume);

    public void PlaySound()
    {
        _audioSource.clip = _clip;
        _audioSource.Play();
    }

    public void OfOnSound()
    {
        if (_audioMixerGroup.audioMixer.GetFloat(MasterVolume, out float currentVolume))
        {
            float targetVolume = 0;
            float minVolume = -80;

            if (currentVolume > minVolume)
                _audioMixerGroup.audioMixer.SetFloat(MasterVolume, minVolume);
            else
                _audioMixerGroup.audioMixer.SetFloat(MasterVolume, targetVolume);
        }
    }

    public void ChangeButtonsVolume(float volume)
    {
        _audioMixerGroup.audioMixer.SetFloat(ButtonsVolume, ValueToVolume(volume));
    }

    public void ChangeMasterVolume(float volume)
    {
        _audioMixerGroup.audioMixer.SetFloat(MasterVolume, ValueToVolume(volume));
    }

    public void ChangeMusicVolume(float volume)
    {
        _audioMixerGroup.audioMixer.SetFloat(MusicVolume, ValueToVolume(volume));
    }

    private float ValueToVolume(float value)
    {
        float maxVolume = 0;
        float zeroVolume = -80; 
        float minValue = 0.0001f;
        float maxValue = 1f;
        float coefficient = 4f;

        float res = Mathf.Log10(Mathf.Clamp(value, minValue, maxValue)) * (maxVolume - zeroVolume) / coefficient + maxVolume;

        return res;
    }
}