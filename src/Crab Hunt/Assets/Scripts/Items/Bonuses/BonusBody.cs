using System;
using Characters;
using Characters.Players;
using Network;
using Photon.Pun;
using UnityEngine;
using Visual;

namespace Items.Bonuses
{
    [RequireComponent(typeof(Collider2D))]
    public class BonusBody : MonoBehaviour
    {
        [SerializeField] private BonusAnimation _animation;

        private NetworkBonusApplier _networkBonusApplier;
        private string _id;

        private void Start()
        {
            _networkBonusApplier = FindObjectOfType<NetworkBonusApplier>();
        }

        private void OnEnable()
        {
            NetworkBonusApplier.BonusApplied += OnNetworkBonusApplied;
            
            if (_animation)
            {
                _animation.SetDefault();
            }
        }

        public void SetId(string id)
        {
            _id = id;
        }

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            Player player = col.GetComponentInParent<Player>();
            
            if (!PhotonNetwork.IsConnected && player)
            {
                ActivateBonus(player);
            }

            if (PhotonNetwork.IsConnected && player && player.IsMine())
            {
                _networkBonusApplier.SendApplyNetworkEvent(_id, player.GetViewId());
                ActivateBonus(player);
            }
        }

        private void ActivateBonus(Player player)
        {
            BaseBonus baseBonus = GetComponentInParent<BaseBonus>();
            baseBonus.SendEvent(player);
            if(_animation) _animation.StartPickupAnimation();
            gameObject.SetActive(false);
        }
        
        private void OnNetworkBonusApplied(string bonusId, Player player)
        {
            if (_id == bonusId)
            {
                ActivateBonus(player);
            }
        }

        private void OnDisable()
        {
            NetworkBonusApplier.BonusApplied -= OnNetworkBonusApplied;
        }
    }
}