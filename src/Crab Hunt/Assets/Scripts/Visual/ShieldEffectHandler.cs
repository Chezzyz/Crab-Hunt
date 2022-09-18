using System;
using Characters.Players;
using DG.Tweening;
using Items.Bonuses;
using Network;
using UnityEngine;

namespace Visual
{
    public class ShieldEffectHandler : BaseEffectHandler
    {
        [SerializeField] private SpriteRenderer _effectSpriteRendererPrefab;
        [SerializeField] private Sprite _shieldSprite;
        [SerializeField] private float _animationDuration;
        [SerializeField] private string _rendererName;

        private void OnEnable()
        {
            ShieldBonus.ShieldBonusStateChanged += OnShieldBonusStateChanged;
        }

        private void OnShieldBonusStateChanged(Player player, bool state)
        {
            if (state)
            {
                SpriteRenderer spriteRenderer = CreateSpriteRenderer(player, _rendererName);
                SetSprite(spriteRenderer, _shieldSprite);
                spriteRenderer.transform.DOLocalRotate(new Vector3(0, 0, 360), _animationDuration,
                    RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
            }
            else
            {
                Destroy(player.transform.Find(_rendererName).gameObject);
            }
        }

        protected override SpriteRenderer GetSpriteRendererPrefab()
        {
            return _effectSpriteRendererPrefab;
        }
        
        private void OnDisable()
        {
            ShieldBonus.ShieldBonusStateChanged -= OnShieldBonusStateChanged;
        }
        
    }
}