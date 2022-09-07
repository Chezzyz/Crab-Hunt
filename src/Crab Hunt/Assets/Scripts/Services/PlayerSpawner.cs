using System;
using System.Collections.Generic;
using Characters.Players;
using Network;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services
{
    public class PlayerSpawner : MonoBehaviourPun
    {
        [SerializeField] private string _prefabFolder;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private NetworkInstantiator _instantiator;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private List<Sprite> _skinsList;
        [SerializeField] private Transform _playersParent;

        public static event Action<Player> PlayerCreated;

        private bool _myPlayerCreated = false;

        private void OnEnable()
        {
            NetworkEventsHandler.PlayerJoinedRoom += OnPlayerJoinedRoom;
            NetworkEventsHandler.GameCreatedForPlayer += OnPlayerJoinedRoom;
        }

        private void OnPlayerJoinedRoom(PlayerData playerData)
        {
            if (_myPlayerCreated) return;

            int index = playerData.SkinVariant;
            Player player = _instantiator.InstantiateObject<Player>(_prefabFolder + _playerPrefab.name,
                _spawnPoints[index].position,
                _playersParent);
            photonView.RPC(nameof(SetPlayerNameRPC), RpcTarget.AllBuffered, player.GetViewId(), playerData.Name);
            photonView.RPC(nameof(SendEventRPC), RpcTarget.AllBuffered, player.GetViewId());
            photonView.RPC(nameof(SetPlayerSpriteRPC), RpcTarget.AllBuffered, player.GetViewId(), index);
            _myPlayerCreated = true;
        }

        [PunRPC]
        private void SetPlayerNameRPC(int playerId, string playerName)
        {
            Player player = PhotonView.Find(playerId).GetComponent<Player>();
            player.SetName(playerName);
        }

        [PunRPC]
        private void SendEventRPC(int playerId)
        {
            Player player = PhotonView.Find(playerId).GetComponent<Player>();
            PlayerCreated?.Invoke(player);
        }

        [PunRPC]
        private void SetPlayerSpriteRPC(int playerId, int skinIndex)
        {
            Player player = PhotonView.Find(playerId).GetComponent<Player>();
            player.SetSkin(_skinsList[skinIndex]);
        }
        
        private void OnDisable()
        {
            NetworkEventsHandler.PlayerJoinedRoom -= OnPlayerJoinedRoom;
            NetworkEventsHandler.GameCreatedForPlayer -= OnPlayerJoinedRoom;
        }
    }
}