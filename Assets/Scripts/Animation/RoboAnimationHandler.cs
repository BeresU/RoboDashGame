using System;
using RoboDash.Attack;
using RoboDash.Attack.Extensions;
using RoboDash.Attack.Interfaces;
using RoboDash.Damage;
using RoboDash.Defense;
using RoboDash.Movement.Interfaces;
using UnityEngine;

namespace RoboDash.Animation
{
    [Serializable]
    public class RoboAnimationHandler : IDisposable
    {
        [SerializeField] private Animator _roboAnimator;

        // Triggers
        private readonly int _landTriggerHash = Animator.StringToHash("Land");
        private readonly int _jumpTriggerHash = Animator.StringToHash("Jump");
        private readonly int _punchTriggerHash = Animator.StringToHash("Punch");
        private readonly int _hitTriggerHash = Animator.StringToHash("Hit");
        private readonly int _defenseTriggerHash = Animator.StringToHash("Defense");

        // Bools
        private readonly int _inAirBoolHash = Animator.StringToHash("InAir");
        private readonly int _isDashingBoolHash = Animator.StringToHash("IsDashing");
        private readonly int _isPunchingBoolHash = Animator.StringToHash("IsPunching");

        private IMovementData MovementData { get; set; }
        private IAttackHandler AttackHandler { get; set; }
        private IDamageHanalder DamageHanalder { get; set; }

        private IDefenseHandler DefenseHandler { get; set; }

        public void Init(IMovementData movementData, IAttackHandler attackHandler, IDamageHanalder damageHanalder,
            IDefenseHandler defenseHandler)
        {
            MovementData = movementData;
            AttackHandler = attackHandler;
            AttackHandler.OnAttack += Attack;
            AttackHandler.PunchStateChange += OnPunchStateChange;
            MovementData.OnDashStateChanged += DashStateChanged;
            MovementData.OnJump += OnJump;
            MovementData.OnLand += OnLand;
            DamageHanalder = damageHanalder;
            DamageHanalder.OnDamage += OnPlayerHit;
            DefenseHandler = defenseHandler;
            DefenseHandler.DefenseStarted += OnDefense;
        }

        public void Dispose()
        {
            MovementData.OnDashStateChanged -= DashStateChanged;
            MovementData.OnJump -= OnJump;
            MovementData.OnLand -= OnLand;
            AttackHandler.OnAttack -= Attack;
            AttackHandler.PunchStateChange -= OnPunchStateChange;
            DamageHanalder.OnDamage -= OnPlayerHit;
            DefenseHandler.DefenseStarted -= OnDefense;
        }

        private void OnPunchStateChange(AttackType attackType) =>
            _roboAnimator.SetBool(_isPunchingBoolHash, IsAttacking(attackType));

        private static bool IsAttacking(AttackType type) => type != AttackType.None && type != AttackType.ReflectHigh;

        private void OnPlayerHit(AttackType type)
        {
            if (type.IsDefending()) return;
            _roboAnimator.SetTrigger(_hitTriggerHash);
        }

        private void OnLand()
        {
            _roboAnimator.SetBool(_inAirBoolHash, false);
            _roboAnimator.SetTrigger(_landTriggerHash);
        }

        private void OnJump()
        {
            _roboAnimator.SetBool(_inAirBoolHash, true);
            _roboAnimator.SetTrigger(_jumpTriggerHash);
        }

        private void OnDefense() => _roboAnimator.SetTrigger(_defenseTriggerHash);

        private void DashStateChanged(bool isDashing) => _roboAnimator.SetBool(_isDashingBoolHash, isDashing);

        private void Attack(AttackType type)
        {
            if (!type.IsPunching()) return;
            _roboAnimator.SetTrigger(_punchTriggerHash);
        }
    }
}