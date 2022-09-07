using System;
using UnityEngine;

namespace Characters.Players
{
    [RequireComponent(typeof(Player))]
    public class TutorRotation : MonoBehaviour
    {
        [SerializeField] private float _sensitivity;
        
        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
            float mouseY = -Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
            SetPlayerRotation(new Vector3(mouseX, mouseY));
        }

        private void SetPlayerRotation(Vector3 rot)
        {
            _player.transform.rotation = Quaternion.Euler(rot);
        }
    }
}