using System;
using Services.Tutorial.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Tutorial
{
    public class TaskCompletedEffectHandler : MonoBehaviour
    {
        [SerializeField] private Image _completedImage;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            AbstractTask.TaskCompleted += OnTaskCompleted;
        }

        public void DisableImage()
        {
            _completedImage.enabled = false;
        }

        private void OnTaskCompleted()
        {
            _completedImage.enabled = true;
            _audioSource.Play();
        }

        private void OnDisable()
        {
            AbstractTask.TaskCompleted -= OnTaskCompleted;
        }
    }
}