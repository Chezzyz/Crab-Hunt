using System;
using Characters.Players;
using Photon.Pun;
using UnityEngine;

namespace Network
{
    public class NetworkBonusApplier : MonoBehaviourPun
    {
        public static event Action<string, Player> BonusApplied;
        
        public void SendApplyNetworkEvent(string bonusId, int playerId)
        {
            photonView.RPC(nameof(SendApplyEventRpc), RpcTarget.OthersBuffered, bonusId, playerId);
        }

        [PunRPC]
        private void SendApplyEventRpc(string bonusId, int playerId)
        {
            Player player = PhotonView.Find(playerId).GetComponent<Player>();
            BonusApplied?.Invoke(bonusId, player);
        } 
    }
}