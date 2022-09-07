using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Players;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player = Characters.Players.Player;

namespace Network
{
    public class NetworkEventsHandler : BaseGameHandler<NetworkEventsHandler>
    {
        public static event Action<PlayerData> PlayerJoinedRoom;
        
        public static event Action<PlayerData> GameCreatedForPlayer;

        private List<PlayerData> _playersData = new();
        private PlayerData _myPlayer;
        
        private readonly Dictionary<byte, Action<EventData>> _handlersMap = new()
        {
            {1, OnPlayerJoinedRoom},
            {2, OnGameCreated}
        };

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnEventReceived(EventData obj)
        {
            if (_handlersMap.ContainsKey(obj.Code))
            {
                _handlersMap[obj.Code].Invoke(obj);
            }
        }

        private static void OnPlayerJoinedRoom(EventData data)
        {
            string playerName = (string)((object[]) data.CustomData)[0];
            int skinVariant = (int)((object[]) data.CustomData)[1];

            PlayerData player = new (playerName, skinVariant);
            if (Instance._playersData.Count == 0)
            {
                Instance._myPlayer = player;
            }
            Instance._playersData.Add(player);
            
            Debug.Log($"Игрок {playerName} подключился!");
            
            PlayerJoinedRoom?.Invoke(player);
        }
        
        private static void OnGameCreated(EventData data)
        {
            Instance.StartCoroutine(WaitForRoomLoaded(data));
        }

        private static IEnumerator WaitForRoomLoaded(EventData data)
        {
            while (true)
            {
                if (SceneManager.GetActiveScene().isLoaded && SceneManager.GetActiveScene().name == "Room")
                {
                    break;
                }
                yield return null;
            }
            
            Dictionary<string, int> dict = (Dictionary<string, int>) data.CustomData;
            PlayerData myPlayer = dict.Keys
                .Select(key => new PlayerData(key, dict[key]))
                .ToList()
                .Find(p => p.Name == Instance._myPlayer.Name);
            
            GameCreatedForPlayer?.Invoke(myPlayer);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Room" && PhotonNetwork.IsMasterClient)
            {
                Dictionary<string, int> dict = new();
                _playersData.ForEach(data => dict.Add(data.Name, data.SkinVariant));
                SendNetworkEvent(dict);
            }

            if (scene.name == "ConnectMenu")
            {
                Destroy(gameObject);
            }
        }
        
        private void SendNetworkEvent(Dictionary<string, int> content)
        {
            RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All, CachingOption = EventCaching.AddToRoomCache};
            PhotonNetwork.RaiseEvent(2, content, options, SendOptions.SendReliable);
        }

        
        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}