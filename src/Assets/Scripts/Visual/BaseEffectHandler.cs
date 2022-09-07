using Characters;
using Characters.Players;
using Network;
using UnityEngine;

namespace Visual
{
    public abstract class BaseEffectHandler : MonoBehaviour
    {
        protected SpriteRenderer GetNewSpriteRenderer(Player player, string rendererName)
        {
            SpriteRenderer spriteRenderer = Instantiate(GetSpriteRendererPrefab(), player.transform);
            spriteRenderer.name = rendererName;
            return spriteRenderer;
        }

        protected void SetSprite(SpriteRenderer spriteRenderer, Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        protected abstract SpriteRenderer GetSpriteRendererPrefab();
    }
}