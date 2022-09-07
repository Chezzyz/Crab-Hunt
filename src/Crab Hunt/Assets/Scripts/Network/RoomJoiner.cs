using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Services;
using TMPro;
using UI;
using UnityEngine;

namespace Network
{
    public class RoomJoiner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private byte _eventCode;
        [SerializeField] private TMP_InputField _nameInput;
        
        public override void OnEnable()
        {
            base.OnEnable();
            JoinRoomButtonHandler.JoinRoomButtonPressed += OnJoinRoomButtonPressed;
        }

        private void OnJoinRoomButtonPressed(string code)
        {
            PhotonNetwork.JoinRoom(code);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            if(!PhotonNetwork.IsMasterClient) PhotonNetwork.IsMessageQueueRunning = false;
            _sceneLoader.LoadLobby();
            SendNetworkEvent();
        }

        private void SendNetworkEvent()
        {
            RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All};
            object[] content = { _nameInput.text, PhotonNetwork.CurrentRoom.PlayerCount - 1 };
            PhotonNetwork.RaiseEvent(_eventCode, content, options, SendOptions.SendReliable);
        }
        
        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            JoinRoomButtonHandler.JoinRoomButtonPressed -= OnJoinRoomButtonPressed;
        }
    }
}