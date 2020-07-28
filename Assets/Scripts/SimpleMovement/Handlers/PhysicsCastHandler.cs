using System;
using SimpleMovement.Enums;
using SimpleMovement.Utils;
using UnityEngine;

namespace SimpleMovement.Handlers
{
    [Serializable]
    public class PhysicsCastHandler 
    {
        [SerializeField] private CastType _castType;
        [SerializeField] private CastParameters _castParameters;
        [SerializeField] private LayerMask _mask;

        private RaycastHit _hit;
        private bool _wasHit = false;
        private Func<bool> _physicsCast;
        private Func<RaycastHit[]> _physicsCastAll;

        public event Action<RaycastHit> OnHit;
        public event Action<RaycastHit[]> OnHits;
        public event Action<RaycastHit> OnHitUpdate;
        public event Action OnHitLost;

        public CastParameters CastParameters => _castParameters;

        public CastType CastType
        {
            get => _castType;
            set
            {
                _castType = value;
                Init();
            }
        }

        // TODO: make this lazy?
        public void Init()
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

        public void Reset()
        {
            _wasHit = false;
        }

        public void CastAll()
        {
            OnHits?.Invoke(_physicsCastAll());
        }

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

        private bool RayCast()
        {
            return Physics.Raycast(_castParameters.Origin,
                _castParameters.Direction, out _hit, _castParameters.Length, _mask);
        }

        private RaycastHit[] RayCastAll()
        {
            return Physics.RaycastAll(_castParameters.Origin, 
                _castParameters.Direction, _castParameters.Length, _mask);
        }

        private bool SphereCast()
        {
            return Physics.SphereCast(_castParameters.Origin, _castParameters.Radius,
                _castParameters.Direction, out _hit, _castParameters.Length,_mask);
        }

        private RaycastHit[] SphereCastAll()
        {
            return Physics.SphereCastAll(_castParameters.Origin, _castParameters.Radius,
                _castParameters.Direction, _castParameters.Length, _mask);
        }
    }
}
