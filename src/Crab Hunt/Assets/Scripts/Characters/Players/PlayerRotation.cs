using Photon.Pun;
using Services;
using UnityEngine;

namespace Characters.Players
{
    public class PlayerRotation : MonoBehaviourPun
    {
        private void OnEnable()
        {
            InputHandler.DirectionButtonPressed += OnDirectionButtonPressed;
        }

        private void OnDirectionButtonPressed(Vector2Int dir)
        {
            if(!photonView.IsMine) return;
            RotatePlayer(dir);
        }

        private void RotatePlayer(Vector2Int dir)
        {
            float angle = Vector2.SignedAngle(Vector2.up, dir);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        private void OnDisable()
        {
            InputHandler.DirectionButtonPressed -= OnDirectionButtonPressed;
        }
    }
}