using System;
using UnityEngine;

namespace SimpleMovement.Utils
{
    [Serializable]
    public class CastParameters
    {
        [SerializeField] private Transform _originRef;
        [SerializeField] private Transform _directionRef;
        [SerializeField] private float _length;
        [SerializeField] private float _radius;

        // TODO: Editor script that hide refs when thoes bools enable
        [SerializeField] private bool _customOrigin;       
        [SerializeField] private bool _customDirection;

        private Vector3 _origin;
        private Vector3 _direction;

        private Func<Vector3> _directionMethod;
        private Func<Vector3> _originMethod;

        public void Init()
        {
            _originMethod = _customOrigin ? () => _origin : (Func<Vector3>) GetOrigin;

            _directionMethod = (_customDirection || _customOrigin) ?
                () => _direction : (Func<Vector3>)GetDirection;
        }

        public bool CustomOrigin
        {
            get => _customOrigin;
            set
            {
                _customOrigin = value;
                Init();
            }
        }

        public bool CustomDirection
        {
            get => _customDirection;
            set
            {
                _customDirection = value;
                Init();
            }
        }

        public Transform OriginRef
        {
            get => _originRef;
            set => _originRef = value;
        }

        public Transform DirectionRef
        {
            get => _directionRef;
            set => _directionRef = value;
        }

        public Vector3 Origin
        {
            get => _originMethod();
            set => _origin = value;
        }

        public Vector3 Direction
        {
            get => _directionMethod();
            set => _direction = value;
        }

        public float Length
        {
            get => _length;
            set => _length = value;
        }

        public float Radius
        {
            get => _radius;
            set => _radius = value;
        }

        private Vector3 GetDirection() => (_directionRef.position - GetOrigin()).normalized;

        private Vector3 GetOrigin() => _originRef.position;
    }
}
