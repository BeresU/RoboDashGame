using System;
using UnityEngine;

namespace Extensions
{
    public static class Vector2Extensions
    {
        public enum Axis
        {
            X,
            Y,
            XY
        }

        public static Vector2 NormalizeAndSignAxis(this Vector2 value, Axis axis)
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

            value.x = Math.Sign(value.x);
            value.y = Math.Sign(value.y);

            return value;
        }
    }
}
