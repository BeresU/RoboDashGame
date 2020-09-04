using RoboDash.Attack;
using UnityEngine;

namespace RoboDash.Damage
{
    [System.Serializable]
    public struct DamageConfig
    {
        [SerializeField] private AttackType _attackType;
        [SerializeField] private float _force;

        public AttackType AttackType => _attackType;
        public float Force => _force;
    }
}