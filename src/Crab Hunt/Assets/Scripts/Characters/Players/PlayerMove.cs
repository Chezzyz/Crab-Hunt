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
        private FloorService _floorService;

        private BoxCollider2D _bodyCollider;
        
        private void OnEnable()
        {
            InputHandler.DirectionButtonPressed += OnDirectionButtonPressed;
        }

        private void Start()
        {
            _wallsService = FindObjectOfType<WallsService>();
            _floorService = FindObjectOfType<FloorService>();
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

            translation = GetFinalTranslation(translation, dir);
            
            transform.Translate(translation, Space.World);
        }

        private bool CheckForWallCollision(Vector2 worldPosition, Vector2Int dir)
        {
            Vector2 firstBodyOffset = (dir + Vector2.Perpendicular(dir)) * _bodyCollider.size.x / 2;
            Vector2 secondBodyOffset = (dir - Vector2.Perpendicular(dir)) * _bodyCollider.size.x / 2;
            return _wallsService.HasTileOnPosition(_wallsService.WorldToCellPosition(worldPosition + firstBodyOffset))
                || _wallsService.HasTileOnPosition(_wallsService.WorldToCellPosition(worldPosition + secondBodyOffset));
        }

        private bool CheckForFloor(Vector2 worldPosition, Vector2 offset)
        {
            return _floorService.HasTileOnPosition(_floorService.WorldToCellPosition(worldPosition + offset));
        }

        private Vector3 GetFinalTranslation(Vector3 translation, Vector2Int dir)
        {
            Vector2 nextPos = transform.position + translation;
            Vector2 firstBodyOffset = (dir + Vector2.Perpendicular(dir)) * _bodyCollider.size.x / 2;
            Vector2 lastBodyOffset = (dir - Vector2.Perpendicular(dir)) * _bodyCollider.size.x / 2;
            
            Vector2 middleBodyOffset = Vector2.Lerp(firstBodyOffset, lastBodyOffset, 0.5f);
            
            bool firstFloorCond = CheckForFloor( nextPos, firstBodyOffset);
            bool lastFloorCond = CheckForFloor(nextPos, lastBodyOffset);
            
            //Если первый и последний офсет имеют пол, то движемся прямо
            if (firstFloorCond && lastFloorCond) return translation;
            
            bool middleFloorCond = CheckForFloor(nextPos, middleBodyOffset);
            
            //Если только один из крайних офсетов не имеют пола, то телепортируем игрока к соот. офсету
            if (firstFloorCond && middleFloorCond)
            {
                return  Vector2.Perpendicular(dir) * _bodyCollider.size.x / 4;
            }
            if (lastFloorCond && middleFloorCond)
            {
                return  -Vector2.Perpendicular(dir) * _bodyCollider.size.x / 4;
            }
            
            return Vector2.zero;
        }
        
        private void OnDisable()
        {
            InputHandler.DirectionButtonPressed -= OnDirectionButtonPressed;
        }
        
    }
}
