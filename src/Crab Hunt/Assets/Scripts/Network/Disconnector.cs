using Photon.Pun;
using UnityEngine;

namespace Network
{
    public class Disconnector : MonoBehaviour
    {
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void LeaveLobby()
        {
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.Disconnect();
        }
    }
}