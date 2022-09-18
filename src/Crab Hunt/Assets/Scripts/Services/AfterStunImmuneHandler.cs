using System;
using System.Collections;
using Characters.Players;
using UnityEngine;

namespace Services
{
    public class AfterStunImmuneHandler : MonoBehaviour
    {
        [SerializeField] private float _duration;
        
        public static event Action<Player, float, bool> AfterStunImmuneStateChanged; 
        private void OnEnable()
        {
            InputHandler.StateChanged += OnPlayerMoveStateChanged;
        }

        private void OnPlayerMoveStateChanged(Player player, bool state)
        {
            if(state) return;

            StartCoroutine(ImmuneCoroutine(player, _duration));
        }

        private IEnumerator ImmuneCoroutine(Player player, float duration)
        {
            player.SetIsImmune(true);
            AfterStunImmuneStateChanged?.Invoke(player, duration, true);
            yield return new WaitForSeconds(duration);
            player.SetIsImmune(false);
            AfterStunImmuneStateChanged?.Invoke(player, duration, false);
        }

        private void OnDisable()
        {
            InputHandler.StateChanged -= OnPlayerMoveStateChanged;
        }
    }
}