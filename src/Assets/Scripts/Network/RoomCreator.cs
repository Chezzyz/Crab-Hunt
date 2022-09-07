using System.Text;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Services;
using TMPro;
using UnityEngine;

namespace Network
{
    public class RoomCreator : MonoBehaviourPunCallbacks
    {
        [SerializeField] private int _codeLength;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private byte _eventCode;
        [SerializeField] private TMP_InputField _nameInput;
        
        public void Create()
        {
            string code = GenerateCode(_codeLength);
            PhotonNetwork.CreateRoom(code);
            Debug.Log($"Комната создана. Код - {code}");
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            PhotonNetwork.AutomaticallySyncScene = true;
            SendNetworkEvent();
        }
        
        private void SendNetworkEvent()
        {
            RaiseEventOptions options = RaiseEventOptions.Default;
            PhotonNetwork.RaiseEvent(_eventCode, _nameInput.text, options, SendOptions.SendReliable);
        }

        private string GenerateCode(int length)
        {
            StringBuilder builder = new();
            for (int i = 0; i < length; i++)
            {
                builder.Append((char) Random.Range('A', 'Z'));
            }

            return builder.ToString();
        }
    }
}