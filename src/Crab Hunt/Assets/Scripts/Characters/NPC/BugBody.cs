﻿using System;
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
            if (col.GetComponentInParent<Player>() != null)
            {
                BaseBonus baseBonus = GetComponentInParent<BaseBonus>();
                baseBonus.SendEvent(col.GetComponentInParent<Player>());
                gameObject.SetActive(false);
            }

            ShootingProjectile projectile = col.GetComponentInParent<ShootingProjectile>();
            if (projectile != null)
            {
                projectile.GetPlayer().ChangeScore(GetComponentInParent<Bug>().GetScorePlus());
                projectile.gameObject.SetActive(false);
                GetComponentInParent<BaseBonus>().gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }

    }
}