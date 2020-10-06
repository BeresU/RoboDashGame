using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoboDash.Controllers
{
    public class EndGameController : MonoBehaviour
    {
        [SerializeField] private GameObject _ui;
        [SerializeField] private CinemachineVirtualCamera _battleCam;
        [SerializeField] private CinemachineVirtualCamera _winCam;
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineBrain _cinemachineBrain;

        public void RestartGame() => SceneManager.LoadScene("Battle");

        public void OnGameOver(RoboCharacter win, RoboCharacter lose)
        {
            var orthoSize = _camera.orthographicSize;
            _battleCam.Follow = null;
            _battleCam.m_Lens.OrthographicSize = orthoSize;
            _winCam.Follow = win.transform;
            _winCam.Priority = _battleCam.Priority + 1;
            _winCam.m_Transitions.m_OnCameraLive.AddListener(OnCameraLive);
        }

        private void OnCameraLive(ICinemachineCamera arg0, ICinemachineCamera arg1) =>
            StartCoroutine(WaitForBlendEnd());

        private IEnumerator WaitForBlendEnd()
        {
            _winCam.m_Transitions.m_OnCameraLive.RemoveListener(OnCameraLive);
            while (_cinemachineBrain.IsBlending && _cinemachineBrain.ActiveBlend.BlendWeight < 0.95f)
            {
                yield return null;
            }

            OnBlendFinished();
        }

        private void OnBlendFinished()
        {
            _ui.SetActive(true);
        }
    }
}