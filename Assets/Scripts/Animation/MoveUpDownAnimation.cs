using UnityEngine;

namespace RoboDash.Animation
{
    public class MoveUpDownAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _steps;
        private float _offset;

        private void Awake()
        {
            _curve.postWrapMode = WrapMode.PingPong;
            _offset = transform.position.y;
        }

        private void Update()
        {
            var curveValue = _curve.Evaluate(Time.time);
            var pos = transform.position;
            pos.y = curveValue * _steps + _offset; 
            transform.position = pos;
        }
    }
}