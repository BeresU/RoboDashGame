using UnityEngine;

namespace CameraUtils
{
    // TODO: think of better name
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private float _updateRate = 2;
        [SerializeField] private float _edgeDistToZoomOut = 2f;
        [SerializeField] private float _edgeDistToZoomIn = 2f;
        [SerializeField] private Vector3 _cameraOffset;

        [SerializeField] private Transform _target1;
        [SerializeField] private Transform _target2;

        [SerializeField] private Camera _camera;
        [SerializeField] private bool _debug;
        
        private float _lastDist;

        private void FixedUpdate()
        {
            FollowTargets();
        }

        private void FollowTargets()
        {
            var dist = Mathf.Abs(_target1.position.x - _target2.position.x);
            var average = (_target1.position.x + _target2.position.x) / 2;
            var delta = dist - _lastDist;
            var edgeDist = Mathf.Abs(_target1.transform.position.x - _camera.RightScreenEdge());
            var offsetPosition = _camera.transform.position.y;

            if (_debug)
            {
                Debug.Log(
                    $"Dist: {dist} average: {average} delta: {delta} edgeDist: {edgeDist}");
            }

            if (edgeDist < _edgeDistToZoomOut)
            {
                _camera.SetScreenWidth(_camera.ScreenWidth() + _updateRate * delta);
                offsetPosition += _cameraOffset.y;
            }

            if (edgeDist > _edgeDistToZoomIn)
            {
                _camera.SetScreenWidth(_camera.ScreenWidth() - _updateRate * delta);
                offsetPosition -= _cameraOffset.y;
            }

            _camera.transform.position =
                new Vector3(average, offsetPosition, _camera.transform.position.z);

            _lastDist = dist;
        }
    }
}