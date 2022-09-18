using System;
using Characters;
using Characters.Players;
using Photon.Pun;
using UnityEngine;
using Visual;

namespace Items.Bonuses
{
    [RequireComponent(typeof(Collider2D))]
    public class BonusBody : MonoBehaviourPun
    {
        [SerializeField] private BonusAnimation _animation;

        private void OnEnable()
        {
            if (_animation)
            {
                _animation.SetDefault();
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            Player player = col.GetComponentInParent<Player>();
            if (player)
            {
                BaseBonus baseBonus = GetComponentInParent<BaseBonus>();
                baseBonus.SendEvent(player);
                if(_animation) _animation.StartPickupAnimation();
                gameObject.SetActive(false);
            }
        }

    }
}