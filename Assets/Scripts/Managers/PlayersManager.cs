using Helpers;
using Lean.Touch;
using RoboDash.Controllers;
using UnityEngine;

namespace RoboDash.Managers
{
    public class PlayersManager : MonoBehaviour
    {
        [SerializeField] private RoboCharacter _rightCharacter;
        [SerializeField] private RoboCharacter _leftCharacter;

        private Camera _sceneCamera;
        
        private void Awake()
        {
            _sceneCamera = Camera.main;
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

        private RoboCharacter GetCharacterAccordingGesture(LeanFinger leanFinger) =>
            CameraHelper.PosRightToCamera(_sceneCamera, leanFinger.StartScreenPosition) ? _rightCharacter : _leftCharacter;
    }
}
