using UnityEngine;

namespace RoboDash.Animation
{
    public class MoveAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxPos = 2.78f;
        [SerializeField] private bool _randomize;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        
        private Vector3 _initPosition;
        
        private void Awake()
        {
            _initPosition = transform.position;
            _initPosition.x = _maxPos;

            _speed = _randomize ? Random.Range(_minSpeed, _maxSpeed) : _speed;
        }

        private void Update()
        {
            var pos = transform.position;
            var newPos = Time.deltaTime * _speed * Vector3.left;
            pos += newPos;
            transform.position = pos;

            if (transform.position.x <= -_maxPos)
            {
                transform.position = _initPosition;
            }
        }
    }
}