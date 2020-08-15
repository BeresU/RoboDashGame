using System;
using Movement;
using RoboDash.Attack;
using RoboDash.Damage;
using UnityEngine;

namespace RoboDash.Controllers
{
    public class RoboCharacter : MonoBehaviour
    {
        [SerializeField] private AttackHandler _attackHandler;
        [SerializeField] private DamageHandler _damageHandler;
        [SerializeField] private RoboMovement _movementHandler;
        
        private void OnDestroy()
        {
            _movementHandler.Dispose();   
        }
        
        public void OnTap(Vector2 leanFingerScreenPosition)
        {
            _attackHandler.OnTap();
        }

        public void OnSwipe(Vector2 direction)
        {
            _movementHandler.OnSwipe(direction);
        }
    }
}
