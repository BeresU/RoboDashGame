using System;
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

        // Bools
        private readonly int InAirBoolHash = Animator.StringToHash("InAir");
        private readonly int IsDashingBoolHash = Animator.StringToHash("IsDashing");
        private readonly int IsPunchingBoolHash = Animator.StringToHash("IsPunching");
        
        private IMovementData MovementData { get; set; }
        private IAttackData AttackData { get; set; }
        
        public void Init(IMovementData movementData, IAttackData attackData)
        {
            MovementData = movementData;
            AttackData = attackData;
            AttackData.OnPunch += OnPunch;
            AttackData.PunchStateChange += OnPunchStateChange;
            MovementData.OnDashStateChanged += DashStateChanged;
            MovementData.OnJump += OnJump;
            MovementData.OnLand += OnLand;
        }


        public void Dispose()
        {
            MovementData.OnDashStateChanged -= DashStateChanged;
            MovementData.OnJump -= OnJump;
            MovementData.OnLand -= OnLand;
            AttackData.OnPunch -= OnPunch;
            AttackData.PunchStateChange -= OnPunchStateChange;
        }
        
        private void OnPunchStateChange(bool isPunching) => _roboAnimator.SetBool(IsPunchingBoolHash, isPunching);

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