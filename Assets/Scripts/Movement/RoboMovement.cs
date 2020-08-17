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
        [SerializeField] private MovementModuleBase2D _movementModule;

        [Header("Jump settings")]
        [SerializeField] private float _minFingerAngleForJump = 60f;

        [SerializeField] private float _minXValDirectionOnJump = 0;
        [SerializeField] private float _maxXValDirectionOnJump = 0.5f;
        
        [SerializeField] private float _dashTime = 1f;

        private bool _onGround = false;
        private bool _isMoving; // TODO: someone need to set _isMoveing to true/false.
        private bool _movementLocked;
        private bool _isDashing;

        private const float MinAngleForDirectionOnJump = 45f;

        private Vector2 _moveDirection = Vector2.zero;

        public event Action OnJump;
        public event Action<bool> OnGroundStateChange;
        
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
                direction.x = GetAxisValueAccordAngle(direction, Vector2.up, MinAngleForDirectionOnJump,
                    _minXValDirectionOnJump, _maxXValDirectionOnJump);
                
                Jump(direction.SignAxis(Vector2Extensions.Axis.Y));
            }
        }
        
        private float GetAxisValueAccordAngle(Vector2 val, Vector2 direction, float angle, float minVal,
            float maxVal)
        {
            var a = Vector2.Angle(val, direction);
            return  (a > angle ? maxVal : minVal) * Math.Sign(val.x);
        }

        private bool ShouldDash(Vector2 direction)
        {
            if (_isDashing) return false;
            if (direction.y <= 0) return true;
            var angle = Vector2.Angle(direction, Vector2.up);
            return angle >= _minFingerAngleForJump;
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