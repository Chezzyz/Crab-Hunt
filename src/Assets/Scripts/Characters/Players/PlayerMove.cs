using Photon.Pun;
using Services;
using UnityEngine;

namespace Characters.Players
{
    public class PlayerMove : MonoBehaviourPun
    {
        [SerializeField] 
        private float _speed;

        private WallsService _wallsService;

        private BoxCollider2D _bodyCollider;
        
        private void OnEnable()
        {
            InputHandler.DirectionButtonPressed += OnDirectionButtonPressed;
        }

        private void Start()
        {
            _wallsService = FindObjectOfType<WallsService>();
            _bodyCollider = GetComponentInChildren<BoxCollider2D>();
        }

        public void SetSpeed(float speed)
        {
            if (speed > 0)
            {
                _speed = speed;
            }
        }

        public float GetSpeed()
        {
            return _speed;
        }

        private void OnDirectionButtonPressed(Vector2Int dir)
        {
            if(!photonView.IsMine) return;
            MovePlayer(dir, _speed);
        }

        private void MovePlayer(Vector2Int dir, float speed)
        {
            Vector3 translation = new Vector3(dir.x, dir.y) * Time.deltaTime * speed;
            if (!CheckForWallCollision(transform.position + translation, dir))
            {
                transform.Translate(translation, Space.World);
            }
        }

        private bool CheckForWallCollision(Vector2 worldPosition, Vector2Int dir)
        {
            Vector2 firstBodyOffset = (dir + Vector2.Perpendicular(dir)) * _bodyCollider.size.x / 2;
            Vector2 secondBodyOffset = (dir - Vector2.Perpendicular(dir)) * _bodyCollider.size.x / 2;
            return _wallsService.HasTileOnPosition(_wallsService.WorldToCellPosition(worldPosition + firstBodyOffset))
                || _wallsService.HasTileOnPosition(_wallsService.WorldToCellPosition(worldPosition + secondBodyOffset));
        }
        
        private void OnDisable()
        {
            InputHandler.DirectionButtonPressed -= OnDirectionButtonPressed;
        }
        
    }
}
