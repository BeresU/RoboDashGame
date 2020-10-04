using System;
using Extensions;
using RoboDash.Attack;
using UnityEngine;

namespace RoboDash.Tests
{
    public class RoboCharacterDebugger : MonoBehaviour
    {
        [SerializeField] private AttackHandler _attackHandler;
        [Header("Attack")]
        [SerializeField] private bool _attack;
        [Header("Auto Attack")]
        [SerializeField] private bool _autoAttack;
        [SerializeField] private float _attackInterval = 3f;
        [Header("Input Attack")]
        [SerializeField] private bool _inputAttack;
        [SerializeField] private KeyCode _attackInput;

        private void Update()
        {
            HandleAttacks();
        }

        private void HandleAttacks()
        {
            if(!_attack) return;
            
            DoAutoAttack();
            DoInputAttack();
        }
        
        private async void DoAutoAttack()
        {
            if (!_autoAttack) return;

            await TimeSpan.FromSeconds(_attackInterval);
            _attackHandler.Attack(AttackType.Punch);
        }
        
        private void DoInputAttack()
        {
            if(!_inputAttack) return;

            if (Input.GetKeyDown(_attackInput))
            {
                _attackHandler.Attack(AttackType.Punch);
            }
        }

    }
}
