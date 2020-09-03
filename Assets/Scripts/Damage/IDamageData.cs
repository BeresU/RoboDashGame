using System;

namespace RoboDash.Damage
{
    public interface IDamageData
    {
       event Action OnDamage;

       void ApplyDamage();
    }
}