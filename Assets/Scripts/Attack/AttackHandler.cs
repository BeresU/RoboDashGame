using System;
using Extensions;
using RoboDash.Attack.Interfaces;
using RoboDash.Damage;
using SimpleMovement.Handlers.PhysicCastHandler;
using UnityEngine;

namespace RoboDash.Attack
{
    public class AttackHandler : MonoBehaviour, IAttackData
    {
        [SerializeField] private float _punchTime = 1f;
        [SerializeField] private PhysicsCastHandler2D _castHandler;

        private bool _isPunching;
        public bool IsAttacking => _isPunching;
        public event Action OnPunch;
        public event Action<bool> PunchStateChange;

        private void Awake()
        {
            _castHandler.Init();
        }

        private void OnDestroy()
        {
            OnPunch = null;
            PunchStateChange = null;
        }

        public void OnTap()
        {
            if (_isPunching) return;
            ActivateCoolDown();
            Attack();
        }

        private void Attack()
        {
            var hit = _castHandler.Cast();

            if (hit)
            {
                DoAttack(hit.collider);
            }

            OnPunch?.Invoke();
        }

        private void DoAttack(Component hitCollider)
        {
            var damageHandler = hitCollider.GetComponent<IDamageHanalder>();
            var direction = (hitCollider.transform.position -transform.position).ToAxis(Vector3Extentions.Axis.X)
                .Sign();
            var payload = new AttackPayload(AttackType.Punch, direction, 10f);
            damageHandler?.ApplyDamage(payload);
        }

        private async void ActivateCoolDown()
        {
            PunchStateChange?.Invoke(true);
            _isPunching = true;
            await TimeSpan.FromSeconds(_punchTime);
            _isPunching = false;
            PunchStateChange?.Invoke(false);
        }
    }
}