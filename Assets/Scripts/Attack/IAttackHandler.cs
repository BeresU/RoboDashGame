using System;

namespace RoboDash.Attack.Interfaces
{
    public interface IAttackHandler
    {
        event Action<AttackType> OnAttack;
        event Action<AttackType> PunchStateChange;
        void Attack(AttackType type);
    }
}
