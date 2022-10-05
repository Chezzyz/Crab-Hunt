using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Players;
using Network;
using Other;
using Photon.Pun;
using UnityEngine;

namespace Services
{
    public class PlayerSpawner : BaseGameHandler<PlayerSpawner>
    {
        [SerializeField] private string _prefabFolder;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private NetworkInstantiator _instantiator;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private List<Sprite> _skinsList;
        [SerializeField] private Transform _playersParent;

        private PlayerData _myPlayer;

        public static event Action<Player> PlayerCreated;

        private void OnEnable()
        {
            SceneLoader.SceneLoaded += OnSceneLoaded;
            SceneLoader.SceneLoadingStarted += OnSceneLoadingStarted;
        }
        
        private void OnSceneLoadingStarted(string scene)
        {
            if (scene is "Lobby" && Instance != null || scene is "ConnectMenu") Destroy(gameObject);
        }

        private void OnSceneLoaded(string scene)
        {
            if (scene is "Lobby" or "Room")
            {
                if (NetworkPlayerHandler.Instance.IsObserver) return;
                
                _spawnPoints = FindObjectsOfType<SpawnPoint>().Select(point => point.transform).ToList();
                _instantiator = FindObjectOfType<NetworkInstantiator>();
                _playersParent = FindObjectOfType<PlayersPool>().transform;

                if (_myPlayer == null)
                {
                    StartCoroutine(NetworkInstantiateMyPlayer(NetworkPlayerHandler.Instance.PlayerName));
                }
                else
                {
                    StartCoroutine(NetworkInstantiateMyPlayer(_myPlayer.Name, _myPlayer.SkinVariant));
                }
            }
        }

        private IEnumerator NetworkInstantiateMyPlayer(string playerName, int index = -1)
        {
            if (index == -1)
            {
                yield return new WaitForSeconds(1);
                index = FindObjectsOfType<Player>(true).Length;
            }

            Player player = _instantiator.InstantiateObject<Player>(_prefabFolder + _playerPrefab.name,
                _spawnPoints[index].position,
                _playersParent);
            GetComponent<PhotonView>()
                .RPC(nameof(SendEventRPC), RpcTarget.AllBuffered, player.GetViewId(), playerName, index);

            _myPlayer = new PlayerData(playerName, index);
        }

        [PunRPC]
        private void SendEventRPC(int playerId, string playerName, int skinIndex)
        {
            Player player = PhotonView.Find(playerId).GetComponent<Player>();
            player.SetName(playerName);
            player.SetSkin(_skinsList[skinIndex]);

            PlayerCreated?.Invoke(player);
        }

        private void OnDisable()
        {
            SceneLoader.SceneLoaded -= OnSceneLoaded;
            SceneLoader.SceneLoadingStarted -= OnSceneLoadingStarted;
        }
    }
}