using UnityEngine;

namespace Helpers
{
    public class AveragePosSetter : MonoBehaviour
    {
        [SerializeField] private Transform _transform1;
        [SerializeField] private Transform _transform2;

        private Transform _selfTrans;

        private void Awake() => _selfTrans = GetComponent<Transform>();

        private void FixedUpdate()
        {
            var average = (_transform1.position + _transform2.position) / 2;
            var pos = _selfTrans.position;
            pos.x = average.x;
            _selfTrans.position = pos;
        }
    }
}
