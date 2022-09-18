using System;
using Characters.NPC;
using Characters.Players;
using Items.Bonuses;
using UnityEngine;

namespace Services.Tutorial.Tasks
{
    public class PickupCrabTask : AbstractTask
    {
        [SerializeField] private Crab _crab;

        private void OnEnable()
        {
            BaseBonus.BonusPickedUp += OnCrabPickedUp;
        }

        private void OnCrabPickedUp(Player arg1, Action<Player> arg2)
        {
            if(_isActive && !_isCompleted) CompleteTask();
        }

        protected override void OnActive()
        {
            _crab.gameObject.SetActive(true);
        }
        
        private void OnDisable()
        {
            BaseBonus.BonusPickedUp -= OnCrabPickedUp;
        }
    }
}