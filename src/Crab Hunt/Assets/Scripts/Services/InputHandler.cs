using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Players;
using Photon.Pun;
using UnityEngine;

namespace Services
{
    public class InputHandler : MonoBehaviourPun
    {
        public static event Action<Vector2Int> DirectionButtonPressed;
        public static event Action ShootButtonPressed;
        public static event Action<Player, bool> StateChanged; 

        private Player _player;
        private bool _isDisabled;

        
        private readonly Dictionary<KeyCode, Action> _buttonsMap = new Dictionary<KeyCode, Action>()
        {
            { KeyCode.W, () => DirectionButtonPressed?.Invoke(Vector2Int.up) },
            { KeyCode.UpArrow, () => DirectionButtonPressed?.Invoke(Vector2Int.up) },
            { KeyCode.A, () => DirectionButtonPressed?.Invoke(Vector2Int.left) },
            { KeyCode.LeftArrow, () => DirectionButtonPressed?.Invoke(Vector2Int.left) },
            { KeyCode.D, () => DirectionButtonPressed?.Invoke(Vector2Int.right) },
            { KeyCode.RightArrow, () => DirectionButtonPressed?.Invoke(Vector2Int.right) },
            { KeyCode.S, () => DirectionButtonPressed?.Invoke(Vector2Int.down) },
            { KeyCode.DownArrow, () => DirectionButtonPressed?.Invoke(Vector2Int.down) },
        };
        
        private void OnEnable()
        {
            GameTimerHandler.GameEnded += OnGameEnded;
        }

        private void OnGameEnded()
        {
            SetState(false);
        }

        private void Start()
        {
            _player = GetComponent<Player>();
            if (_player.GetComponent<PhotonView>() == null || !_player.GetComponent<PhotonView>().IsMine) return;
            StartCoroutine(DirectionButtonPressCheckLoop());
            StartCoroutine(ShootButtonPressCheckLoop());
        }

        public void SetState(bool state)
        {
            StateChanged?.Invoke(_player, state);
            _isDisabled = !state;
        }

        private IEnumerator DirectionButtonPressCheckLoop()
        {
            List<KeyCode> keyCodesStack = new();
            KeyCode currentKeyCode = KeyCode.None;
            keyCodesStack.Add(currentKeyCode);

            while (true)
            {
                if (_isDisabled)
                {
                    if (keyCodesStack.Count > 1)
                    {
                        keyCodesStack.Clear();
                        keyCodesStack.Add(KeyCode.None);
                    }
                    yield return null;
                    continue;
                }

                //Если в стэке есть кнопка, которую отжали, убираем ее из стэка
                keyCodesStack = DeleteKeyUpFromStack(keyCodesStack);
                
                currentKeyCode = GetLastPressedKeyCode(keyCodesStack);
                
                SendEvent(currentKeyCode);

                yield return null;
            }
        }

        private List<KeyCode> DeleteKeyUpFromStack(List<KeyCode> stack)
        {
            List<KeyCode> codesToDelete = _buttonsMap.Keys.Where(code => !Input.GetKey(code) && stack.Contains(code)).ToList();
            
            return codesToDelete.Count == 0 ? stack : new List<KeyCode>(stack.Where(code => !codesToDelete.Contains(code)));
        }

        private KeyCode GetLastPressedKeyCode(List<KeyCode> keyCodesStack)
        {
            //Если нажата кнопка передвижения, которая отличается от предыдущей возвращаем ее, иначе предыдущую
            foreach (KeyCode code in _buttonsMap.Keys.Where(code =>
                         Input.GetKeyDown(code) && !keyCodesStack.Contains(code)))
            {
                keyCodesStack.Add(code);
            }

            return keyCodesStack.Last();
        }

        private void SendEvent(KeyCode code)
        {
            if (_buttonsMap.ContainsKey(code))
            {
                _buttonsMap[code].Invoke();
            }
        }

        private IEnumerator ShootButtonPressCheckLoop()
        {
            while (true)
            {
                if (_isDisabled)
                {
                    yield return null;
                    continue;
                }
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ShootButtonPressed?.Invoke();
                }

                yield return null;
            }
        }
        
        private void OnDisable()
        {
            GameTimerHandler.GameEnded -= OnGameEnded;
        }
    }
}