using Extensions;
using UnityEngine;

namespace RoboDash.Movement
{
    // TODO: probably want to abstract this 
    public class MovementLimiter : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _stopVal = 1f;
        [SerializeField] private Camera _camera;

        public bool CanMove(Vector2 direction)
        {
            var nextPos = _rigidbody.NextPos();
            var leftDirection = direction.x < 0;
            
            var distFromCurrentEdge =
                leftDirection ? nextPos.x - _camera.LeftScreenEdge() : _camera.RightScreenEdge() - nextPos.x;
            
            return distFromCurrentEdge > _stopVal;
        }
    }
}