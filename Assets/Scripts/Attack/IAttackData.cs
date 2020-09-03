using System;

namespace RoboDash.Attack.Interfaces
{
    public interface IAttackData
    {
        event Action OnPunch;
        event Action<bool> PunchStateChange;
    }
}
