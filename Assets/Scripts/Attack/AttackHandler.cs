using System;
using Extensions;
using RoboDash.Movement.Interfaces;
using UnityEngine;

namespace RoboDash.Attack
{
    public class AttackHandler : MonoBehaviour, IAttackData
    {
        [SerializeField] private float _punchTime = 1f;
        private bool _isPunching;
        public event Action OnPunch;
        public event Action<bool> PunchStateChange;


        private void OnDestroy()
        {
            OnPunch = null;
            PunchStateChange = null;
        }
        
        public void OnTap()
        {
            if(_isPunching) return;
            ActivateCoolDown();
            OnPunch?.Invoke();
        }

        private async void ActivateCoolDown()
        {
            PunchStateChange?.Invoke(true);
            _isPunching = true;
            await TimeSpan.FromSeconds(_punchTime);
            _isPunching = false;
            PunchStateChange?.Invoke(false);
        }
    }
}
