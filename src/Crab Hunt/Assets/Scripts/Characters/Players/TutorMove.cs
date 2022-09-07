using System;
using System.Collections;
using Services;
using UnityEngine;

namespace Characters.Players
{
    public class TutorMove : MonoBehaviour
    {
        [SerializeField] 
        private float _speed;

        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpDuration;

        private Camera _mainCamera;
        
        private void OnEnable()
        {
            InputHandler.DirectionButtonPressed += OnDirectionButtonPressed;
        }

        private void Start()
        {
            _mainCamera = FindObjectOfType<Camera>();
        }

        private void OnDirectionButtonPressed(Vector2Int dir)
        {
            MovePlayer(dir, _speed);
        }

        private void MovePlayer(Vector2Int dir, float speed)
        {
            Vector3 translation = new Vector3(dir.x, dir.y) * Time.deltaTime * speed;
            
            Vector3 viewportProjection = _mainCamera.WorldToViewportPoint(transform.position + translation);
            if(viewportProjection.x is < 0 or > 1 || viewportProjection.y is < 0 or > 1) return;
            
            transform.Translate(translation, Space.World);
        }
        
        private void OnDisable()
        {
            InputHandler.DirectionButtonPressed -= OnDirectionButtonPressed;
        }
    }
}