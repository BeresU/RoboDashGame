using System;
using UnityEngine;

namespace Extensions
{
    public static class Vector3Extentions 
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }

        public static Vector3 ToAxis(this Vector3 value, Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    value.y = 0;
                    value.z = 0;
                    break;
                case Axis.Y:
                    value.x = 0;
                    value.z = 0;
                    break;
                case Axis.Z:
                    value.x = 0;
                    value.y = 0;
                    break;
            }

            return value;
        }

        public static Vector3 SignAxis(this Vector3 value, Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    value.x = Math.Sign(value.x);
                    break;
                case Axis.Y:
                    value.y = Math.Sign(value.y);
                    break;
                case Axis.Z:
                    value.y = Math.Sign(value.z);
                    break;
            }

            return value;
        }

        public static Vector3 Sign(this Vector3 value)
        {
            value.x = Math.Sign(value.x);
            value.y = Math.Sign(value.y);
            value.z = Math.Sign(value.z);
            return value;
        }
    }
}
