using System;
using System.Collections.Generic;
using Extensions;
using RoboDash.Attack.Interfaces;
using RoboDash.Controllers.Battle;
using SimpleMovement.Handlers.PhysicCastHandler;
using UnityEngine;

namespace RoboDash.Attack
{
    public class AttackHandler : MonoBehaviour, IAttackHandler
    {
        [SerializeField] private PhysicsCastHandler2D _castHandler;
        [SerializeField] private AttackConfig[] _configs;
        
        private readonly Dictionary<AttackType, AttackConfig> _damageConfigLookUp =
            new Dictionary<AttackType, AttackConfig>();

        private bool _isPunching;
        public bool IsAttacking => _isPunching;
        public event Action<AttackType> OnAttack;
        public event Action<AttackType> PunchStateChange;

        private void Awake()
        {
            _castHandler.Init();
            InitDictionary();
        }
        
        private void OnDestroy()
        {
            OnAttack = null;
            PunchStateChange = null;
        }
        
        private void InitDictionary()
        {
            foreach (var damageConfig in _configs)
            {
                _damageConfigLookUp[damageConfig.AttackType] = damageConfig;
            }
        }
        
        public void OnTap()
        {
            if (_isPunching) return;
            Attack(AttackType.Punch);
        }

        public void Attack(AttackType type)
        {
            ActivateCoolDown(type);
            var hit = _castHandler.Cast();

            if (hit)
            {
                DoAttack(hit.collider, type);
            }

            OnAttack?.Invoke(type);
        }

        private void DoAttack(Component hitCollider, AttackType type)
        {
            var battleHandler = hitCollider.GetComponent<IBattleHandler>();
            if(battleHandler ==  null) return;
            
            var direction = (hitCollider.transform.position -transform.position).ToAxis(Vector3Extentions.Axis.X)
                .Sign();
            
            var attackType = battleHandler.IsDefending ? AttackType.Defence : type;
            
            var force = _damageConfigLookUp[attackType].Force;
            
            var payload = new AttackPayload(attackType, direction, force);
            battleHandler.ApplyDamage(payload);
        }

        private async void ActivateCoolDown(AttackType type)
        {
            PunchStateChange?.Invoke(type);
            _isPunching = true;
            var coolDown = _damageConfigLookUp[type].CoolDown;
            await TimeSpan.FromSeconds(coolDown);
            _isPunching = false;
            PunchStateChange?.Invoke(AttackType.None);
        }
    }
}