using System;
using UnityEngine;

[Serializable]
public class InputMovementHandler : MovementHandlerBase
{
    [SerializeField] protected PhysicsCastHandler _castHandler;
    [SerializeField] private bool _rotate;

    private bool _onGround = false;
    private bool _fingerDown = false;
    private bool _isMoving;
    private bool _initRotate = false;
    private bool _jumpLocked;
    private bool _movementLocked;

    private Vector3 _lastDirection = Vector3.zero;

    private const float MinSpeedForInit = 0.001f;
    
    public event Action OnJump;
    public event Action<bool> OnGroundStateChange;

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
        base.Init();
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
        // TODO: assign all OnUpdates to main update loop?
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
        rigidBodyMovementModule.ApplyGravity();
        rigidBodyMovementModule.Move(_lastDirection);

        if (!_isMoving)
        {
            ActivateOnMoveEvent(_lastDirection, rigidBodyMovementModule.Speed);
            rigidBodyMovementModule.Deaccelerate();
            OnDeaccelerate();
        }


#if UNITY_EDITOR
        HandleMovementVieKeyboard();
#endif
    }
    
    private void OnDeaccelerate()
    {
        if (rigidBodyMovementModule.Speed < MinSpeedForInit)
        {
            ResetRotate();
        }
    }

    private void ResetRotate()
    {
        if (!_initRotate)
        {
            Rotate(Vector3.zero);
        }

        _initRotate = true;
    }

    private void OnRayCastHit(RaycastHit hit)
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
            OnMoveInput(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            OnMoveInput(Vector3.left);
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
        rigidBodyMovementModule.Jump();
    }

    private void OnMoveInput(Vector3 direction)
    {
        if (_movementLocked) return;

        _isMoving = true;
        Rotate(direction);
        Move(direction);
    }

    private void Rotate(Vector3 direction)
    {
        _initRotate = false;
        ActivateOnRoateEvent(direction);

        if (!_rotate) return;
        rigidBodyMovementModule.Rotate(direction);
    }

    private void Move(Vector3 direction)
    {
        _lastDirection = direction;
        ActivateOnMoveEvent(direction, rigidBodyMovementModule.CurrentSpeed);
        rigidBodyMovementModule.Accelerate();
    }
}