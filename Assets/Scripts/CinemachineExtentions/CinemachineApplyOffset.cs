using Cinemachine;
using UnityEngine;

namespace CinemachineExtentions
{
    // TODO: support x axis as well
    [ExecuteInEditMode]
    public class CinemachineApplyOffset : CinemachineExtension
    {
        [SerializeField] private Vector3 _offset;
        
        [Tooltip("When to apply the offset")] [SerializeField]
        private CinemachineCore.Stage _applyAfter = CinemachineCore.Stage.Aim;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != _applyAfter) return;
            
            var offset = GetSizeAccordState(state) - _offset.y;

            state.PositionCorrection += new Vector3(state.PositionCorrection.x, offset, state.PositionCorrection.z);
        }
        
        // TODO: need also do for non orthographic camera.
        private static float GetSizeAccordState(CameraState state) =>
            state.Lens.Orthographic ? state.Lens.OrthographicSize : 0f;
    }
}