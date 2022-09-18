using Services.Settings;
using UnityEngine;

namespace Services.Audio
{
    public class BackgroundAudioHandler : BaseAudioHandler<BackgroundAudioSource>
    {
        [Header("Menu")] [SerializeField] private AudioClip _menuMusic;
        [SerializeField] [Range(0f, 1f)] private float _menuMusicVolumeScale;
        [Header("Lobby")] [SerializeField] private AudioClip _lobbyMusic;
        [SerializeField] [Range(0f, 1f)] private float _lobbyMusicVolumeScale;
        [Header("Room")] [SerializeField] private AudioClip _roomMusic;
        [SerializeField] [Range(0f, 1f)] private float _roomMusicVolumeScale;

        protected override void OnEnable()
        {
            base.OnEnable();
            SettingsHandler.SoundVolumeLevelChanged -= OnSoundVolumeChanged;
            SettingsHandler.SoundStateChanged -= OnSoundStateChanged;
            SettingsHandler.MusicStateChanged += OnMusicStateChanged;
            SettingsHandler.MusicVolumeLevelChanged += OnMusicVolumeChanged;
        }

        protected override void OnSceneLoaded(string scene)
        {
            base.OnSceneLoaded(name);

            System.Action play = scene switch
            {
                "Main" => () => PlayClip(_menuMusic, volumeScale: _menuMusicVolumeScale),
                "ConnectMenu" => () => PlayClip(_menuMusic, volumeScale: _menuMusicVolumeScale),
                "Lobby" => () => PlayClip(_lobbyMusic, volumeScale: _lobbyMusicVolumeScale),
                "Room" => () => PlayClip(_roomMusic, volumeScale: _roomMusicVolumeScale),
                "Tutorial" => () => PlayClip(_lobbyMusic, volumeScale: _lobbyMusicVolumeScale),
                _ => () => Debug.LogWarning("Music for this stage is not implemented")
            };
            play();
        }
        
        private void OnMusicVolumeChanged(float coef)
        {
            if (_audioSource != null && SettingsHandler.MusicIsOn())
            {
                _audioSource.volume = coef * _volumeScale;
            }
        }
        
        private void OnMusicStateChanged()
        {
            _audioSource.volume = SettingsHandler.MusicIsOn() ? _volumeScale * SettingsHandler.GetMusicVolumeCoef() : 0;
        }
        
    }
}