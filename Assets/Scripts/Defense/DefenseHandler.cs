using System;
using System.Threading;
using System.Threading.Tasks;
using Extensions;
using RoboDash.Attack;
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
        
        private bool _shouldReflect;
        
        private readonly CancellationTokenSource _ct = new CancellationTokenSource();

        private IAttackHandler _attackHandler;
        private IDamageHanalder _damageHanalder;
        public event Action DefenseStarted;
        
        public bool IsDefending { get; private set; }
        public bool IsReflecting => _shouldReflect;
        public float DefenseTime => _defenseTime;

        private void OnDestroy() => _ct.Cancel();
        
        public void OnSwipe(Vector2 direction)
        {
            if(direction.y > _minYForDirectionForDefense) return;
            OnDefense();
        }

        private void OnDefense()
        {
            if(IsDefending) return;
          //  ActivateReflectCounter();
            DefenseStarted?.Invoke();
            ActivateDefenseTimer();
        }

        private async void ActivateReflectCounter()
        {
            _shouldReflect = true;
            
            Debug.Log($"Reflecting");
            
            for (var i = 0; i < _framesForReflect; i++)
            {
                await Task.Yield();
            }

            _shouldReflect = false;
            
            Debug.Log($"Finished Reflecting");
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
