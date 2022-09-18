using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Tutorial
{
    public class TutorialWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _closeButton;

        private string _currentTask;

        public static event Action<string> TutorialWindowClosed;

        private void Start()
        {
            _closeButton.onClick.AddListener(CloseWindow);
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetTask(string task)
        {
            _currentTask = task;
        }

        public void ShowWindow()
        {
            _window.SetActive(true);
        }

        private void CloseWindow()
        {
            _window.SetActive(false);
            TutorialWindowClosed?.Invoke(_currentTask);
        }
        
    }
}