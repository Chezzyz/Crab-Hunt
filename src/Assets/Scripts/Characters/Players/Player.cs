using Characters.Players.Shooting;
using Photon.Pun;
using Services;
using UnityEngine;

namespace Characters.Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private string _playerName;

        [SerializeField] private PlayerMove _playerMove;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private PlayerScoreHandler _scoreHandler;

        private bool _isImmuneToStun;

        public int GetViewId()
        {
            return GetComponent<PhotonView>().ViewID;
        }
        
        public void SetName(string playerName)
        {
            _playerName = playerName;
        }

        public void SetSkin(Sprite skin)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = skin;
        }
        
        public string GetName()
        {
            return _playerName;
        }

        public float GetSpeed()
        {
            return _playerMove.GetSpeed();
        }

        public void SetSpeed(float value)
        {
            _playerMove.SetSpeed(value);
        }

        public void SetIsImmune(bool value)
        {
            _isImmuneToStun = value;
        }

        public bool GetIsImmune()
        {
            return _isImmuneToStun;
        }

        public void SetInputState(bool state)
        {
            if(!state && _isImmuneToStun) return;
            
            _inputHandler.SetState(state);
        }

        public void ChangeScore(int diff)
        {
            _scoreHandler.ChangeScore(diff);
        }
        
    }
}