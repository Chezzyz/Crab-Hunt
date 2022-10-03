using System;
using Characters.Players;
using Characters.Players.Shooting;
using Items.Bonuses;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters.NPC
{
    public class BugBody : BonusBody
    {
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);

            ShootingProjectile projectile = col.GetComponentInParent<ShootingProjectile>();
            if (projectile != null)
            {
                projectile.gameObject.SetActive(false);
                projectile.GetPlayer().ChangeScore(GetComponentInParent<Bug>().GetScorePlus());
                GetComponentInParent<BaseBonus>().gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }

    }
}