using System.Collections.Generic;
using System.Linq;
using Characters.Players;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Network;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreboardHandler : MonoBehaviour
    {
        [SerializeField] private ScoreboardLine _playerScorePrefab;
        [SerializeField] private RectTransform _lineParent;
        [SerializeField] private float _sortAnimationDuration;

        private List<ScoreboardLine> _scoreboardLines = new();
        private List<Vector2> _linePositions = new();

        private void OnEnable()
        {
            PlayerScoreHandler.ScoreChanged += OnScoreChanged;
            PlayerSpawner.PlayerCreated += OnPlayerCreated;

            for (int i = 0; i < 4; i++)
            {
                float posY = -(_lineParent.rect.height / 8) * (2 * i + 1);
                _linePositions.Add(new Vector2(0, posY));
            }
        }

        public List<ScoreboardLine> GetScoreboardLines()
        {
            return _scoreboardLines;
        }

        private void OnPlayerCreated(Player player)
        {
            ScoreboardLine line = AddLineToBoard(_playerScorePrefab, player, _lineParent);
            line.GetComponent<RectTransform>().anchoredPosition = _linePositions[_scoreboardLines.Count];
            _scoreboardLines.Add(line);
        }

        private ScoreboardLine AddLineToBoard(ScoreboardLine prefab, Player player, Transform tableTransform)
        {
            ScoreboardLine line = Instantiate(prefab, tableTransform);
            line.SetPlayerName(player.GetName());
            return line;
        }

        private void OnScoreChanged(Player player, int diff, int score)
        {
            ScoreboardLine boardLine = _scoreboardLines.Find(line => line.GetPlayerName() == player.GetName());
            boardLine.SetScore(score);
            SortScoreboard(_scoreboardLines, _linePositions);
        }

        private void SortScoreboard(List<ScoreboardLine> lines, List<Vector2> positions)
        {
            lines.Sort((line1, line2) => line1.GetScore().CompareTo(line2.GetScore()));
            lines.Reverse();
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].GetComponent<RectTransform>()
                    .DOAnchorPosY(positions[i].y, _sortAnimationDuration).SetEase(Ease.OutSine);
            }
        }

        private void OnDisable()
        {
            PlayerScoreHandler.ScoreChanged -= OnScoreChanged;
            PlayerSpawner.PlayerCreated -= OnPlayerCreated;
        }
    }
}