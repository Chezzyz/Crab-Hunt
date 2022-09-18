using System;
using UnityEngine;

namespace Services.Tutorial.Tasks
{
    public abstract class AbstractTask : MonoBehaviour
    {
        public static event Action TaskCompleted;

        protected bool _isCompleted;
        protected bool _isActive;

        public void SetActive(bool state)
        {
            _isActive = state;
            if(_isActive && !_isCompleted) OnActive();
        }

        protected abstract void OnActive();

        protected void CompleteTask()
        {
            _isCompleted = true;
            TaskCompleted?.Invoke();
            SetActive(false);
        }
    }
}