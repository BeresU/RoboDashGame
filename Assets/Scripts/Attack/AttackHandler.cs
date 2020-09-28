using System;
using System.Collections.Generic;
using Extensions;
using RoboDash.Attack.Interfaces;
using RoboDash.Damage;
using SimpleMovement.Handlers.PhysicCastHandler;
using UnityEngine;

namespace RoboDash.Attack
{
    public class AttackHandler : MonoBehaviour, IAttackData
    {
        [SerializeField] private PhysicsCastHandler2D _castHandler;
        [SerializeField] private AttackConfig[] _configs;
        
        private readonly Dictionary<AttackType, AttackConfig> _damageConfigLookUp =
            new Dictionary<AttackType, AttackConfig>();

        private bool _isPunching;
        public bool IsAttacking => _isPunching;
        public event Action OnPunch;
        public event Action<bool> PunchStateChange;

        private void Awake()
        {
            _castHandler.Init();
            InitDictionary();
        }
        
        private void OnDestroy()
        {
            OnPunch = null;
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

        private void Attack(AttackType type)
        {
            ActivateCoolDown(type);
            var hit = _castHandler.Cast();

            if (hit)
            {
                DoAttack(hit.collider, type);
            }

            OnPunch?.Invoke();
        }

        private void DoAttack(Component hitCollider, AttackType type)
        {
            var damageHandler = hitCollider.GetComponent<IDamageHanalder>();
            var direction = (hitCollider.transform.position -transform.position).ToAxis(Vector3Extentions.Axis.X)
                .Sign();

            var force = _damageConfigLookUp[type].Force; 
            var payload = new AttackPayload(AttackType.Punch, direction, force);
            damageHandler?.ApplyDamage(payload);
        }

        private async void ActivateCoolDown(AttackType type)
        {
            PunchStateChange?.Invoke(true);
            _isPunching = true;
            var coolDown = _damageConfigLookUp[type].CoolDown;
            await TimeSpan.FromSeconds(coolDown);
            _isPunching = false;
            PunchStateChange?.Invoke(false);
        }
    }
}