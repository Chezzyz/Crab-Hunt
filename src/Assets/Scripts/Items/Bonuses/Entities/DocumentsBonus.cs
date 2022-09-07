using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Players;
using UnityEngine;

namespace Items.Bonuses
{
    public class DocumentsBonus : BaseBonus
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _slowModifier;
        [SerializeField] private int _score;

        public static event Action<Player, bool> DocumentsBonusStateChanged;
        
        protected override Action<Player> GetAction()
        {
            return player =>
            {
                AddScore(player, _score);
                StartCoroutine(SlowPlayer(player, _duration, _slowModifier));
            };
        }

        private IEnumerator SlowPlayer(Player player, float duration, float slowModifier)
        {
            player.SetSpeed(player.GetSpeed() * slowModifier);
            DocumentsBonusStateChanged?.Invoke(player, true);
            yield return new WaitForSeconds(duration);
            player.SetSpeed(player.GetSpeed() / slowModifier);
            DocumentsBonusStateChanged?.Invoke(player, false);
            ReturnToPool();
        }

        private void AddScore(Player player, int score)
        {
            player.ChangeScore(score);    
        }
        
    }
}