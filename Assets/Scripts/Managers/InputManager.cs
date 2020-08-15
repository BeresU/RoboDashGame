using System;
using Lean.Touch;
using UnityEngine;

namespace RoboDash.Managers
{
    public class InputManager : MonoBehaviour
    {
        private void Awake()
        {
            LeanTouch.OnFingerSwipe += OnSwipe;
            LeanTouch.OnFingerTap += OnTap;
        }

        private void OnDestroy()
        {
            LeanTouch.OnFingerSwipe -= OnSwipe;
            LeanTouch.OnFingerTap -= OnTap;
        }

        private void OnTap(LeanFinger leanFinger)
        {
            Debug.Log($"On Tap; {leanFinger.Tap}");
        }

        private void OnSwipe(LeanFinger leanFinger)
        {
            Debug.Log($"On Swipe; {leanFinger.Swipe}");
        }
    }
}
