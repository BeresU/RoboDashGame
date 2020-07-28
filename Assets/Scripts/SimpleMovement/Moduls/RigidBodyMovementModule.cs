using UnityEngine;

namespace SimpleMovement.Modules
{
    [System.Serializable]
    public class RigidBodyMovementModule
    {
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _graviryMultiplier = 2.5f;
        [SerializeField] private float _maxSpeed = 10f;

        [SerializeField] private float _timeZeroToMax = 0.2f;
        [SerializeField] private float _timeMaxToZero = 1f;

        [SerializeField] private Rigidbody _rigidBody;

        private float _speed;

        private float _accelRatePerSecond;
        private float _deacelRatePerSecond;

        public Transform ControlledTransform => _rigidBody.transform;

        public float Speed => _speed;

        public float CurrentSpeed => _speed;

        public float JumpForce
        {
            get => _jumpForce;
            set => _jumpForce = value;
        }

        public float MaxSpeed
        {
            get => _maxSpeed;
            set => _maxSpeed = value;
        }

        public void Init()
        {
            _rigidBody.velocity = Vector3.zero;
            _accelRatePerSecond = _maxSpeed / _timeZeroToMax;
            _deacelRatePerSecond = -_maxSpeed / _timeMaxToZero;
        }

        public void Move(Vector3 direction)
        {
            _rigidBody.MovePosition(_rigidBody.position + direction * _speed * Time.fixedDeltaTime);
        }

        public void MoveTo(Vector3 position)
        {
            var target = Vector3.MoveTowards(_rigidBody.position, position, _speed * Time.deltaTime);
            Accelerate();
            _rigidBody.MovePosition(target);
        }

        public void Stop()
        {
            _rigidBody.velocity = Vector3.zero;
            _speed = 0;
        }

        public void Jump()
        {
            _rigidBody.AddForce(Vector3.up * 
                                Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        public void Rotate(Vector3 position)
        {
            var targetRot = Quaternion.LookRotation(position);
            _rigidBody.MoveRotation(targetRot);
        }

        public void Accelerate()
        {
            _speed += _accelRatePerSecond * Time.deltaTime;
            _speed = Mathf.Min(_speed, _maxSpeed);
        }

        public void Deaccelerate()
        {
            _speed += _deacelRatePerSecond * Time.deltaTime;
            _speed = Mathf.Max(_speed, 0);
        }

        public void ApplyGravity()
        {
            _rigidBody.velocity += Vector3.up * Physics.gravity.y * (_graviryMultiplier - 1) * Time.deltaTime;
        }
    }
}
