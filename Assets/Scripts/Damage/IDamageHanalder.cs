using System;
using RoboDash.Attack;

namespace RoboDash.Damage
{
    public interface IDamageHanalder
    {
       event Action OnDamage;

       void ApplyDamage(AttackPayload payload);
    }
}