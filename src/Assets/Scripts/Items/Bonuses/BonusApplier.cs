using System;
using Characters;
using Characters.Players;
using UnityEngine;

namespace Items.Bonuses
{
    public class BonusApplier : MonoBehaviour
    {
        private void OnEnable()
        {
            BaseBonus.BonusPickedUp += OnBonusPickedUp;
        }

        private void OnBonusPickedUp(Player player, Action<Player> action)
        {
            action.Invoke(player);
        }

        private void OnDisable()
        {
            BaseBonus.BonusPickedUp -= OnBonusPickedUp;
        }
        
    }
}