using System;
using Photon.Pun;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class HostHandler : MonoBehaviourPun
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private SceneLoader _sceneLoader;
        
        private void Start()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                _startGameButton.gameObject.SetActive(false);
            }
            _startGameButton.onClick.AddListener(LoadRoom);
        }

        private void LoadRoom()
        {
            photonView.RPC(nameof(LoadRoomRPC), RpcTarget.AllBuffered);
        }

        [PunRPC]
        private void LoadRoomRPC()
        {
            _sceneLoader.LoadRoom();
        }
    }
}