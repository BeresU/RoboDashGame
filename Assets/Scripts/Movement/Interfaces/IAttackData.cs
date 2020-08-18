using System;

namespace RoboDash.Movement.Interfaces
{
    public interface IAttackData
    {
        event Action OnPunch;
        event Action<bool> PunchStateChange;
    }
}
