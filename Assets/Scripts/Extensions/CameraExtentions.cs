using UnityEngine;

public static class CameraExtentions
{
    public static float FrustumHeight(this Camera camera, float distance)
        => 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);

    public static float FrustumWidth(this Camera camera, float distance)
        => camera.aspect * FrustumHeight(camera, distance);

    public static float RightEdge(this Camera camera, float distance)
        => FrustumWidth(camera, distance) / 2;

    public static float LeftEdge(this Camera camera, float distance)
    {
        var rightEdge = RightEdge(camera, distance);
        return rightEdge - rightEdge * 2;
    }

    // TODO: give a better name
    public static float CurrentRightEdge(this Camera camera, float distance)
        => FrustumWidth(camera, distance) / 2 + camera.transform.position.x;

    // TODO: give a better name
    public static float CurrentLeftEdge(this Camera camera, float distance)
    {
        var rightEdge = RightEdge(camera, distance);
        return rightEdge - rightEdge * 2 + camera.transform.position.x;
    }

    public static float ScreenHeight(this Camera camera) => camera.orthographicSize * 2;

    public static float ScreenWidth(this Camera camera) => camera.ScreenHeight() * camera.aspect;

    public static float RightScreenEdge(this Camera camera) => camera.transform.position.x + camera.ScreenWidth() / 2;

    public static float BottomScreenEdge(this Camera camera) => camera.transform.position.y - camera.orthographicSize;

    public static void SetScreenWidth(this Camera camera, float value) =>
        camera.orthographicSize = value / camera.aspect / 2;
}