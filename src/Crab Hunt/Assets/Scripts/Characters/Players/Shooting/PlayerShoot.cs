using System;
using Photon.Pun;
using Services;
using UnityEngine;

namespace Characters.Players.Shooting
{
    public class PlayerShoot : MonoBehaviourPun
    {
        [SerializeField] private float _createDistance;

        private BoxCollider2D _bodyCollider;
        private ProjectilesPool _projectilesPool;
        private WallsService _wallsService;

        public static event Action Shooted;

        private void OnEnable()
        {
            InputHandler.ShootButtonPressed += OnShootButtonPressed;
        }

        private void Start()
        {
            _bodyCollider = GetComponentInChildren<BoxCollider2D>();
            _projectilesPool = FindObjectOfType<ProjectilesPool>();
            _wallsService = FindObjectOfType<WallsService>();
        }

        private void OnShootButtonPressed()
        {
            if (!photonView.IsMine) return;
            if (PhotonNetwork.IsConnected)
            {
                photonView.RPC(nameof(Shoot), RpcTarget.All, transform.rotation.eulerAngles.z, transform.position);
                return;
            }
            Shoot(transform.rotation.eulerAngles.z, transform.position);
         }

        [PunRPC]
        private void Shoot(float angleZ, Vector3 position)
        {
            //Так как cos(z) при z=-90 должен давать 1 берем z + 90
            Vector2 forward = new(Mathf.Cos(Mathf.Deg2Rad * (angleZ + 90)), Mathf.Sin(Mathf.Deg2Rad * (angleZ + 90)));
            Vector2 createOffset = forward * (_bodyCollider.size.x / 2 + _createDistance);
            Vector2 createPosition = position + new Vector3(createOffset.x, createOffset.y);

            if (!_wallsService.HasTileOnPosition(_wallsService.WorldToCellPosition(createPosition)))
            {
                EnableProjectile(createPosition, Quaternion.Euler(0, 0, angleZ));
                Shooted?.Invoke();
            }
        }

        private void EnableProjectile(Vector2 createPosition, Quaternion createRotation)
        {
            ShootingProjectile projectile = _projectilesPool.GetNextFreeElement();
            projectile.gameObject.SetActive(true);
            projectile.GetComponent<SpriteRenderer>().enabled = true;
            projectile.transform.GetChild(0).gameObject.SetActive(true);
            projectile.SetPosition(createPosition);
            projectile.SetRotation(createRotation);

            Player player = GetComponent<Player>();

            projectile.SetPlayerFrom(player);
            projectile.StartMoving(player.GetSpeed());
        }

        private void OnDisable()
        {
            InputHandler.ShootButtonPressed -= OnShootButtonPressed;
        }
    }
}