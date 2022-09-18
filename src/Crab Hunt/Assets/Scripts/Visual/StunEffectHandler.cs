using System;
using Characters;
using Characters.Players;
using DG.Tweening;
using Network;
using Services;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Visual
{
    public class StunEffectHandler : BaseEffectHandler
    {
        [SerializeField] private SpriteRenderer _effectSpriteRendererPrefab;
        [SerializeField] private Sprite _stunSprite;
        [SerializeField] private float _animationDuration;
        [SerializeField] private string _rendererName;

        private void OnEnable()
        {
            InputHandler.StateChanged += OnInputStateChanged;
        }

        private void OnInputStateChanged(Player player, bool state)
        {
            if (!state)
            {
                SpriteRenderer spriteRenderer = CreateSpriteRenderer(player, _rendererName);
                SetSprite(spriteRenderer, _stunSprite);
                spriteRenderer.transform.DOLocalRotate(new Vector3(0, 0, 360), _animationDuration,
                    RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
            }
            else
            {
                Destroy(player.transform.Find(_rendererName)?.gameObject);
            }
        }

        protected override SpriteRenderer GetSpriteRendererPrefab()
        {
            return _effectSpriteRendererPrefab;
        }

        private void OnDisable()
        {
            InputHandler.StateChanged -= OnInputStateChanged;
        }
        
    }
}