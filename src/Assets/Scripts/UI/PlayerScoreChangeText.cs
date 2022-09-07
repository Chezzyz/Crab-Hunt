using System;
using Characters.Players;
using DG.Tweening;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerScoreChangeText : MonoBehaviourPun
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _animationHeight;
        [SerializeField] private float _animationDuration;

        private Camera _mainCamera;

        private Player _player;

        private void Start()
        {
            _mainCamera = FindObjectOfType<Camera>();
        }

        public void StartAnimation()
        {
            Vector3 screenPos = _mainCamera.WorldToScreenPoint(_player.transform.position);
            screenPos.z = 0;
            _text.enabled = true;
            _text.transform.position = screenPos;
            _text.DOFade(1, _animationDuration / 8);
            _text.transform.DOMoveY(screenPos.y + _animationHeight, _animationDuration);
            _text.DOFade(0, _animationDuration).OnComplete(() => _text.enabled = false);
        }

        public void SetPlayer(Player player)
        {
            _player = player;
        }

        public Player GetPlayer()
        {
            return _player;
        }

        public void SetText(string diff)
        {
            _text.text = diff;
        }

        public void SetTextColor(Color color)
        {
            _text.color = color;
        }
    }
}