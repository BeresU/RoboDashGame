using UnityEngine;

namespace SimpleMovement.Modules
{
    [System.Serializable]
    public class RigidBodyBase2DMovementModule : MovementModuleBase2D
    {
        [SerializeField] private Rigidbody2D _rigidBody;

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

        [ContextMenu("Init")]
        public override void Init()
        {
            _timeZeroToMax = Mathf.Max(_timeZeroToMax, float.Epsilon);
            _timeMaxToZero = Mathf.Max(_timeMaxToZero, float.Epsilon);

            _rigidBody.velocity = Vector2.zero;
            // TODO: to base class.
            _accelRatePerSecond = _maxSpeed / _timeZeroToMax;
            _deacelRatePerSecond = -_maxSpeed / _timeMaxToZero;
        }

        public override void Move(Vector2 direction)
        {
            var dir = new Vector2(direction.x * _speed,_rigidBody.velocity.y );
            _rigidBody.velocity = dir;
        }

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

        public override void Jump()
        {
            _rigidBody.AddForce(Vector2.up *
                                Mathf.Sqrt(_jumpForce * -2f * Physics2D.gravity.y), ForceMode2D.Impulse);
            
        }

        public override void Rotate(Vector2 position)
        {

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

        public override void ApplyGravity()
        {
            _rigidBody.velocity += Vector2.up * Physics.gravity.y * (_graviryMultiplier - 1) * Time.deltaTime;
        }
    }
}