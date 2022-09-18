using System;
using Characters.Players;
using UnityEngine;

namespace Services.Tutorial.Tasks
{
    public class StunEnemyTask : AbstractTask
    {
        [SerializeField] private Player _player;

        private void OnEnable()
        {
            InputHandler.StateChanged += OnInputStateChanged;
        }

        private void OnInputStateChanged(Player arg1, bool state)
        {
            if(!state && _isActive && !_isCompleted) CompleteTask();
        }

        protected override void OnActive()
        {
            _player.gameObject.SetActive(true);    
        }
        
        private void OnDisable()
        {
            InputHandler.StateChanged -= OnInputStateChanged;
        }
    }
}