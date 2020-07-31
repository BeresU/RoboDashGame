using System;
using SimpleMovement.Handlers.PhysicCastHandler;
using SimpleMovement.Modules;
using UnityEngine;

namespace SimpleMovement.Handlers
{
    public class Basic2DInputMovementHandler : MovementHandlerBase<Vector2>
    {
        [SerializeField] protected PhysicsCastHandler2D _castHandler;
        [SerializeField] private bool _rotate;
        [SerializeField] private MovementModuleBase2D _movementModule;

        private bool _onGround = false;
        private bool _fingerDown = false;
        private bool _isMoving;
        private bool _initRotate = false;
        private bool _jumpLocked;
        private bool _movementLocked;

        private Vector2 _lastDirection = Vector2.zero;
        
        public event Action OnJump;
        public event Action<bool> OnGroundStateChange;

        private const float MinSpeedForInit = 0.001f;
        public bool LockJump
        {
            get => _jumpLocked;
            set => _jumpLocked = value;
        }

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

        public override void OnUpdate()
        {
            _castHandler.Cast();

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
#endif
        }

        public override void OnFixedUpdate()
        {
            _movementModule.ApplyGravity();
            _movementModule.Move(_lastDirection);

            if (!_isMoving)
            {
                ActivateOnMoveEvent(_lastDirection, _movementModule.Speed);
                _movementModule.Deaccelerate();
                OnDeaccelerate();
            }
            
#if UNITY_EDITOR
            HandleMovementVieKeyboard();
#endif
        }
    
        private void OnDeaccelerate()
        {
            if (_movementModule.Speed < MinSpeedForInit)
            {
                ResetRotate();
            }
        }

        private void ResetRotate()
        {
            if (!_initRotate)
            {
                Rotate(Vector2.zero);
            }

            _initRotate = true;
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

        private void HandleMovementVieKeyboard()
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.D))
            {
                OnMoveInput(Vector2.right);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                OnMoveInput(Vector2.left);
            }
            else if (_isMoving && !_fingerDown)
            {
                _isMoving = false;
            }
#endif
        }

        private void Jump()
        {
            if (LockJump) return;
            if (!_onGround) return;
            OnJump?.Invoke();
            _movementModule.Jump();
        }

        private void OnMoveInput(Vector2 direction)
        {
            if (_movementLocked) return;

            _isMoving = true;
            Rotate(direction);
            Move(direction);
        }

        private void Rotate(Vector2 direction)
        {
            _initRotate = false;
            ActivateOnRotateEvent(direction);

            if (!_rotate) return;
            _movementModule.Rotate(direction);
        }

        private void Move(Vector2 direction)
        {
            _lastDirection = direction;
            ActivateOnMoveEvent(direction, _movementModule.CurrentSpeed);
            _movementModule.Accelerate();
        }
    }
}
