using Movement;
using RoboDash.Attack;
using RoboDash.Damage;
using RoboDash.Movement.Interfaces;
using UnityEngine;

namespace RoboDash.Animation
{
    [System.Serializable]
    public class RoboAnimationHandler
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
        }
    }
}