using System;
using Characters;
using Characters.Players;
using Photon.Pun;
using UnityEngine;

namespace Items.Bonuses
{
    public abstract class BaseBonus : MonoBehaviour
    {
        public static event Action<Player, Action<Player>> BonusPickedUp;

        public void SendEvent(Player player)
        {
            BonusPickedUp?.Invoke(player, GetAction());
        }

        public int GetSelfIndexInPool()
        {
            return GetPool().GetIndexOfElement(this);
        }

        public BaseBonusPool GetPool()
        {
            return GetComponentInParent<BaseBonusPool>();
        }

        protected void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        protected abstract Action<Player> GetAction();
    }
}