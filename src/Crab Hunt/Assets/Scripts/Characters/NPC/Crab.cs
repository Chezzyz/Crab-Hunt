using System;
using Characters.Players;
using Items.Bonuses;
using UnityEngine;

namespace Characters.NPC
{
    public class Crab : BaseBonus
    {
        [SerializeField] private int _scoreValue;

        protected override Action<Player> GetAction()
        {
            return player =>
            {
                AddScore(player, _scoreValue);
            };
        }

        private void AddScore(Player player, int value)
        {
            player.ChangeScore(value);
            ReturnToPool();
        }
    }
}