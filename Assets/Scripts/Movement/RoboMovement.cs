using System;
using System.Threading.Tasks;
using Extensions;
using SimpleMovement.Handlers;
using SimpleMovement.Handlers.PhysicCastHandler;
using SimpleMovement.Modules;
using UnityEngine;

namespace Movement
{
    // TODO: handle dash
    public class RoboMovement : MovementHandlerBase<Vector2>
    {
        [SerializeField] protected PhysicsCastHandler2D _castHandler;
        [SerializeField] private bool _rotate;
        [SerializeField] private MovementModuleBase2D _movementModule;

        [SerializeField] private float _minAngleForJump = 30f;
        [SerializeField] private float _dashTime = 1f;

        private bool _onGround = false;
        private bool _isMoving; // TODO: someone need to set _isMoveing to true/false.
        private bool _movementLocked;
        private bool _isDashing;

        private Vector2 _moveDirection = Vector2.zero;

        public event Action OnJump;
        public event Action<bool> OnGroundStateChange;

        private const float MinSpeedForInit = 0.001f;
        public bool LockJump { get; set; }

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
            OnGroundStateChange = null;
            _castHandler.OnHit -= OnRayCastHit;
            _castHandler.OnHitLost -= OnHitLost;
        }

        // TODO: bugs:
        // bug 1: angle calculation is wrong, sometimes input direction will get as jump.
        // TODO: need tweek jump forces.
        // TODO: need to clamp y jump direction. 
        public void OnSwipe(Vector2 direction)
        {
            var dash = ShouldDash(direction);
            Debug.Log($"Dash: {dash}");

            if (dash)
            {
                _ = Dash(direction.NormalizeAndSignAxis(Vector2Extensions.Axis.X));
            }
            else
            {
                Jump(direction);
            }
        }

        // TODO: return 180 on negative values.
        private bool ShouldDash(Vector2 direction)
        {
            if (_isDashing) return false;
            if (direction.y <= 0) return true;
            var angle = Vector2.Angle(direction, Vector2.right);
            Debug.Log($"Angle: {angle}");
            return angle <= _minAngleForJump;
        }

        protected override void OnUpdate()
        {
            _castHandler.Cast();
        }

        // TODO: read again about fixed update
        protected override void OnFixedUpdate()
        {
            if (_isMoving)
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

        // TODO: consider activate by async void, need to check best practices. 
        private async Task Dash(Vector2 direction)
        {
            _isDashing = _isMoving = true;
            _moveDirection = direction;
            await TimeSpan.FromSeconds(_dashTime);
            _moveDirection = Vector2.zero;
            _isDashing = _isMoving = false;
        }

        private void OnRayCastHit(RaycastHit2D hit)
        {
            _onGround = true;
            OnGroundStateChange?.Invoke(_onGround);
        }

        private void OnHitLost()
        {
            _onGround = false;
            OnGroundStateChange?.Invoke(_onGround);
        }
    }
}