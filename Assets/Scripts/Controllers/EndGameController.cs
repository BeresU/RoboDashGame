using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoboDash.Controllers
{
    public class EndGameController : MonoBehaviour
    {
        [SerializeField] private GameObject _ui;
        [SerializeField] private CinemachineTargetGroup _targetGroup;
        
        public void RestartGame() => SceneManager.LoadScene("Battle");
        
        public void OnGameOver(RoboCharacter win, RoboCharacter lose)
        {
            _ui.SetActive(true);
            var target = _targetGroup.m_Targets[0];
            target.target = win.transform;
            _targetGroup.m_Targets = new[] {target };
        }
    }
}
