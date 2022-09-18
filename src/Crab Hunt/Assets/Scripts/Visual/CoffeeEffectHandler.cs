using Characters;
using Characters.Players;
using DG.Tweening;
using Items.Bonuses;
using Network;
using Services;
using UnityEngine;

namespace Visual
{
    public class CoffeeEffectHandler : BaseEffectHandler
    {
        [SerializeField] private SpriteRenderer _effectSpriteRendererPrefab;
        [SerializeField] private Sprite _coffeeSprite;
        [SerializeField] private Vector2 _spritePosition;
        [SerializeField] private string _rendererName;
        
        private void OnEnable()
        {
            CoffeeBonus.CoffeeBonusStateChanged += OnCoffeeBonusStateChanged;
        }

        private void OnCoffeeBonusStateChanged(Player player, bool state)
        {
            if (state)
            {
                SpriteRenderer spriteRenderer = CreateSpriteRenderer(player, _rendererName);
                SetSprite(spriteRenderer, _coffeeSprite);
                spriteRenderer.transform.localPosition = _spritePosition;
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
            CoffeeBonus.CoffeeBonusStateChanged -= OnCoffeeBonusStateChanged;
        }
        
    }
}