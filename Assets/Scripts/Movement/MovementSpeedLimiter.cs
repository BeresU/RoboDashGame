using UnityEngine;

namespace RoboDash.Movement
{
    public static class MovementSpeedLimiter
    {
        private const float Offset = 0.1f;
        public static void PredictNextPos(Rigidbody2D rigidbody2D, Vector2 direction)
        {
            
            var nextPos = rigidbody2D.position + direction  * Time.deltaTime;
            
            Debug.Log($"NextPos: {nextPos.x + Offset} RigidBodyPosition:{rigidbody2D.position.x}");
        }
    }
}
