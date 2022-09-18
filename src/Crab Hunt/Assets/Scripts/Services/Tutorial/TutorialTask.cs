using System;
using TMPro;
using UnityEngine;

namespace Services.Tutorial
{
    public class TutorialTask : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TaskCompletedEffectHandler _effectHandler;

        private void OnEnable()
        {
            TutorialWindow.TutorialWindowClosed += OnTutorialWindowClosed;
        }

        private void OnTutorialWindowClosed(string text)
        {
            _effectHandler.DisableImage();
            SetText(text);
        }

        private void SetText(string text)
        {
            _text.text = text;
        }
        
        private void OnDisable()
        {
            TutorialWindow.TutorialWindowClosed -= OnTutorialWindowClosed;
        }
    }
}