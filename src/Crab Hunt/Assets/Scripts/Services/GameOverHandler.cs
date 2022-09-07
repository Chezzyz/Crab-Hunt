using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Services
{
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverTable;
        [SerializeField] private Transform _lineParent;
        [SerializeField] private ScoreboardHandler _scoreboardHandler;
        [SerializeField] private ScoreboardLine _linePrefab;

        [Header("Table Colors")] 
        [SerializeField]
        private Color _firstPlaceColor;
        [SerializeField]
        private Color _secondPlaceColor;
        [SerializeField]
        private Color _thirdPlaceColor;
        [SerializeField]
        private Color _fourthPlaceColor;

        private List<Color> _colours;
        
        private void OnEnable()
        {
            GameTimerHandler.GameEnded += OnGameEnded;
            _colours = new List<Color>() { _firstPlaceColor, _secondPlaceColor, _thirdPlaceColor, _fourthPlaceColor };
        }

        private void OnGameEnded()
        {
            _gameOverTable.SetActive(true);
            SetupTable(_scoreboardHandler.GetScoreboardLines(), _linePrefab);
        }

        private void SetupTable(List<ScoreboardLine> lines, ScoreboardLine prefab)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                ScoreboardLine line = Instantiate(prefab, _lineParent);
                ScoreboardLine current = lines[i];
                line.SetPlayerName(current.GetPlayerName());
                line.SetScore(current.GetScore());
                line.SetTextColor(_colours[i]);
            }
        }
        
        private void OnDisable()
        {
            GameTimerHandler.GameEnded -= OnGameEnded;
        }
    }
}