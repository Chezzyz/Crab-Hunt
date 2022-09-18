using System;
using System.Collections.Generic;
using Characters.Players;
using Services;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerNamesHandler : MonoBehaviour
    {
        [SerializeField] private Transform _textsParent;
        [SerializeField] private PlayerNameText _prefab;

        private void OnEnable()
        {
            PlayerSpawner.PlayerCreated += OnPlayerCreated;
        }

        private void OnPlayerCreated(Player player)
        {
            CreateNameText(_prefab, player, _textsParent);
        }
        
        private void CreateNameText(PlayerNameText prefab, Player player, Transform parent)
        {
            PlayerNameText text = Instantiate(prefab, parent);
            text.SetPlayer(player);
        }
    }
}