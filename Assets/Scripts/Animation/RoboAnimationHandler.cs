using System;
using RoboDash.Attack.Interfaces;
using RoboDash.Damage;
using RoboDash.Movement.Interfaces;
using UnityEngine;

namespace RoboDash.Animation
{
    [Serializable]
    public class RoboAnimationHandler : IDisposable
    {
        [SerializeField] private Animator _roboAnimator;
        
        // Triggers
        private readonly int LandTriggerHash = Animator.StringToHash("Land");
        private readonly int JumpTriggerHash = Animator.StringToHash("Jump");
        private readonly int PunchTriggerHash = Animator.StringToHash("Punch");
        private readonly int HitTriggerHash = Animator.StringToHash("Hit");

        // Bools
        private readonly int InAirBoolHash = Animator.StringToHash("InAir");
        private readonly int IsDashingBoolHash = Animator.StringToHash("IsDashing");
        private readonly int IsPunchingBoolHash = Animator.StringToHash("IsPunching");
        
        private IMovementData MovementData { get; set; }
        private IAttackData AttackData { get; set; }
        
        private IDamageHanalder DamageHanalder { get; set; }
        
        public void Init(IMovementData movementData, IAttackData attackData, IDamageHanalder damageHanalder)
        {
            MovementData = movementData;
            AttackData = attackData;
            AttackData.OnPunch += OnPunch;
            AttackData.PunchStateChange += OnPunchStateChange;
            MovementData.OnDashStateChanged += DashStateChanged;
            MovementData.OnJump += OnJump;
            MovementData.OnLand += OnLand;
            DamageHanalder = damageHanalder;
            DamageHanalder.OnDamage += OnPlayerHit;
        }


        public void Dispose()
        {
            MovementData.OnDashStateChanged -= DashStateChanged;
            MovementData.OnJump -= OnJump;
            MovementData.OnLand -= OnLand;
            AttackData.OnPunch -= OnPunch;
            AttackData.PunchStateChange -= OnPunchStateChange;
            DamageHanalder.OnDamage -= OnPlayerHit;
        }
        
        private void OnPunchStateChange(bool isPunching) => _roboAnimator.SetBool(IsPunchingBoolHash, isPunching);

        private void OnPlayerHit() => _roboAnimator.SetTrigger(HitTriggerHash);

        private void OnLand()
        {
            _roboAnimator.SetBool(InAirBoolHash, false);
            _roboAnimator.SetTrigger(LandTriggerHash);
        }

        private void OnJump()
        {
            _roboAnimator.SetBool(InAirBoolHash, true);
            _roboAnimator.SetTrigger(JumpTriggerHash);
        }

        private void DashStateChanged(bool isDashing) => _roboAnimator.SetBool(IsDashingBoolHash, isDashing);

        private void OnPunch()
        {
            _roboAnimator.SetTrigger(PunchTriggerHash);
        }
    }
}