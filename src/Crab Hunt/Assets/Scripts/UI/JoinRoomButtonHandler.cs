using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class JoinRoomButtonHandler : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _input;

        public static event Action<string> JoinRoomButtonPressed;

        public void ButtonPressed()
        {
            JoinRoomButtonPressed?.Invoke(_input.text);
        }
    }
}