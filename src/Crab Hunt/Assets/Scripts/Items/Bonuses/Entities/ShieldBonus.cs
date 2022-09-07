using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Players;
using UnityEngine;

namespace Items.Bonuses
{
    public class ShieldBonus : BaseBonus
    {
        [SerializeField] private float _duration;

        public static event Action<Player, bool> ShieldBonusStateChanged;
        
        protected override Action<Player> GetAction()
        {
            return player => StartCoroutine(ShieldPlayer(player, _duration));
        }

        private IEnumerator ShieldPlayer(Player player, float duration)
        {
            player.SetIsImmune(true);
            ShieldBonusStateChanged?.Invoke(player, true);
            yield return new WaitForSeconds(duration);
            player.SetIsImmune(false);
            ShieldBonusStateChanged?.Invoke(player, false);
            ReturnToPool();
        }
        
    }
}