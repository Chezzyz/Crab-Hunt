using System.Collections.Generic;
using Characters.Players;
using Services;
using Services.Tutorial;
using UnityEngine;


namespace UI
{
    public class PlayerScoreEffectHandler : MonoBehaviour
    {
        [SerializeField] private PlayerScoreChangeText _playerChangeTextPrefab;
        [SerializeField] private Transform _textsParent;
        [SerializeField] private Color _plusColor;
        [SerializeField] private Color _minusColor;

        private List<PlayerScoreChangeText> _playerTexts = new();

        private void OnEnable()
        {
            PlayerScoreHandler.ScoreChanged += OnScoreChanged;
            PlayerSpawner.PlayerCreated += OnPlayerCreated;
            TutorialScript.TutorialStarted += OnPlayerCreated;
        }

        private void OnPlayerCreated(Player player)
        {
            AddTextToList(_playerChangeTextPrefab, player, _textsParent);
        }

        private void OnScoreChanged(Player player, int diff, int score)
        {
            PlayerScoreChangeText text = _playerTexts.Find(txt => txt.GetPlayer() == player);
            string sign = diff > 0 ? "+" : "";
            text.SetTextColor(diff > 0 ? _plusColor : _minusColor);
            text.SetText(sign + diff);
            text.StartAnimation();
        }

        private void AddTextToList(PlayerScoreChangeText prefab, Player player, Transform parent)
        {
            PlayerScoreChangeText text = Instantiate(prefab, parent);
            text.SetPlayer(player);
            _playerTexts.Add(text);
        }

        private void OnDisable()
        {
            PlayerScoreHandler.ScoreChanged -= OnScoreChanged;
            PlayerSpawner.PlayerCreated -= OnPlayerCreated;
            TutorialScript.TutorialStarted -= OnPlayerCreated;
        }
    }
}