using Characters.NPC;
using Characters.Players;
using Characters.Players.Shooting;
using Items.Bonuses;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Services.Audio
{
    public class GameSoundsHandler : BaseAudioHandler<GameSoundsSource>
    {
        [Header("Shot")] 
        [SerializeField] private AudioClip _shotSound;
        [SerializeField] [Range(0f, 1f)] private float _shotSoundVolumeScale;
        [SerializeField] [Range(-3f, 3f)] private float _shotSoundPitch;
        
        [Header("Stun")] 
        [SerializeField] private AudioClip _stunSound;
        [SerializeField] [Range(0f, 1f)] private float _stunSoundVolumeScale;
        [SerializeField] [Range(-3f, 3f)] private float _stunSoundPitch;

        [Header("Add Score")] 
        [SerializeField] private AudioClip _addScoreSound;
        [SerializeField] [Range(0f, 1f)] private float _addScoreSoundVolumeScale;
        [SerializeField] [Range(-3f, 3f)] private float _addScoreSoundPitch;

        [Header("Coffee Bonus")] 
        [SerializeField] private AudioClip _coffeeBonusSound;
        [SerializeField] [Range(0f, 1f)] private float _coffeeBonusSoundVolumeScale;
        [SerializeField] [Range(-3f, 3f)] private float _coffeeBonusSoundPitch;

        [Header("Shield Bonus")] 
        [SerializeField] private AudioClip _shieldBonusSound;
        [SerializeField] [Range(0f, 1f)] private float _shieldBonusSoundVolumeScale;
        [SerializeField] [Range(-3f, 3f)] private float _shieldBonusSoundPitch;

        [Header("Holidays Bonus")] 
        [SerializeField] private AudioClip _holidaysBonusSound;
        [SerializeField] [Range(0f, 1f)] private float _holidaysBonusSoundVolumeScale;
        [SerializeField] [Range(-3f, 3f)] private float _holidaysBonusSoundPitch;

        [Header("Documents Bonus")] 
        [SerializeField] private AudioClip _documentsBonusSound;
        [SerializeField] [Range(0f, 1f)] private float _documentsBonusSoundVolumeScale;
        [SerializeField] [Range(-3f, 3f)] private float _documentsBonusSoundPitch;
        
        

        protected override void OnEnable()
        {
            base.OnEnable();
            CoffeeBonus.CoffeeBonusStateChanged += (_, state) =>
            {
                if (state) PlayOneShot(_coffeeBonusSound, _coffeeBonusSoundPitch, _coffeeBonusSoundVolumeScale);
            };
            ShieldBonus.ShieldBonusStateChanged += (_, state) =>
            {
                if (state) PlayOneShot(_shieldBonusSound, _shieldBonusSoundPitch, _shieldBonusSoundVolumeScale);
            };
            DocumentsBonus.DocumentsBonusStateChanged += (_, state) =>
            {
                if (state)
                    PlayOneShot(_documentsBonusSound, _documentsBonusSoundPitch, _documentsBonusSoundVolumeScale);
            };
            PlayerShoot.Shooted += () =>
            {
                PlayOneShot(_shotSound, _shotSoundPitch * Random.Range(0.8f, 1.2f), _shotSoundVolumeScale);
            };
            HolidaysBonus.HolidaysBonusPickedUp += () =>
            {
                PlayOneShot(_holidaysBonusSound, _holidaysBonusSoundPitch, _holidaysBonusSoundVolumeScale);
            };
            PlayerScoreHandler.ScoreChanged += (_, diff, _) =>
            {
                if (diff > 0) PlayOneShot(_addScoreSound, _addScoreSoundPitch, _addScoreSoundVolumeScale);
            };
            InputHandler.StateChanged += (_, state) =>
            {
                if (!state) PlayOneShot(_stunSound, _stunSoundPitch, _stunSoundVolumeScale);
            };
        }
    }
}