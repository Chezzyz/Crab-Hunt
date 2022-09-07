using System;
using Services;
using UnityEngine;

namespace Characters.Players.Shooting
{
    [RequireComponent(typeof(Player))]
    public class TutorShoot : MonoBehaviour
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private float _speed;
        private Player _player;
        
        private void OnEnable()
        {
            InputHandler.ShootButtonPressed += OnShootButtonPressed;
        }

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        private void OnShootButtonPressed()
        {
            Shoot();
        }

        private void Shoot()
        {
            Projectile proj = Instantiate(_prefab, _player.transform.position, Quaternion.identity);
            proj.SetDirection(_player.transform.forward);
            proj.SetSpeed(_speed);
        }

        private void OnDisable()
        {
            InputHandler.ShootButtonPressed -= OnShootButtonPressed;
        }
    }
}