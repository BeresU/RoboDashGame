using System;
using RoboDash.Attack;

namespace RoboDash.Damage
{
    public interface IDamageHanalder
    {
       event Action OnDamage;
       event Func<bool> DamagePredicate;

       void ApplyDamage(AttackPayload payload);
    }
}