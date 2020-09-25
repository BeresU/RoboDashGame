using System;
using System.Threading;
using Extensions;
using UnityEngine;

namespace RoboDash.Defense
{
    public class DefenseHandler : MonoBehaviour, IDefenseData
    {
        [SerializeField] private float _defenseTime;
        [SerializeField] private float _minYForDirectionForDefense = -0.5f;
        private CancellationTokenSource _ct = new CancellationTokenSource();
        public event Action DefenseStarted;
        
        public bool IsDefending { get; private set; }
        public float DefenseTime => _defenseTime;

        private void OnDestroy() => _ct.Cancel();

        public void OnSwipe(Vector2 direction)
        {
            if(direction.y > _minYForDirectionForDefense) return;
            OnDefense();
        }

        private void OnDefense()
        {
            DefenseStarted?.Invoke();
            ActivateDefenseTimer();
        }

        private async void ActivateDefenseTimer()
        {
            IsDefending = true;
            await TimeSpan.FromSeconds(_defenseTime);
            if(_ct.IsCancellationRequested) return;
            IsDefending = false;
        }
    }
}
