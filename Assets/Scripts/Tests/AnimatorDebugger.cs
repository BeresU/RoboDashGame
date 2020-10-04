using System;
using UnityEngine;

namespace Tests
{
    public class AnimatorDebugger : MonoBehaviour
    {
        [SerializeField] private AnimatorDebuggerInstance _debugger;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _debugger.DebugStates();
            }
        }

        [SerializeField] private Animator _animator;

        public void DebugStates()
        {
            var animatorClipsInfo = _animator.GetCurrentAnimatorClipInfo(0);
            foreach (var clipInfo in animatorClipsInfo)
            {
                Debug.Log($"clip: {clipInfo.clip.name}");
            }
        }
    }


    [System.Serializable]
    public class AnimatorDebuggerInstance
    {
        [SerializeField] private Animator _animator;

        public void DebugStates()
        {
            var animatorClipsInfo = _animator.GetCurrentAnimatorClipInfo(0);
            foreach (var clipInfo in animatorClipsInfo)
            {
                Debug.Log($"clip: {clipInfo.clip.name}");
            }
        }
    }
}