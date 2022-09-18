using System;
using System.Collections;
using Characters;
using Characters.Players;
using Services;
using UnityEngine;

namespace Items.Bonuses
{
    public class HolidaysBonus : BaseBonus
    {
        [SerializeField] private float _duration;

        public static event Action HolidaysBonusPickedUp;
        
        protected override Action<Player> GetAction()
        {
            return player =>
            {
                StartCoroutine(StunAllExceptPlayer(player, _duration));
                HolidaysBonusPickedUp?.Invoke();
            };
        }

        private IEnumerator StunAllExceptPlayer(Player player, float duration)
        {
            Player[] players = FindObjectsOfType<Player>();
            foreach (var p in players)
            {
               if(p == player) continue;
               p.SetInputState(false);
            }
            
            yield return new WaitForSeconds(duration);
            
            foreach (var p in players)
            {
                if(p == player) continue;
                p.SetInputState(true);
            }
            ReturnToPool();
        }
    }
}