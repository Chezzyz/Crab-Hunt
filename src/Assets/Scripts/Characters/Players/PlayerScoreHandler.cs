﻿using System;
using UnityEngine;

namespace Characters.Players
{
    public class PlayerScoreHandler : MonoBehaviour
    {
        [SerializeField] private int _score;

        public static event Action<Player, int, int> ScoreChanged; 

        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        public int GetScore()
        {
            return _score;
        }

        public void ChangeScore(int diff)
        {
            if(_score + diff < 0) return;
            _score += diff;
            ScoreChanged?.Invoke(_player, diff, _score);
        }
    }
}