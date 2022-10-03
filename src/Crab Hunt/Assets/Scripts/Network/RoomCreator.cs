using System;
using System.Text;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Network
{
    public class RoomCreator : MonoBehaviourPunCallbacks
    {
        [SerializeField] private byte _eventCode;
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private TMP_Text _emptyNameError;
        
        public void Create()
        {
            if (String.IsNullOrWhiteSpace(_nameInput.text))
            {
                _emptyNameError.enabled = true;
                return;
            }
            string code = GenerateCode();
            PhotonNetwork.CreateRoom(code);
            Debug.Log($"Комната создана. Код - {code}");
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            PhotonNetwork.AutomaticallySyncScene = true;
            //SendNetworkEvent();
        }
        
        private void SendNetworkEvent()
        {
            RaiseEventOptions options = RaiseEventOptions.Default;
            PhotonNetwork.RaiseEvent(_eventCode, _nameInput.text, options, SendOptions.SendReliable);
        }

        private string GenerateCode()
        {
            // StringBuilder builder = new();
            // for (int i = 0; i < length; i++)
            // {
            //     builder.Append((char) Random.Range('A', 'Z'));
            // }
            //
            // return builder.ToString();
            return Random.Range(100, 1000).ToString();
        }
    }
}