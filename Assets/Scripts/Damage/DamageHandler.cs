using System;
using System.Collections.Generic;
using RoboDash.Attack;
using UnityEngine;

namespace RoboDash.Damage
{
    public class DamageHandler : MonoBehaviour, IDamageHanalder
    {
        [SerializeField] private DamageConfig[] _configs;
        [SerializeField] private Rigidbody2D _rigidbody;

        private readonly Dictionary<AttackType, DamageConfig> _damageConfigLookUp =
            new Dictionary<AttackType, DamageConfig>();
        public event Action OnDamage;
        public event Func<bool> DamagePredicate; 

        private void Awake() => InitDictionary();

        private void InitDictionary()
        {
            foreach (var damageConfig in _configs)
            {
                _damageConfigLookUp[damageConfig.AttackType] = damageConfig;
            }
        }

        public void ApplyDamage(AttackPayload payload)
        {
            if(DamagePredicate != null && !DamagePredicate.Invoke()) return;
            
            var config = _damageConfigLookUp[payload.AttackType];
            
            _rigidbody.AddForce(payload.Direction * (payload.Force * config.Force), ForceMode2D.Impulse);
            OnDamage?.Invoke();
        }
    }
}