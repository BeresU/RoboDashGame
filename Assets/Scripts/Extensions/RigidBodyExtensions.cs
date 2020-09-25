using UnityEngine;

namespace Extensions
{
    public static class RigidBodyExtensions
    {
        // NOTE: not very accurate.
        public static Vector2 NextPos(this Rigidbody2D rigidbody2D) =>
            rigidbody2D.position + rigidbody2D.velocity * Time.fixedDeltaTime;
        
        public static Vector2 NextPos(this Rigidbody2D rigidbody2D, Vector2 direction) =>
            rigidbody2D.position + direction * Time.fixedDeltaTime;
    }
}