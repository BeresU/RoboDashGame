using Lean.Touch;
using RoboDash.Controllers;
using UnityEngine;

namespace RoboDash.Managers
{
    public class InputManager : MonoBehaviour
    {
        // TODO: not set this vie inspector, get character from scene .
        [SerializeField] private RoboCharacter _rightCharacter;
        [SerializeField] private RoboCharacter _leftCharacter;
        
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
            var character = GetCharacterAccordingGesture(leanFinger);
            character.OnTap(leanFinger.ScreenPosition);
        }

        private void OnSwipe(LeanFinger leanFinger)
        {
            var character = GetCharacterAccordingGesture(leanFinger);
            var direction = (leanFinger.ScreenPosition - leanFinger.StartScreenPosition).normalized;
            character.OnSwipe(direction);
        }

        private RoboCharacter GetCharacterAccordingGesture(LeanFinger leanFinger)
        {
            return _rightCharacter;
        }
    }
}
