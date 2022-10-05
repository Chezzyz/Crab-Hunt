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
            
            NetworkPlayerHandler.Instance.PlayerName = _nameInput.text;
            NetworkPlayerHandler.Instance.IsObserver = _isObserverToggle.isOn;
            
            _sceneLoader.LoadLobby();
        }
        
        public override void OnDisable()
        {
            base.OnDisable();
            JoinRoomButtonHandler.JoinRoomButtonPressed -= OnJoinRoomButtonPressed;
        }
    }
}