using System;
using System.Collections;
using Characters.Players;
using Services;
using UnityEngine;

namespace Visual
{
    public class AfterStunImmuneEffectHandler : BaseEffectHandler
    {
        [SerializeField] private float _animationFrequency;

        private void OnEnable()
        {
            AfterStunImmuneHandler.AfterStunImmuneStateChanged += OnAfterStunImmuneStateChanged;
        }

        private void OnAfterStunImmuneStateChanged(Player player, float duration, bool state)
        {
            if (state)
            {
                StartCoroutine(ImmuneAnimationCoroutine(player, duration, _animationFrequency));
            }
            else
            {
                player.SetSpriteVisible(true);
            }
        }

        private IEnumerator ImmuneAnimationCoroutine(Player player, float duration, float freq)
        {
            WaitForSeconds wait = new(1.0f / freq);
            float elapsedTime = 0;
            bool state = true;
            while (elapsedTime <= duration)
            {
                state = !state;
                player.SetSpriteVisible(state);
                yield return wait;
                elapsedTime += 1.0f / freq;
            }
            player.SetSpriteVisible(true);
        }

        private void OnDisable()
        {
            AfterStunImmuneHandler.AfterStunImmuneStateChanged -= OnAfterStunImmuneStateChanged;
        }

        protected override SpriteRenderer GetSpriteRendererPrefab()
        {
            return null;
        }
    }
}