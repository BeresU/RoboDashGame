using System;
using UnityEngine;

namespace RoboDash.Tests
{
    public class PhysicsTester : MonoBehaviour
    {
        [SerializeField] private float _forcePower = 1;
        [SerializeField] private ForceMode2D _forceMode;
        [SerializeField] private float _velocityChange = 1f;
        [SerializeField] private Rigidbody2D _rigidbody;

        private void OnGUI()
        {
            if (GUILayout.Button("Add force up"))
            {
                _rigidbody.AddForce(Vector2.up * _forcePower, _forceMode);
            }
            
            if (GUILayout.Button("Add force down"))
            {
                _rigidbody.AddForce(Vector2.down * _forcePower, _forceMode);
            }
            
            if (GUILayout.Button("Add force right"))
            {
                _rigidbody.AddForce(Vector2.right * _forcePower, _forceMode);
            }
            
            if (GUILayout.Button("Add force left"))
            {
                _rigidbody.AddForce(Vector2.left * _forcePower, _forceMode);
            }
        }
    }
}
