using System;
using DG.Tweening;
using UnityEngine;

namespace Visual
{
    public class BonusAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _bonusBody;
        
        [SerializeField] private Ease _animationEase;
        [SerializeField] private float _animationTime;
        [SerializeField] private float _animationDelay;
        [SerializeField] private float _animationHeight;
        
        private void OnEnable()
        {
            StartAnimation(_animationTime, _animationHeight, _animationEase, _animationDelay);
        }

        private void StartAnimation(float duration, float height, Ease ease, float delay)
        {
            var seq = DOTween.Sequence();
            Tween move = _bonusBody.transform.DOLocalMoveY(-height, duration);
            move.SetEase(ease);
            seq.Append(move);
            seq.SetLoops(-1, LoopType.Yoyo).PrependInterval(delay);
            seq.Play();
        }
       
    }
}