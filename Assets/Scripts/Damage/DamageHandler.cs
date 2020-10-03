using System;
using System.Collections.Generic;
using Extensions;
using RoboDash.Attack;
using UnityEngine;

namespace RoboDash.Damage
{
    public class DamageHandler : MonoBehaviour, IDamageHanalder
    {
        [SerializeField] private DamageConfig[] _configs;
        [SerializeField] private DamageConfig _defaultDamageConfig;
        [SerializeField] private Rigidbody2D _rigidbody;

        private readonly Dictionary<AttackType, DamageConfig> _damageConfigLookUp =
            new Dictionary<AttackType, DamageConfig>();
        public event Action<AttackType> OnDamage;

        private void Awake() => InitDictionary();

        private void InitDictionary()
        {
            foreach (var damageConfig in _configs)
            {
                _damageConfigLookUp[damageConfig.AttackType] = damageConfig;
            }
            
            foreach (AttackType value in Enum.GetValues(typeof(AttackType)))
            {
                if(_damageConfigLookUp.ContainsKey(value)) continue;

                _damageConfigLookUp[value] = _defaultDamageConfig;
            }
        }

        public void ApplyDamage(AttackPayload payload)
        {
            var config = _damageConfigLookUp[payload.AttackType];
            
            _rigidbody.AddForce(payload.Direction * (payload.Force * config.Force), ForceMode2D.Impulse);
            OnDamage?.Invoke(payload.AttackType);
        }
    }
}