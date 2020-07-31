using System;
using SimpleMovement.Enums;
using SimpleMovement.Utils;
using UnityEngine;

namespace SimpleMovement.Handlers.PhysicCastHandler
{
    [Serializable]
    public abstract class PhysicsCastHandlerBase <T> where T : struct
    {
        [SerializeField] protected CastType _castType;
        [SerializeField] protected CastParameters _castParameters;
        [SerializeField] protected LayerMask _mask;
        
        protected T _hit;
        private bool _wasHit = false;
        private Func<bool> _physicsCast;
        private Func<T[]> _physicsCastAll;

        public event Action<T> OnHit;
        public event Action<T[]> OnHits;
        public event Action<T> OnHitUpdate;
        public event Action OnHitLost;

        public CastParameters CastParameters => _castParameters;
        
        public virtual void Init()
        {
            _castParameters.Init();

            switch (_castType)
            {
                case CastType.Ray:
                    _physicsCast = RayCast;
                    _physicsCastAll = RayCastAll;
                    break;
                case CastType.Sphere:
                    _physicsCast = SphereCast;
                    _physicsCastAll = SphereCastAll;
                    break;
            }
        }
        
        public void Reset() => _wasHit = false;
        public void CastAll() => OnHits?.Invoke(_physicsCastAll());

        public void Cast()
        {
            if (_physicsCast())
            {
                OnRayHit();
            }
            else
            {
                HitLost();
            }
        }

        private void HitLost()
        {
            if (!_wasHit) return;
            _wasHit = false;
            OnHitLost?.Invoke();
        }

        private void OnRayHit()
        {
            if (!_wasHit)
            {
                _wasHit = true;
                OnHit?.Invoke(_hit);
            }

            OnHitUpdate?.Invoke(_hit);
        }

        protected abstract bool RayCast();
        protected abstract T[] RayCastAll();
        protected abstract bool SphereCast();
        protected abstract T[] SphereCastAll();
    }
}
