using UnityEngine;

namespace RoboDash.Attack
{
    public class AttackPayload
    {
        public readonly AttackType AttackType;
        public readonly Vector2 Direction;
        public readonly float Force;
        
        public AttackPayload(AttackType attackType, Vector2 direction, float force)
        {
            Force = force;
            Direction = direction;
            AttackType = attackType;
        }
    }
}