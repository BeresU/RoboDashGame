using System;
using Movement;
using RoboDash.Animation;
using RoboDash.Attack;
using RoboDash.Damage;
using RoboDash.Defense;
using UnityEngine;

namespace RoboDash.Controllers
{
    public class RoboCharacter : MonoBehaviour
    {
        [SerializeField] private AttackHandler _attackHandler;
        [SerializeField] private DamageHandler _damageHandler;
        [SerializeField] private RoboMovement _movementHandler;
        [SerializeField] private DefenseHandler _defenseHandler;
        [SerializeField] private RoboAnimationHandler _roboAnimationHandler;

        private void Awake()
        {
            _roboAnimationHandler.Init(_movementHandler, _attackHandler, _damageHandler, _defenseHandler);
        }

        private void OnDestroy()
        {
            _movementHandler.Dispose();   
        }
        
        public void OnTap(Vector2 leanFingerScreenPosition)
        {
            if(_defenseHandler.IsDefending) return;
            _attackHandler.OnTap();
        }

        public void OnSwipe(Vector2 direction)
        {
            if(_defenseHandler.IsDefending) return;
            _defenseHandler.OnSwipe(direction);
            _movementHandler.OnSwipe(direction);
        }
    }
}
