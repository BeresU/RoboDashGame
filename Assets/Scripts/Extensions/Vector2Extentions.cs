using System;
using UnityEngine;

namespace Extensions
{
    public static class Vector2Extensions
    {
        public enum Axis
        {
            X,
            Y
        }

        public static Vector2 ToAxis(this Vector2 value, Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    value.y = 0;
                    break;
                case Axis.Y:
                    value.x = 0;
                    break;
            }

            return value;
        }

        public static Vector2 SignAxis(this Vector2 value, Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    value.x = Math.Sign(value.x);
                    break;
                case Axis.Y:
                    value.y = Math.Sign(value.y);
                    break;
            }

            return value;
        }

        public static Vector2 Sign(this Vector2 value)
        {
            value.x = Math.Sign(value.x);
            value.y = Math.Sign(value.y);
            return value;
        }
    }
}