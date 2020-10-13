using UnityEngine;

public static class TransformExtensions
{
    public static void SetZPos(this Transform transform, float pos)
    {
        var position = transform.position;
        position.z = pos;
        transform.position = position;
    }

    public static void ResetPosAndRot(this Transform transform, bool local = false)
    {
        if (local)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            return;
        }
        
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}