using System;
using System.Collections;
using Characters;
using Characters.Players;
using UnityEngine;

namespace Items.Bonuses
{
    public class CoffeeBonus : BaseBonus
    {
        [SerializeField] private float _speedModifier;
        [SerializeField] private float _duration;

        public static event Action<Player, bool> CoffeeBonusStateChanged;  

        protected override Action<Player> GetAction()
        {
            return player => StartCoroutine(SpeedBuff(player, _speedModifier, _duration));
        }

        private IEnumerator SpeedBuff(Player player, float speedModifier, float duration)
        {
            player.SetSpeed(player.GetSpeed() * speedModifier);
            CoffeeBonusStateChanged?.Invoke(player, true);
            yield return new WaitForSeconds(duration);
            player.SetSpeed(player.GetSpeed() / speedModifier);
            CoffeeBonusStateChanged?.Invoke(player, false);
            ReturnToPool();
        }
    }
}