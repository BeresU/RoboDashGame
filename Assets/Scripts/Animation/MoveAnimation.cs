using System;
using UnityEngine;

namespace RoboDash.Animation
{
    public class MoveAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed;

        [SerializeField] private float _maxPos = 2.78f;

        private Vector3 _initPosition;

        private void Awake()
        {
            _initPosition = transform.position;
            _initPosition.x = _maxPos;
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