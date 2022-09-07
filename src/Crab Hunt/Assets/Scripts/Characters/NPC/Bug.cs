using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Players;
using Items.Bonuses;
using Photon.Pun;
using Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.NPC
{
    public class Bug : BaseBonus
    {
        [SerializeField] private int _scoreMinus;
        [SerializeField] private int _scorePlus;
        [SerializeField] private float _stunDuration;
        [SerializeField] private float _moveDelay;
        [SerializeField] private float _speed;

        private WallsService _wallsService;
        private Vector2 _baseCoordinate;
        private float _colliderLength;

        private void Awake()
        {
            _baseCoordinate = transform.position;
        }

        private void Start()
        {
            _wallsService = FindObjectOfType<WallsService>();
            _colliderLength = GetComponentInChildren<BoxCollider2D>().bounds.extents.x;
        }

        private void OnEnable()
        {
            GameTimerHandler.GameEnded += OnGameEnded;
            transform.position = _baseCoordinate;
            GetComponentInChildren<BugBody>(true).gameObject.SetActive(true);
            if(!PhotonNetwork.IsMasterClient) return;
            StartCoroutine(BugMoveCoroutine(_moveDelay));
        }

        private void OnGameEnded()
        {
            StopAllCoroutines();
        }

        public int GetScorePlus()
        {
            return _scorePlus;
        }

        protected override Action<Player> GetAction()
        {
            return player =>
            {
                StartCoroutine(StunPlayer(player, _stunDuration));
                SubtractScore(player, _scoreMinus);
            };
        }

        private IEnumerator StunPlayer(Player player, float duration)
        {
            player.SetInputState(false);
            yield return new WaitForSeconds(duration);
            player.SetInputState(true);
            ReturnToPool();
        }

        private void SubtractScore(Player player, int score)
        {
            if (player.GetIsImmune()) return;
            player.ChangeScore(-score);
        }

        private IEnumerator BugMoveCoroutine(float delay)
        {
            WaitForSeconds wait = new(delay);
            
            while (true)
            {
                yield return wait;
                float angle = 30 * Random.Range(0, 13);
                Vector2 dir = new(MathF.Cos(Mathf.Deg2Rad * angle), MathF.Sin(Mathf.Deg2Rad * angle));
                bool hasMoved = true;
                RotatePlayer(dir);
                
                while (hasMoved)
                {
                    hasMoved = Move(dir, _speed);
                    yield return null;
                }
            }
        }
        
        private bool Move(Vector2 dir, float speed)
        {
            Vector3 translation = new Vector3(dir.x, dir.y) * (Time.deltaTime * speed);
            if (!CheckForWallCollision(transform.position + translation, dir, _colliderLength))
            {
                transform.Translate(translation, Space.World);
                return true;
            }

            return false;
        }
        
        private void RotatePlayer(Vector2 dir)
        {
            float angle = Vector2.SignedAngle(Vector2.up, dir);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private bool CheckForWallCollision(Vector2 worldPosition, Vector2 dir, float colliderLength)
        {
            Vector2 firstBodyOffset = (dir + Vector2.Perpendicular(dir)) * colliderLength;
            Vector2 secondBodyOffset = (dir - Vector2.Perpendicular(dir)) * colliderLength;
            
            return _wallsService.HasTileOnPosition(_wallsService.WorldToCellPosition(worldPosition + firstBodyOffset))
                   || _wallsService.HasTileOnPosition(_wallsService.WorldToCellPosition(worldPosition + secondBodyOffset));
        }

        private void OnDisable()
        {
            GameTimerHandler.GameEnded -= OnGameEnded;
        }
    }
}