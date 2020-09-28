using UnityEngine;

namespace RoboDash.Attack
{
    [System.Serializable]
    public class AttackConfig 
    {
        [SerializeField] private AttackType _attackType;
        [SerializeField] private float _force;
        [SerializeField] private float _coolDown;
        
        public AttackType AttackType => _attackType;
        public float Force => _force;
        public float CoolDown => _coolDown;
    }
}
