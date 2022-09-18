using Characters.Players;
using Items.Bonuses;
using Network;
using UnityEngine;

namespace Visual
{
    public class DocumentsEffectHandler : BaseEffectHandler
    {
        [SerializeField] private SpriteRenderer _effectSpriteRendererPrefab;
        [SerializeField] private Sprite _documentsSprite;
        [SerializeField] private Vector2 _spritePosition;
        [SerializeField] private string _rendererName;
        
        private void OnEnable()
        {
            DocumentsBonus.DocumentsBonusStateChanged += OnDocumentsBonusStateChanged;
        }

        private void OnDocumentsBonusStateChanged(Player player, bool state)
        {
            if (state)
            {
                SpriteRenderer spriteRenderer = CreateSpriteRenderer(player, _rendererName);
                SetSprite(spriteRenderer, _documentsSprite);
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
            CoffeeBonus.CoffeeBonusStateChanged -= OnDocumentsBonusStateChanged;
        }
    }
}