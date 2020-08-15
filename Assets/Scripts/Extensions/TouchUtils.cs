using UnityEngine;

public static class TouchUtils 
{
    public enum Axis
    {
        X,
        Y,
    }

    public static bool IsRightToTransform(this Touch touch, Transform transform, Camera camera)
    {
        var xTouchWorldPos = camera.ScreenToWorldPoint(touch.position).x;
        return xTouchWorldPos > transform.position.x;
    }

    public static bool IsUpToTransform(this Touch touch, Transform transform, Camera camera)
    {
        var yTouchWorldPos = camera.ScreenToWorldPoint(touch.position).y;
        return yTouchWorldPos > transform.position.y;
    }

    public static float DistFromTransform (this Touch touch, Transform transform,Camera camera)
    {
        var screenPos = camera.ScreenToWorldPoint(touch.position);

        return Vector3.Distance
            (new Vector3(screenPos.x,screenPos.y,0)
            , new Vector3(transform.position.x,transform.position.y,0));
    }

    public static float AxisDistFromTransform(this Touch touch, Transform transform, Camera camera, Axis axis)
    {
        var screenPos = camera.ScreenToWorldPoint(touch.position);

        var axisValue = 0f;

        if(axis == Axis.X)
        {
            axisValue = Mathf.Abs(screenPos.x - transform.position.x);
        }
        else
        {
            axisValue = Mathf.Abs(screenPos.y - transform.position.y);
        }

        return axisValue;
    }
}
