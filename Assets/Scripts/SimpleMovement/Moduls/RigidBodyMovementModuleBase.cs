using UnityEngine;

namespace SimpleMovement.Modules
{
    [System.Serializable]
    public class RigidBodyMovementModuleBase : MovementModuleBase3D
    {
        [SerializeField] private Rigidbody _rigidBody;
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
            _rigidBody.velocity = Vector3.zero;
            _accelRatePerSecond = _maxSpeed / _timeZeroToMax;
            _deacelRatePerSecond = -_maxSpeed / _timeMaxToZero;
        }

        public override void Move(Vector3 direction) 
            => _rigidBody.MovePosition(_rigidBody.position + direction * _speed * Time.fixedDeltaTime);

        public override void MoveTo(Vector3 position)
        {
            var target = Vector3.MoveTowards(_rigidBody.position, position, _speed * Time.deltaTime);
            Accelerate();
            _rigidBody.MovePosition(target);
        }

        public override void Stop()
        {
            _rigidBody.velocity = Vector3.zero;
            _speed = 0;
        }

        public override void Jump() =>
            _rigidBody.AddForce(Vector3.up * 
                                Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);

        public override void Rotate(Vector3 position)
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
            _rigidBody.velocity += Vector3.up * Physics.gravity.y * (_graviryMultiplier - 1) * Time.deltaTime;
    }
}
