using UnityEngine;

namespace SimpleMovement.Modules
{
    public abstract class MovementModuleBase<T> : MonoBehaviour where T : struct
    {
        [SerializeField] protected float _jumpForce = 5f;
        [SerializeField] protected float _graviryMultiplier = 2.5f;
        [SerializeField] protected float _maxSpeed = 10f;

        [SerializeField] protected float _timeZeroToMax = 0.2f;
        [SerializeField] protected float _timeMaxToZero = 1f;

        protected float _speed;
        protected float _accelRatePerSecond;
        protected float _deacelRatePerSecond;

        public abstract Transform ControlledTransform { get; }

        public abstract float Speed { get; }

        public abstract float CurrentSpeed { get; }

        public abstract float JumpForce { get; set; }

        public abstract float MaxSpeed { get; set; }

        private void Awake() => Init();
        
        public abstract void Init();
        
        public abstract void Move(T direction);
        
        public abstract void MoveTo(T position);
        
        public abstract void Stop();
        
        public abstract void Jump();

        public abstract void Rotate(T position);

        public abstract void Accelerate();
        
        public abstract void Deaccelerate();
        
        public abstract void ApplyGravity();
    }
}