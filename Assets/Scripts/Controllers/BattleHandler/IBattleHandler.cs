using System;
using RoboDash.Attack;

namespace RoboDash.Controllers.Battle
{
    public interface IBattleHandler
    {
        bool IsDefending { get; }
        void ApplyDamage(AttackPayload payload);
        void PlayerFail();
        event Action OnDeath;
    }
}