using System;
using RoboDash.Attack;
using RoboDash.Attack.Interfaces;
using RoboDash.Damage;
using RoboDash.Defense;
using UnityEngine;

namespace RoboDash.Controllers.Battle
{
    public class BattleHandler : MonoBehaviour, IBattleHandler 
    {
        private IDefenseHandler _defenseHandler;
        private IDamageHanalder _damageHanalder;
        private IAttackHandler _attackHandler;
        
        public event Action OnDeath;
        public bool IsDefending => _defenseHandler.IsDefending;

        public void Init(IDamageHanalder damageHanalder, IDefenseHandler defenseHandler, IAttackHandler attackHandler)
        {
            _damageHanalder = damageHanalder;
            _defenseHandler = defenseHandler;
            _attackHandler = attackHandler;
        }

        public void ApplyDamage(AttackPayload payload)
        {
            _damageHanalder.ApplyDamage(payload);
            
            if (!_defenseHandler.IsDefending) return;

            var attackType = _defenseHandler.IsReflecting ? AttackType.ReflectHigh : AttackType.ReflectLow;
            _attackHandler.Attack(attackType);
        }

        public void PlayerFail() => OnDeath?.Invoke();
    }
}
