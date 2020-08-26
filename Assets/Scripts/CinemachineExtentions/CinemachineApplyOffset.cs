using Cinemachine;
using UnityEngine;

namespace CinemachineExtentions
{
    public class CinemachineApplyOffset : CinemachineExtension
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private CinemachineTargetGroup _cinemachineTarget;

        private float _lastOthoSize;

        [Tooltip("When to apply the offset")] [SerializeField]
        private CinemachineCore.Stage _applyAfter = CinemachineCore.Stage.Aim;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
        //   if (stage != _applyAfter) return;
        //   var transposer = _cam.GetCinemachineComponent<CinemachineTransposer>();
        //   
        //   transposer.m_FollowOffset = _offset;
        //   var dist = Vector3.Distance(_cinemachineTarget.m_Targets[0].target.position,
        //       _cinemachineTarget.m_Targets[1].target.position);
        //   Debug.Log(_cinemachineTarget.BoundingBox.center.x );
        // var offset = _offset *_cinemachineTarget.BoundingBox.center.x;
        // state.PositionCorrection = offset;
        }
    }
}