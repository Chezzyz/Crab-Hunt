using System;
using System.Collections;
using Items.Bonuses;
using UnityEngine;

namespace Characters.Players.Shooting
{
    public class ShootingProjectile : BaseBonus
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _stunDuration;

        private Player _playerFrom;
        
        private IEnumerator MoveForward(float baseSpeed, float speed)
        {
            float angleZ = transform.rotation.eulerAngles.z;
            Vector2 forward = new(Mathf.Cos(Mathf.Deg2Rad * (angleZ + 90)), Mathf.Sin(Mathf.Deg2Rad * (angleZ + 90)));
            
            while (true)
            {
                Vector2 translation = forward * (Time.deltaTime * (baseSpeed + speed));
                transform.Translate(translation, Space.World);
                yield return null;
            }
        }

        public void StartMoving(float playerSpeed)
        {
            StartCoroutine(MoveForward(playerSpeed, _speed));
        }

        public void SetPosition(Vector2 pos)
        {
            transform.position = pos;
        }

        public void SetRotation(Quaternion rot)
        {
            transform.rotation = rot;
        }

        public Player GetPlayer()
        {
            return _playerFrom;
        }

        public void SetPlayerFrom(Player player)
        {
            _playerFrom = player;
        }

        protected override Action<Player> GetAction()
        {
            return player =>
            {
                GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(StunPlayer(player, _stunDuration));
            };
        }
        
        private IEnumerator StunPlayer(Player player, float duration)
        {
            player.SetInputState(false);
            yield return new WaitForSeconds(duration);
            player.SetInputState(true);
            ReturnToPool();
        }
    }
}