using System;
using UnityEngine;

namespace Characters.Players.Shooting
{
    public class Projectile : MonoBehaviour
    {
        private Vector2 _direction;
        private float _speed;
        
        public void SetDirection(Vector2 dir)
        {
            _direction = dir;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
        
        private void Update()
        {
            transform.Translate(_direction * (Time.deltaTime * _speed), Space.World);
        }
    }
}