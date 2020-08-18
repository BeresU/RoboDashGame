using System;

namespace RoboDash.Movement.Interfaces
{
    public interface IMovementData 
    {
        float Speed { get; }
        bool InAir { get; }
        bool IsDashing { get; }
        
        event Action<bool> OnDashStateChanged;
        event Action OnLand;
        event Action OnJump;
    }
}
