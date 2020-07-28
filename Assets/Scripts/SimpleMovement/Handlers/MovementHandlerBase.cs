using System;
using SimpleMovement.Modules;
using UnityEngine;

namespace SimpleMovement.Handlers
{
    [Serializable]
    public abstract class MovementHandlerBase : IDisposable
    {
        [SerializeField] protected RigidBodyMovementModule rigidBodyMovementModule;

        public event Action<Vector3, float> OnMove;
        public event Action<Vector3> OnRotate;

        public float MaxSpeed
        {
            get => rigidBodyMovementModule.MaxSpeed;
            set => rigidBodyMovementModule.MaxSpeed = value;
        }

        public float JumpForce
        {
            get => rigidBodyMovementModule.JumpForce;
            set => rigidBodyMovementModule.JumpForce = value;
        }

        public MonoBehaviour MonoHelper { get; set; }

        public virtual void Init()
        {
            rigidBodyMovementModule.Init();
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
}
