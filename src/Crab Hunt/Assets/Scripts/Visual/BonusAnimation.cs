using System;
using DG.Tweening;
using UnityEngine;

namespace Visual
{
    public class BonusAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [Header("Idle Animation")]
        [SerializeField] private Ease _animationEase;
        [SerializeField] private float _animationTime;
        [SerializeField] private float _animationDelay;
        [SerializeField] private float _animationHeight;
        
        [Header("Pickup Animation")]
        [SerializeField] private Ease _pickupAnimationEase;
        [SerializeField] private float _pickupAnimationTime;
        [SerializeField] private float _pickupAnimationScale;
        
        private void OnEnable()
        {
            StartIdleAnimation(_animationTime, _animationHeight, _animationEase, _animationDelay);
        }

        public void StartPickupAnimation()
        {
            StartPickupAnimation(_pickupAnimationTime, _pickupAnimationScale, _pickupAnimationEase);    
        }

        public void SetDefault()
        {
            _spriteRenderer.color = Color.white;
            _spriteRenderer.transform.localScale = Vector3.one;
        }
        
        private void StartPickupAnimation(float duration, float scale, Ease ease)
        {
            _spriteRenderer.transform.DOScale(scale, duration).SetEase(ease);
            _spriteRenderer.DOFade(0, duration).SetEase(ease);
        }

        private void StartIdleAnimation(float duration, float height, Ease ease, float delay)
        {
            var seq = DOTween.Sequence();
            Tween move = _spriteRenderer.transform.DOLocalMoveY(-height, duration);
            move.SetEase(ease);
            seq.Append(move);
            seq.SetLoops(-1, LoopType.Yoyo).PrependInterval(delay);
            seq.Play();
        }
       
    }
}