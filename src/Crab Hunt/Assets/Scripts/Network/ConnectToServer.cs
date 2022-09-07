using System;
using Photon.Pun;
using Services;
using UnityEngine;

namespace Network
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        [SerializeField] private SceneLoader _sceneLoader;
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            _sceneLoader.LoadScene("ConnectMenu");
        }
    }
}
