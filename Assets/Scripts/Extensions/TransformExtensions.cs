using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions 
{
    public static void SetZPos(this Transform transform, float pos)
    {
        var position = transform.position;
        position.z = pos;
        transform.position = position;
    }
}
