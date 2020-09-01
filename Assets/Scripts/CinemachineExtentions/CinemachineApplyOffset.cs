using Cinemachine;
using UnityEngine;

namespace CinemachineExtentions
{
    public class CinemachineApplyOffset : CinemachineExtension
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private CinemachineTargetGroup _cinemachineTarget;
        [SerializeField] private Vector3 _startingPosition;

        private float _lastOthoSize;

        [Tooltip("When to apply the offset")] [SerializeField]
        private CinemachineCore.Stage _applyAfter = CinemachineCore.Stage.Aim;


        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != _applyAfter) return;

            var offset = _startingPosition.y + state.Lens.OrthographicSize - _offset.y;
            
             state.PositionCorrection += new Vector3(state.FinalPosition.x, offset, state.FinalPosition.z);
        }
    }
}