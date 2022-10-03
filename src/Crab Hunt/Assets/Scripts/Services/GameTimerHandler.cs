using System;
using System.Collections;
using Characters.Players;
using Network;
using Other;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Services
{
    public class GameTimerHandler : BaseGameHandler<GameTimerHandler>
    {
        [SerializeField] private TextMeshProUGUI _minutesText;
        [SerializeField] private TextMeshProUGUI _secondsText;
        [SerializeField] private GameObject _timer;
        
        private int _timeInSeconds;
        private int _minutes;
        private int _seconds;
        private TextMeshProUGUI _timerText;

        public static event Action GameEnded; 

        private void OnEnable()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                _timer.SetActive(false);
                return;
            }
            NetworkEventsHandler.GameCreatedForPlayer += OnGameStarted;
            SceneLoader.SceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(string scene)
        {
            if(scene == "Main") Destroy(gameObject);
        }

        private void OnGameStarted(PlayerData obj)
        {
            _timeInSeconds = _minutes * 60 + _seconds;
            
            GetComponent<PhotonView>().RPC(nameof(StartTimer), RpcTarget.AllBuffered, _timeInSeconds);
        }

        [PunRPC]
        private void StartTimer(int time)
        {
            TextMeshProUGUI timerText = FindObjectOfType<TimerText>().GetComponent<TextMeshProUGUI>();
            StartCoroutine(TimerLoop(time, timerText));
        }

        private IEnumerator TimerLoop(int time, TMP_Text timerText)
        {
            WaitForSeconds second = new(1);
            while (time > 0)
            {
                time -= 1;
                timerText.text = $"{(time / 60):00}:{(time % 60):00}";
                yield return second;
            }
            GameEnded?.Invoke();
        }

        public void ChangeMinutes(int value)
        {
            _minutes += value;
            if(_minutes is < 0 or > 60) _minutes = 0;
            _minutesText.text = $"{_minutes:00}";
        }

        public void ChangeSeconds(int value)
        {
            _seconds += value;
            if(_seconds is < 0 or >= 60) _seconds = 0;
            _secondsText.text =$"{_seconds:00}";
        }

        private void OnDisable()
        {
            NetworkEventsHandler.GameCreatedForPlayer -= OnGameStarted;
            SceneLoader.SceneLoaded -= OnSceneLoaded;
        }
    }
}