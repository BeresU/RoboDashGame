using System;
using Extensions;
using RoboDash.Attack;
using UnityEngine;

namespace RoboDash.Tests
{
    public class RoboCharacterDebugger : MonoBehaviour
    {
        [SerializeField] private AttackHandler _attackHandler;
        [SerializeField] private bool _attack;
        [SerializeField] private float _attackInterval = 3f;

        private void Start()
        {
            ActivateAttack();
        }

        private async void ActivateAttack()
        {
            if(!_attack) return;

            while (true)
            {
                await TimeSpan.FromSeconds(_attackInterval);
                _attackHandler.Attack(AttackType.Punch);
            }
        }
    }
}
