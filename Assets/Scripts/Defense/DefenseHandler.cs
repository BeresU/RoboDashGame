using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Extensions;
using RoboDash.Attack.Interfaces;
using RoboDash.Damage;
using UnityEngine;

namespace RoboDash.Defense
{
    public class DefenseHandler : MonoBehaviour, IDefenseHandler
    {
        [SerializeField] private float _defenseTime;
        [SerializeField] private float _minYForDirectionForDefense = -0.5f;
        [SerializeField] private int _framesForReflect = 3;
        [SerializeField] private Animator _roboAnimator;

        private readonly CancellationTokenSource _ct = new CancellationTokenSource();

        private IAttackHandler _attackHandler;
        private IDamageHanalder _damageHanalder;
        public event Action DefenseStarted;

        public bool IsDefending { get; private set; }
        public bool IsReflecting { get; private set; }

        public float DefenseTime => _defenseTime;

        private void OnDestroy() => _ct.Cancel();

        public void OnSwipe(Vector2 direction)
        {
            if (direction.y > _minYForDirectionForDefense) return;
            OnDefense();
        }

        private void OnDefense()
        {
            if (IsDefending) return;
            DefenseStarted?.Invoke();
            StartCoroutine(ActivateReflectCounterRoutine());
            ActivateDefenseTimer();
        }
        
        
        private IEnumerator ActivateReflectCounterRoutine()
        {
            IsReflecting = true;
            
            yield return null;

            var currentState = _roboAnimator.GetCurrentAnimatorStateInfo(0);
            var animationClip = _roboAnimator.GetCurrentAnimatorClipInfo(0)[0];

            while (currentState.normalizedTime < 0.9f)
            {
                var currentFrame =
                    (int) (currentState.normalizedTime * (animationClip.clip.length * animationClip.clip.frameRate));
                
                yield return null;
                
                currentState = _roboAnimator.GetCurrentAnimatorStateInfo(0);

                if (currentFrame >= _framesForReflect)
                {
                    IsReflecting = false;
                }
            }
        }

        private async void ActivateDefenseTimer()
        {
            IsDefending = true;
            await TimeSpan.FromSeconds(_defenseTime);
            if (_ct.IsCancellationRequested) return;
            IsDefending = false;
        }
    }
}