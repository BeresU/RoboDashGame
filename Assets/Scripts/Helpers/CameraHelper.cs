using UnityEngine;

namespace Helpers
{
    public static class CameraHelper
    {
        public static bool PosRightToCamera(Camera camera, Vector3 screenPosition)
        {
            var screenPosCamera = camera.WorldToScreenPoint(camera.transform.position);
            return screenPosition.x > screenPosCamera.x;
        }
    }
}
