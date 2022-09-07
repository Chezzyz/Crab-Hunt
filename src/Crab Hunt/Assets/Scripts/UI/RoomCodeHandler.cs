using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RoomCodeHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            _text.text = $"Код комнаты - {PhotonNetwork.CurrentRoom.Name}";
        }

    }
}