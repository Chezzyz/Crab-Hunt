using Services.Audio;
using Services.Settings;
using UnityEngine;

namespace Services
{
    public class BaseAudioHandler<T> : BaseGameHandler<BaseAudioHandler<T>> where T : MonoBehaviour
    {
        protected AudioSource _audioSource;

        protected float _volumeScale;

        protected virtual void OnEnable()
        {
            SceneLoader.SceneLoaded += OnSceneLoaded;
            SettingsHandler.SoundVolumeLevelChanged += OnSoundVolumeChanged;
            SettingsHandler.SoundStateChanged += OnSoundStateChanged;
        }

        protected virtual void OnSceneLoaded(string scene)
        {
            _audioSource = FindObjectOfType<T>()?.GetComponent<AudioSource>();
            OnSoundVolumeChanged(SettingsHandler.GetSoundVolumeCoef());
            if (_audioSource == null && scene != "MenuScene")
            {
                Debug.LogWarning(typeof(T) + " not found on scene");
            }
        }

        protected virtual void OnSoundVolumeChanged(float coef)
        {
            if (_audioSource != null)
            {
                _audioSource.volume = coef;
            }
        }
        
        protected virtual void OnSoundStateChanged()
        {
            _audioSource.volume = SettingsHandler.SoundIsOn() ? SettingsHandler.GetSoundVolumeCoef() : 0;
        }

        protected void PlayClip(AudioClip clip, float pitch = 1f, float volumeScale = 1f)
        {
            _audioSource.pitch = pitch;
            _audioSource.volume = SettingsHandler.MusicIsOn() ? volumeScale * SettingsHandler.GetMusicVolumeCoef() : 0;
            _audioSource.clip = clip;
            _audioSource.Play();

            _volumeScale = volumeScale;
        }

        protected void PlayOneShot(AudioClip clip, float pitch = 1f, float volumeScale = 1f)
        {
            _audioSource.pitch = pitch;
            _audioSource.PlayOneShot(clip, volumeScale * SettingsHandler.GetSoundVolumeCoef());
        }

    }
}