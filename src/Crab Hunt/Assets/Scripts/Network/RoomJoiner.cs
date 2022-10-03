using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Services;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class RoomJoiner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private byte _eventCode;
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private Toggle _isObserverToggle;
        [SerializeField] private TMP_Text _emptyNameError;
        
        public override void OnEnable()
        {
            base.OnEnable();
            JoinRoomButtonHandler.JoinRoomButtonPressed += OnJoinRoomButtonPressed;
        }

        private void OnJoinRoomButtonPressed(string code)
        {
            if (String.IsNullOrWhiteSpace(_nameInput.text))
            {
                _emptyNameError.enabled = true;
                return;
            }
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
            object[] content = { _nameInput.text, PhotonNetwork.CurrentRoom.PlayerCount - 1, _isObserverToggle.isOn };
            PhotonNetwork.RaiseEvent(_eventCode, content, options, SendOptions.SendReliable);
        }

        public override void OnDisable()
        {
            base.OnDisable();
            JoinRoomButtonHandler.JoinRoomButtonPressed -= OnJoinRoomButtonPressed;
        }
    }
}