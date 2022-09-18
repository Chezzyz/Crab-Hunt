using Characters.Players;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PlayerNameText : MonoBehaviour
    {
        private Player _player;
        private Camera _mainCamera;
        private TextMeshProUGUI _text;
        private RectTransform _textTransform;
        
        private void OnEnable()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _mainCamera = FindObjectOfType<Camera>();
            _textTransform = _text.GetComponent<RectTransform>();
        }
        
        public void SetPlayer(Player player)
        {
            _player = player;
            _text.text = player.GetName();
        }
        
        private void Update()
        {
            Vector3 screenPos = _mainCamera.WorldToScreenPoint(_player.transform.position);
            screenPos.z = 0;
            _text.transform.position = screenPos;
        }
    }
}