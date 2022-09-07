using Characters;
using Characters.Players;
using Photon.Pun;
using UnityEngine;

namespace Items.Bonuses
{
    [RequireComponent(typeof(Collider2D))]
    public class BonusBody : MonoBehaviourPun
    {
        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            Player player = col.GetComponentInParent<Player>();
            if (player)
            {
                BaseBonus baseBonus = GetComponentInParent<BaseBonus>();
                baseBonus.SendEvent(player);
                gameObject.SetActive(false);
            }
        }

    }
}