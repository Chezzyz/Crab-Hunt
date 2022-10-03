using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Settings
{
    public class SettingsHandler : BaseGameHandler<SettingsHandler>
    {
        [Header("Sound")] 
        private float _soundVolumeLevel = 0.5f;
        private bool _soundIsOn = true;
        private Slider _soundVolumeSlider;
        private Toggle _soundIsOnToggle;

        [Header("Music")] 
        private float _musicVolumeLevel = 0.5f;
        private bool _musicIsOn = true;
        private Slider _musicVolumeSlider;
        private Toggle _musicIsOnToggle;

        [Header("Resolution")]
        private TMP_Dropdown _resolutionDropdown;

        public static event Action MusicStateChanged;
        public static event Action SoundStateChanged;
        public static event Action<float> SoundVolumeLevelChanged;
        public static event Action<float> MusicVolumeLevelChanged;

        private void OnEnable()
        {
            SceneLoader.SceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(string scene)
        {
            if (scene != "Main") return;

            _soundVolumeSlider = FindObjectOfType<SoundVolumeSlider>(true).GetComponent<Slider>();
            _soundVolumeSlider.onValueChanged.AddListener(SetSoundVolumeLevel);
            _soundIsOnToggle = FindObjectOfType<SoundToggle>(true).GetComponent<Toggle>();
            _soundIsOnToggle.onValueChanged.AddListener(SetSoundIsOn);
            _soundVolumeSlider.value = _soundVolumeLevel;
            
            _musicVolumeSlider = FindObjectOfType<MusicVolumeSlider>(true).GetComponent<Slider>();
            _musicVolumeSlider.onValueChanged.AddListener(SetMusicVolumeLevel);
            _musicIsOnToggle = FindObjectOfType<MusicToggle>(true).GetComponent<Toggle>();
            _musicIsOnToggle.onValueChanged.AddListener(SetMusicIsOn);
            _musicVolumeSlider.value = _musicVolumeLevel;

            _resolutionDropdown = FindObjectOfType<ResolutionDropdown>(true).GetComponent<TMP_Dropdown>();
            _resolutionDropdown.options =
                new List<TMP_Dropdown.OptionData>(Screen.resolutions.Reverse().Select(res => new TMP_Dropdown.OptionData(res.ToString())));
            _resolutionDropdown.value = 0;
            _resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        }

        #region Sound

        public static float GetSoundVolumeCoef()
        {
            return Instance._soundVolumeLevel;
        }

        private void SetSoundVolumeLevel(float value)
        {
            _soundVolumeLevel = value;
            SoundVolumeLevelChanged?.Invoke(_soundVolumeLevel);
        }
        
        public static bool SoundIsOn()
        {
            return Instance._soundIsOn;
        }

        private void SetSoundIsOn(bool state)
        {
            _soundIsOn = state;
            _soundVolumeSlider.interactable = state;
            SoundStateChanged?.Invoke();
        }

        #endregion

        #region Music

        public static bool MusicIsOn()
        {
            return Instance._musicIsOn;
        }
        
        private void SetMusicIsOn(bool state)
        {
            _musicIsOn = state;
            _musicVolumeSlider.interactable = state;
            MusicStateChanged?.Invoke();
        }
        
        private void SetMusicVolumeLevel(float value)
        {
            _musicVolumeLevel = value;
            MusicVolumeLevelChanged?.Invoke(_musicVolumeLevel);
        }
        
        public static float GetMusicVolumeCoef()
        {
            return Instance._musicVolumeLevel;
        }
        
        #endregion

        #region Resolution

        private void OnResolutionChanged(int index)
        {
            Resolution resolution = Screen.resolutions[index]; 
            Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.ExclusiveFullScreen);
        }

        #endregion
    }
}