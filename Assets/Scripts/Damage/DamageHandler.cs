using System;
using UnityEngine;

namespace RoboDash.Damage
{
    public class DamageHandler : MonoBehaviour, IDamageData
    {
        public event Action OnDamage;
        public void ApplyDamage()
        {
            throw new NotImplementedException();
        }
    }
}
