using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreboardLine : MonoBehaviourPun
    {
        [SerializeField]
        private TextMeshProUGUI _playerNameText;
        [SerializeField]
        private TextMeshProUGUI _scoreText;
        
        private string _playerName;
        private int _score;

        public string GetPlayerName()
        {
            return _playerName;
        }
                
        public void SetPlayerName(string playerName)
        {
            _playerName = playerName;
            _playerNameText.text = _playerName;
        }

        public void SetTextColor(Color color)
        {
            _playerNameText.color = color;
            _scoreText.color = color;
        }

        public int GetScore()
        {
            return _score;
        }
        
        public void SetScore(int score)
        {
            _score = score;
            _scoreText.text = _score.ToString();
        }
        
    }
}