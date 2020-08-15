using System;
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

        private bool _onGround = false;
        private bool _isMoving;    // TODO: someone need to set _isMoveing to true/false.
        private bool _movementLocked;

        private Vector2 _lastDirection = Vector2.zero;

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
        
        public void OnSwipe(Vector2 direction)
        {
            
        }
        
        protected override void OnUpdate()
        {
            _castHandler.Cast();
        }
        
        protected override void OnFixedUpdate()
        {
            _movementModule.Move(_lastDirection);

            if (!_isMoving)
            {
                ActivateOnMoveEvent(_lastDirection, _movementModule.Speed);
                _movementModule.Deaccelerate();
            }
        }
        
        public void Jump()
        {
            if (LockJump) return;
            if (!_onGround) return;
            OnJump?.Invoke();
            _movementModule.Jump();
        }
        
        public void Move(Vector2 direction)
        {
            _isMoving = true;
            _lastDirection = direction;
            ActivateOnMoveEvent(direction, _movementModule.CurrentSpeed);
            _movementModule.Accelerate();
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