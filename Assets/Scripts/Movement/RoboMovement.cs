using System;
using System.Threading.Tasks;
using Extensions;
using RoboDash.Movement;
using RoboDash.Movement.Interfaces;
using SimpleMovement.Handlers;
using SimpleMovement.Handlers.PhysicCastHandler;
using SimpleMovement.Modules;
using UnityEngine;

namespace Movement
{
    public class RoboMovement : MovementHandlerBase<Vector2>, IMovementData
    {
        [SerializeField] protected PhysicsCastHandler2D _castHandler;
        [SerializeField] private MovementModuleBase2D _movementModule;

        [Header("Jump settings")] [SerializeField]
        private float _minFingerAngleForJump = 60f;

        [SerializeField] private float _minXValDirectionOnJump = 0;
        [SerializeField] private float _maxXValDirectionOnJump = 0.5f;

        [SerializeField] private float _dashTime = 1f;

        [SerializeField] private MovementLimiter _movementLimiter;
        
        private bool _onGround = false;
        private bool _isMoving; // TODO: someone need to set _isMoveing to true/false.
        private bool _movementLocked;

        private const float MinAngleForDirectionOnJump = 45f;

        private Vector2 _moveDirection = Vector2.zero;
        public bool LockJump { get; set; }

        public bool IsDashing { get; private set; }
        public float Speed => _movementModule.Speed;
        public bool InAir => !_onGround;
        public event Action<bool> OnDashStateChanged;
        public event Action OnLand;
        public event Action OnJump;

        public bool LockMovement
        {
            get => _movementLocked;
            set => _movementLocked = value;
        }

        public override void Init()
        {
            _castHandler.Init();
            _castHandler.OnHit += OnRayCastHit;
            _castHandler.OnHitLost += OnHitLost;
        }

        public override void Dispose()
        {
            base.Dispose();
            OnJump = null;
            OnDashStateChanged = null;
            _castHandler.OnHit -= OnRayCastHit;
            _castHandler.OnHitLost -= OnHitLost;
        }

        // TODO: need tweek jump forces.
        public void OnSwipe(Vector2 direction)
        {
            var dash = ShouldDash(direction);

            if (dash)
            {
                _ = Dash(direction.ToAxis(Vector2Extensions.Axis.X).Sign());
            }
            else
            {
                HandleJump(direction);
            }
        }
        
        // TODO: consider activate by async void, need to check best practices. 
        private async Task Dash(Vector2 direction)
        {
            OnDashStateChanged?.Invoke(true);
            IsDashing = _isMoving = true;
            _moveDirection = direction;
            await TimeSpan.FromSeconds(_dashTime);
            _moveDirection = Vector2.zero;
            OnDashStateChanged?.Invoke(false);
            IsDashing = _isMoving = false;
        }
        
        private void HandleJump(Vector2 direction)
        {
            direction.x = GetAxisValueAccordAngle(direction, Vector2.up, MinAngleForDirectionOnJump,
                _minXValDirectionOnJump, _maxXValDirectionOnJump);

            if (direction.y <= 0) return;
            Jump(direction.SignAxis(Vector2Extensions.Axis.Y));
        }

        private float GetAxisValueAccordAngle(Vector2 val, Vector2 direction, float angle, float minVal,
            float maxVal)
        {
            var a = Vector2.Angle(val, direction);
            return (a > angle ? maxVal : minVal) * Math.Sign(val.x);
        }

        private bool ShouldDash(Vector2 direction)
        {
            if (IsDashing) return false;
            if (direction.y <= 0) return true;
            var angle = Vector2.Angle(direction, Vector2.up);
            return angle >= _minFingerAngleForJump;
        }

        protected override void OnUpdate() => _castHandler.Cast();
        
        protected override void OnFixedUpdate()
        {
            if (_isMoving && _movementLimiter.CanMove(_moveDirection))
            {
                Move(_moveDirection);
            }
            else 
            {
                ActivateOnMoveEvent(_moveDirection, _movementModule.Speed);
                _movementModule.Deaccelerate();
            }
        }

        public void Jump(Vector2 direction)
        {
            if (LockJump) return;
            if (!_onGround) return;
            OnJump?.Invoke();
            _movementModule.Jump(direction);
        }

        private void Move(Vector2 direction)
        {
            _movementModule.Move(direction);
            ActivateOnMoveEvent(direction, _movementModule.CurrentSpeed);
            _movementModule.Accelerate();
        }

        private void OnRayCastHit(RaycastHit2D hit)
        {
            _onGround = true;
            OnLand?.Invoke();
        }

        private void OnHitLost() => _onGround = false;
    }
}