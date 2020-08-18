using System;
using Movement;
using RoboDash.Attack;
using RoboDash.Damage;
using RoboDash.Movement.Interfaces;
using UnityEngine;

namespace RoboDash.Animation
{
    [System.Serializable]
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
        
        private IMovementData MovementData { get; set; }
        
        public void Init(IMovementData movementData)
        {
            MovementData = movementData;

            MovementData.OnDashStateChanged += DashStateChanged;
            MovementData.OnJump += OnJump;
            MovementData.OnLand += OnLand;
        }
        public void Dispose()
        {
            MovementData.OnDashStateChanged -= DashStateChanged;
            MovementData.OnJump -= OnJump;
            MovementData.OnLand -= OnLand;
        }

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

        private void DashStateChanged(bool isDashing)
        {
            _roboAnimator.SetBool(IsDashingBoolHash, isDashing);
        }
    }
}