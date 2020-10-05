using System;
using Movement;
using RoboDash.Animation;
using RoboDash.Attack;
using RoboDash.Damage;
using RoboDash.Defense;
using UnityEngine;
using RoboDash.Controllers.Battle;

namespace RoboDash.Controllers
{
    public class RoboCharacter : MonoBehaviour
    {
        [SerializeField] private AttackHandler _attackHandler;
        [SerializeField] private DamageHandler _damageHandler;
        [SerializeField] private RoboMovement _movementHandler;
        [SerializeField] private DefenseHandler _defenseHandler;
        [SerializeField] private BattleHandler _battleHandler;
        [SerializeField] private RoboAnimationHandler _roboAnimationHandler;

        public event Action<RoboCharacter> OnDeath;
        

        private void Awake()
        {
            _battleHandler.Init(_damageHandler, _defenseHandler, _attackHandler);
            _battleHandler.OnDeath += ActivateOnDeath;
            _roboAnimationHandler.Init(_movementHandler, _attackHandler, _damageHandler, _defenseHandler);
        }


        private void OnDestroy()
        {
            _battleHandler.OnDeath -= ActivateOnDeath;
            _movementHandler.Dispose();
        }

        private void ActivateOnDeath() => OnDeath?.Invoke(this);

        public void OnTap(Vector2 leanFingerScreenPosition)
        {
            if (_defenseHandler.IsDefending) return;
            _attackHandler.OnTap();
        }

        public void OnSwipe(Vector2 direction)
        {
            if (!_movementHandler.IsDashing && !_movementHandler.InAir && !_attackHandler.IsAttacking)
            {
                _defenseHandler.OnSwipe(direction);
            }
            
            if (!_defenseHandler.IsDefending)
            {
                _movementHandler.OnSwipe(direction);
            }
        }
    }
}