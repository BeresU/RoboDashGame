using UnityEngine;

namespace SimpleMovement.Modules
{
    [System.Serializable]
    public class RigidBody2DMovementModule : MovementModuleBase<Vector2>
    {
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _graviryMultiplier = 2.5f;
        [SerializeField] private float _maxSpeed = 10f;

        [SerializeField] private float _timeZeroToMax = 0.2f;
        [SerializeField] private float _timeMaxToZero = 1f;

        [SerializeField] private Rigidbody2D _rigidBody;

        private float _speed;

        private float _accelRatePerSecond;
        private float _deacelRatePerSecond;

        public override Transform ControlledTransform => _rigidBody.transform;

        public override float Speed => _speed;

        public override float CurrentSpeed => _speed;

        public override float JumpForce
        {
            get => _jumpForce;
            set => _jumpForce = value;
        }

        public override float MaxSpeed
        {
            get => _maxSpeed;
            set => _maxSpeed = value;
        }

        public override void Init()
        {
            _rigidBody.velocity = Vector2.zero;
            _accelRatePerSecond = _maxSpeed / _timeZeroToMax;
            _deacelRatePerSecond = -_maxSpeed / _timeMaxToZero;
        }
        
        public override void Move(Vector2 direction) =>
            _rigidBody.MovePosition(_rigidBody.position + direction * _speed * Time.fixedDeltaTime);

        public override void MoveTo(Vector2 position)
        {
            var target = Vector2.MoveTowards(_rigidBody.position, position, _speed * Time.deltaTime);
            Accelerate();
            _rigidBody.MovePosition(target);
        }

        public override void Stop()
        {
            _rigidBody.velocity = Vector2.zero;
            _speed = 0;
        }

        public override void Jump() =>
            _rigidBody.AddForce(Vector2.up *
                                Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y), ForceMode2D.Force);
        
        public override void Rotate(Vector2 position)
        {
            var targetRot = Quaternion.LookRotation(position);
            _rigidBody.MoveRotation(targetRot);
        }

        public override void Accelerate()
        {
            _speed += _accelRatePerSecond * Time.deltaTime;
            _speed = Mathf.Min(_speed, _maxSpeed);
        }

        public override void Deaccelerate()
        {
            _speed += _deacelRatePerSecond * Time.deltaTime;
            _speed = Mathf.Max(_speed, 0);
        }

        public override void ApplyGravity() => 
            _rigidBody.velocity += Vector2.up * Physics.gravity.y * (_graviryMultiplier - 1) * Time.deltaTime;
    }
}