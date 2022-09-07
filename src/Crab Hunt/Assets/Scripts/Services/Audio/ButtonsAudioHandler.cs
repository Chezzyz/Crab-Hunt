using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Audio
{
    public class ButtonsAudioHandler : BaseAudioHandler<ButtonsAudioSource>
    { 
        [Header("Button Sound")]
        [SerializeField]
        private AudioClip _buttonSound;
        [SerializeField]
        [Range(0f, 1f)]
        private float _buttonSoundVolumeScale;

        protected override void OnSceneLoaded(string scene)
        {
            base.OnSceneLoaded(scene);

            //Некоторые кнопки могут не сразу загрузиться после загрузки сцены, поэтому ждем немного
            StartCoroutine(AddButtonsListenerAfterDelay(0.5f));
        }

        private IEnumerator AddButtonsListenerAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            List<Button> buttons = FindObjectsOfType<Button>(true).ToList();
            buttons.ForEach(button => button.onClick.AddListener(() => PlayOneShot(_buttonSound, volumeScale: _buttonSoundVolumeScale)));
        }

    }
}