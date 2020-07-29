using System;
using UnityEngine;

namespace SimpleMovement.Handlers
{
    [Serializable]
    public abstract class MovementHandlerBase<T> : MonoBehaviour, IDisposable where T : struct
    {
        public event Action<T, float> OnMove;
        public event Action<T> OnRotate;
        
        private void Update() => OnUpdate();
        private void FixedUpdate() => OnFixedUpdate();
        private void Awake() => Init();

        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
        public abstract void Init();

        public virtual void Dispose()
        {
            OnRotate = null;
            OnMove = null;
        }

        public void ActivateOnMoveEvent(T direction, float speed) => OnMove?.Invoke(direction,speed);

        protected void ActivateOnRotateEvent(T direction) => OnRotate?.Invoke(direction);
    }
}
