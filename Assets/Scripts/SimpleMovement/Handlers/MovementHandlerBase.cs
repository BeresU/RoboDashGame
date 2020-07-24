using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MovementHandlerBase : IDisposable
{
    [SerializeField] protected MovementModule _movementModule;

    public event Action<Vector3, float> OnMove;
    public event Action<Vector3> OnRotate;

    public float MaxSpeed
    {
        get { return _movementModule.MaxSpeed; }
        set { _movementModule.MaxSpeed = value; }
    }

    public float JumpForce
    {
        get { return _movementModule.JumpForce; }
        set { _movementModule.JumpForce = value; }
    }

    public MonoBehaviour MonoHelper { get; set; }

    public virtual void Init()
    {
        _movementModule.Init();
    }
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();

    public virtual void Dispose()
    {
        OnRotate = null;
        OnMove = null;
    }

    protected void ActivateOnMoveEvent(Vector3 direction, float speed)
    {
        OnMove?.Invoke(direction,speed);
    }

    protected void ActivateOnRoateEvent(Vector3 direction)
    {
        OnRotate?.Invoke(direction);
    }
}
