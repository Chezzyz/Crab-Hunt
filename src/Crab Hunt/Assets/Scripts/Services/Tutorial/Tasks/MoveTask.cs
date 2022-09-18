using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services.Tutorial.Tasks
{
    public class MoveTask : AbstractTask
    {
        private Dictionary<Vector2Int, bool> _directionsCompleteMap = new()
        {
            { Vector2Int.up, false },
            { Vector2Int.left, false },
            { Vector2Int.down, false },
            { Vector2Int.right, false }
        };

        private void OnEnable()
        {
            InputHandler.DirectionButtonPressed += OnDirectionButtonPressed;
        }

        protected override void OnActive()
        {
        }

        private void OnDirectionButtonPressed(Vector2Int dir)
        {
            if(!_isActive || _isCompleted) return;
            
            _directionsCompleteMap[dir] = true;
            if (_directionsCompleteMap.All(pair => pair.Value == true))
            {
                CompleteTask();
            }
        }
        
        private void OnDisable()
        {
            InputHandler.DirectionButtonPressed -= OnDirectionButtonPressed;
        }
    }
}